﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace The_Apocalypse
{
    enum SquareContent
    {
        Empty,
        Monster,
        Hero,
        Wall,
        Active
    };
    
    class PathFinder
    {
        
        SquareContent[,] _squares;

        int width, height;

        public PathFinder(int width, int height)
        {
            _squares = new SquareContent[width, height];
            this.height = height;
            this.width = width;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _squares[i, j] = SquareContent.Empty;
                }
            }
        }

        public void resetSquares()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _squares[i, j] = SquareContent.Empty;
                }
            }
        }

        public void removeData(Position oldDataSet, int width, int height)
        {
            for (int i = (int)oldDataSet.X; i < oldDataSet.X + width; i++)
            {
                for (int j = (int)oldDataSet.Y; j < oldDataSet.Y + height; j++)
                {
                    if(ValidCoordinates(i,j))
                        _squares[i,j] = SquareContent.Empty;
                }
            }
        }

        public void ChangeData(Position newDataSet, int width, int height, SquareContent type)
        {
            for (int i = (int)newDataSet.X; i < newDataSet.X + width; i++)
            {
                for (int j = (int)newDataSet.Y; j < newDataSet.Y + height; j++)
                {
                    if (ValidCoordinates(i, j))
                        _squares[i, j] = type;
                }
            }
        }

        /**************************
          POSSIBLE MOVE AUTHORIZED
          [-1, -1] [0, -1] [1, -1]
          [-1,  0] [0,  0] [1,  0]
          [-1,  1] [0,  1] [1,  1] 
         **************************/
        enum direction
        {
            UP = 0,
            DOWN = 1,
            RIGHT = 2,
            LEFT = 3,
            RIGHT_UP = 2,
            RIGHT_DOWN = 1,
            LEFT_UP = 0,
            LEFT_DOWN = 3,
            RANDOM = 4
        }

        Point[][] _moveByDirection = 
        {
            new Point[]
	        {
	            new Point(1, 0), // Right
                new Point(0, -1), // Up
	            new Point(1, -1)
	        }
        ,
            new Point[]
	        {
                new Point(-1, 0), // Left
                new Point(0, 1), // Down
	            new Point(-1, 1)
	        }
        ,
            new Point[]
	        {
                new Point(0, 1), // Down
                new Point(1, 0), // Right
	            new Point(1, 1),
	        }
        ,
            new Point[]
	        {
                new Point(0, -1), // Up
                new Point(-1, 0), // Left
                new Point(-1, -1)
	        }
        ,
            new Point[]
	        {
	            new Point(0, -1), // Up
                new Point(1, 0), // Right
                new Point(0, 1), // Down
                new Point(-1, 0), // Left
            
                new Point(-1, -1),
	            new Point(1, -1),
	            new Point(1, 1),
	            new Point(-1, 1)
	        
	        }
        };


        private bool ValidCoordinates(int x, int y)
        {
            // Our coordinates are constrained between 0 and WIDTH or HEIGHT.
            if (x < 0)
            {
                return false;
            }
            if (y < 0)
            {
                return false;
            }
            if (x > width -1)
            {
                return false;
            }
            if (y > height -1)
            {
                return false;
            }
            return true;
        }

        public Position nextMove(Position evaluate,int width, int height, Position target)
        {
             //DEBUG SI L'OBJET EST A L'INTERIEUR D'UN AUTRE OBJET, TRES INSTABLE... A AMÉLIORER
             /*if (ValidCoordinates((int)evaluate.X, (int)evaluate.Y) && ValidCoordinates((int)evaluate.X + width, (int)evaluate.Y) && ValidCoordinates((int)evaluate.X, (int)evaluate.Y + height) && ValidCoordinates((int)evaluate.X + width, (int)evaluate.Y + height))
                if (_squares[(int)evaluate.X, (int)evaluate.Y] != SquareContent.Empty || _squares[(int)evaluate.X + width, (int)evaluate.Y] != SquareContent.Empty || _squares[(int)evaluate.X, (int)evaluate.Y + height] != SquareContent.Empty || _squares[(int)evaluate.X + width, (int)evaluate.Y + height] != SquareContent.Empty)
                {
                    Random rand = new Random();
                    int randomX = rand.Next(-200, 1110);
                    int randomY = rand.Next(-200, 750);
                    if (randomX <= 960 && randomX >= -50)
                    {
                        if (randomY > 550 || randomY < -50)
                            return new Position(randomX, randomY);
                        else
                            return new Position(randomX, -200);
                    }
                    else
                        return new Position(randomX, randomY);
                }*/



            bool findPath = false;
            //temporairement bouger normalement vers le joueur
            //Modification du X
            bool[] d = { false, false, false, false };
            Point[] _movements = {};
            int x, y;
            if (evaluate.X < target.X)      { x = (int)evaluate.X + 1; d[0] = true; }
            else if (evaluate.X > target.X) { x = (int)evaluate.X - 1; d[1] = true; }
            else                              x = (int)evaluate.X;

            //Modification du Y
            if (evaluate.Y < target.Y)      { y = (int)evaluate.Y + 1; d[2] = true; }
            else if (evaluate.Y > target.Y) { y = (int)evaluate.Y - 1; d[3] = true; }
            else                              y = (int)evaluate.Y;

            if (d[0] && !d[1] && !d[2] && !d[3])
                _movements = _moveByDirection[(int)direction.RIGHT];
            else if (!d[0] && d[1] && !d[2] && !d[3])
                _movements = _moveByDirection[(int)direction.LEFT];
            else if (!d[0] && !d[1] && d[2] && !d[3])
                _movements = _moveByDirection[(int)direction.DOWN];
            else if (!d[0] && !d[1] && !d[2] && d[3])
                _movements = _moveByDirection[(int)direction.UP];
            else if (d[0] && !d[1] && d[2] && !d[3])
                _movements = _moveByDirection[(int)direction.RIGHT_DOWN];
            else if (d[0] && !d[1] && !d[2] && d[3])
                _movements = _moveByDirection[(int)direction.RIGHT_UP];
            else if (!d[0] && d[1] && d[2] && !d[3])
                _movements = _moveByDirection[(int)direction.LEFT_DOWN];
            else if (!d[0] && d[1] && !d[2] && d[3])
                _movements = _moveByDirection[(int)direction.LEFT_UP];

            //Vérifier si cette case est occupé, si oui essayé de contourner
            if (_squares[x, y] == SquareContent.Hero || _squares[x + width, y] == SquareContent.Hero || _squares[x, y + height] == SquareContent.Hero || _squares[x + width, y + height] == SquareContent.Hero)
            {
                return evaluate;
            }
            else if (ValidCoordinates(x, y) && ValidCoordinates(x + width, y) && ValidCoordinates(x, y + height) && ValidCoordinates(x + width, y + height))
                foreach (Point a in _movements)
                {
                    if (_squares[x, y] == SquareContent.Empty && _squares[x + width, y] == SquareContent.Empty && _squares[x, y + height] == SquareContent.Empty && _squares[x + width, y + height] == SquareContent.Empty)
                    {
                        findPath = true;
                        break;
                    }
                    if (ValidCoordinates((int)evaluate.X + a.X, (int)evaluate.Y + a.Y) && ValidCoordinates((int)evaluate.X + a.X + width, (int)evaluate.Y + a.Y) && ValidCoordinates((int)evaluate.X + a.X, (int)evaluate.Y + a.Y + height) && ValidCoordinates((int)evaluate.X + a.X + width, (int)evaluate.Y + a.Y + height))
                    {
                        x = (int)evaluate.X + a.X;
                        y = (int)evaluate.Y + a.Y;
                    }
                }
            if (!findPath)
            {
                return evaluate;
            }
            else
            {
                findPath = false;
                return new Position(x, y);
            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            /*int x, y;
            bool additionX = false;
            bool additionY = false;
            bool changeX = false;
            bool changeY = false;
            if (evaluate.X < target.X)
            {
                x = (int)evaluate.X + 1;
                additionX = true;
                changeX = true;
            }
            else if (evaluate.X > target.X)
            {
                x = (int)evaluate.X - 1;
                changeX = true;
            }
            else
                x = (int)evaluate.X;

            if (evaluate.Y < target.Y)
            {
                y = (int)evaluate.Y + 1;
                additionY = true;
                changeY = true;
            }
            else if (evaluate.Y > target.Y)
            {
                y = (int)evaluate.Y - 1;
                changeY = true;
            }
            else
                y = (int)evaluate.Y;



            bool stuck = true;
            if (ValidCoordinates(x, y) && ValidCoordinates(x + width, y + height))
            {
                if (_squares[x, y] == SquareContent.Hero || _squares[x + width, y] == SquareContent.Hero || _squares[x, y + height] == SquareContent.Hero || _squares[x + width, y + height] == SquareContent.Hero)
                    return evaluate;
                else
                    if (_squares[x, y] != SquareContent.Empty || _squares[x + width, y] != SquareContent.Empty || _squares[x, y + height] != SquareContent.Empty || _squares[x + width, y + height] != SquareContent.Empty)
                    {
                        foreach (Point position in _movements)
                        {
                            if (ValidCoordinates((int)evaluate.X + width + position.X, (int)evaluate.Y + height + position.Y) && ValidCoordinates((int)evaluate.X + position.X, (int)evaluate.Y + position.Y))
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] != SquareContent.Monster && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] != SquareContent.Monster && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] != SquareContent.Monster && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] != SquareContent.Monster)
                                {
                                    //ADDITION / CHANGES
                                    if ((changeY && ((additionY && position.Y >= 0) || (!additionY && position.Y <= 0))) || (changeX && ((additionX && position.X >= 0) || (!additionX && position.X <= 0))))
                                    {
                                        x = position.X + (int)evaluate.X;
                                        y = position.Y + (int)evaluate.Y;
                                    }
                                    stuck = false;
                                    break;
                                }
                            }
                        }
                        if (stuck)
                            return evaluate;
                        else
                            return new Position(x, y);
                    }
                    else
                        return new Position(x, y);
            }
            else
                return evaluate;*/

            //N'ESSAIE PAS D'ALLER JUSQU'AU JOUEUR!
            /*bool stuck = true;
            if (ValidCoordinates(x + width, y + height))
            {
                if (_squares[x, y] == SquareContent.Hero || _squares[x + width, y] == SquareContent.Hero || _squares[x, y + height] == SquareContent.Hero || _squares[x + width, y + height] == SquareContent.Hero)
                {
                    return evaluate;
                }else
                if (_squares[x, y] != SquareContent.Empty || _squares[ x + width, y] != SquareContent.Empty || _squares[x, y + height] != SquareContent.Empty || _squares[x + width,  y + height] != SquareContent.Empty)
                {
                    foreach (Point position in _movements)
                    {
                        if (ValidCoordinates((int)position.X + (int)evaluate.X, (int)position.Y + (int)evaluate.Y) && ValidCoordinates((int)position.X + (int)evaluate.X + width, (int)position.Y + (int)evaluate.Y + height))
                        {
                            if (position.X == -1 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == -1 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == 0 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == 0 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == 0)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                            if (position.X == -1 && position.Y == 0)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    stuck = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (stuck)
                        return evaluate;
                }
                
            }   
            return new Position(x, y);


            */



            /*if (position.X == -1 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == -1 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == 0 && position.Y == 1)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == 0 && position.Y == -1)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == 1 && position.Y == 0)
                            {
                                if (_squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
                            if (position.X == -1 && position.Y == 0)
                            {
                                if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                                {
                                    x = position.X + (int)evaluate.X;
                                    y = position.Y + (int)evaluate.Y;
                                    path = true;
                                    break;
                                }
                            }
             */



















           /* if (findPath)
                return false;
            else if (ValidCoordinates((int)evaluate.X, (int)evaluate.Y))
            {
                if (_squares[(int)evaluate.X, (int)evaluate.Y] == SquareContent.Hero)
                {
                    findPath = true;
                    return true;
                }
                else
                {
                    if
                    foreach (Point position in _movements)
                    {
                        
                        
                        
                        if (ValidCoordinates((int)evaluate.X + position.X, (int)evaluate.Y + position.Y))
                        {
                            if (_squares[(int)evaluate.X + position.X, (int)evaluate.Y + position.Y] == SquareContent.Empty || _squares[(int)evaluate.X + position.X, (int)evaluate.Y + position.Y] == SquareContent.Hero)
                            {
                                if (seek(new Position((int)evaluate.X + position.X, (int)evaluate.Y + position.Y)))
                                {
                                    path.Add(new Position((int)evaluate.X + position.X - 1, (int)evaluate.Y + position.Y));
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            
            return false;*/
        }
    }
}
