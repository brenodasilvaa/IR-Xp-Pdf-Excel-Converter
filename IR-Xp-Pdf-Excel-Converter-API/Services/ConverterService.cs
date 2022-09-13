using FilesLibrary.Interfaces;

namespace IR_Xp_Pdf_Excel_Converter_API.Services
{
    public class ConverterService : IConverterService
    {
        public IPdfService _pdfService { get; set; }
        public ConverterService(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public string GetString()
        {
            return "";
        }
    }
}