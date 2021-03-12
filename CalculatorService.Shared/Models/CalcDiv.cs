using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public class CalcDiv
    {
        public int Dividend { get; set; }
        public int Divisor { get; set; }

        public CalcDiv()
        {
            this.Dividend = 0;
            this.Divisor = 0;
        }
    }
}
