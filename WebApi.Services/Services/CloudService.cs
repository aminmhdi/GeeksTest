using System;
using System.Threading.Tasks;
using WebApi.Services.Models.Base;
using WebApi.Services.Models.Enums;
using WebApi.Services.Models.Server;
using WebApi.Services.Services.Contracts;
using WebApi.Services.Utilities;

namespace WebApi.Services.Services
{
    public class CloudService : ICloudService
    {
        #region Constructor

        private readonly string _fileStoragePath;
        public CloudService(string fileStoragePath)
        {
            _fileStoragePath = fileStoragePath;
        }

        #endregion

        #region Create

        public ServiceResponseModel Create(ServerModel model)
        {
            try
            {
                var (filename, directory) = FileAndDirectoryName(model);
                var isCreated = Files.Create(model.ToJson(), filename, directory);
                return new ServiceResponseModel
                {
                    Message = isCreated ? "Service is created successfully" : "Failed to create",
                    Success = isCreated
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponseModel> CreateAsync(ServerModel model)
        {
            try
            {
                var (filename, directory) = FileAndDirectoryName(model);
                var isCreated = await Files.CreateAsync(model.ResourceType.ToJson(), filename, directory);
                return new ServiceResponseModel
                {
                    Message = isCreated ? "Service is created successfully" : "Failed to create",
                    Success = isCreated
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel { Message = ex.Message, Success = false };
            }
        }

        #endregion

        #region Delete

        public ServiceResponseModel Delete(ServerModel model)
        {
            try
            {
                var (filename, directory) = FileAndDirectoryName(model);

                if (IsVmExists(model.Name))
                {
                    return new ServiceResponseModel
                    {
                        Success = false,
                        Message = "You have to remove vm first"
                    };
                }

                if (model.ResourceType == null)
                {
                    Files.DeleteDirectory(directory);
                    return new ServiceResponseModel
                    {
                        Message = "Service is completely deleted successfully",
                        Success = true
                    };
                }

                Files.Delete(filename, directory);
                return new ServiceResponseModel
                {
                    Message = "Service is deleted successfully",
                    Success = true
                };
            }

            catch (Exception ex)
            {
                return new ServiceResponseModel { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponseModel> DeleteAsync(ServerModel model)
        {
            try
            {
                var (filename, directory) = FileAndDirectoryName(model);

                if (IsVmExists(model.Name))
                {
                    return new ServiceResponseModel
                    {
                        Success = false,
                        Message = "You have to remove vm first"
                    };
                }

                if (model.ResourceType == null)
                {
                    await Files.DeleteDirectoryAsync(directory);
                    return new ServiceResponseModel
                    {
                        Message = "Service is completely deleted successfully",
                        Success = true
                    };
                }

                await Files.DeleteAsync(filename, directory);
                return new ServiceResponseModel
                {
                    Message = "Service is deleted successfully",
                    Success = true
                };
            }

            catch (Exception ex)
            {
                return new ServiceResponseModel { Message = ex.Message, Success = false };
            }
        }

        #endregion

        #region Private

        private (string filename, string directory) FileAndDirectoryName(ServerModel model)
        {
            var directory = _fileStoragePath + @"\" + model.Name;
            var fileName = string.Empty;

            if (model.ResourceType == null) 
                return (fileName, directory);

            switch (model.ResourceType.Id)
            {
                case ResourceType.Aws:
                    directory += @"\AWS\";
                    fileName += "AWS_Server.json";
                    break;
                case ResourceType.Uat:
                    directory += @"\UAT\";
                    fileName += "UAT_Server.json";
                    break;
                case ResourceType.Vm:
                    directory += @"\VM\";
                    fileName += "VM_Server.json";
                    break;
                case ResourceType.Azure:
                    directory += @"\Azure\";
                    fileName += "Azure_Server.json";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (fileName, directory);
        }

        private bool IsVmExists(string name)
        {
            var directory = $@"{_fileStoragePath}\{name}\VM\";
            const string fileName = "VM_Server.json";

            return Files.Exists(fileName, directory);
        }

        #endregion

    }
}
