using Mishbetzet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameObjects
{
    public abstract class Piece : TileObject
    {
        public List<Tile> AvailableMoves { get; private set; } = new();

        public void SetAvailableMoves(List<Tile> availableTilesList)
        {
            AvailableMoves = availableTilesList;
        }

        public Piece()
        {
            OnMove += Core.Main.TurnManager.EndTurn;
        }


        public override void Move(Point position)
        {
            //RemainingSteps = 1;
            base.Move(position);
        }



        //override trystep to only allow diagonal movement
        public override bool TryStep(Point position)
        {

            //Check if we are trying to step on a black tile
            //We can check that using that formula (x + y) % 2 = 1
            //if the result is 1, then the tile is black
            if ((position.X + position.Y) % 2 == 0) return false;

            // Makes sure position is in available moves list
            if (!InAvailableMoves(position))
            {
                return false;
            }

            //TODO make sure men can only move diagonally
            return base.TryStep(position);


        }

        /// <summary>
        /// checks if position is in available moves list
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool InAvailableMoves(Point position)
        {
            foreach (var tile in AvailableMoves)
            {
                if (tile.Position.Equals(position))
                {
                    return true;
                }
            }
            return false;
        }

        public abstract List<Tile> CheckAvailableMoves();


        protected Tile CheckPointForward(Point direction, Tile tileToCheckFrom)
        {
            if(tileToCheckFrom == null)
            {
                return null;
            }

            Tile? tileInDirection = Core.Main.Tilemap.GetTile(tileToCheckFrom.Position + direction);

            if (tileInDirection == null)
            {
                return null;
            }

            TileObject? tileObjectInDirection = tileInDirection.tileObject;

            if (tileObjectInDirection == null)
            {
                return tileInDirection;
            }
            //if ally tileobject break
            if (tileObjectInDirection.Actor == Core.Main.TurnManager.CurrentTurn)
            {
                return null;
            }

            //if enemyObject check another NE/SE
            Tile? tileInDirectionAgain = Core.Main.Tilemap.GetTile(tileInDirection.Position + direction);

            if (tileInDirectionAgain == null)
            {
                return null;
            }

            TileObject? tileObjectInDirectionAgain = tileInDirectionAgain.tileObject;

            if (tileObjectInDirectionAgain != null)
            {
                return null;
            }

            return tileInDirectionAgain;
        }
    }

}

