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
    }
}
