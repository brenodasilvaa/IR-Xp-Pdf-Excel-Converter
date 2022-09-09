using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using System.Linq;
using System.Text;

namespace PdfReader.Services
{
    public class PdfService : IPdfService
    {
        public PdfDocument _pdfDocument { get; set; }

        public string GetPdfDocument(string pdfFilePath)
        {
            PdfDocument pdfDocument = new PdfDocument(new iText.Kernel.Pdf.PdfReader(pdfFilePath));

            return "";
        }

        public string GetPdfDocument(Stream pdfFile)
        {
            PdfDocument pdfDocument = new PdfDocument(new iText.Kernel.Pdf.PdfReader(pdfFile));
            StringBuilder processed = new StringBuilder();
            var strategy = new LocationTextExtractionStrategy();
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                processed.Append(PdfTextExtractor.GetTextFromPage(page, strategy));
            }
            return processed.ToString();
        }
    }
}
