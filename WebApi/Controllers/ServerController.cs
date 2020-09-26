using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Models.Server;
using WebApi.Services.Models.Enums;
using WebApi.Services.Models.ResourceProperties;
using WebApi.Services.Models.ResourceType;
using WebApi.Services.Models.Server;
using WebApi.Services.Services.Contracts;

namespace WebApi.Controllers
{
    public class ServerController : BaseController
    {
        private readonly ILogger<ServerController> _logger;
        private readonly ICloudService _cloudService;

        public ServerController
        (
            ILogger<ServerController> logger,
            ICloudService cloudService
        )
        {
            _logger = logger;
            _cloudService = cloudService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ServerCreateViewModel viewModel)
        {
            var model = new ServerModel
            {
                Name = viewModel.Name,
                ResourceType = new ResourceTypeModel
                {
                    Id = (ResourceType)viewModel.ResourceType.Id,
                    Properties = viewModel.ResourceType.Properties
                        .Select(q => new ResourcePropertiesModel { Property = q.Property }).ToList()
                }
            };

            var result = await _cloudService.CreateAsync(model);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] ServerDeleteViewModel viewModel)
        {
            var model = new ServerModel
            {
                Name = viewModel.Name,
                ResourceType = viewModel.ResourceType.HasValue ? new ResourceTypeModel { Id = (ResourceType)viewModel.ResourceType.Value } : null
            };
            var result = await _cloudService.DeleteAsync(model);
            return Ok(result);

        }
    }
}
