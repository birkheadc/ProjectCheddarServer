using ProjectCheddarServer.Game;
using ProjectCheddarServer.Network;

namespace ProjectCheddarServer;

public class ServerPacketSender
{
    private readonly Server server;
    public ServerPacketSender(Server server)
    {
        this.server = server;
    }

    private void SendTCPData(Guid clientId, Packet packet)
    {
        packet.WriteLength();
        server.SendDataToClient(clientId, packet);
    }
    public void SendWelcome(Guid clientId, string msg)
    {
        using (Packet packet = PacketBuilder.CreateWelcomePacket(clientId, msg))
        {
            SendTCPData(clientId, packet);
        }
    }
}