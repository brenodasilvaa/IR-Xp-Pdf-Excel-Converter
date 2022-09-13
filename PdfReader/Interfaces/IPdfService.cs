using FilesLibrary.Models;
using System.IO;

namespace FilesLibrary.Interfaces
{
    public interface IPdfService
    {
        PdfTextReturn GetPdfDocument(string pdfFilePath);
        PdfTextReturn GetPdfDocument(Stream pdfFileStream);
        EarningsReturn GetEarnings(PdfTextReturn pdfTextReturn);
    }
}
