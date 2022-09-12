using ConverterTests.Common;
using PdfReader.Services;

namespace ConverterTests
{
    public class PdfServiceTest : IClassFixture<TestFixture>
    {
        public TestFixture _fixture { get; set; }
        public PdfServiceTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ReadPdfFile()
        {
            //Arrange
            var pdf = new PdfService();
            //Act
            pdf.GetPdfDocument(_fixture.PdfFilePath);
            //Assert
        }

        [Fact]
        public void ReadPdfStream()
        {
            //Arrange
            var pdf = new PdfService();
            var pdfStream = File.OpenRead(_fixture.PdfFilePath);
            //Act
            var pdfReturn = pdf.GetPdfDocument(pdfStream);
            //Assert
            Assert.True(pdfReturn.NumberOfPages > 0);
        }
    }
}