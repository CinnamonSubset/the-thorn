namespace The_Thorn
{
    internal interface ISpawner
    {
        public MovingObject Spawn();
        public void WindowSizeChanged(int height, int width);
    }
}
