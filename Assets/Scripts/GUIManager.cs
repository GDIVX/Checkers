using Mishbetzet;
using System.Collections.Generic;

public class GUIManager
{
    List<Tile> highlights = new();
    public void ShowHighlights(List<Tile> tiles)
    {
        //replace the tiles with highlight tiles
        foreach (var tile in tiles)
        {
            Point position = tile.Position;
            TileObject tileObject = tile.tileObject;
            var highlight = Core.Main.CreateTile<Highlight>(position);
            highlight.tileObject = tileObject;

            //Add to the list for later use
            highlights.Add(highlight);

        }

    }

    public void HideHighlights()
    {
        //replace the highlight tiles with the original tiles
        foreach (Highlight highlight in highlights)
        {
            Point position = highlight.Position;
            TileObject tileObject = highlight.tileObject;

            Tile tile = (position.X + position.Y) % 2 == 0 ?
                Core.Main.CreateTile<WhiteTile>(position) : Core.Main.CreateTile<BlackTile>(position);
            tile.tileObject = tileObject;

            Core.Main.Tilemap.AddTile(tile);
        }

        highlights.Clear();
    }

}
