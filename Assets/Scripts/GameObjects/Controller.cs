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

        Piece piece = tileObject as Piece;
        piece.SetAvailableMoves(piece.CheckAvailableMoves());
        
        foreach(var t in piece.AvailableMoves)
        {
            Debug.Log(t.ToString());
        }
    }

}
