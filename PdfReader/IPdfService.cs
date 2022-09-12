using PdfReader.Models;
using System.IO;

namespace PdfReader.Services
{
    public interface IPdfService
    {
        PdfTextReturn GetPdfDocument(string pdfFilePath);
        PdfTextReturn GetPdfDocument(Stream pdfFileStream);
        EarningsReturn GetEarnings(PdfTextReturn pdfTextReturn);
    }
}
