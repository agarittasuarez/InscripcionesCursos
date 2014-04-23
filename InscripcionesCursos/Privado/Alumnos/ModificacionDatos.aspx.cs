using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class ModificacionDatos : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;

        #endregion

        #region Objects

        Usuario loggedUser;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                FillUserData();
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

        #region Methods

        /// <summary>
        /// Method to fill user data
        /// </summary>
        private void FillUserData()
        {
            try
            {
                loggedUser = new Usuario();
                loggedUser = (Usuario)Session["user"];

                txtDni.Text = loggedUser.DNI.ToString();
                txtApellidoNombre.Text = loggedUser.ApellidoNombre;
                txtEmail.Text = loggedUser.Email;
                //txtNombre.Text = loggedUser.Nombre;
                //txtApellido.Text = loggedUser.Apellido;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "FillUserData", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}