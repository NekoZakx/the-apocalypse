using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
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
        ContentManager Content;
        Player player;
        List<Monster> monster;
        PathFinder pathData;
        SoundEffectInstance bgm;


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
            monster = new List<Monster>();
            pathData = new PathFinder(width, height);
            player.addPathData(pathData);
            
        }

        public bool saveData()
        {
            //À faire
            return true;
        }

        public bool loadData()
        {
            //À faire
            return true;
        }

        public void ForceClose()
        {
            player.Delete();
        }

        public void resetData()
        {
            bgm.Play();
            player.reset();
            monster = new List<Monster>();
        }

        public void LoadContent(GraphicsDevice GraphicsDevice,ContentManager Content)
        {
            this.Content = Content;
            this.GraphicsDevice = GraphicsDevice;
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            player.LoadContent(Content, GraphicsDevice);
            Random rand = new Random();
            bgm = (Content.Load<SoundEffect>(@"Music/bgmusic")).CreateInstance();
            bgm.IsLooped = true;
            bgm.Volume = (float)0.25;

            /*for (int i = 0; i < 15; i++)
            {
                Monster enemy = new Normal();
                enemy.hp = 100;
                enemy.position = new Position(rand.Next(0, 910), rand.Next(0, 550));
                enemy.Initialize();
                enemy.LoadContent(Content, GraphicsDevice);
                monster.Add(enemy);
                player.Attach(enemy);
            }*/
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
            /*for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].hp > 0)
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
                    spriteBatch.Draw(whiteTexture, new Rectangle((int)enemy[i].position.X, (int)enemy[i].position.Y, 50, 50), new Color(50, 50, 50, 255));
                    spriteBatch.End();
                }
            }*/

            GraphicsDevice.BlendState = BlendState.Opaque;
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            foreach(Monster m in monster)
            {
                m.Draw(spriteBatch, pause);
            }
            player.Draw(spriteBatch, pause);
        }

        public void Update(GameTime gameTime)
        {
            if (monster.Count < 10)
            {
                Monster enemy = new Normal();
                enemy.Initialize();
                enemy.LoadContent(Content, GraphicsDevice);
                enemy.addPathData(pathData);
                monster.Add(enemy);
                player.Attach(enemy);

            }
            bool restart = true;
            while(restart)
            {
                restart = false;
                foreach (Monster m in monster)
                {
                    if (m.hp <= 0)
                    {
                        m.delete();
                        player.Detach(m);
                        monster.Remove(m);
                        restart = true;
                        break;
                    }
                }
            }
            player.Update(gameTime);
            foreach (Monster m in monster)
            {
                m.Update(gameTime);
            }
            /****** TESTING FOR COLLISION ************/
            /*for (int i = 0; i < enemy.Length; i++)
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
            }*/
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

        public SoundEffectInstance getBGM()
        {
            return this.bgm;
        }
    }
}
