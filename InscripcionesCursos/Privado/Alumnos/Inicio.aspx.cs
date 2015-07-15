using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Threading;
using InscripcionesCursos.BE;

namespace InscripcionesCursos
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") +
                                          ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                    if (((Usuario)Session["user"]).LimitacionRelevada)
                        ucRelevamiento.Visible = false;
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "Page_Load", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}