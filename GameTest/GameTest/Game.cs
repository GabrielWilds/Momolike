using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MapGen;

namespace GameTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MomolikeGame : Microsoft.Xna.Framework.Game
    {
        public static Texture2D RED { get; private set; }


        public static readonly Rectangle SCREEN_BOUNDS = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;
        public const float UPDATES_PER_SECOND = 60;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle _screenRectangle;
        private Player _player;
        private int frameSkip = 0;

        private List<ObjectBase> _activeObjects = new List<ObjectBase>();
        
        public MomolikeGame()
        {            
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _screenRectangle = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));
        }

        public Texture2D LoadSprite(string path)
        {
            return this.Content.Load<Texture2D>(path);
        }

        public void ClearActiveObjects()
        {
            _activeObjects.Clear();
            _activeObjects.Add(_player);
        }

        public void AddActiveObject(ObjectBase obj)
        {
            _activeObjects.Add(obj);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            CollisionHashMap.Initialize();

            this._player = new Player();
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / UPDATES_PER_SECOND);


            ClearActiveObjects();
            for (int i = 0; i < 0; i++)
            {
                Rock rock = new Rock(new Vector2(MapGen.Randomizer.GetRandomNumber(SCREEN_WIDTH), MapGen.Randomizer.GetRandomNumber(SCREEN_HEIGHT)));
                rock.EnforceBounds();
                AddActiveObject(rock);
            }
            AddActiveObject(new Rock(new Vector2(500f, 300f)));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content into pipeline.  Shouldn't be doing this in the middle of gameplay.
            Content.Load<Texture2D>("bunnySprite");

            MomolikeGame.RED = new Texture2D(_graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            RED.SetData(new Color[] { new Color(255, 0, 0) });

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach (var item in _activeObjects)
                item.Update();

            CollisionHashMap.CheckCollisions(_activeObjects);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var item in _activeObjects)
                item.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
