using System;
using System.Collections.Generic;
using System.Text;

namespace PdfReader.Models
{
    public class EarningsHeader
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public short Year { get; set; }
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }
        public string ClienteName { get; set; }
        public string CPF { get; set; }
        public int BankBranch { get; set; }
        public int Account { get; set; }

    }
}
