using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Models
{
    public class BadCalc
    {
        public string ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }

        public BadCalc()
        {
            this.ErrorCode = "";
            this.ErrorStatus = 0;
            this.ErrorMessage = "";
        }
    }
}
