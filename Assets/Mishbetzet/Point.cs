using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point North => new Point(0, 1);
        public static Point South => new Point(0, -1);
        public static Point East => new Point(1, 0);
        public static Point West => new Point(-1, 0);
        public static Point NorthEast => new Point(1, 1);
        public static Point NorthWest => new Point(-1, 1);
        public static Point SouthEast => new Point(1, -1);
        public static Point SouthWest => new Point(-1, -1);



        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        override public string ToString()
        {
            return $"({X}, {Y})";
        }

        override public bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Point p = (Point)obj;
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            var x = X + 1;
            var y = Y + 1;
            return x * y;
        }
    }
}
