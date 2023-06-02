using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Thorn
{
    internal class Cloud : MovingObject
    {
        public Cloud(Game1 game1, int windowHeight, int windowWidth)
            : base(game1, new Vector2(), windowHeight, windowWidth)
        {
            _velocity = new Vector2(-1f, 0f);  // Set the velocity of the cloud to (-1, 0) to make it move to the left
            _textureName = "cloud";  // Set the name of the cloud's texture to "cloud"
            Initialize();  // Call the Initialize method of the base class
            Position = new Vector2(windowWidth + Texture.Width, 70);  // Set the initial position of the cloud to the right side of the window with a Y position of 70
        }
    }
}

