using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    interface Monster : Character
    {

        string name
        {
            get;
            set;
        }

        int hp
        {
            get;
            set;
        }

        Position position
        {
            get;
            set;
        }

        SpriteSheet spriteSheet
        {
            get;
            set;
        }

        int width
        {
            get;
            set;
        }

        int height
        {
            get;
            set;
        }

        Vector2 speed
        {
            get;
            set;
        }

        Position PlayerPosition
        {
            get;
            set;
        }

        int damage
        {
            get;
            set;
        }

        Area area
        {
            get;
            set;
        }

        SoundEffect[] sounds
        {
            get;
            set;
        }

        /*public void attack()
        {
            //À faire
        }

        public int getDamage()
        {
            //À faire
            return 0;
        }

        public void reset() { }

        public void Update(GameTime gameTime) 
        {
            move(gameTime);
        }

        public void Update(Character Player)
        {
            _playerPosition = Player.position;
        }

        public void move(GameTime gameTime)
        {
            if (position.X - PlayerPosition.X < 0)
            {
                position.X += 1;
            }
            else if (position.X - PlayerPosition.X > 0)
            {
                position.X -= 1;
            }

            if (position.Y - PlayerPosition.Y < 0)
            {
                position.Y += 1;
            }
            else if (position.Y - PlayerPosition.Y > 0)
            {
                position.Y -= 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            spriteBatch.Draw(whiteTexture, new Rectangle((int)position.X, (int)position.Y, 50, 50), new Color(50, 50, 50, 255));
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice)
        {
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void Initialize()
        {
            
            brightnessBlend = new BlendState();
            brightnessBlend.ColorSourceBlend = brightnessBlend.AlphaSourceBlend = Blend.Zero;
            brightnessBlend.ColorDestinationBlend = brightnessBlend.AlphaDestinationBlend = Blend.SourceColor;
        }*/

        void attack();

        void delete();

        void addPathData(PathFinder pathData);

        int getDamage();

        void Update(GameTime gameTime);
        
        void Update(Character Player);

        void move(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, bool pause);

        void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice);

        void Initialize();
    }
}
