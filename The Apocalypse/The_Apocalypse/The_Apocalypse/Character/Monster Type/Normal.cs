using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class Normal : Monster
    {
        private string _name = "Normal";
        private int _hp;
        private Area _area; //À faire (Area::Near)
        private Position _position;
        private int _damage = 1;
        private Vector2 _speed = new Vector2(150, 150);
        private GraphicsDevice GraphicsDevice;
        private Vector2 limit;
        private Character _player;
        private Position _playerPosition = new Position(0, 0);
        private SpriteSheet _spriteSheet;
        private int _width = 25;
        private int _height = 50;
        private PathFinder pathData;
        private int score = 100;
        private SoundEffect[] _sounds;
        private float _soundVolume;
        private float _soundPitch;
        private float _soundPan;
        private int timeBeforeSoundPlay, timeElapsed = 0;
        private bool soundWillPlay = true;
        private bool isMoving = true;
        private int currentFrameTime = 0;
        private int frameTime = 180;
        private bool reverseAnimation = false;

        public Position PlayerPosition
        {
            get
            {
                return _playerPosition;
            }
            set
            {
                _playerPosition = value;
            }
        }

        public int damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }

        public Area area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
            }
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

        public int scores
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public SoundEffect[] sounds
        {
            get
            {
                return _sounds;
            }
            set
            {
                _sounds = value;
            }
        }

        public Normal()
        {
            _hp = 10;

            Random rand = new Random();
            int randomX = rand.Next(-200, 1110);
            int randomY = rand.Next(-200, 750);
            if (randomX <= 960 && randomX >= -50)
            {
                if (randomY > 550 || randomY < -50)
                    position = new Position(randomX, randomY);
                else
                    position = new Position(randomX, -200);
            }
            else
                position = new Position(randomX, randomY);

            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            int width = Int32.Parse(file.FindReadNode("width"));
            int height = Int32.Parse(file.FindReadNode("height"));

            _soundVolume = float.Parse(file.FindReadNode("SFXvolume"));
            _soundPitch = float.Parse(file.FindReadNode("SFXpitch"));
            _soundPan = float.Parse(file.FindReadNode("pan"));


            file.ReadClose();

            timeBeforeSoundPlay = rand.Next(0, 20000);

            limit = new Vector2(width + _width, height + _height);
        }

        public void delete()
        {
            pathData.removeData(_position, _width, _height);
        }

        public void attack()
        {
            //À faire
        }

        void animate(GameTime gameTime)
        {
            if (isMoving)
            {
                currentFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (currentFrameTime >= frameTime)
                {
                    switch (_spriteSheet.getCurrentFrame())
                    {
                        case 0:
                            reverseAnimation = false;
                            _spriteSheet.setCurrentFrame(1);
                            break;
                        case 1:
                            if (reverseAnimation)
                                _spriteSheet.setCurrentFrame(0);
                            else
                                _spriteSheet.setCurrentFrame(2);
                            break;
                        case 2:
                            reverseAnimation = true;
                            _spriteSheet.setCurrentFrame(1);
                            break;
                    }
                    currentFrameTime = 0;
                }
            }
            else
            {
                _spriteSheet.setCurrentFrame(1);
                currentFrameTime = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            Random rand = new Random();
            orientation((int)_playerPosition.X, (int)_playerPosition.Y);
            if (timeElapsed >= timeBeforeSoundPlay && soundWillPlay)
            {
                sounds[rand.Next(0, _sounds.Length)].Play(_soundVolume, _soundPitch, _soundPan);
                soundWillPlay = false;
            }
            else
                timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            move(gameTime);
            animate(gameTime);
        }

        public void orientation(int pX, int pY)
        {
            double angle = Angle(_position.X, -_position.Y, pX, -pY);

            // 22.5 + 45 + 22.5 + 22.5 +45 +22.5 +22.5 +45 +22.5 +22.5 +45 +22.5 = 360

            // 22.5 + 45 + 45 + 45 + 45 +45 + 45 + 45 + 22.5 = 360

            if ((angle >= 0 && angle < 22.5) || (angle >= 337.5 && angle < 360))
                _spriteSheet.setCurrentDirection(0);
            else if (angle >= 22.5 && angle < 67.5)
                _spriteSheet.setCurrentDirection(1);
            else if (angle >= 67.5 && angle < 112.5)
                _spriteSheet.setCurrentDirection(2);
            else if (angle >= 112.5 && angle < 157.5)
                _spriteSheet.setCurrentDirection(3);
            else if (angle >= 157.5 && angle < 202.5)
                _spriteSheet.setCurrentDirection(4);
            else if (angle >= 202.5 && angle < 247.5)
                _spriteSheet.setCurrentDirection(5);
            else if (angle >= 247.5 && angle < 292.5)
                _spriteSheet.setCurrentDirection(6);
            else if (angle >= 292.5 && angle < 337.5)
                _spriteSheet.setCurrentDirection(7);

        }
        public void move(GameTime gameTime)
        {
            /*if (position.X - PlayerPosition.X < 0)
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
            }*/
            if (_position.X < 0 || _position.X + _width > 960 || _position.Y < 0 || _position.Y + _height > 600)
            {
                if (_position.X < 1)
                    _position.X++;
                else if (_position.X + width > 960)
                    _position.X--;

                if (_position.Y < 1)
                    _position.Y++;
                else if (_position.Y + _height >= 600)
                    _position.Y--;
            }
            else
            {
                
                pathData.removeData(_position, _width, _height);
                Position temp = pathData.nextMove(_position,_width,_height, _playerPosition);
                if (temp.X - _position.X == 0 && temp.Y - _position.Y == 0)
                    isMoving = false;
                else
                    isMoving = true;
                _position = temp;
                pathData.ChangeData(_position, _width,_height, SquareContent.Monster);
            }
        }

        //Pour générer un nombre aléatoire.
        //Source : http://msdn.microsoft.com/en-us/library/system.security.cryptography.rngcryptoserviceprovider.aspx
        public byte getRandomNumber(byte max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException("max");
            // Create a new instance of the RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFair(randomNumber[0], max));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % max) + 1);
        }

        private static bool IsFair(byte pick, byte max)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / max;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return pick < max * fullSetsOfValues;
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice)
        {
            this.GraphicsDevice = GraphicsDevice;
            _spriteSheet = new SpriteSheet(@"SpriteSheet/monster/m", contentManager);
            sounds = new SoundEffect[] { contentManager.Load<SoundEffect>(@"SoundFX/zombie1"),
                                         contentManager.Load<SoundEffect>(@"SoundFX/zombie2") };
        }

        //Fonction pour calculer l'angle entre deux points
        //Source : http://www.carlosfemmer.com/post/2006/02/Calculate-Angle-between-2-points-using-C.aspx
        private double Angle(double px1, double py1, double px2, double py2)
        {
            // Negate X and Y values
            double pxRes = px2 - px1;
            double pyRes = py2 - py1;
            double angle = 0.0;
            // Calculate the angle
            if (pxRes == 0.0)
            {
                if (pxRes == 0.0)
                    angle = 0.0;
                else if (pyRes > 0.0) angle = System.Math.PI / 2.0;
                else
                    angle = System.Math.PI * 3.0 / 2.0;
            }
            else if (pyRes == 0.0)
            {
                if (pxRes > 0.0)
                    angle = 0.0;
                else
                    angle = System.Math.PI;
            }
            else
            {
                if (pxRes < 0.0)
                    angle = System.Math.Atan(pyRes / pxRes) + System.Math.PI;
                else if (pyRes < 0.0) angle = System.Math.Atan(pyRes / pxRes) + (2 * System.Math.PI);
                else
                    angle = System.Math.Atan(pyRes / pxRes);
            }
            // Convert to degrees
            angle = angle * 180 / System.Math.PI; return angle;

        }

        public void Update(Character Player)
        {
            _playerPosition = Player.position;
            _player = Player;
        }

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {
            spriteBatch.Draw(_spriteSheet.Frame(), new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
        }

        public void Initialize()
        {
        }

        public int getDamage()
        {
            return 0;
        }

        public void addPathData(PathFinder pathData)
        {
            this.pathData = pathData;
        }
    }
}
