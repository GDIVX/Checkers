using Mishbetzet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTilemapRenderer : IRenderer
{
    RendererData data;
    bool renderedTiles = false;

    //create grid of tileObjects[,]
    UnityEngine.GameObject[,] tileObjectGrid = new UnityEngine.GameObject[Core.Main.Tilemap.Width, Core.Main.Tilemap.Height];

    public void Render(Tilemap tilemap)
    {
        //Load data
        data ??= Resources.Load<RendererData>("RendererData");


        //Instantiate the tilemap prefab
        for (int x = 0; x < tilemap.Width; x++)
        {
            for (int y = 0; y < tilemap.Width; y++)
            {
                if (!renderedTiles) RenderTile(x, y, tilemap);

                //Render tile object
                RenderTileObject(x, y, tilemap);
            }
        }
        renderedTiles = true;
    }

    private void RenderTileObject(int x, int y, Tilemap tilemap)
    {
        Tile tile = tilemap[x, y];

        if (tile == null) return;

        //Get the tile object
        TileObject tileObject = tile.tileObject;
        if (tileObject == null)
        {
            //destroy tileobject if there is one instantiated
            if (tileObjectGrid[tile.Position.X, tile.Position.Y] != null)
            {
                UnityEngine.MonoBehaviour.Destroy(tileObjectGrid[tile.Position.X, tile.Position.Y]);
            }
            return;
        }

        //if tileobject already exists on grid at this location
        Vector3 position = new(tileObject.Tile.Position.X, tileObject.Tile.Position.Y, 0);
        if(tileObjectGrid[(int)position.x, (int)position.y] != null)
        {
            return;
        }

        //Get the tile object prefab
        UnityEngine.GameObject prefab = data.GetPrefab(tileObject.Name);

        //Instantiate game object and
        //every new tileobject gets added to the grid at its location
        tileObjectGrid[(int)position.x, (int)position.y] = UnityEngine.GameObject.Instantiate(prefab, position, Quaternion.identity);
    }

    private void RenderTile(int x, int y, Tilemap tilemap)
    {
        Tile tile = tilemap[x, y];

        if (tile == null) return;

        //Get the tile prefab
        UnityEngine.GameObject prefab = data.GetPrefab(tile.Name);

        //Instantiate the prefab
        Vector3 position = new(tile.Position.X, tile.Position.Y, 0);
        UnityEngine.GameObject instance = UnityEngine.GameObject.Instantiate(prefab, position, Quaternion.identity);

    }
}
