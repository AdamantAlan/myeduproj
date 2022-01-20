using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public abstract class Converter
    {
        Currency inputCurrency;
        public Currency InputCurrency { get { return InputCurrency; } }
        
        public abstract Currency OutputCurrency { get; set; }   
        public abstract double Value { get; set; }

        public Converter(Currency inputCurrency)
        {
            this.inputCurrency = inputCurrency;
        }
    }
}
