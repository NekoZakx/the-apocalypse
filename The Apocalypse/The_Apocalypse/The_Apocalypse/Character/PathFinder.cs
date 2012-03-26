using System;
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

        public void addData(Position newDataSet, int width, int height, SquareContent type)
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

        public List<Position> travellingData()
        {
            return path;
        }

        /*public Position nextMove()
        {
            if (path.Count == 0)
                return new Position(-1, -1);
            else
                return path[path.Count - 1];
        }*/

        private List<Position> path = new List<Position>();
        bool findPath = false;

        /**************************
          POSSIBLE MOVE AUTHORIZED
          [-1, -1] [0, -1] [1, -1]
          [-1,  0] [0,  0] [1,  0]
          [-1,  1] [0,  1] [1,  1] 
         **************************/
        Point[] _movements = new Point[]
	    {
	        new Point(-1, -1),
	        new Point(0, -1),
	        new Point(1, -1),
	        new Point(1, 0),
	        new Point(1, 1),
	        new Point(0, 1),
	        new Point(-1, 1),
	        new Point(-1, 0)
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
            int x, y;
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





            //N'ESSAIE PAS D'ALLER JUSQU'AU JOUEUR!
            bool stuck = true;
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
                            if (_squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y] == SquareContent.Empty && _squares[position.X + (int)evaluate.X, position.Y + (int)evaluate.Y + height] == SquareContent.Empty && _squares[position.X + (int)evaluate.X + width, position.Y + (int)evaluate.Y + height] == SquareContent.Empty)
                            {
                                x = position.X + (int)evaluate.X;
                                y = position.Y + (int)evaluate.Y;
                                stuck = false;
                                break;
                            }
                        }
                    }
                    if (stuck)
                        return evaluate;
                }
                
            }   
            return new Position(x, y);






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
