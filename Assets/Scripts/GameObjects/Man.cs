using Mishbetzet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.GameObjects
{
    public class Man : Piece
    {
        bool isMovingRight;
        public bool IsMovingRight { get => isMovingRight; set => isMovingRight = value; }

        //override trystep to only allow diagonal movement
        public override bool TryStep(Point position)
        {

            //Make sure that we are not moving backwards
            if (IsMovingRight && position.X > Tile.Position.X) return false;
            if (!IsMovingRight && position.X < Tile.Position.X) return false;


            return base.TryStep(position);

        }

        public override List<Tile> CheckAvailableMoves()
        {
            List<Tile> availablePositions = new List<Tile>();
            if (!isMovingRight)
            {
                // check NE/SE
                Tile? NETile = CheckPointForward(Point.NorthEast, Tile);

                //if not null then add to availablePositions
                if (NETile != null)
                {
                    availablePositions.Add(NETile);
                }

                Tile? SETile = CheckPointForward(Point.SouthEast, Tile);

                if (SETile != null)
                {
                    availablePositions.Add(SETile);
                }
            }
            else
            {
                // check NW/SW
                Tile? NWTile = CheckPointForward(Point.NorthWest, Tile);

                //if not null then add to availablePositions
                if (NWTile != null)
                {
                    availablePositions.Add(NWTile);
                }

                Tile? SWTile = CheckPointForward(Point.SouthWest, Tile);

                if (SWTile != null)
                {
                    availablePositions.Add(SWTile);
                }
            }

            return availablePositions;
        }

        public void CheckCreateKing()
        {
            if(isMovingRight)
            {
                if(Tile.Position.X == 0)
                {
                    GameManager.Instance.CreateKing<BlackKing>(Tile.Position.X, Tile.Position.Y, GameManager.Instance.GetBlackTeam());
                }
            }
            else
            {
                if(Tile.Position.X == 7)
                {
                    GameManager.Instance.CreateKing<WhiteKing>(Tile.Position.X, Tile.Position.Y, GameManager.Instance.GetWhiteTeam());
                }
            }
        }
        
    }
}