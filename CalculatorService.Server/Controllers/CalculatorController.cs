using CalculatorService.Server.Models;
using CalculatorService.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorService.Server.Controllers
{
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalcLogging _log;
        private readonly ITracking _trk;
        public CalculatorController(ICalcLogging log, ITracking trk)
        {
            _log = log;
            _trk = trk;
        }

        [HttpGet]
        [Route("calculator")]
        public string Get()
        {
            return "This is the calculator service. Please run the correct route and enjoy it.";
        }

        #region "add"
        [HttpPost]
        [Route("calculator/add")]
        public IActionResult add([FromBody] CalcAdd par)
        {
            var headers = Request.Headers;
            string track = headers.Where(x => x.Key == "X-Evi-Tracking-Id").FirstOrDefault().Value;

            if (par.Addends.Count < 2)
            {
                return BadRequest(new BadCalc { ErrorCode = "InternalError", ErrorStatus = 400, ErrorMessage = "Unable to process request: it's necessary at least 2 numbers for add." });
            }

            string msg = "(";
            double total = 0;
            for (int i = 0; i < par.Addends.Count; i++)
            {
                total += par.Addends[i];
                msg += par.Addends[i] + (i < (par.Addends.Count - 1) ? " + " : "");
            }
            msg += ") = " + total.ToString("0.#####");

            if (!string.IsNullOrEmpty(track))
            {
                _trk.Operations.Add(new Operations { Id = track, Operation = "Sum", Calculation = msg, Date = DateTime.Now });
            }

            return Ok(new { Sum = total });
        }
        #endregion

        #region "sub"
        [HttpPost]
        [Route("calculator/sub")]
        public IActionResult sub([FromBody] CalcSub par)
        {
            var headers = Request.Headers;
            string track = headers.Where(x => x.Key == "X-Evi-Tracking-Id").FirstOrDefault().Value;

            double total = par.Minuend - par.Subtrahend;
            string msg = "(" + par.Minuend + " - " + par.Subtrahend + " = " + total.ToString("0.#####") + ")";

            if (!string.IsNullOrEmpty(track))
            {
                _trk.Operations.Add(new Operations { Id = track, Operation = "Sub", Calculation = msg, Date = DateTime.Now });
            }

            return Ok(new { Difference = total });
        }
        #endregion

        #region "mult"
        [HttpPost]
        [Route("calculator/mult")]
        public IActionResult mult([FromBody] CalcMult par)
        {
            var headers = Request.Headers;
            string track = headers.Where(x => x.Key == "X-Evi-Tracking-Id").FirstOrDefault().Value;

            if (par.Factors.Count < 2)
            {
                return BadRequest(new BadCalc { ErrorCode = "InternalError", ErrorStatus = 400, ErrorMessage = "Unable to process request: it's necessary at least 2 numbers for multiply." });
            }

            string msg = "(" + par.Factors[0].ToString() + " * ";
            double total = par.Factors[0];
            for (int i = 1; i < par.Factors.Count; i++)
            {
                total *= par.Factors[i];
                msg += par.Factors[i] + (i < (par.Factors.Count - 1) ? " * " : "");
            }
            msg += ") = " + total.ToString("0.#####");

            if (!string.IsNullOrEmpty(track))
            {
                _trk.Operations.Add(new Operations { Id = track, Operation = "Mult", Calculation = msg, Date = DateTime.Now });
            }

            return Ok(new { Product = total });
        }
        #endregion

        #region "div"
        [HttpPost]
        [Route("calculator/div")]
        public IActionResult div([FromBody] CalcDiv par)
        {
            var headers = Request.Headers;
            string track = headers.Where(x => x.Key == "X-Evi-Tracking-Id").FirstOrDefault().Value;

            if (par.Dividend == 0 || par.Divisor == 0)
            {
                return BadRequest(new BadCalc { ErrorCode = "InternalError", ErrorStatus = 400, ErrorMessage = "Unable to process request: can't divide any number by 0." });
            }

            int total = par.Dividend / par.Divisor;
            int remainder = par.Dividend % par.Divisor;
            string msg = "(" + par.Dividend + " / " + par.Divisor + " = " + total.ToString() + " remainder : " + remainder.ToString() + ")";

            if (!string.IsNullOrEmpty(track))
            {
                _trk.Operations.Add(new Operations { Id = track, Operation = "Div", Calculation = msg, Date = DateTime.Now });
            }

            return Ok(new { Quotient = total, Remainder = remainder });
        }
        #endregion

        #region "sqrt"
        [HttpPost]
        [Route("calculator/sqrt")]
        public IActionResult sqrt([FromBody] CalcSqrt par)
        {
            var headers = Request.Headers;
            string track = headers.Where(x => x.Key == "X-Evi-Tracking-Id").FirstOrDefault().Value;

            double total = Math.Sqrt(par.Number);
            string msg = "(Square root :" + par.Number + " = " + total.ToString() + ")";

            if (!string.IsNullOrEmpty(track))
            {
                _trk.Operations.Add(new Operations { Id = track, Operation = "Div", Calculation = msg, Date = DateTime.Now });
            }

            return Ok(new { Square = total });
        }
        #endregion

        #region "query"
        [HttpPost]
        [Route("calculator/query")]
        public IActionResult query([FromBody] CalcQuery par)
        {
            var headers = Request.Headers;

            if (string.IsNullOrEmpty(par.Id))
            {
                return BadRequest(new BadCalc { ErrorCode = "InternalError", ErrorStatus = 400, ErrorMessage = "Unable to process request: Not id." });
            }

            var res = _trk.Operations.Where(x => x.Id == par.Id).ToList();

            if (res.Count > 0)
                return Ok(res);
            else
                return NotFound(new BadCalc { ErrorCode = "InternalError", ErrorStatus = 400, ErrorMessage = "Not found." });
        }

        #endregion
        

    }
}
