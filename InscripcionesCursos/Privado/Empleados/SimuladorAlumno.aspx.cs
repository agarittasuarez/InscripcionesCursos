using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class SimuladorAlumno : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        string coleccionDniResend = ConfigurationManager.AppSettings["UserEmployeesResend"];
        private wucMenuNavegacionSimulador menuControl;

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

                menuControl = (wucMenuNavegacionSimulador)Master.FindControl("menuSimulador");
                Session.Remove("user");
                //menuControl.BtnBackClick += new EventHandler(btnBack_Click);
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

        #region Events

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            GetStudentProfile(Convert.ToInt32(txtDni.Text));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get student profile and set student interface
        /// </summary>
        /// <param name="dni"></param>
        private void GetStudentProfile(int dni)
        {
            var user = ValidateStudent(Convert.ToInt32(txtDni.Text));

            if (user != null && user.DNI != 0)
            {
                Session.Add("user", user);
                divSearchBox.Visible = false;
                menuControl.LabelUsuario(user);
                menuControl.Visible = true;
            }
            else
                FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageDniInexistente"];
        }

        /// <summary>
        /// Method to validate if student exists
        /// </summary>
        /// <param name="state"></param>
        private Usuario ValidateStudent(int dni)
        {
            try
            {
                return UsuarioDTO.GetUsuario(new Usuario(dni));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ValidateStudent", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}