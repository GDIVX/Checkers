using Assets.Scripts.GameObjects;
using Mishbetzet;
using Mishbetzet.Turns;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singelton
    public static GameManager Instance { get; private set; }


    Core Core => Core.Main;
    CheckersActor blackTeam;
    CheckersActor whiteTeam;


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
        GeneratePlayers();
        GenerateMap();
        GeneratePawnStart(3, 5);
        Core.Run();

    }

    private void Update()
    {
        Core.Update();
    }

    void GeneratePlayers()
    {
        blackTeam = new();
        whiteTeam = new();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="topBorder"></param>
    /// <param name="BotBorder">the place you think :)</param>
    void GeneratePawnStart(int topBorder, int BotBorder)
    {
        for (int i = 0; i < topBorder; i++)
        {
            for (int j = 0; j < Core.Tilemap.Width; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 1)
                    {
                        CreateMan<WhiteMan>(i, j, whiteTeam, false);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        CreateMan<WhiteMan>(i, j, whiteTeam, false);
                    }
                }
            }
        }

        for (int i = BotBorder; i < Core.Tilemap.Height; i++)
        {
            for (int j = 0; j < Core.Tilemap.Width; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 1)
                    {
                        CreateMan<BlackMan>(i, j, blackTeam, true);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        CreateMan<BlackMan>(i, j, blackTeam, true);
                    }
                }
            }
        }
    }

    private void CreateMan<T>(int i, int j, CheckersActor actor, bool isMovingRight) where T : TileObject
    {
        Tile tile = Core.Tilemap[i, j];
        var tileObj = Core.CreateGameObject<T>(actor, tile);
        Man man = tileObj as Man;
        man.IsMovingRight = isMovingRight;
    }


}
