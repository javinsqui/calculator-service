using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public class CalcMult
    {
        public List<double> Factors { get; set; }

        public CalcMult()
        {
            this.Factors = new List<double>();
        }
    }
}
