using Microsoft.AspNetCore.Mvc;
using System.IO;
using FilesLibrary.Interfaces;
using FilesLibrary.Models;
using System;
using IR_Xp_Pdf_Excel_Converter_API.Models;
using CloudManager.Interfaces;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using System.Collections.Generic;

namespace IR_Xp_Pdf_Excel_Converter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        public IPdfService _pdfService { get; set; }
        public INpoiService _npoiService { get; set; }
        public ICloudinaryService _cloudinaryService { get; set; }

        public ConverterController(IPdfService pdfService, INpoiService npoiService, ICloudinaryService cloudinaryService)
        {
            _pdfService = pdfService;
            _npoiService = npoiService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var downloadLinks = new List<Uri>();

                foreach (var file in Request.Form.Files)
                {
                    PdfTextReturn pdfDocument = _pdfService.GetPdfDocument(file.OpenReadStream());
                    EarningsReturn earnings = _pdfService.GetEarnings(pdfDocument);
                    MemoryStream excelFile = _npoiService.GenerateExcelFile(earnings);

                    var uploadResult = await _cloudinaryService.Upload(excelFile);

                    downloadLinks.Add(uploadResult.SecureUrl);
                }

                return Ok(new ReturnModel() { success = true, downloadLinks = downloadLinks });
            }
            catch (Exception ex)
            {
                return Ok(new ReturnModel() { success = false, message = ex.Message });
            }
        }
    }
}
