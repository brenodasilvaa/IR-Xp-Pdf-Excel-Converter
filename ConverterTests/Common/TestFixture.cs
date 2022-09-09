using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterTests.Common
{
    public class TestFixture
    {
        public string BaseDirectory => _baseDirectory;
        public string PdfFilePath => _pdfFilePath;
        private string _baseDirectory { get; set; }
        private string _pdfFilePath { get; set; }

        public TestFixture()
        {
            _baseDirectory = Directory.GetCurrentDirectory().ToString();
            _pdfFilePath = Path.Combine(_baseDirectory, "Data", "test.pdf");
        }
    }
}
