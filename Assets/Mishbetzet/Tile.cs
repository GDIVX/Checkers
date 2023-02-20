using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    public abstract class Tile
    {
        public TileObject tileObject { get; set; }
        public Actor? Actor { get; set; }
        public Point Position { get; set; }

        public abstract string Name { get; }


        #region Constructors
        public Tile(Point position , string name)
        {
            Position = position;
        }

        #endregion

        public void SetGameObject(TileObject newTileObject)
        {
            this.tileObject = newTileObject;
            newTileObject.SetTile(this);
        }

        public override string ToString()
        {
            return $"{Position.X}, {Position.Y}, object: {tileObject}";
        }


    }
}