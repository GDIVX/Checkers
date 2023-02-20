using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    public abstract class GameObject : IMovable, ICloneable
    {
        Tile _currentTile;
        Actor _actor;
        /// <summary>
        /// The distance the game object is allowed to move per turn. 
        /// </summary>
        public int MovementRange { get; set; }
        public int RemainingSteps { get; set; }


        public Tile Tile { get => _currentTile; private set => _currentTile = value; }
        public Actor? Actor { get => _actor; internal set => _actor = value; }

        public Point RednerablePoint => Tile.Position;

        public event Action OnStep;


        //should be called if a game object steps on a tile without finishing his move function,
        //meaning he did not land on the tile, just passed over it.
        public event Action<GameObject> OnPassOver;

        #region Senquancing
        /// <summary>
        /// Called every turn
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Called when the game object is added to the game
        /// </summary>
        public virtual void Start() { }

        #endregion


        /// <summary>
        /// Move the game object to the given position
        /// </summary>
        /// <param name="position"></param>
        public virtual void Move(Point position)
        {
            Tilemap? tilemap = Core.Main.Tilemap;

            if (tilemap == null)
            {
                throw new NullReferenceException("Trying to move before the tilemap was created");
            }

            Tile? tile = tilemap[position.X, position.Y];

            if (tile == null) return;

            SetTile(tile);
        }

        /// <summary>
        /// Handle setting tiles
        /// </summary>
        /// <param name="tile"></param>
        internal void SetTile(Tile tile)
        {
            //Remove old tile if needed
            if (Tile != null)
            {
                Tile.gameObject = null;
                Actor?.RemoveTile(Tile);
            }

            //Set new tile
            Actor?.AddTile(tile);
            Tile = tile;
            tile.gameObject = this;
        }


        //onstep should contain one tile movement in any direction
        //based on the move logic for each game object. for example moves in stepdirection North (N) so y--
        public abstract void Step(Point direction);

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
