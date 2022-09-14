using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using System;
using FilesLibrary.Models;
using System.Linq;
using FilesLibrary.Interfaces;

namespace FilesLibrary.Services
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

            result.EarningsHeader.Caption = pdfTextReturn.PageText[1];

            short.TryParse(new String(pdfTextReturn.PageText[2].Where(x => char.IsDigit(x)).ToArray()), out short year);
            result.EarningsHeader.Year = year;

            result.EarningsHeader.CompanyName = pdfTextReturn.PageText[4].Split(' ')[0];
            result.EarningsHeader.CNPJ = pdfTextReturn.PageText[4].Split(' ')[1];

            result.EarningsHeader.ClienteName = pdfTextReturn.PageText[6].Split(' ')[0];
            result.EarningsHeader.CPF = pdfTextReturn.PageText[6].Split(' ')[1];

            result.EarningsHeader.BankBranch = GetBankInformation(pdfTextReturn.PageText[7].Split(' ')[0]);

            result.EarningsHeader.Account = GetBankInformation(pdfTextReturn.PageText[7].Split(' ')[1]);

            for (int i = 9; i < pdfTextReturn.PageText.Count; i++)
            {
                if (string.IsNullOrEmpty(pdfTextReturn.PageText[i].Trim()))
                    continue;

                var earning = new Earning();

                string[] earningLineSplit = pdfTextReturn.PageText[i].Split(' ');

                if (earningLineSplit.Length != 7)
                    continue;

                earning.Asset = earningLineSplit[0];
                earning.Event = (Common.EventType)Enum.Parse(typeof(Common.EventType), earningLineSplit[1]);

                if(int.TryParse(earningLineSplit[2].Trim(), out int quantity))
                    earning.Quantity = quantity;
                else
                    earning.Quantity = null;

                decimal.TryParse(earningLineSplit[3].Trim(), out decimal amount);
                earning.GrossValue = amount;

                decimal.TryParse(earningLineSplit[4].Trim(), out decimal taxValue);
                earning.TaxValue = taxValue;

                decimal.TryParse(earningLineSplit[5].Trim(), out decimal netValue);
                earning.NetValue = netValue;

                if (DateTime.TryParse(earningLineSplit[6], out DateTime payDay))
                    earning.PayDay = payDay;
                else
                    earning.PayDay = null;

                result.Earnings.Add(earning);
            }

            return result;
        }

        private int GetBankInformation(string bankInformation)
        {
            int.TryParse(bankInformation.Split(':')[1], out int resultBanckInformation);

            return resultBanckInformation;
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
