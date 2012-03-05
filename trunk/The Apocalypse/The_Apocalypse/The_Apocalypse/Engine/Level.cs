using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace The_Apocalypse
{
    class Level
    {
        int width = 960, height = 600;
        int brightness,contrast;
        GraphicsDevice GraphicsDevice;
        BlendState brightnessBlend;
        BlendState contrastBlend;
        Texture2D whiteTexture;
        Player player;
        Monster monster;

        public void initialize(int WIDTH, int HEIGHT)
        {
            width = WIDTH; 
            height = HEIGHT;
            LoadPreferenceData();

            brightnessBlend = new BlendState();
            brightnessBlend.ColorSourceBlend = brightnessBlend.AlphaSourceBlend = Blend.Zero;
            brightnessBlend.ColorDestinationBlend = brightnessBlend.AlphaDestinationBlend = Blend.SourceColor;

            contrastBlend = new BlendState();
            contrastBlend.ColorSourceBlend = contrastBlend.AlphaSourceBlend = Blend.DestinationColor;
            contrastBlend.ColorDestinationBlend = contrastBlend.AlphaDestinationBlend = Blend.SourceColor;

            player = new Player();
            monster = new Normal();
            
        }

        public void resetData()
        {
            player.reset();
            monster.reset();
        }

        public void LoadContent(GraphicsDevice GraphicsDevice,ContentManager Content)
        {
            this.GraphicsDevice = GraphicsDevice;
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            player.LoadContent(Content, GraphicsDevice);
        }

        public void DrawContrastAndBrightness(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(brightness, brightness, brightness, 255));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, contrastBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(contrast, contrast, contrast, 255));
            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            ((Normal)monster).playerPosition = player.position;
            ((Normal)monster).Update(gameTime);
        }

        public void LoadPreferenceData()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            brightness = Int32.Parse(file.FindReadNode("brightness"));
            contrast = Int32.Parse(file.FindReadNode("contrast"));

            file.ReadClose();
        }
    }
}
