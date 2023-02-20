using Mishbetzet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singelton
    public static GameManager Instance { get; private set; }


    Core Core => Core.Main;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        //Create the tilemap
        GenerateMap();
        Core.Run();


    }

    private void GenerateMap()
    {
        //Create 8*8 map
        Tilemap map = Core.CreateTileMap(8, 8);

        //Create tiles in checkers pattern
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                Tile tile;
                //Determin if we need to place a white or black tile
                if ((x + y) % 2 == 0)
                {
                    tile = Core.CreateTile<WhiteTile>(new(x, y));
                }
                else
                {
                    tile = Core.CreateTile<BlackTile>(new(x, y));
                }

                //Add the tile to the map
                map.AddTile(tile);
            }
        }
    }
}
