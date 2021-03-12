using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Server.Models
{
    public interface ICalcLogging
    {
        void Tracking(string filename, string strMsg, params string[] varparams);
    }

    public class CalcLogging : ICalcLogging
    {
        private ICalcSession _session;

        public CalcLogging(ICalcSession session)
        {
            this._session = session;
        }

        ////////////////////////
        /// Methods

        public void Tracking(string filename, string strMsg, params string[] varparams)
        {
            WriteLog(filename, strMsg);
            for (int i = 0; i < varparams.Length; i++)
                WriteLog(filename, varparams[i]);
        }

        #region "WriteError"
        private void WriteLog(string filename, string strMsg)
        {
            string dirTrace = "log";
            string strFileLog = dirTrace + "/" +
                                (!string.IsNullOrEmpty(filename) ? filename : "errlog") + "." +
                                DateTime.Now.ToString("yyyyMMdd") + ".log";

            try
            {
                if (!Directory.Exists(dirTrace))
                    Directory.CreateDirectory(dirTrace);

                StreamWriter sw;
                if (!File.Exists(strFileLog))
                {
                    sw = File.CreateText(strFileLog);
                    sw.Close();
                }
                sw = new StreamWriter(strFileLog, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ":" + strMsg);
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
