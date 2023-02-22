using Mishbetzet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

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
            


            

            //TODO make sure men can only move diagonally
            return base.TryStep(position);


        }

        List<Tile> AvailableMoves(bool isMovingRight, Point position)
        {
            List<Tile> availablePositions = new List<Tile>();
            if (isMovingRight)
            {
                // check NE/SE
                Tile? NETile = CheckDiagonal(Point.NorthEast);

                //if not null then add to availablePositions
                
                


            }
        }

        Tile CheckDiagonal(Point direction)
        {
            // check NE/SE
            Tile? tileInDirection = Core.Main.Tilemap.GetTile(Tile.Position + direction);

            if(tileInDirection == null)
            {
                return null;
            }

            TileObject? tileObjectInDirection = tileInDirection.tileObject;

            if(tileObjectInDirection == null)
            {
                return tileInDirection;
            }
            //if ally tileobject break
            if(tileObjectInDirection.Actor == Core.Main.TurnManager.CurrentTurn)
            {
                return null;
            }

            //if enemyObject check another NE/SE
            Tile? tileInDirectionAgain = Core.Main.Tilemap.GetTile(tileInDirection.Position + direction);

            if(tileInDirectionAgain == null)
            {
                return null;
            }

            TileObject? tileObjectInDirectionAgain = tileInDirectionAgain.tileObject;

            if(tileObjectInDirectionAgain != null)
            {
                return null;
            }

            return tileInDirectionAgain;

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
