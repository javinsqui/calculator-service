using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public class CalcQuery
    {
        public string Id { get; set; }

        public CalcQuery()
        {
            this.Id = string.Empty;
        }
    }
}
