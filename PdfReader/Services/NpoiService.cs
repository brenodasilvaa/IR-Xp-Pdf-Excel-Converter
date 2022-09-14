using FilesLibrary.Interfaces;
using FilesLibrary.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System;
using FilesLibrary.Common;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Drawing.Imaging;
using iText.Kernel.Colors;
using static NPOI.HSSF.Util.HSSFColor;
using FilesLibrary.Npoi;

namespace FilesLibrary.Services
{
    public class NpoiService : INpoiService
    {
        public IWorkbook _workbook { get; private set; }
        public void GenerateExcelFile(EarningsReturn earningsReturn)
        {
            using (var fs = new FileStream("Result.xlsx", FileMode.Create, FileAccess.Write))
            {
                _workbook = new XSSFWorkbook();

                ISheet excelSheet = _workbook.CreateSheet($"Proventos {earningsReturn.EarningsHeader.Year}");

                CreateHeader(excelSheet, earningsReturn.EarningsHeader);

                CreateEarnings(excelSheet, earningsReturn.Earnings);

                excelSheet.DefaultColumnWidth = 30;

                _workbook.Write(fs);
            }
            
        }

        private void CreateEarnings(ISheet excelSheet, ICollection<Earning> earnings)
        {
            var groupEarnings = earnings.GroupBy(x => new { x.Asset, x.Event });

            int rowIndex = 12;

            foreach (var earning in groupEarnings)
            {
                IRow row = excelSheet.CreateRow(rowIndex);

                row.CreateCells(8);

                var cra = new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 4);

                excelSheet.AddMergedRegion(cra);

                var titleCell = row.GetCell(0);

                titleCell.SetCellValue($"{earning.Key.Asset} - {earning.Key.Event}");

                titleCell.SetCellColor(new Grey40Percent(), _workbook);

                rowIndex++;

                IRow row_header = excelSheet.CreateRow(rowIndex);

                CreateHeaderEarning(row_header);

                rowIndex++;

                foreach (var item in earning)
                {
                    if (!item.Quantity.HasValue)
                        continue;

                    if (item.Event == EventType.TOTAL)
                        continue;

                    IRow row_earning = excelSheet.CreateRow(rowIndex);

                    row_earning.CreateCells(6);

                    row_earning.GetCell(0).SetCellValue(item.Quantity.Value);
                }

                rowIndex += 2;
            }
        }

        private void CreateHeader(ISheet excelSheet, EarningsHeader earningsHeader)
        {
            for (int i = 0; i <= 8; i++)
            {
                IRow row = excelSheet.CreateRow(i);

                row.CreateCells(6);

                var cra = new NPOI.SS.Util.CellRangeAddress(i, i, 0, 4);
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

        }

        private void CreateHeaderEarning(IRow row_header)
        {
            int columnIndex = 0;

            foreach (var columnName in Constants.ColumnsNames)
            {
                ICell cell = row_header.CreateCell(columnIndex);
                cell.SetCellValue(columnName);
                cell.SetCellColor(new Grey25Percent(), _workbook);
                columnIndex++;
            }
        }
    }
}
