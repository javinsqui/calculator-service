using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Models
{
    public interface ICalcSession
    {
        string GetDirectoryLog();
        string GetFileLog();
    }

    public class CalcSession : ICalcSession
    {
        // Log
        private string _DirectoryLog;

        private string _FileLog;


        public CalcSession(IConfiguration _conf, IHostEnvironment _env)
        {
            this._DirectoryLog = _env.ContentRootPath + "/" + _conf.GetSection("ConfigApp").GetSection("DirectoryLog").Value;
            this._FileLog = _conf.GetSection("ConfigApp").GetSection("FileLog").Value;
        }


        public string GetDirectoryLog()
        {
            return this._DirectoryLog;
        }

        public string GetFileLog()
        {
            return this._FileLog;
        }
    }
}
