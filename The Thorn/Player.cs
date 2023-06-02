using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace The_Thorn
{
    internal class Player : InteractiveObject
    {
        private bool _isJumping;

        private const float JumpVelocity = -10f;
        private const float Gravity = 0.5f;

        public Player(Game1 game1, int windowHeight, int windowWidth)
     : base(game1, new Vector2(), windowHeight, windowWidth)
        {
            _velocity = new Vector2(0f, 0f);  // Set the initial velocity of the player
            _textureName = "player";  // Set the name of the texture used for the player
            Initialize();  // Initialize the player object
            Position = new Vector2(50, _game1.GetPlatformHeight());  // Set the initial position of the player
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_isJumping)
            {
                _isJumping = true;
                _velocity.Y = JumpVelocity;  // Set the vertical velocity to perform a jump
            }

            if (_isJumping)
            {
                _velocity.Y += Gravity;  // Apply gravity to the vertical velocity

                if (Position.Y + Texture.Height > _game1.GraphicsDevice.Viewport.Height - _game1.GetPlatformHeight())
                {
                    _isJumping = false;
                    _velocity.Y = 0f;
                    _position.Y = _game1.GraphicsDevice.Viewport.Height - _game1.GetPlatformHeight() - Texture.Height;  // Adjust the position to stay on the platform
                }
            }

            Position += _velocity;  // Update the position based on the velocity

            base.Update();
        }


        public override void WindowSizeChange(int oldHeight, int oldWidth, int newHeight, int newWidth)
        {
            // calculate new position relative the new window size
            float relativeWidth = (Position.X + Texture.Width / 2) / (float)oldWidth;

            float newPosY = newHeight - _game1.GetPlatformHeight() - Texture.Height;
            float newPosX = relativeWidth * newWidth - Texture.Width / 2;
            Position = new Vector2(newPosX, newPosY);

            // cache new window size
            _wHeight = newHeight;
            _wWidth = newWidth;
        }
    }
}
