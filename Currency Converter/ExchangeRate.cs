using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency_Converter
{
    public class ExchangeRate
    {
        public ExchangeRate() { }
        public Rate Rates { get; set; }

        public long timestamp;
        public string disclaimer;
        public string license;
    }
}
