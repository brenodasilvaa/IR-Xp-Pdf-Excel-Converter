using iText.IO.Font;
using iText.Layout;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilesLibrary.Npoi
{
    internal static class StyleHelper
    {
        internal static void SetCellColor(this ICell cell, HSSFColor color, IWorkbook workbook)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();

            cellStyle.FillForegroundColor = color.Indexed;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            cell.CellStyle = cellStyle;
        }

        internal static void CreateCells(this IRow row, int cellsNumber)
        {
            for (int i = 0; i < cellsNumber; i++)
            {
                row.CreateCell(i);
            }
        }

        internal static void MergeCells(this ISheet sheet, int firstRow, int lastRow, int firstColumn, int lastColumn)
        {
            var cra = new NPOI.SS.Util.CellRangeAddress(firstRow, lastRow, firstColumn, lastColumn);
            sheet.AddMergedRegion(cra);
        }

        internal static void SetCellStyle(this ISheet sheet, ICellStyle style, int cell, int row)
        {
            for (int i = 0; i <= 8; i++)
            {
                sheet.GetRow(i).GetCell(cell).CellStyle = style;
            }
        }

        internal static Dictionary<string, ICellStyle> CreateStyles(IWorkbook workbook)
        {
            var styles = new Dictionary<string, ICellStyle>();
            string fontName = "Ebrima";

            ICellStyle style;
            IFont titleFont = workbook.CreateFont();
            titleFont.FontName = fontName;
            titleFont.FontHeightInPoints = 14;
            titleFont.IsBold = true;
            style = workbook.CreateCellStyle();
            style.FillForegroundColor = IndexedColors.Grey50Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.SetFont(titleFont);
            styles.Add("title", style);

            IFont earningFont = workbook.CreateFont();
            style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            earningFont.FontName = fontName;
            earningFont.FontHeightInPoints = 12;
            style.SetFont(earningFont);
            style.VerticalAlignment = VerticalAlignment.Center;
            style.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.DataFormat = workbook.CreateDataFormat().GetFormat("0.00");
            styles.Add("earning", style);

            style = workbook.CreateCellStyle();
            IFont earningHeaderFont = workbook.CreateFont();
            earningHeaderFont.FontName = fontName;
            earningHeaderFont.FontHeightInPoints = 12;
            earningHeaderFont.IsBold = true;
            style.SetFont(earningHeaderFont);
            style.VerticalAlignment = VerticalAlignment.Center;
            style.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            styles.Add("earningHeader", style);

            style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.SetFont(earningFont);
            style.VerticalAlignment = VerticalAlignment.Center;
            style.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.DataFormat = workbook.CreateDataFormat().GetFormat("dd/MM/yyyy");
            styles.Add("date", style);

            style = workbook.CreateCellStyle();
            IFont informationFont = workbook.CreateFont();
            style = workbook.CreateCellStyle();
            informationFont.FontHeightInPoints = 12;
            informationFont.FontName = fontName;
            style.SetFont(informationFont);
            style.VerticalAlignment = VerticalAlignment.Center;
            style.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            styles.Add("information", style);

            return styles;
        }
    }
}
