using IR_Xp_Pdf_Excel_Converter_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IR_Xp_Pdf_Excel_Converter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Converter : ControllerBase
    {
        public IConverterService _converterService { get; set; }

        public Converter(IConverterService converterService)
        {
            _converterService = converterService;
        }

        [HttpGet]
        public string Get()
        {
            return _converterService.GetString();
        }
    }
}
