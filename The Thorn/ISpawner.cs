namespace The_Thorn
{
    internal interface ISpawner
    {
        public MovingObject Spawn(bool force = false);  // Method signature for spawning an object, with an optional 'force' parameter
        public void WindowSizeChanged(int height, int width);  // Method signature for handling changes in window size
    }
}
