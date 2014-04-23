using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace InscripcionesCursos
{
    class LogWriter
    {
        #region Objects

        private string path = String.Empty;
        TextWriter twError;

        #endregion

        #region Constructor

        public LogWriter()
        {
            path = ConfigurationManager.AppSettings["PathLogWriter"] + DateTime.Now.Year.ToString() + "."
                + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + ".txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                twError = new StreamWriter(path, true);
            }
            else if (File.Exists(path))
            {
                twError = new StreamWriter(path, true);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to write text file log
        /// </summary>
        /// <param name="error"></param>
        /// <param name="source"></param>
        public void WriteLog(string error, string method, string source)
        {
            this.twError.WriteLine("[" + source + "-->" + method + "]- [" + DateTime.Now.ToString("dd-MM-yyyy HH:ss") + "] = " + error);
            twError.Close();
        }

        #endregion
    }
}