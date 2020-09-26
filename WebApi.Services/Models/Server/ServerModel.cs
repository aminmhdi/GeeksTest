using WebApi.Services.Models.Base;
using WebApi.Services.Models.ResourceType;

namespace WebApi.Services.Models.Server
{
    public class ServerModel : BaseEntityModel
    {
        public string Name { get; set; }
        public ResourceTypeModel ResourceType { get; set; }
    }
}
