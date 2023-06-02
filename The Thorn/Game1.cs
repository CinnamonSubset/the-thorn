using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Thorn
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private Texture2D platformtexture;

        private bool isPlaying;

        private int wHeight;
        private int wWidth;

        private List<MovingObject> _movingObjects;
        private List<ISpawner> _spawners;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _spawners = new List<ISpawner>();  // Initialize the list of spawners
            _movingObjects = new List<MovingObject>();  // Initialize the list of moving objects
        }


        protected override void Initialize()
        {
            this.Window.AllowUserResizing = true;  
            wHeight = this.Window.ClientBounds.Height;  // Get the current height of the game window
            wWidth = this.Window.ClientBounds.Width;  // Get the current width of the game window
            this.Window.ClientSizeChanged += Window_ClientSizeChanged;  // Subscribe to the event when the client size of the window changes

            base.Initialize();  
        }


        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            foreach (var obj in _movingObjects)
            {
                obj.WindowSizeChange(wHeight, wWidth, GraphicsDevice.Viewport.Bounds.Height, GraphicsDevice.Viewport.Bounds.Width);
            }

            // Cache the new window boundaries
            wHeight = GraphicsDevice.Viewport.Bounds.Height; 
            wWidth = GraphicsDevice.Viewport.Bounds.Width;  

            foreach (var spawner in _spawners)
            {
                spawner.WindowSizeChanged(wHeight, wWidth);  // Notify each spawner that the window size has changed
            }
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font"); 
            platformtexture = Content.Load<Texture2D>("platform");  

            // Create and add game objects and spawners
            _movingObjects.Add(new Player(this, wHeight, wWidth));  
            _spawners.Add(new CloudSpawner(this, wHeight, wWidth)); 
            _spawners.Add(new MountainSpawner(this, wHeight, wWidth)); 
            _spawners.Add(new ThornSpawner(this, wHeight, wWidth));  
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

            // If we are not playing, Update() should not be run further
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
                        // Normalize the collision rectangles relative to the intersection
                        Rectangle r1 = Thorn.Normalize(playerBox, collision);
                        Rectangle r2 = Thorn.Normalize(thornBox, collision);

                        if (Thorn.TestCollision(_movingObjects[0].Texture, r1, thorn.Texture, r2))
                        {
                            // If there is a collision, set the isPlaying flag to false to indicate that the game is no longer playing
                            isPlaying = false;
                        }
                    }
                }
            }

            foreach (var spawner in _spawners)
            {
                MovingObject item = spawner.Spawn();  // Call the Spawn method of each spawner to get a new MovingObject item

                if (item != null)
                {
                    _movingObjects.Add(item);  // Add the spawned item to the _movingObjects list if it is not null
                }
            }
            foreach (var movingObject in _movingObjects)
            {
                movingObject.Update();  // Call the Update method of each moving object in the _movingObjects list
            }

            base.Update(gameTime);  // Call the base class's Update method to perform any necessary base updates
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);  

            _spriteBatch.Begin();  

            if (isPlaying)
            {
                // Draw mountains and clouds first
                foreach (var item in _movingObjects)
                {
                    if (item is Mountain || item is Cloud)
                    {
                        item.Draw(_spriteBatch);  
                    }
                }

                // Draw thorns and player on top
                foreach (var item in _movingObjects)
                {
                    if (item is Thorn || item is Player)
                    {
                        item.Draw(_spriteBatch); 
                    }
                }
            }
            else
            {
                // Draw "Press ENTER to begin" message
                Vector2 stringSize = font.MeasureString("Press ENTER to begin");  
                float textPosX = GraphicsDevice.Viewport.Bounds.Center.X - stringSize.X / 2; 
                float textPosY = GraphicsDevice.Viewport.Bounds.Center.Y - stringSize.Y / 2;  
                _spriteBatch.DrawString(font, "Press ENTER to begin", new Vector2(textPosX, textPosY), Color.White);  
            }

            _spriteBatch.Draw(platformtexture, new Vector2(0, wHeight - platformtexture.Height), Color.White); 

            _spriteBatch.End();  

            base.Draw(gameTime);  
        }

        private void Reset()
        {
            // Remove all moving objects except the player
            while (_movingObjects.Count > 1)
            {
                _movingObjects.RemoveAt(1);  
            }
        }

        public int GetPlatformHeight()
        {
            return platformtexture.Height;  // Return the height of the platform texture
        }

    }
}