using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Shared.Models
{
    public interface ITracking
    {
        List<Operations> Operations { get; set; }
    }

    public class Tracking : ITracking
    {
        public List<Operations> Operations { get; set; }

        public Tracking()
        {
            this.Operations = new List<Operations>();
        }
    }

    public class Operations
    {
        public string Id { get; set; }
        public string Operation { get; set; }
        public string Calculation { get; set; }
        public DateTime Date { get; set; }

        public Operations()
        {
            this.Id = "";
            this.Operation = "";
            this.Calculation = "";
            this.Date = DateTime.MinValue;
        }
    }
}
