using System;
using System.Collections.Generic;
using System.Text;

namespace FilesLibrary.Models
{
    public class EarningsReturn
    {
        public int NumberOfPages { get; set; }
        public EarningsHeader EarningsHeader { get; set; }
        public ICollection<Earning> Earnings { get; set; }

        public EarningsReturn()
        {
            EarningsHeader = new EarningsHeader();
            Earnings = new List<Earning>();
        }
    }

}
