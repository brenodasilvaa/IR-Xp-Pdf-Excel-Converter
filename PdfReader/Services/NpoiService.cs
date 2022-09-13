using FilesLibrary.Interfaces;
using FilesLibrary.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System;
using FilesLibrary.Common;
using System.IO;

namespace FilesLibrary.Services
{
    public class NpoiService : INpoiService
    {
        public void GenerateExcelFile(EarningsReturn earningsReturn)
        {
            using (var fs = new FileStream("Result.xlsx", FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet($"Proventos {earningsReturn.EarningsHeader.Year}");

                CreateHeader(excelSheet, earningsReturn.EarningsHeader);

                workbook.Write(fs);
            }
            
        }

        private void CreateHeader(ISheet excelSheet, EarningsHeader earningsHeader)
        {
            for (int i = 0; i <= 8; i++)
            {
                IRow row = excelSheet.CreateRow(i);
                row.CreateCell(0);
                row.CreateCell(1);
                row.CreateCell(2);
                row.CreateCell(3);
                row.CreateCell(4);
                row.CreateCell(5);
                row.CreateCell(6);
                row.CreateCell(7);
                row.CreateCell(8);

                var cra = new NPOI.SS.Util.CellRangeAddress(i, i, 0, 6);
                excelSheet.AddMergedRegion(cra);
            }

            IRow row_0 = excelSheet.GetRow(0);
            row_0.GetCell(0).SetCellValue(earningsHeader.Title);

            IRow row_1 = excelSheet.GetRow(1);
            row_1.GetCell(0).SetCellValue(earningsHeader.Caption);

            IRow row_2 = excelSheet.GetRow(2);
            row_2.GetCell(0).SetCellValue("Ano " + earningsHeader.Year);

            IRow row_3 = excelSheet.GetRow(3);
            row_3.GetCell(0).SetCellValue(earningsHeader.CompanyName);

            IRow row_4 = excelSheet.GetRow(4);
            row_4.GetCell(0).SetCellValue("CNPJ: " + earningsHeader.CNPJ);

            IRow row_5 = excelSheet.GetRow(5);
            row_5.GetCell(0).SetCellValue(earningsHeader.ClienteName);

            IRow row_6 = excelSheet.GetRow(6);
            row_6.GetCell(0).SetCellValue("CPF: " + earningsHeader.CPF);

            IRow row_7 = excelSheet.GetRow(7);
            row_7.GetCell(0).SetCellValue("Agência: " + earningsHeader.BankBranch);

            IRow row_8 = excelSheet.GetRow(8);
            row_8.GetCell(0).SetCellValue("Conta: " + earningsHeader.Account);

            IRow row_earnings = excelSheet.CreateRow(10);
            int columnIndex = 0;

            foreach (var columnName in Constants.ColumnsNames)
            {
                row_earnings.CreateCell(columnIndex).SetCellValue(columnName);
                columnIndex++;
            }
        }
    }
}
