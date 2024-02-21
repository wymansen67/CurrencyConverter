using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCurrencyConverter
{
    public class Valute
    {
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{Nominal} {Name}";
        }

        public Valute() { }

        public Valute(int numcode, string charcode, int nominal, string name, double value)
        {
            NumCode = numcode;
            CharCode = charcode;
            Nominal = nominal;
            Name = name;
            Value = value;
        }
    }
}