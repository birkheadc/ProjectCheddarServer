using ProjectCheddarServer.Game;

namespace ProjectCheddarServer.Network;

public static class PacketBuilder
{
    public static Packet CreateWelcomePacket(Guid clientId, string msg)
    {
        Packet packet = new((int)ServerPacket.Welcome);
        packet.Write(msg);
        packet.Write(clientId.ToString());
        return packet;
    }
}