using System.Collections.Generic;

namespace WebApi.Models.Server
{
    public class ServerCreateViewModel
    {
        public string Name { get; set; }
        public ResourceTypeViewModel ResourceType { get; set; }
    }

    public class ResourceTypeViewModel
    {
        public byte Id { get; set; }

        public List<ResourcePropertiesViewModel> Properties { get; set; }
    }

    public class ResourcePropertiesViewModel
    {
        public string Property { get; set; }
    }
}
