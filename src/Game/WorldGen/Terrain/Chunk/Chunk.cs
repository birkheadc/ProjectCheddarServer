using ProjectCheddarServer.Constants;

namespace ProjectCheddarServer.Game;

public class Chunk
{
    private HeightMap map;
    private ChunkPosition position;
    private int size;

    public Chunk(int xPos, int zPos)
    {
        size = GameConstants.CHUNK_SIZE;
        map = new(size);
        position = new(xPos, zPos);
    }
    
}