using IR_Xp_Pdf_Excel_Converter_API.Services;
using Microsoft.AspNetCore.Mvc;
using PdfReader.Services;
using System.IO;
using static System.Net.WebRequestMethods;

namespace IR_Xp_Pdf_Excel_Converter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        public IPdfService _pdfService { get; set; }

        public ConverterController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public struct MyStruct
        {
            public Stream file { get; set; }
        }
        [HttpPost]
        public string Get()
        {
            foreach (var file in Request.Form.Files)
            {
                var pdfDocument = _pdfService.GetPdfDocument(file.OpenReadStream());
                var earnings = _pdfService.GetEarnings(pdfDocument);
            }

            return "";
        }
    }
}
