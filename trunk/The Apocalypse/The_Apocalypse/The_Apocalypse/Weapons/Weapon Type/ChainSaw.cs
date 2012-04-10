using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace The_Apocalypse.Weapons.Weapon_Type
{
    class ChainSaw : Weapon
    {
        private string _name = "ChainSaw";
        private int _damage = 30;
        private int _ammo = -1;
        private bool _shot = false;
        private Proximity proximity;
        private Position _playerPosition = new Position(0,0);

        private float _speed = 0;

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

        public int ammo
        {
            get
            {
                return _ammo;
            }
            set
            {
                _ammo = value;
            }
        }

        public float speed
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

        public ChainSaw()
        {
            proximity = new Proximity();
        }

        public int hit(Position point1, Position point2)
        {
            //point1 et point2 sont les points qui définissent la zone de la cible a vérifier.
            //Verifier a l'aide de la variable du positionnement du joueur
            if (_shot)
            {
                MouseState mousePosition = Mouse.GetState();
                if(proximity.evaluate(point1, point2, mousePosition))//Code qui retourne true ou false
                {
                    return _damage;
                }
            }
            _shot = false;
            return 0;
        }

        public void Delete()
        {
            //Supprimer la zone.
            _playerPosition = null; 
        }

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {
            return;
        }

        public void shoot(Position playerPosition, GraphicsDevice GraphicsDevice)
        {
            _playerPosition = playerPosition;
            _shot = true;
        }

        public void reset()
        {
            _playerPosition.X = 0;
            _playerPosition.Y = 0;
            _shot = false;
        }
    }
}
