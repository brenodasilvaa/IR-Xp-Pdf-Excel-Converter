using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using System.Collections.Generic;
using System;
using PdfReader.Models;
using System.Linq;

namespace PdfReader.Services
{
    public class PdfService : IPdfService
    {
        public PdfDocument _pdfDocument { get; set; }

        public PdfTextReturn GetPdfDocument(string pdfFilePath)
        {
            var pdfDocument = new PdfDocument(new iText.Kernel.Pdf.PdfReader(pdfFilePath));

            return GetPdfResult(pdfDocument);
        }

        public PdfTextReturn GetPdfDocument(Stream pdfFile)
        {
            var pdfDocument = new PdfDocument(new iText.Kernel.Pdf.PdfReader(pdfFile));

            return GetPdfResult(pdfDocument);
        }

        public EarningsReturn GetEarnings(PdfTextReturn pdfTextReturn)
        {
            var result = new EarningsReturn();

            result.EarningsHeader.Title = pdfTextReturn.PageText[0];

            short.TryParse(new String(pdfTextReturn.PageText[2].Where(x => char.IsDigit(x)).ToArray()), out short year);
            result.EarningsHeader.Year = year;

            result.EarningsHeader.CNPJ = pdfTextReturn.PageText[4].Split(' ')[1];

            result.EarningsHeader.CPF = pdfTextReturn.PageText[6].Split(' ')[1];


            result.EarningsHeader.BankBranch = GetBankInformation(pdfTextReturn.PageText[7].Split(' ')[0]);

            result.EarningsHeader.Account = GetBankInformation(pdfTextReturn.PageText[7].Split(' ')[1]);

            for (int i = 9; i < pdfTextReturn.PageText.Count; i++)
            {
                if (string.IsNullOrEmpty(pdfTextReturn.PageText[i].Trim()))
                    continue;

                var earning = new Earning();

                string[] earningLineSplit = pdfTextReturn.PageText[i].Split(' ');

                earning.Company = earningLineSplit[0];
                earning.Event = (Common.EventType)Enum.Parse(typeof(Common.EventType), earningLineSplit[1]);

            }

            return result;

            int GetBankInformation(string bankInformation)
            {
                int.TryParse(bankInformation.Split(':')[1], out int resultBanckInformation);

                return resultBanckInformation;
            }
        }

        private PdfTextReturn GetPdfResult(PdfDocument pdfDocument)
        {
            var result = new PdfTextReturn
            {
                NumberOfPages = pdfDocument.GetNumberOfPages()
            };

            for (int i = 1; i <= result.NumberOfPages; ++i)
            {
                var strategy = new LocationTextExtractionStrategy();
                var page = pdfDocument.GetPage(i);
                string pageData = PdfTextExtractor.GetTextFromPage(page, strategy);
                string[] pageDataList = pageData.Split(Environment.NewLine.ToCharArray());
                result.PageText.AddRange(pageDataList);
            }

            return result;
        }
    }
}
