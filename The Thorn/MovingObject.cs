using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace The_Thorn
{
    internal abstract class MovingObject
    {
        protected Vector2 _position = Vector2.Zero;
        public Texture2D Texture { get; set; }
        // initialized by the inheriting class
        protected Vector2 _velocity;
        protected Game1 _game1;
        protected string _textureName;
        // current window width and height used for window resizing 
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


        public MovingObject(Game1 game1,Vector2 position, int windowHeight, int windowWidth)
        {
            _game1 = game1;
            Position = position;
            

            _wHeight = windowHeight;
            _wWidth = windowWidth;


        }

        protected void Initialize()
        {
            Texture = _game1.Content.Load<Texture2D>(_textureName);
        }

        public virtual void Update()
        {
            Position += _velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public float getPosX()
        {
            return Position.X;
        }

        

        public virtual void WindowSizeChange(int oldHeight, int oldWidth, int newHeight, int newWidth)
        {
            // calculate new position relative the new window size
            float relativeHeigth = (Position.Y + Texture.Height / 2) / (float)oldHeight;
            float relativeWidth = (Position.X + Texture.Width / 2) / (float)oldWidth;

            float newPosY = relativeHeigth * newHeight - Texture.Height / 2;
            float newPosX = relativeWidth * newWidth - Texture.Width / 2;
            Position = new Vector2(newPosX, newPosY);

            // cache new window size
            _wHeight = newHeight;
            _wWidth = newWidth;
        }

        public float getPosXCenter()
        {
            return Position.X + Texture.Width / 2;
        }
        public float getTopPosY()
        {
            return Position.Y;
        }


    }
}
