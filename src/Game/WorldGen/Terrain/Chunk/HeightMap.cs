namespace ProjectCheddarServer.Game;

public class HeightMap
{
    private byte[,] map;
    public byte[,] Map { get { return map; } set { map = value; } }
    public HeightMap(int size)
    {
        map = new byte[size, size];
    }
}