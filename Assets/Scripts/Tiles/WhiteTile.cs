using Mishbetzet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WhiteTile : Tile
{
    public WhiteTile(Point position , string name) : base(position , name)
    {
    }

    public override string Name => "WhiteTile";
}
