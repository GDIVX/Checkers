using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mishbetzet.GameObject;

namespace Mishbetzet
{
    internal interface IMovable
    {
        //bool isMoving { get; set; } //a way to check if the game object is stationary?

        public abstract void Step(Point direction);

        public abstract void Move(Point position);
    }
}