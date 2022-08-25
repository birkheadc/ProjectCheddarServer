using ProjectCheddarServer.Game;
using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer;

public class ServerPacketHandler
{
    private readonly Server server;
    private readonly GameLogic gameLogic;

    public ServerPacketHandler(Server server, GameLogic gameLogic)
    {
        this.server = server;
        this.gameLogic = gameLogic;
    }

    public void WelcomeReceived(Guid clientId, Packet packet)
    {
        Guid id = Guid.Parse(packet.ReadString());

        Console.WriteLine("{0} connected successfully and is now registered as id {1}", server.Clients[clientId].Tcp.Socket.Client.RemoteEndPoint, clientId);
    }

    public void UpdatePlayerChunk(Guid clientId, Packet packet)
    {
        Vector2Int last = packet.ReadVector2Int();
        Vector2Int curr = packet.ReadVector2Int();
        gameLogic.PlayerMove(clientId, last, curr);
        // Todo: Actually move the player; send it data from the new chunks that will be in its new radius, and register it to receive updates on those chunks.
    }

    public void PlayerSpawn(Guid clientId, Packet packet)
    {
        Vector2Int curr = packet.ReadVector2Int();
        gameLogic.PlayerSpawn(clientId, curr);
        // Todo: Actually spawn the player; send it data from all the chunks in its radius, and register it to receive updates on those chunks.
    }
}