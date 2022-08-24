namespace ProjectCheddarServer.Vectors;

public class Vector2Int
{
    private int x;
    public int X { get { return x; } set { x = value; } }
    private int y;
    public int Y { get { return y; } set { y = value; } }

    public Vector2Int()
    {
        x = 0;
        y = 0;
    }

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}