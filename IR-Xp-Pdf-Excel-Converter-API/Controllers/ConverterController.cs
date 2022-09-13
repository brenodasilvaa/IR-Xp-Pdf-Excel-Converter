using IR_Xp_Pdf_Excel_Converter_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.WebRequestMethods;
using FilesLibrary.Interfaces;
using FilesLibrary.Models;

namespace IR_Xp_Pdf_Excel_Converter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        public IPdfService _pdfService { get; set; }
        public INpoiService _npoiService { get; set; }

        public ConverterController(IPdfService pdfService, INpoiService npoiService)
        {
            _pdfService = pdfService;
            _npoiService = npoiService;
        }

        [HttpPost]
        public string Get()
        {
            foreach (var file in Request.Form.Files)
            {
                PdfTextReturn pdfDocument = _pdfService.GetPdfDocument(file.OpenReadStream());
                EarningsReturn earnings = _pdfService.GetEarnings(pdfDocument);
                _npoiService.GenerateExcelFile(earnings);
            }

            return "";
        }
    }
}
