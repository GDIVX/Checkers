using Mishbetzet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Highlight : Tile
{
    public Highlight(Point position , string name) : base(position , name)
    {
    }

    public override string Name => "Highlight";
}
