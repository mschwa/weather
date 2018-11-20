using System.Threading.Tasks;

namespace Weather.Api.Services
{
    public interface IResourceRetrievalService<T>
    {
        Task<T> GetResourceAsync(string identifier);
    }
}
