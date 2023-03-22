using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Common
{
    public class ImagesService : IImagesService
    {
        private readonly ImageStoresConfig imageStoreConfig;

        public ImagesService(ImageStoresConfig imageStoreConfig)
        {
            this.imageStoreConfig = imageStoreConfig;
        }

        //TODO split to separate services (?)

        #region contracts

        public Task<string> SaveDeliveryCargoImage(ImageDto image, Guid contractId) => SaveContractImage(image, contractId, "delivery", "cargo");

        public Task<string> SaveDeliveryProofImage(ImageDto image, Guid contractId) => SaveContractImage(image, contractId, "delivery", "proof");

        public Task<string> SavePickUpCargoImage(ImageDto image, Guid contractId) => SaveContractImage(image, contractId, "pickup", "cargo");

        public Task<string> SavePickUpProofImage(ImageDto image, Guid contractId) => SaveContractImage(image, contractId, "pickup", "proof");

        private Task<string> SaveContractImage(ImageDto image, Guid contractId, string action, string type)
        {
            var directory1 = contractId.ToString().Substring(0, 5);
            var directory2 = contractId.ToString();
            var fileName = $"{action}_{type}{image.Extension.ToLower().Trim()}";
            var basePath = $"{directory1}/{directory2}/{fileName}";
            var phisicalPath = $"{imageStoreConfig.Contracts.PhisicalPath}/{basePath}";
            var path = $"{imageStoreConfig.Contracts.Path}/{basePath}";

            CreateDirectoryIfNotExists(phisicalPath);

            File.WriteAllBytes(phisicalPath, Convert.FromBase64String(image.Content));

            return Task.FromResult(path);
        }

        #endregion

        #region users

        public Task<string> SaveUserImage(ImageDto image, Guid userId) => SaveUserImage(image, userId, "image");

        public Task<string> SaveDrivingLicenseImage(ImageDto image, Guid userId) => SaveUserImage(image, userId, "license");

        public Task<string> SaveIdDocumentImage(ImageDto image, Guid userId) => SaveUserImage(image, userId, "idDocument");

        private Task<string> SaveUserImage(ImageDto image, Guid userId, string fileType)
        {
            var path = $"{imageStoreConfig.Users.Path}/{imageStoreConfig.Users.Default}";

            if (image != null)
            {
                var directory1 = userId.ToString().Substring(0, 5);
                var directory2 = userId.ToString();
                var fileName = $"{fileType}{image.Extension.ToLower().Trim()}";
                var basePath = $"{directory1}/{directory2}/{fileName}";
                var phisicalPath = $"{imageStoreConfig.Users.PhisicalPath}/{basePath}";
                path = $"{imageStoreConfig.Users.Path}/{basePath}";

                CreateDirectoryIfNotExists(phisicalPath);

                File.WriteAllBytes(phisicalPath, Convert.FromBase64String(image.Content));
            }

            return Task.FromResult(path);
        }

        #endregion

        #region jobs

        public Task<string> SaveCargoImage(ImageDto image, Guid jobtId) => SaveJobImage(image, jobtId, "cargo");

        private Task<string> SaveJobImage(ImageDto image, Guid jobId, string fileType)
        {
            var directory1 = jobId.ToString().Substring(0, 5);
            var directory2 = jobId.ToString();
            var fileName = $"{fileType}{image.Extension.ToLower().Trim()}";
            var basePath = $"{directory1}/{directory2}/{fileName}";
            var phisicalPath = $"{imageStoreConfig.Jobs.PhisicalPath}/{basePath}";
            var path = $"{imageStoreConfig.Jobs.Path}/{basePath}";

            CreateDirectoryIfNotExists(phisicalPath);

            File.WriteAllBytes(phisicalPath, Convert.FromBase64String(image.Content));

            return Task.FromResult(path);
        }

        #endregion

        private void CreateDirectoryIfNotExists(string phisicalPath)
        {
            var dirPath = Path.GetDirectoryName(phisicalPath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}
