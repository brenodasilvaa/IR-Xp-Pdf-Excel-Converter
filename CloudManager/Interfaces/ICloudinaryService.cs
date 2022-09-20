using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CloudManager.Interfaces
{
    public interface ICloudinaryService
    {
        Task<RawUploadResult> Upload(MemoryStream fileStream);
    }
}
