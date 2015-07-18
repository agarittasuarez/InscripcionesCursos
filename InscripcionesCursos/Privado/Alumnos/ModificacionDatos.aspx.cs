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

        private const string SiValue = "S";
        private const string NoValue = "N";
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

        #region Events

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["user"];

            user.ApellidoNombre = txtApellidoNombre.Text;
            user.Email = txtEmail.Text;

            user.Limitacion = ((RadioButton)ucRelevamientoMod.FindControl("rbOption1")).Checked ? SiValue : NoValue;
            user.LimitacionVision = ((RadioButton)ucRelevamientoMod.FindControl("rbOption3")).Checked
                ? SiValue
                : NoValue;
            user.LimitacionAudicion = ((RadioButton)ucRelevamientoMod.FindControl("rbOption5")).Checked
                ? SiValue
                : NoValue;
            user.LimitacionMotriz = ((RadioButton)ucRelevamientoMod.FindControl("rbOption7")).Checked
                ? SiValue
                : NoValue;
            user.LimitacionAgarre = ((RadioButton)ucRelevamientoMod.FindControl("rbOption9")).Checked
                ? SiValue
                : NoValue;
            user.LimitacionHabla = ((RadioButton)ucRelevamientoMod.FindControl("rbOption11")).Checked
                ? SiValue
                : NoValue;
            user.LimitacionOtra = ((TextBox)ucRelevamientoMod.FindControl("txtOtras")).Text;
            user.LimitacionRelevada = true;

            //Cambiar el metodo cuando se habilite la modificacion de datos
            UsuarioDTO.UpdateLimitaciones(user);
            Session.Add("user", user);
            Response.Redirect(Request.Url.AbsoluteUri);
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
                txtCarrera.Text = loggedUser.Carrera;

                ucRelevamientoMod.ShowEnviarButton(false);
                ucRelevamientoMod.ShowControlTitle(false);
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