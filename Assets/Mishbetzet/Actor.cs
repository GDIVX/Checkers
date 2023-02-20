using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    public class Actor
    {
        internal List<Tile> tiles;
        internal List<TileObject> gameObjects;

        internal Actor()
        {
            tiles = new();
            gameObjects = new();
        }

        /// <summary>
        /// Add a tile to the actor
        /// </summary>
        /// <param name="tile"></param>
        public void AddTile(Tile tile)
        {
            if (tile is null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            tile.Actor = this;
            tiles.Add(tile);
        }

        /// <summary>
        /// Remove a tile from the actor
        /// </summary>
        /// <param name="tile"></param>
        public void RemoveTile(Tile tile)
        {
            tile.Actor = null;
            tiles.Remove(tile);
        }

        /// <summary>
        /// Add a game object to the actor
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(TileObject gameObject)
        {
            gameObject.Actor = this;
            gameObjects.Add(gameObject);
            AddTile(gameObject.Tile);
        }


        /// <summary>
        /// Remove a game object from the actor
        /// </summary>
        /// <param name="gameObject"></param>
        public void RemoveGameObject(TileObject gameObject)
        {
            gameObject.Actor = null;
            gameObjects.Remove(gameObject);
            RemoveTile(gameObject.Tile);
        }
    }
}