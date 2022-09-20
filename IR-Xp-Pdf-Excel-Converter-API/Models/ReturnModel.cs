using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;

namespace IR_Xp_Pdf_Excel_Converter_API.Models
{
    public class ReturnModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public IEnumerable<Uri> downloadLinks { get; set; }
    }
}
