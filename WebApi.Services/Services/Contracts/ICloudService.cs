using System.Threading.Tasks;
using WebApi.Services.Models.Base;
using WebApi.Services.Models.Server;

namespace WebApi.Services.Services.Contracts
{
    public interface ICloudService
    {
        ServiceResponseModel Create(ServerModel model);
        Task<ServiceResponseModel> CreateAsync(ServerModel model);
        ServiceResponseModel Delete(ServerModel model);
        Task<ServiceResponseModel> DeleteAsync(ServerModel model);
    }
}
