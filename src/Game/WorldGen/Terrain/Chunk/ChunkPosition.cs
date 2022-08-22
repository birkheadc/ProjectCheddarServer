namespace ProjectCheddarServer.Game;

public class ChunkPosition
{
    private int x;
    public int X { get { return x; } set { x = value; } }
    private int z;
    public int Z { get { return z; } set { z = value; } }
    public ChunkPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}