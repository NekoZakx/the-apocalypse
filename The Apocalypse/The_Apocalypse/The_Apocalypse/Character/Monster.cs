using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class Monster : Character
    {
        private string _name;
        private int _hp;
        private Position _position;
        private SpriteSheet _spriteSheet;
        private int _width = 50;
        private int _height = 50;
        private Vector2 _speed;
        private Position _playerPosition;
        BlendState brightnessBlend;
        Texture2D whiteTexture;
        
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

        Position PlayerPosition
        {
            get;
            set;
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public Position position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public SpriteSheet spriteSheet
        {
            get
            {
                return _spriteSheet;
            }
            set
            {
                _spriteSheet = value;
            }
        }

        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public Vector2 speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public void reset() { }

        public void Update(GameTime gameTime) { }

        public void Update(Character Player)
        {
            _playerPosition = Player.position;
        }

        public void move(GameTime gameTime)
        {
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
        }
    }
}
