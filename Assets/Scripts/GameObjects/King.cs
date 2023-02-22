using Mishbetzet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameObjects
{
    public class King : Piece
    {
        public override List<Tile> CheckAvailableMoves()
        {
            //CheckPointForward

            //continue in diagonal 8 moves
            //if tile is empty return tile
            //if tile has object, check if object behind
            //if object behind it then return
            //if no object then return tile

            List<Tile> allAvailableTiles = new();

            allAvailableTiles.AddRange(CheckMultiplePointsForward(Point.NorthEast));
            allAvailableTiles.AddRange(CheckMultiplePointsForward(Point.NorthWest));
            allAvailableTiles.AddRange(CheckMultiplePointsForward(Point.SouthEast));
            allAvailableTiles.AddRange(CheckMultiplePointsForward(Point.SouthWest));

            return allAvailableTiles;
        }

        List<Tile> CheckMultiplePointsForward(Point direction)
        {
            List<Tile> multipleTilesForward = new();

            Tile? tileToCheck = Tile;

            //for loop 7 times for every direction, with nested if for double object
            for (int i = 0; i < 7; i++)
            {
                Tile? tileToAdd = CheckPointForward(direction, tileToCheck);
                if (tileToAdd == null)
                {
                    break;
                }
                //check if tile to add == tile to check + direction twice. is yes then break;
                if(tileToAdd == Core.Main.Tilemap.GetTile(tileToCheck.Position + direction + direction))
                {
                    multipleTilesForward.Add(tileToAdd);
                    break;
                }

                multipleTilesForward.Add(tileToAdd);
                tileToCheck = Core.Main.Tilemap.GetTile(tileToCheck.Position + direction);
            }
            return multipleTilesForward;
        }
    }
}
