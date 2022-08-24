using ProjectCheddarServer.Constants;
using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer.Game;

public class Chunk
{
    private byte[,] heightMap;
    private Vector2Int position;
    private int size;

    public Chunk(int xPos, int zPos)
    {
        size = GameConstants.CHUNK_SIZE;
        heightMap = new byte[size, size];
        position = new(xPos, zPos);
    }
}