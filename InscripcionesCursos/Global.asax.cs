using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using System.Threading;
using System.IO;

namespace InscripcionesCursos
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            try
            {
                // Code that runs when a new session is started
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["ServerFlag"]))
                {
                    if (Request.UrlReferrer != null)
                    {
                        if ((Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["UrlEconomicasUNL"]) && (Request.UrlReferrer.ToString() != ConfigurationManager.AppSettings["UrlFranjaCece"]))
                            if (Request.Url.ToString().ToLower().IndexOf("activacioncuenta.aspx?") == -1)
                                Response.Redirect(ConfigurationManager.AppSettings["UrlEconomicasUNL"]);
                    }
                    else
                    {
                        if (Request.Url.ToString().ToLower().IndexOf("activacioncuenta.aspx?") == -1)
                            Response.Redirect(ConfigurationManager.AppSettings["UrlEconomicasUNL"]);
                    }
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "Session_Start", Path.GetFileName(Request.PhysicalPath));
            }
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
