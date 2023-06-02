using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Thorn
{
    internal class Mountain : MovingObject
    {
        public Mountain(Game1 game1, int windowHeight, int windowWidth)
           : base(game1, new Vector2(), windowHeight, windowWidth)
        {
            _velocity = new Vector2(-2f, 0f);
            _textureName = "mountain";
            Initialize();
            Position = new Vector2(windowWidth + Texture.Width, windowHeight - _game1.GetPlatformHeight() - Texture.Height + 100);
        }
    }
}
