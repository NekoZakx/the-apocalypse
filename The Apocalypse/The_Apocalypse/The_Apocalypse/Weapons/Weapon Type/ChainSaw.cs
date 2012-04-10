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
        private List<Proximity> near;//Modifier pour l'objet lui-même(Retire la liste)

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
            //Aucune liste est nécessaire.
            //Créer l'instance de ton objet proximity ici.
            near = new List<Proximity>();
        }

        public int hit(Position point1, Position point2)
        {
            //point1 et point2 sont les points qui définissent la zone de la cible a vérifier.
            //Verifier a l'aide de la variable du positionnement du joueur
            if (_shot)
            {
                MouseState mousePosition = Mouse.GetState();
                if(true)//Code qui retourne true ou false
                {
                    return _damage;
                }
            }
            _shot = false;
            return 0;
        }

        public void Delete()
        {
            //Supprimer la zone et la position du joueur.
        }

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {
            //Laisser vide
            return;
        }

        public void shoot(Position playerPosition, GraphicsDevice GraphicsDevice)
        {
            //ne pas créer de nouvelles instance de l'objet
            //Enregistré la position du joueur dans une variable
            _shot = true;
        }

        public void reset()
        {
            //ne pas faire de redéfinition d'objet
            _shot = false;
        }
    }
}
