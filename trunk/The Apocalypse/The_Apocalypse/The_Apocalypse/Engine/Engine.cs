using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace The_Apocalypse
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isPaused = false, isMainMenu = true;
        Options options;
        MainMenu main;
        Level game;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = 960;
            this.graphics.PreferredBackBufferHeight = 600;

            //Définit si l'on voit la souris ou non. Nous pouvons la généré.
            this.IsMouseVisible = true;
            if (!File.Exists("Preference.xml"))
            {
                //First Logon Process

                XmlReaderWriter file = new XmlReaderWriter();
                file.OpenWrite("Preference.xml");

                file.WriteCategory("Preference");

                file.WriteNextTextNode("username", "Player");
                file.WriteNextTextNode("brightness", "255");
                file.WriteNextTextNode("contrast", "128");
                file.WriteNextTextNode("volume", "1");
                file.WriteNextTextNode("pitch", "0");
                file.WriteNextTextNode("pan", "0");
                file.WriteNextTextNode("fullscreen", "False");
                file.WriteNextTextNode("width", this.graphics.PreferredBackBufferWidth.ToString());
                file.WriteNextTextNode("height", this.graphics.PreferredBackBufferHeight.ToString());

                file.WriteEndCategory();

                file.WriteClose();
            }

            options = new Options((this.Window.ClientBounds.Width / 2),(this.Window.ClientBounds.Height / 2));
            main = new MainMenu();
            game = new Level();
            game.initialize(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            options.LoadTexture(Content, GraphicsDevice, graphics);
            main.LoadTexture(Content);
            game.LoadContent(GraphicsDevice,Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        KeyboardState oldState = Keyboard.GetState();
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (oldState.IsKeyDown(Keys.Escape) && newState.IsKeyUp(Keys.Escape))
            {
                isPaused = !isPaused;
                options.LoadPreferenceData();
                game.LoadPreferenceData();
            }

            if (isPaused)
            {
                if (options.closeProgram)
                {

                    this.Exit();
                }
                    
                if (options.mainMenu)
                {
                    isMainMenu = true;
                    main.play = false;
                    isPaused = false;
                    options.mainMenu = false;
                    game.LoadPreferenceData();
                }
                options.Update(gameTime);
            }
            else
            {
                if (isMainMenu)
                {
                    if (main.closeProgram)
                        this.Exit();
                    if (main.play)
                    {
                        this.isMainMenu = false;
                        game.resetData();
                    }
                    main.Update(gameTime);
                }
                game.Update(gameTime);
            }

            oldState = newState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            if (isMainMenu)
            {
                main.Draw(gameTime, spriteBatch);
            }
            else
            {
                if (main.play)
                {
                    spriteBatch.Begin();
                    game.Draw(spriteBatch,isPaused);
                    spriteBatch.End();
                }
                
                else
                {
                    //Draw des animations du menu. Extension des possibilités
                }
            }
            if (isPaused)
            {
                options.setButtonData((this.Window.ClientBounds.Width / 2), (this.Window.ClientBounds.Height / 2));
                options.Draw(spriteBatch);
            }
            else
            {
                //Control brightness/contrast, in else to desactivate it for option preview
                game.DrawContrastAndBrightness(spriteBatch);
            }
            
            base.Draw(gameTime);
        }
    }
}
