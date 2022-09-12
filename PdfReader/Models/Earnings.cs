using PdfReader.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdfReader.Models
{
    public class Earning
    {
        public string Company { get; set; }
        public EventType Event { get; set; }
        public int Quantity { get; set; }
        public decimal GrossValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal NetValue { get; set; }
        public DateTime PayDay { get; set; }

    }
}
