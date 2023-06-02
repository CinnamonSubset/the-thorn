using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Thorn
{
    internal abstract class MovingObject
    {
        protected Vector2 _position = Vector2.Zero;  // The position of the moving object
        public Texture2D Texture { get; set; }  // The texture of the moving object
       
        protected Vector2 _velocity;  // The velocity of the moving object
        protected Game1 _game1;  // Reference to the Game1 object
        protected string _textureName;  // The name of the texture used for loading
        // Current window width and height used for window resizing 
        protected int _wHeight;  
        protected int _wWidth;  

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        // Constructor for the MovingObject class
        public MovingObject(Game1 game1, Vector2 position, int windowHeight, int windowWidth)
        {
            _game1 = game1;
            Position = position;
            _wHeight = windowHeight;
            _wWidth = windowWidth;
        }

        protected void Initialize()
        {
            Texture = _game1.Content.Load<Texture2D>(_textureName);  // Load the texture using the provided texture name
        }

        public virtual void Update()
        {
            Position += _velocity;  // Update the position of the moving object based on its velocity
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);  // Draw the moving object on the screen
        }

        public float getPosX()
        {
            return Position.X;  // Get the X-coordinate of the moving object's position
        }

        public virtual void WindowSizeChange(int oldHeight, int oldWidth, int newHeight, int newWidth)
        {
            // Calculate the new position relative to the new window size
            float relativeHeight = (Position.Y + Texture.Height / 2) / (float)oldHeight;
            float relativeWidth = (Position.X + Texture.Width / 2) / (float)oldWidth;

            float newPosY = relativeHeight * newHeight - Texture.Height / 2;
            float newPosX = relativeWidth * newWidth - Texture.Width / 2;
            Position = new Vector2(newPosX, newPosY);

            // Cache the new window size
            _wHeight = newHeight;
            _wWidth = newWidth;
        }

        public float getPosXCenter()
        {
            return Position.X + Texture.Width / 2;  // Get the X-coordinate of the center point of the moving object
        }

        public float getTopPosY()
        {
            return Position.Y;  // Get the Y-coordinate of the top position of the moving object
        }
    }
}
