using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace The_Thorn
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private Texture2D platformtexture;
        private Vector2 startPos;

        private bool isPlaying;

        private int wHeight;
        private int wWidth;

        private List<MovingObject> _movingObjects;
        private List<ISpawner> _spawners;

        //private MovingObject.Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _spawners = new List<ISpawner>();
            _movingObjects = new List<MovingObject>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.AllowUserResizing = true;
            wHeight = this.Window.ClientBounds.Height;
            wWidth = this.Window.ClientBounds.Width;
            this.Window.ClientSizeChanged += Window_ClientSizeChanged;

            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _movingObjects[0].WindowSizeChange(wHeight, wWidth, GraphicsDevice.Viewport.Bounds.Height, GraphicsDevice.Viewport.Bounds.Width);
            // cache the new window bounderies
            wHeight = GraphicsDevice.Viewport.Bounds.Height;
            wWidth = GraphicsDevice.Viewport.Bounds.Width;

            foreach (var spawner in _spawners)
            {
                spawner.WindowSizeChanged(wHeight, wWidth);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // playerTexture = Content.Load<Texture2D>("player");
            font = Content.Load<SpriteFont>("font");
            platformtexture = Content.Load<Texture2D>("platform");

            _movingObjects.Add(new Player(this, wHeight, wWidth));
            _spawners.Add(new CloudSpawner(this, wHeight, wWidth));
            _spawners.Add(new MountainSpawner(this, wHeight, wWidth));
            _spawners.Add(new ThornSpawner(this, wHeight, wWidth));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState state = Keyboard.GetState();


            if (state.IsKeyDown(Keys.Enter) && !isPlaying)
            {
                // Resets the game (starts over)
                Reset();
                isPlaying = true;
            }

            // If we are not playing, should Update() not be run futher 
            if (!isPlaying)
            {
                return;
            }

            // Hit tests
            Rectangle playerBox = ((InteractiveObject)(_movingObjects[0])).Hitbox();
            foreach (var obj in _movingObjects)
            {
                if (obj is Thorn)
                {
                    Thorn thorn = (Thorn)obj;
                    Rectangle thornBox = thorn.Hitbox();

                    var collision = Thorn.Intersection(playerBox, thornBox);

                    if (collision.Width > 0 && collision.Height > 0)
                    {
                        Rectangle r1 = Thorn.Normalize(playerBox, collision);
                        Rectangle r2 = Thorn.Normalize(thornBox, collision);
                        
                        if (Thorn.TestCollision(_movingObjects[0].Texture, r1, thorn.Texture, r2))
                        {
                            isPlaying = false;
                        }
                    }
                }
            }

            foreach (var spawner in _spawners)
            {
                MovingObject item = spawner.Spawn();

                if (item != null)
                {
                    _movingObjects.Add(item);
                }
            }
            foreach (var movingObject in _movingObjects)
            {
                movingObject.Update();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //  _spriteBatch.Draw(playerTexture, new Vector2(40, 200), Color.White);
            _spriteBatch.Draw(platformtexture, new Vector2(0, wHeight - platformtexture.Height), Color.White);

            if (isPlaying)
            {
                foreach (var item in _movingObjects)
                {
                    item.Draw(_spriteBatch);
                }
            }
            else
            {
                Vector2 stringSize = font.MeasureString("Press ENTER to begin");
                float textPosX = GraphicsDevice.Viewport.Bounds.Center.X - stringSize.X / 2;
                float textPosY = GraphicsDevice.Viewport.Bounds.Center.Y - stringSize.Y / 2;
                _spriteBatch.DrawString(font, "Press ENTER to begin", new Vector2(textPosX, textPosY), Color.White);
            }

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void Reset()
        {
            while (_movingObjects.Count > 1)
            {
                _movingObjects.RemoveAt(1);
            }
          
        }
        public int GetPlatformHeight()
        {
            return platformtexture.Height;
        }
    }
}