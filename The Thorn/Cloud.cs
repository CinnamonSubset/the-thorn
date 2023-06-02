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
            _velocity = new Vector2(-1f, 0f);
            _textureName = "cloud";
            Initialize();
            Position = new Vector2(windowWidth + Texture.Width, 70);
        }
    }
}
