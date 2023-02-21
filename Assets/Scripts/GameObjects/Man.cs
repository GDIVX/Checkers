using Mishbetzet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameObjects
{
    public class Man : TileObject
    {

        public override void Move(Point position)
        {
            RemainingSteps = 1;
            base.Move(position);
        }

        //override trystep to only allow diagonal movement
        public override bool TryStep(Point direction)
        {
            //TODO make sure men can only move diagonally
            return base.TryStep(direction);
        }

    }
}
