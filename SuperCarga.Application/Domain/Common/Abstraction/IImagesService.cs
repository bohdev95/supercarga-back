using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Common.Abstraction
{
    public interface IImagesService
    {
        Task<string> SaveUserImage(ImageDto image, Guid userId);

        Task<string> SaveDrivingLicenseImage(ImageDto image, Guid userId);

        Task<string> SaveIdDocumentImage(ImageDto image, Guid userId);

        Task<string> SavePickUpCargoImage(ImageDto image, Guid contractId);

        Task<string> SavePickUpProofImage(ImageDto image, Guid contractId);

        Task<string> SaveDeliveryCargoImage(ImageDto image, Guid contractId);

        Task<string> SaveDeliveryProofImage(ImageDto image, Guid contractId);

        Task<string> SaveCargoImage(ImageDto image, Guid jobtId);
    }
}
