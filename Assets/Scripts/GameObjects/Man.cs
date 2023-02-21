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
        bool isMovingRight;
        public bool IsMovingRight { get => isMovingRight; set => isMovingRight = value; }
        public Man()
        {
            OnMove += Core.Main.TurnManager.EndTurn;
        }


        public override void Move(Point position)
        {


            RemainingSteps = 1;
            base.Move(position);
        }



        //override trystep to only allow diagonal movement
        public override bool TryStep(Point position)
        {

            //Check if we are trying to step on a black tile
            //We can check that using that formula (x + y) % 2 = 1
            //if the result is 1, then the tile is black
            if ((position.X + position.Y) % 2 == 0) return false;

            //Make sure that we are not moving backwards
            if (IsMovingRight && position.X > Tile.Position.X) return false;
            if (!IsMovingRight && position.X < Tile.Position.X) return false;

            if(MathF.Abs(position.X-Tile.Position.X) > 1) return false;

            //TODO make sure men can only move diagonally
            return base.TryStep(position);
        }

    }
}
