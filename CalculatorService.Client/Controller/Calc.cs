using CalculatorService.Shared.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorService.Client
{
    public class Calc
    {
        #region "GetUrl"
        /// <summary>
        /// return url api
        /// </summary>
        /// <returns>url</returns>
        public static string GetUrl()
        {
            return "http://localhost:22152";
        }
        #endregion

        #region "Add"
        /// <summary>
        /// Add two or more operands and retrieve the result
        /// </summary>
        /// <param name="id">Id Tracking</param>
        /// <returns>result</returns>
        public static string Add(string id)
        {
            CalcAdd par = new CalcAdd();
            par.Addends = GetNumbers("add", 0);
            if (par.Addends.Count >= 2)
                return CallRestApi<CalcAdd>(par, "add", id);
            else
                return BadCalculate();
        }
        #endregion

        #region "Sub"
        /// <summary>
        /// Sub two operands and retrieve the result
        /// </summary>
        /// <param name="id">Id Tracking</param>
        /// <returns></returns>
        public static string Sub(string id)
        {
            CalcSub par = new CalcSub();
            List<double> numbers = GetNumbers("sub", 2);
            if (numbers.Count == 2)
            {
                par.Minuend = numbers[0];
                par.Subtrahend = numbers[1];
                return CallRestApi<CalcSub>(par, "sub", id);
            }
            else
            {
                return BadCalculate();
            }
        }
        #endregion

        #region "Mult"
        /// <summary>
        /// Multiply two or more operands and retrieve the result
        /// </summary>
        /// <param name="id">Id Tracking</param>
        /// <returns></returns>
        public static string Mult(string id)
        {
            CalcMult par = new CalcMult();
            par.Factors = GetNumbers("mult", 0);
            if (par.Factors.Count >= 2)
                return CallRestApi<CalcMult>(par, "mult", id);
            else
                return BadCalculate();
        }
        #endregion

        #region "Div"
        /// <summary>
        /// Divide two operands and retrieve the result
        /// </summary>
        /// <param name="id">Id Tracking</param>
        /// <returns></returns>
        public static string Div(string id)
        {
            CalcDiv par = new CalcDiv();
            List<double> numbers = GetNumbers("div", 2);
            if (numbers.Count == 2)
            {
                par.Dividend = (int)numbers[0];
                par.Divisor = (int)numbers[1];
                return CallRestApi<CalcDiv>(par, "div", id);
            }
            else
                return BadCalculate();
        }
        #endregion

        #region "Sqrt"
        /// <summary>
        /// Square root one operand and retrieve the result
        /// </summary>
        /// <param name="id">Id Tracking</param>
        /// <returns></returns>
        public static string Sqrt(string id)
        {
            CalcSqrt par = new CalcSqrt();
            List<double> numbers = GetNumbers("sqrt", 1);
            if (numbers.Count == 1)
            {
                par.Number = (int)numbers[0];
                return CallRestApi<CalcSqrt>(par, "sqrt", id);
            }
            else
                return BadCalculate();
        }
        #endregion

        #region "Query"
        /// <summary>
        /// Request all operations for a TrackingId since the last application restart
        /// </summary>
        /// <returns></returns>
        public static string Query()
        {
            Console.Clear();
            Console.WriteLine("\n------------------------");
            Console.WriteLine($"\n******** query ***********");
            Console.Write($"\nPlease, enter the Trackin Id. (press enter key when ready)\n\n");
            Console.Write("Tracking Id : ");
            var value = Console.ReadLine();

            if (!string.IsNullOrEmpty(value))
            {
                CalcQuery par = new CalcQuery();
                par.Id = value;
                return CallRestApi<CalcQuery>(par, "query");
            }
            else
            {
                return BadCalculate();
            }
        } 
        #endregion


        #region "GetNumbers"
        /// <summary>
        /// Get the numbers for operation
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<double> GetNumbers(string operation, int count)
        {
            List<double> numbers = new List<double>();

            Console.Clear();
            Console.WriteLine("\n------------------------");
            Console.WriteLine($"\n******** {operation} ***********");
            Console.Write($"\nPlease, enter the numbers to {operation}. (press enter key when ready)\n\n");

            bool show = true;
            int i = 0;
            while (show)
            {
                i++;
                string value = "";

                if ( i <= count || count == 0 )
                {
                    Console.Write("Number : ");
                    value = Console.ReadLine();
                    double d = 0;
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (double.TryParse(value.Replace(',', '.'), NumberStyles.Any, new CultureInfo("en-US"), out d))
                            numbers.Add(d);
                        else
                            Console.WriteLine("Attention: something is not correct, the last entry has been ignored. Please retry.");
                    }
                    else
                        show = false;
                }

                if (string.IsNullOrEmpty(value) || (i >= count && count != 0))
                    show = false;
            }

            return numbers;
        }
        #endregion

        #region "CallRestApi"
        /// <summary>
        /// Call to rest api and retrieve the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static string CallRestApi<T>(object obj, string method)
        {
            return CallRestApi<T>(obj, method, "");
        }
        private static string CallRestApi<T>(object obj, string method, string id)
        {
            string result = "";

            var client = new RestClient(GetUrl());
            client.Timeout = 5000; // max 5 seconds
            var request = new RestRequest($"/calculator/" + method, Method.POST);
            request.AddJsonBody(JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj)));
            if (!string.IsNullOrEmpty(id))
                request.AddHeader("X-Evi-Tracking-Id", id.ToString());

            var response = client.Execute(request);

            if ((response.ResponseStatus == ResponseStatus.Completed) && (response != null) && (response.Content != string.Empty))
            {
                result = response.Content;
            }
            else
            {
                result = BadCalculate();
            }

            return result;
        }
        #endregion

        #region "BadCalculate"
        /// <summary>
        /// return error message when calculate fail
        /// </summary>
        /// <returns></returns>
        public static string BadCalculate()
        {
            return JsonSerializer.Serialize(
                new
                {
                    ErrorCode = "InternalError",
                    ErrorStatus = 500,
                    ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support."
                });
        } 
        #endregion

    }
}
