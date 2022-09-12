using System;
using System.Collections.Generic;
using System.Text;

namespace PdfReader.Models
{
    public class PdfTextReturn
    {
        public List<string> PageText { get; set; }
        public int NumberOfPages { get; set; }

        public PdfTextReturn()
        {
            PageText = new List<string>();
        }
    }
}
