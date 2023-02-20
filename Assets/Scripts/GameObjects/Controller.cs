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
            if(tileObject == null)
            {
                return;
            }

            //if(tileObject.Actor == ) turn actor
            selected = tileObject;
        }
        else
        {
            if (tileObject != null)
            {
                return;
            }
            
            //move tileobject here

            selected = null;
        }



    }
}
