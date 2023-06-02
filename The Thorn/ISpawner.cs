namespace The_Thorn
{
    internal interface ISpawner
    {
        public MovingObject Spawn(bool force = false);
        public void WindowSizeChanged(int height, int width);
    }
}
