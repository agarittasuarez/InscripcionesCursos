using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class Default : System.Web.UI.Page
    {
        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("user");
        }

        #endregion

        #region Events

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ConfigurationManager.AppSettings["UrlLogin"]);                
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnContinuar_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}