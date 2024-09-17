namespace WebServer.Services
{
    public interface IGuidManager
    {
        Guid GetNewGuid();
    }

    public class GuidManager : IGuidManager
    {
        public Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
