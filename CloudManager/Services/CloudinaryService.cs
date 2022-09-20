using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CloudManager.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        public async Task<RawUploadResult> Upload(MemoryStream fileStream)
        {
            var cloudinaryCredentials = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            var cloudinary = new Cloudinary(cloudinaryCredentials);

            cloudinary.Api.Secure = true;

            string publicId = $@"PdfExcelConverter/{Guid.NewGuid()}_{DateTime.Now:dd-MM-yyyy}.xlsx";

            using (fileStream)
            {
                var acc = new AccessControlRule()
                {
                    Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(10),
                };

                var file = new RawUploadParams()
                {
                    PublicId = publicId,
                    AccessControl = new List<AccessControlRule>() { acc },
                    File = new FileDescription(publicId, fileStream)
                };

                return await cloudinary.UploadAsync(file);

            }
        }
    }
}
