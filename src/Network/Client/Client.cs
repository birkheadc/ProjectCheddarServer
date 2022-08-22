namespace ProjectCheddarServer;

public class Client
{
    // Todo: Don't hardcode this.
    public static int DataBufferSize = 4096;
    public Guid Id;
    public Tcp Tcp;

    public Client(Guid id, Server server)
    {
        Id = id;
        Tcp = new(Id, server);
    }
}