using FilesLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FilesLibrary.Interfaces
{
    public interface INpoiService
    {
        MemoryStream GenerateExcelFile(EarningsReturn earnings);
    }
}
