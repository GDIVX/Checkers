using Mishbetzet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTilemapRenderer : IRenderer
{
    RendererData data;
    public void Render(Tilemap tilemap)
    {
        //Load data
        data ??= Resources.Load<RendererData>("RendererData");

        //Load the tilemap prefab


        //Instantiate the tilemap prefab

    }
}
