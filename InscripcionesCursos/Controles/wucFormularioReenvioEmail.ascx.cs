using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;

namespace InscripcionesCursos
{
    public partial class wucFormularioReenvioEmail : System.Web.UI.UserControl
    {
        #region Constants & Variables

        int mailTypeActivation = Convert.ToInt32(ConfigurationManager.AppSettings["MailTypeActivation"]);

        #endregion

        #region Objects

        Usuario resendUser;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Events

        /// <summary>
        /// Event to resend the acount activation email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReenviar_Click(object sender, EventArgs e)
        {
            ResendEmail();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to send again the activation email to the user
        /// </summary>
        private void ResendEmail()
        {
            try
            {
                if (Utils.ValidateDni(txtDni.Text))
                {
                    resendUser = new Usuario(Convert.ToInt32(txtDni.Text));
                    resendUser = UsuarioDTO.GetUsuario(resendUser);
                    if (resendUser != null)
                    {
                        if (resendUser.Email != null)
                        {
                            if (Utils.SendMail(resendUser, mailTypeActivation))
                                SetMessage(ConfigurationManager.AppSettings["ContentResendEmailOk"], 1);
                            else
                                SetMessage(ConfigurationManager.AppSettings["ErrorMessageResendEmailFailed"], 2);
                        }
                        else
                            SetMessage(ConfigurationManager.AppSettings["ErrorMessageUserSinMail"], 2);
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
                log.WriteLog(ex.Message, "ResendEmail", Path.GetFileName(Request.PhysicalPath));
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