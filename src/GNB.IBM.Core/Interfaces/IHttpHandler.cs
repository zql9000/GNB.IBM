namespace GNB.IBM.Core.Interfaces
{
    public interface IHttpHandler<T>
    {
        Task<List<T>?> GetAsync(string url);
    }
}
