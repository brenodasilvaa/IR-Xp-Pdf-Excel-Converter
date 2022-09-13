using FilesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilesLibrary.Interfaces
{
    public interface INpoiService
    {
        void GenerateExcelFile(EarningsReturn earnings);
    }
}
