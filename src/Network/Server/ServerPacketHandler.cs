using ProjectCheddarServer.Vectors;

namespace ProjectCheddarServer;

public class ServerPacketHandler
{
    private readonly Server server;

    public ServerPacketHandler(Server server)
    {
        this.server = server;
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

        Console.WriteLine("Player {0} has moved from {1} to {2}", clientId, last.ToString(), curr.ToString());
        // Todo: Actually move the player; send it data from the new chunks that will be in its new radius, and register it to receive updates on those chunks.
    }

    public void PlayerSpawn(Guid clientId, Packet packet)
    {
        Vector2Int curr = packet.ReadVector2Int();

        Console.WriteLine("Player {0} has spawned in chunk {1}", clientId, curr.ToString());
        // Todo: Actually spawn the player; send it data from all the chunks in its radius, and register it to receive updates on those chunks.
    }
}