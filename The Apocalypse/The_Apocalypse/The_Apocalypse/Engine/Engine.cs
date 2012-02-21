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

namespace The_Apocalypse
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isPaused = false;
        Options options;
        //DÉFINIR CLASSE level

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

            options = new Options((this.Window.ClientBounds.Width / 2),(this.Window.ClientBounds.Height / 2));

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
            }

            if (isPaused)
            {
                if (options.closeProgram)
                    this.Exit();
                options.update_buttons(gameTime);
            }
            else
            {
                //Jouer le jeu. Toutes les données seront modifiées. CLASSE level
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
            
            if (isPaused)
            {
                options.setButtonData((this.Window.ClientBounds.Width / 2), (this.Window.ClientBounds.Height / 2));
                options.Draw(gameTime, spriteBatch);
            }
            //Afficher les données du jeu. Théoriquement les données ne devrait pas être modifier puisque l'update ne se fait pas. CLASSE level
            base.Draw(gameTime);
        }
    }
}
