using FilesLibrary.Interfaces;
using FilesLibrary.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using FilesLibrary.Common;
using System.IO;
using System.Linq;
using FilesLibrary.Npoi;

namespace FilesLibrary.Services
{
    public class NpoiService : INpoiService
    {
        public IWorkbook _workbook { get; private set; }
        public Dictionary<string, ICellStyle> _cellStyles { get; private set; }
        public MemoryStream GenerateExcelFile(EarningsReturn earningsReturn)
        {
            var excelStream = new MemoryStream();
            
            _workbook = new XSSFWorkbook();

            _cellStyles = StyleHelper.CreateStyles(_workbook);

            ISheet excelSheet = _workbook.CreateSheet($"Proventos {earningsReturn.EarningsHeader.Year}");

            CreateHeader(excelSheet, earningsReturn.EarningsHeader);

            CreateEarnings(excelSheet, earningsReturn.Earnings);

            excelSheet.DefaultColumnWidth = 30;

            _workbook.Write(excelStream);

            var excelArrayWorkAround = excelStream.ToArray();

            var resultMs = new MemoryStream(excelArrayWorkAround);

            return resultMs;
        }

        private void CreateEarnings(ISheet excelSheet, ICollection<Earning> earnings)
        {
            var groupEarnings = earnings.GroupBy(x => new { x.Asset, x.Event });

            int rowIndex = 11;

            foreach (var earning in groupEarnings)
            {
                if (earning.Key.Event == EventType.TOTAL)
                    continue;

                IRow row = excelSheet.CreateRow(rowIndex);

                row.CreateCells(8);

                excelSheet.MergeCells(rowIndex, rowIndex, 0, 4);

                var titleCell = row.GetCell(0);

                titleCell.SetCellValue($"{earning.Key.Asset} - {earning.Key.Event}");
                titleCell.CellStyle = _cellStyles["title"];

                rowIndex++;

                IRow row_header = excelSheet.CreateRow(rowIndex);

                CreateHeaderEarning(row_header);

                rowIndex++;

                foreach (var item in earning)
                {
                    IRow row_earning = excelSheet.CreateRow(rowIndex);

                    row_earning.CreateCells(6);

                    var quantityCell = row_earning.GetCell(0);
                    quantityCell.SetCellValue(item.Quantity.Value);
                    quantityCell.CellStyle = _cellStyles["earning"];

                    var grossValueCell = row_earning.GetCell(1);
                    grossValueCell.SetCellValue((double)item.GrossValue);
                    grossValueCell.CellStyle = _cellStyles["earning"];

                    var taxValueCell = row_earning.GetCell(2);
                    taxValueCell.SetCellValue((double)item.TaxValue);
                    taxValueCell.CellStyle = _cellStyles["earning"];

                    var netValueCell = row_earning.GetCell(3);
                    netValueCell.SetCellValue((double)item.NetValue);
                    netValueCell.CellStyle = _cellStyles["earning"];

                    var payDayValueCell = row_earning.GetCell(4);
                    payDayValueCell.SetCellValue(item.PayDay.Value);
                    payDayValueCell.CellStyle = _cellStyles["date"];

                    rowIndex++;
                }

                rowIndex = CreateEmptyCells(excelSheet, rowIndex, 6, 2);

                IRow row_total = excelSheet.GetRow(rowIndex);
                ICell totalCell = row_total.GetCell(3);

                totalCell.SetCellValue("Total (R$)");
                totalCell.CellStyle = _cellStyles["title"];

                ICell totalCellValue = row_total.GetCell(4);

                var totalNetValue = earning.Sum(x => x.NetValue);

                totalCellValue.SetCellValue((double)totalNetValue);
                totalCellValue.CellStyle = _cellStyles["title"];

                rowIndex += 2;
            }
        }

        private int CreateEmptyCells(ISheet excelSheet, int rowIndex, int cells, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                IRow row_empty = excelSheet.CreateRow(rowIndex);
                row_empty.CreateCells(cells);

                for (int j = 0; j < cells - 1; j++)
                {
                    row_empty.GetCell(j).CellStyle = _cellStyles["earning"];
                }

                if (i == (rows - 1))
                    continue;

                rowIndex++;
            }

            return rowIndex;
        }

        private void CreateHeader(ISheet excelSheet, EarningsHeader earningsHeader)
        {
            for (int i = 0; i <= 8; i++)
            {
                IRow row = excelSheet.CreateRow(i);

                row.CreateCells(6);

                excelSheet.MergeCells(i, i, 0, 4);
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

            for (int i = 0; i <= 8; i++)
            {
                excelSheet.GetRow(i).GetCell(0).CellStyle = _cellStyles["information"];
            }
        }

        private void CreateHeaderEarning(IRow row_header)
        {
            int columnIndex = 0;

            foreach (var columnName in Constants.ColumnsNames)
            {
                ICell cell = row_header.CreateCell(columnIndex);
                cell.SetCellValue(columnName);
                cell.CellStyle = _cellStyles["earningHeader"];
                columnIndex++;
            }
        }
    }
}
