using ProjectCheddarServer.Constants;
using ProjectCheddarServer.Helpers;
using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer.Game;

public class PlayerManager
{
    private Dictionary<Vector2Int, HashSet<Guid>> clientsRenderingChunk;
    private Dictionary<Guid, HashSet<Vector2Int>> chunksRenderedByClient;

    public PlayerManager()
    {
        clientsRenderingChunk = new();
        chunksRenderedByClient = new();        
    }

    public void PlayerSpawn(Guid clientId, Vector2Int currentChunk)
    {
        List<Vector2Int> chunks = new();
        chunks.AddRange(ChunkCalculations.CalculateChunksWithinRadiusOfChunk(currentChunk, GameConstants.PLAYER_CHUNK_LOAD_RADIUS));
        foreach(Vector2Int chunk in chunks)
        {
            AddClientToClientsRenderingChunk(clientId, chunk);
        }

        Console.WriteLine("Player {0} is now rendering the following {1} chunks: ", clientId, chunksRenderedByClient[clientId].Count);
        foreach (Vector2Int chunk in chunksRenderedByClient[clientId])
        {
            Console.WriteLine(chunk.ToString());
        }
    }

    public void PlayerMove(Guid clientId, Vector2Int lastChunk, Vector2Int currentChunk)
    {
        if (lastChunk.Equals(currentChunk) == true) return;
        Vector2Int difference = Vector2Int.Difference(lastChunk, currentChunk);

        List<Vector2Int> chunksToRemove = new();
        List<Vector2Int> chunksToAdd = new();

        chunksToAdd.AddRange(ChunkCalculations.CalculateChunksWithinRadiusOfChunk(currentChunk, GameConstants.PLAYER_CHUNK_LOAD_RADIUS));

        foreach (Vector2Int chunk in chunksRenderedByClient[clientId])
        {
            if (chunksToAdd.Contains(chunk) == false)
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (Vector2Int chunk in chunksToRemove)
        {
            RemoveClientFromClientsRenderingChunk(clientId, chunk);
        }
        foreach (Vector2Int chunk in chunksToAdd)
        {
            AddClientToClientsRenderingChunk(clientId, chunk);
        }

        Console.WriteLine("Player {0} is now rendering the following {1} chunks: ", clientId, chunksRenderedByClient[clientId].Count);
        foreach (Vector2Int chunk in chunksRenderedByClient[clientId])
        {
            Console.WriteLine(chunk.ToString());
        }
    }

    private void AddClientToClientsRenderingChunk(Guid clientId, Vector2Int chunk)
    {
        if (clientsRenderingChunk.ContainsKey(chunk) == false)
        {
            clientsRenderingChunk.Add(chunk, new());
        }
        clientsRenderingChunk[chunk].Add(clientId);

        if (chunksRenderedByClient.ContainsKey(clientId) == false)
        {
            chunksRenderedByClient.Add(clientId, new());
        }
        chunksRenderedByClient[clientId].Add(chunk);
    }

    private void RemoveClientFromClientsRenderingChunk(Guid clientId, Vector2Int chunk)
    {
        if (clientsRenderingChunk.ContainsKey(chunk) == true)
        {
            clientsRenderingChunk[chunk].Remove(clientId);
        }
        
        if (chunksRenderedByClient.ContainsKey(clientId) == true)
        {
            chunksRenderedByClient[clientId].Remove(chunk);
        }
    }
}