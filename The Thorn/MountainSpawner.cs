using System;

namespace The_Thorn
{
    internal class MountainSpawner : ISpawner
    {
        private DateTime _lastSpawnTime;
        private Game1 _game1;
        private int _windowHeight;
        private int _windowWidth;

        public MountainSpawner(Game1 game1, int windowHeight, int windowWidth)
        {
            _game1 = game1;
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
            _lastSpawnTime = DateTime.MinValue;
        }

        public MovingObject Spawn(bool force)
        {
            if (force)
            {
                Mountain mountain = new Mountain(_game1, _windowHeight, _windowWidth);
                return mountain;
            }
            else
            {
                TimeSpan deltaSpan = DateTime.Now - _lastSpawnTime;

                if (deltaSpan.TotalSeconds > 5)
                {
                    Mountain mountain = new Mountain(_game1, _windowHeight, _windowWidth);
                    _lastSpawnTime = DateTime.Now;
                    return mountain;
                }
                return null;
            }

        }
        public void WindowSizeChanged(int height, int width)
        {
            _windowHeight = height;
            _windowWidth = width;
        }
    }
}
