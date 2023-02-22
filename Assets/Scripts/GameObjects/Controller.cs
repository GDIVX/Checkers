using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameObjects;
using Mishbetzet;
using Mishbetzet.Turns;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static TileObject selected;

    Tile tile;

    public event Action<List<Tile>> OnAvailableMovesCalculated;

    private void OnMouseDown()
    {
        tile ??= Core.Main.Tilemap[(int)transform.position.x, (int)transform.position.y];

        TileObject? tileObject = tile.tileObject;

        //check whos turn it is here

        if (selected == null)
        {
            Select(tileObject);
        }
        else
        {
            if (tileObject != null)
            {
                //unselect if same tileobject is chosen
                Deselect(tileObject);
                return;
            }

            //move tileobject here
            selected.Move(new Point(tile.Position.X, tile.Position.Y));
            


            selected = null;
        }

    }

    void Deselect(TileObject tileObject)
    {

        if (selected == tileObject)
        {
            selected = null;
        }
    }

    void Select(TileObject tileObject)
    {
        if (tileObject == null)
        {
            return;
        }

        ITurnTracker actor = tileObject.Actor as ITurnTracker;
        if (actor != Core.Main.TurnManager.CurrentTurn)
        {
            print(actor);
            return;
        }

        selected = tileObject; 

        Man man = tileObject as Man;
        man.SetAvailableMoves(CheckAvailableMoves(man.IsMovingRight, man));
        
        foreach(var t in man.AvailableMoves)
        {
            Debug.Log(t.ToString());
        }
    }

    List<Tile> CheckAvailableMoves(bool isMovingRight, Man manToCheck)
    {
        List<Tile> availablePositions = new List<Tile>();
        if (!isMovingRight)
        {
            // check NE/SE
            Tile? NETile = CheckPointForward(Point.NorthEast);

            //if not null then add to availablePositions
            if (NETile != null)
            {
                availablePositions.Add(NETile);
            }

            Tile? SETile = CheckPointForward(Point.SouthEast);

            if (SETile != null)
            {
                availablePositions.Add(SETile);
            }
        }
        else
        {
            // check NW/SW
            Tile? NWTile = CheckPointForward(Point.NorthWest);

            //if not null then add to availablePositions
            if (NWTile != null)
            {
                availablePositions.Add(NWTile);
            }

            Tile? SWTile = CheckPointForward(Point.SouthWest);

            if (SWTile != null)
            {
                availablePositions.Add(SWTile);
            }
        }

        return availablePositions;
    }


    Tile CheckPointForward(Point direction)
    {
        Tile? tileInDirection = Core.Main.Tilemap.GetTile(selected.Tile.Position + direction);

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
