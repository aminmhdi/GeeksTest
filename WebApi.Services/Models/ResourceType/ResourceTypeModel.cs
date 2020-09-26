using System.Collections.Generic;
using WebApi.Services.Models.ResourceProperties;

namespace WebApi.Services.Models.ResourceType
{
    public class ResourceTypeModel
    {
        public Enums.ResourceType Id { get; set; }

        public List<ResourcePropertiesModel> Properties { get; set; }
    }
}
