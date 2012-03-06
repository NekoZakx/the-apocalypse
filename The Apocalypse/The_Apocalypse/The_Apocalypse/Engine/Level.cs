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
            
        }

        public void ForceClose()
        {
            player.Delete();
        }

        public void resetData()
        {
            player.reset();
        }

        public void LoadContent(GraphicsDevice GraphicsDevice,ContentManager Content)
        {
            this.GraphicsDevice = GraphicsDevice;
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            player.LoadContent(Content, GraphicsDevice);
            Random rand = new Random();
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i] = new Normal();
                enemy[i].hp = 100;
                enemy[i].position = new Position(rand.Next(0, 910), rand.Next(0, 550));
            }
        }

        public void DrawContrastAndBrightness(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(brightness, brightness, brightness, 255));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, contrastBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(contrast, contrast, contrast, 255));
            spriteBatch.End();

            /******* For testing Collision *******/
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].hp > 0)
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
                    spriteBatch.Draw(whiteTexture, new Rectangle((int)enemy[i].position.X, (int)enemy[i].position.Y, 50, 50), new Color(50, 50, 50, 255));
                    spriteBatch.End();
                }
            }

            GraphicsDevice.BlendState = BlendState.Opaque;
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            player.Draw(spriteBatch, pause);
            
        }

        Monster[] enemy = new Monster[15];
        public void Update(GameTime gameTime)
        {
            
            
            player.Update(gameTime);
            
            /****** TESTING FOR COLLISION ************/
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].position.X > 910)
                    enemy[i].position.X = 0;
                enemy[i].position.X++;
                int test = player.touchDamage(enemy[i].position, new Position((int)enemy[i].position.X + 50, (int)enemy[i].position.Y + 50));
                if (test > 0 && enemy[i].hp != -1)
                {
                    enemy[i].hp -= test;
                    Console.WriteLine("ENEMY HP : " + enemy[i].hp + "/100 after " + test + " HP of Damage");
                }

                if (enemy[i].hp <= 0 && enemy[i].hp != -1)
                {
                    enemy[i].hp = -1;
                    player.kill++;
                }
            }
            /*****************************************/
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
