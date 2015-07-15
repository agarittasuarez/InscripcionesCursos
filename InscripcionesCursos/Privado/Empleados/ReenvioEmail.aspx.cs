﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.BE;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class ReenvioEmail : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        string coleccionDniResend = ConfigurationManager.AppSettings["UserEmployeesResend"];

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                if (!Utils.CheckAccountStatus(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeePasswordChange"]);

                if (coleccionDniResend.IndexOf(((Usuario)Session["userEmployee"]).DNI.ToString()) == -1)
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployee"]);

                Session.Remove("user");
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