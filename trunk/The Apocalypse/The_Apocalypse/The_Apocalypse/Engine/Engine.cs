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
        SpriteFont font;
        bool isPaused = false;
        Options options;
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
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = 960;
            this.graphics.PreferredBackBufferHeight = 600;
            //Définit si l'on voit la souris ou non. Nous pouvons la généré.
            this.IsMouseVisible = true;
            //Mode plein écran, avec la résolution choisi plus haut.
            //graphics.IsFullScreen = true;
            //Appliquer les changements graphiques.
            graphics.ApplyChanges();
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
            font = Content.Load<SpriteFont>(@"Fonts/TextFont");
            options.LoadTexture(Content, GraphicsDevice);
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
            // Allows the game to exit
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();*/
            if (newState.IsKeyDown(Keys.Escape))
            {
                isPaused = true;
            }
            if (newState.IsKeyDown(Keys.F1) && isPaused)
                isPaused = false;

            if (isPaused)
            {
                MouseState ms = Mouse.GetState();
                options.update_buttons(gameTime);
            }
            // TODO: Add your update logic here
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
            
            string typedText = "PAUSE";
            spriteBatch.Begin();
            if (isPaused)
            {
                spriteBatch.End();
                options.Draw(gameTime, spriteBatch);
                spriteBatch.Begin();
                //spriteBatch.DrawString(font, typedText, new Vector2((this.Window.ClientBounds.Width / 2) - (font.MeasureString(typedText).Length() / 2), (this.Window.ClientBounds.Height / 2) - (7)), Color.Yellow);
            }
                // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
