using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public class CalcSub
    {
        public double Minuend { get; set; }
        public double Subtrahend { get; set; }

        public CalcSub()
        {
            this.Minuend = 0;
            this.Subtrahend = 0;
        }
    }
}
