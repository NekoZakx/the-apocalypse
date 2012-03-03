using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            this.GraphicsDevice = GraphicsDevice;

            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });
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

        public void LoadPreferenceData()
        {
            brightness = 100;
            contrast = 128;
        }
    }
}
