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
            // Makes sure these are the next tiles
            //if(MathF.Abs(position.X-Tile.Position.X) > 1) return false;

            //Eating Try
            if (position.Equals(Tile.Position + Point.NorthWest + Point.NorthWest) && Core.Main.Tilemap.GetTile(Tile.Position + Point.NorthWest + Point.NorthWest).tileObject == null)
            {
                Point enemyPos = Tile.Position + Point.NorthWest;
                TileObject? enemyObject = Core.Main.Tilemap.GetTile(enemyPos).tileObject;
                if (enemyObject != null && enemyObject.Actor != this.Actor)
                {
                    UnityEngine.Debug.Log("fuck");
                    UnityEngine.Debug.Log(Core.Main.Tilemap.GetTile(enemyPos).tileObject);
                    TileObject? nullobject = null;
                    Core.Main.Tilemap.GetTile(enemyPos).tileObject = nullobject;
                    RemainingSteps = 2;
                }
            }


            

            //TODO make sure men can only move diagonally
            return base.TryStep(position);


        }

    }
}

/* get current location and destination
 * check if destniation is eliagble ("right" dir, distance) pawn = 2 eat one; king enemyloc + 1
 * 
 * Man
 * check 2 possible move
 * blocked by enemy? is the next one free?
 * 
 * King
 * check 4 possible move directions
 * blocked by enemy? is the next one free?
 * 
 * 
 * 
 */
