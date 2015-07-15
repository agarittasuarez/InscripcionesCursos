using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;

namespace InscripcionesCursos.Empleados
{
    public partial class CambioContrasenia : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;

        #endregion

        #region Objects

        Usuario loggedUser;
        
        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                FillUserData();

                if (loggedUser.CambioPrimerLogin)
                    SetSuccessView();
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

        #region Events

        /// <summary>
        /// Event to save the password change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!loggedUser.CambioPrimerLogin)
                {
                    if (ValidatePassword())
                    {
                        loggedUser.CambioPrimerLogin = true;
                        SaveNewPassword();
                        Session.Add("userEmployee", loggedUser);
                        SetSuccessView();
                        Response.AddHeader("REFRESH", "5;URL=" + Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"]);
                    }
                    else
                    {
                        FailureText.Text = ConfigurationManager.AppSettings["ErrorMessagePasswordNoCambiada"];
                        divMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnEnviar_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to abort the password change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("userEmployee");
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnCancelar_Click", Path.GetFileName(Request.PhysicalPath));
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
                loggedUser = (Usuario)Session["userEmployee"];

                txtDni.Text = loggedUser.DNI.ToString();
                txtApellidoNombre.Text = loggedUser.ApellidoNombre;
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

        /// <summary>
        /// Method to validate the new password
        /// </summary>
        /// <returns></returns>
        private bool ValidatePassword()
        {
            try
            {
                if (txtPasswordOld.Text.ToLower() != loggedUser.Password.ToLower())
                {
                    FailureText.Text = ConfigurationManager.AppSettings["ErrorMessagePasswordIncorrecta"];
                    divMessage.Visible = true;
                    return false;
                }
                else
                {
                    if (txtPasswordNew.Text.ToLower() != txtPasswordRepeat.Text.ToLower())
                    {
                        FailureText.Text = ConfigurationManager.AppSettings["ErrorMessagePasswordRepeat"];
                        divMessage.Visible = true;
                        return false;
                    }
                    else
                    {
                        if ((txtPasswordNew.Text.ToLower() == loggedUser.Password.ToLower()) && (txtPasswordRepeat.Text.ToLower() == loggedUser.Password.ToLower()))
                        {
                            FailureText.Text = ConfigurationManager.AppSettings["ErrorMessagePasswordEqual"];
                            divMessage.Visible = true;
                            return false;
                        }
                        else
                        {
                            if (txtPasswordNew.Text.Length < 6)
                            {
                                FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageLoginPassword"];
                                divMessage.Visible = true;
                                return false;
                            }
                            else
                            {
                                loggedUser.Password = txtPasswordNew.Text;
                                divMessage.Visible = false;
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ValidatePassword", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to save the new password
        /// </summary>
        private bool SaveNewPassword()
        {
            try
            {
                return UsuarioDTO.UpdateMandatoryPasswordEmail(loggedUser);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SaveNewPassword", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to display and set the success event
        /// </summary>
        private void SetSuccessView()
        {
            try
            {
                FailureText.Text = ConfigurationManager.AppSettings["ContentCambioPasswordEmployee"];
                FailureText.ForeColor = System.Drawing.Color.White;
                txtPasswordOld.Text = loggedUser.Password;
                txtPasswordNew.Text = loggedUser.Password;
                txtPasswordRepeat.Text = loggedUser.Password;
                txtPasswordOld.Enabled = false;
                txtPasswordNew.Enabled = false;
                txtPasswordRepeat.Enabled = false;
                btnEnviar.Visible = false;
                divMessage.Visible = true;
                divLoading.Visible = true;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetSuccessView", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}