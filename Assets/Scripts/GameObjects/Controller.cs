using System.Collections;
using System.Collections.Generic;
using Mishbetzet;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static TileObject selected;

    Tile tile;

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
                return;
            }

            //move tileobject here
            selected.Move(new Point(tile.Position.X, tile.Position.Y));

            selected = null;
        }

    }

    void Select(TileObject tileObject)
    {
        if (tileObject == null)
        {
            return;
        }

        //if(tileObject.Actor == ) turn actor
        selected = tileObject;
    }
}
