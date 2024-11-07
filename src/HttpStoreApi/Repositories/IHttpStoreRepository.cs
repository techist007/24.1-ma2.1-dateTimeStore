using HttpStoreApi.Models;
using System.Threading.Tasks;

namespace HttpStoreApi.Repositories
{
    public interface IHttpStoreRepository
    {
        Task<string> CallTimeZoneApiAsync(TimeZoneRequest request);
        Task<string> CallPacketApiAsync(PacketRequest request);
    }
}
