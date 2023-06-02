using Microsoft.Xna.Framework;

namespace The_Thorn
{
    internal class Thorn : InteractiveObject
    {
        public Thorn(Game1 game1, int windowHeight, int windowWidth)
    : base(game1, new Vector2(), windowHeight, windowWidth)
        {
            _velocity = new Vector2(-5f, 0f);  // Set the initial velocity of the thorn
            _textureName = "thorn";  // Set the name of the texture used for the thorn
            Initialize();  // Initialize the thorn object
            Position = new Vector2(windowWidth, windowHeight - _game1.GetPlatformHeight() - Texture.Height);  // Set the initial position of the thorn
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
