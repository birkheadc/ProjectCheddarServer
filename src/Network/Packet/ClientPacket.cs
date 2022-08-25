namespace ProjectCheddarServer;
/// <summary>Sent from client to server.</summary>
public enum ClientPacket
{
    WelcomeReceived = 1,
    UpdatePlayerChunk = 2,
    PlayerSpawn = 3
}