using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mishbetzet.TileObject;

namespace Mishbetzet
{
    internal interface IMovable
    {

        public abstract bool TryStep(Point direction);

        public void Move(Point position)
        {
            int steps = 0;
            while (TryStep(position))
            {
                if (steps < 20)
                {
                    steps++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}