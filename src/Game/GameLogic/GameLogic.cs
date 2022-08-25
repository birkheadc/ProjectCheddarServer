using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer.Game;

public class GameLogic
{
    private readonly ThreadManager threadManager;
    private PlayerManager playerManager;

    public GameLogic(ThreadManager threadManager)
    {
        this.threadManager = threadManager;
        playerManager = new();
    }

    public void Update()
    {
        threadManager.UpdateMain();
    }

    public void PlayerSpawn(Guid clientId, Vector2Int currentChunk)
    {
        Console.WriteLine("Player {0} has spawned in chunk {1}", clientId, currentChunk.ToString());
        playerManager.PlayerSpawn(clientId, currentChunk);
    }

    public void PlayerMove(Guid clientId, Vector2Int lastChunk, Vector2Int currentChunk)
    {
        Console.WriteLine("Player {0} has moved from {1} to {2}", clientId, lastChunk.ToString(), currentChunk.ToString());
        playerManager.PlayerMove(clientId, lastChunk, currentChunk);
    }
}