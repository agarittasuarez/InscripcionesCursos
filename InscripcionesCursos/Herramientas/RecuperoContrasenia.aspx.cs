using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class RecuperoContrasenia : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;

        int mailTypeActivation = Convert.ToInt32(ConfigurationManager.AppSettings["MailTypePasswordRecovery"]);

        #endregion

        #region Objects

        Usuario recoveryUser;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.UrlReferrer == null)
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
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

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            RecoverUserPasswordAndSend(txtDni.Text);            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to recover user info
        /// </summary>
        private void RecoverUserPasswordAndSend(string userDni)
        {
            try
            {
                if (Utils.ValidateDni(txtDni.Text))
                {
                    recoveryUser = new Usuario(Convert.ToInt32(userDni));
                    recoveryUser = UsuarioDTO.GetUsuario(recoveryUser);

                    if (recoveryUser != null)
                    {
                        if (recoveryUser.IdCargo == UserTypeStudent)
                        {
                            if (recoveryUser.Email != null && recoveryUser.CambioPrimerLogin && recoveryUser.CuentaActivada)
                            {
                                if (Utils.SendMail(recoveryUser, mailTypeActivation))
                                {
                                    SetMessage(String.Format(ConfigurationManager.AppSettings["ContentSendPasswordEmail"], recoveryUser.Email.ToLower()), 1);
                                    Response.AddHeader("REFRESH", "3;URL=" + Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);
                                }
                                else
                                    SetMessage(ConfigurationManager.AppSettings["ErrorMessageSendPasswordEmailFailed"], 2);
                            }
                            else
                                SetMessage(ConfigurationManager.AppSettings["ErrorMessageSendPasswordEmailEmpty"], 2);
                        }
                        else
                            SetMessage(ConfigurationManager.AppSettings["ErrorMessageSendPasswordEmailCargo"], 2);
                    }
                    else
                        SetMessage(ConfigurationManager.AppSettings["ErrorMessageDniInexistente"], 2);
                }
                else
                    SetMessage(ConfigurationManager.AppSettings["ErrorMessageLoginDni"], 2);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "RecoverUserPasswordAndSend", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set status message
        /// </summary>
        private void SetMessage(string message, int type)
        {
            lblEstado.Text = message;
            if (type == 1)
                lblEstado.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            else
                lblEstado.ForeColor = ColorTranslator.FromHtml("#EE3B3B");
        }

        #endregion
    }
}