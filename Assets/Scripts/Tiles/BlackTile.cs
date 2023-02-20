using Mishbetzet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BlackTile : Tile
{
    public BlackTile(Point position) : base(position)
    {
    }

    public override string Name => "BlackTile";
}
