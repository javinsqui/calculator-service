using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public class CalcAdd
    {
        public List<double> Addends { get; set; }

        public CalcAdd()
        {
            this.Addends = new List<double>();
        }
    }
}
