﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Thorn
{
    internal class ThornSpawner : ISpawner
    {
        private DateTime _lastSpawnTime;  
        private Game1 _game1;  
        private int _windowHeight; 
        private int _windowWidth;  

        // Constructor for the ThornSpawner class
        public ThornSpawner(Game1 game1, int windowHeight, int windowWidth)
        {
            _game1 = game1;
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
            _lastSpawnTime = DateTime.MinValue;
        }

        // Method for spawning a thorn object
        public MovingObject Spawn(bool force)
        {
            if (force)
            {
                Thorn thorn = new Thorn(_game1, _windowHeight, _windowWidth);  
                return thorn; 
            }
            else
            {
                TimeSpan deltaSpan = DateTime.Now - _lastSpawnTime; 

                if (deltaSpan.TotalSeconds > 3)  
                {
                    Thorn thorn = new Thorn(_game1, _windowHeight, _windowWidth); 
                    _lastSpawnTime = DateTime.Now;  
                    return thorn; 
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
