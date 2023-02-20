using Mishbetzet.Turns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishbetzet
{
    /// <summary>
    /// The core interface for the engine
    /// </summary>
    public class Core : Engine
    {
        static Core _instance;

        /// <summary>
        /// Singelton for the core engine
        /// </summary>
        public static Core Main
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                }

                return _instance;
            }
        }
        public Tilemap? Tilemap { get; private set; }
        public TurnManager TurnManager { get; private set; } = new();
        public override bool IsRunning => _isRunning;

        public event Action? onEngineStart;
        public event Action? onEngineStop;

        IRenderer renderer;
        ReadlineCommandHandler commandHandler;
        bool _isRunning = false;
        private List<Actor> _actorsInPlay = new();
        private List<TileObject> _gameObjects = new();

        public Core()
        {
            Dictionary<string, Command> newDic = new Dictionary<string, Command>();
            newDic.Add("print", new Print());
            commandHandler = new(newDic);
        }

        #region Factories

        /// <summary>
        /// Creates a tilemap with the given width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Tilemap CreateTileMap(int width, int height)
        {
            Tilemap = new(width, height);
            renderer = new UnityTilemapRenderer();
            return Tilemap;
        }

        /// <summary>
        /// Create a tile and add it to <see cref="Tilemap"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <exception cref="Exception"></exception>
        public void CreateTile<T>(Point position) where T : Tile
        {
            if (Tilemap == null)
            {
                throw new Exception("Tilemap is not initialized");
            }

            var tile = Activator.CreateInstance(typeof(T), position) as Tile;
            if (tile == null)
            {
                throw new Exception($"Tile creation failed for type {typeof(T)}");
            }
            Tilemap.AddTile(tile);
        }

        public TileObject CreateGameObject<T>(Actor owner, Tile tile) where T : TileObject
        {

            #region NULL_CHECKS
            if (owner is null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if (tile is null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            var gameObject = Activator.CreateInstance(typeof(T)) as TileObject;

            if (gameObject == null)
            {
                throw new Exception($"Cannot create a game object of type {typeof(T)} ");
            }
            #endregion

            gameObject.SetTile(tile);
            owner.AddGameObject(gameObject);

            return gameObject;

        }

        public Actor CreateActor()
        {
            var actor = new Actor();
            _actorsInPlay.Add(actor);
            return actor;
        }

        public Actor? CreateActor<T>() where T : Actor
        {
            var actor = Activator.CreateInstance(typeof(T));

            if (actor is Actor act)
            {
                _actorsInPlay.Add(act);
                return act;
            }
            return null;
        }
        #endregion


        /// <summary>
        /// Called at the start
        /// </summary>
        public override void Run()
        {
            onEngineStart?.Invoke();

            if (Tilemap == null) return;
            Update();

        }

        /// <summary>
        /// Called every turn to update the game state
        /// </summary>
        public void Update()
        {
            if (Tilemap == null) return;

            renderer.Render(Tilemap);

            //TODO - create a list of all game objects and call update on them
        }

        public override void Stop()
        {
            onEngineStop?.Invoke();
        }


    }
}
