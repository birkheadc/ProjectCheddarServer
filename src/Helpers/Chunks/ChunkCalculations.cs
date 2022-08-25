using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer.Helpers;

public class ChunkCalculations
{
    ///<summary>Returns the Vector2D of all chunks in a square around the center chunk.</summary>
    public static IEnumerable<Vector2Int> CalculateChunksWithinRadiusOfChunk(Vector2Int centerChunk, int radius)
    {
        List<Vector2Int> chunks = new();
        for (int i = centerChunk.X - radius; i <= centerChunk.X + radius; i++)
        {
            for (int j = centerChunk.Y - radius; j <= centerChunk.Y + radius; j++)
            {
                Vector2Int chunk = new Vector2Int() { X = i, Y = j };
                chunks.Add(chunk);
            }
        }
        return chunks;
    }
}