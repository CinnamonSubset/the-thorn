using System;

namespace The_Thorn
{
    internal class CloudSpawner : ISpawner
    {
        private DateTime _lastSpawnTime;  
        private Game1 _game1;  
        private int _windowHeight; 
        private int _windowWidth;  

        // Constructor for the CloudSpawner class
        public CloudSpawner(Game1 game1, int windowHeight, int windowWidth)
        {
            _game1 = game1;
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
            _lastSpawnTime = DateTime.MinValue;  
        }

        // Method for spawning a cloud object
        public MovingObject Spawn(bool force)
        {
            if (force)
            {
                Cloud cloud = new Cloud(_game1, _windowHeight, _windowWidth);  
                _lastSpawnTime = DateTime.Now; 
                return cloud;  
            }
            else
            {
                TimeSpan deltaSpan = DateTime.Now - _lastSpawnTime;  

                if (deltaSpan.TotalSeconds > 10)  
                {
                    Cloud cloud = new Cloud(_game1, _windowHeight, _windowWidth);  
                    _lastSpawnTime = DateTime.Now; 
                    return cloud; 
                }

                return null;  
            }
        }

        // Method for handling changes in window size
        public void WindowSizeChanged(int height, int width)
        {
            _windowHeight = height;  
            _windowWidth = width; 
        }
    }
}

