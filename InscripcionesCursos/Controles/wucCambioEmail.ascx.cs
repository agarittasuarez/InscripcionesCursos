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

namespace InscripcionesCursos
{
    public partial class wucCambioEmail : System.Web.UI.UserControl
    {
        #region Objects

        Usuario userMailChange;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Events

        /// <summary>
        /// Evento to search email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            SearchEmail(Convert.ToInt32(txtDni.Text));
        }

        /// <summary>
        /// Event to change email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            ChangeEmail();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to search the input email
        /// </summary>
        /// <param name="dni"></param>
        private void SearchEmail(int dni)
        {
            try
            {
                if (Utils.ValidateDni(dni.ToString()))
                {
                    FailureText.Visible = false;
                    ClearFields();
                    userMailChange = new Usuario();
                    userMailChange.DNI = dni;
                    userMailChange = UsuarioDTO.GetUsuario(userMailChange);

                    if (userMailChange != null)
                    {
                        txtEmail.Text = userMailChange.Email;
                        txtApellidoNombre.Text = userMailChange.ApellidoNombre;
                        ShowResults(true);
                    }
                    else
                    {
                        FailureText.Visible = true;
                        FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageDniInexistente"];
                        ShowResults(false);
                    }
                }
                else
                {
                    FailureText.Visible = true;
                    FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageLoginDni"];
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SearchEmail", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to update user email
        /// </summary>
        private void ChangeEmail()
        {
            try
            {
                userMailChange = new Usuario();
                userMailChange.DNI = Convert.ToInt32(txtDni.Text);
                userMailChange.Email = txtEmailChange.Text;
                UsuarioDTO.UpdateEmail(userMailChange);

                lblEstado.Text = ConfigurationManager.AppSettings["ContentCambioEmailOk"];                
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ChangeEmail", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        
        /// <summary>
        /// Method to clear txt fields
        /// </summary>
        private void ClearFields()
        {
            txtEmail.Text = String.Empty;
            txtEmailChange.Text = String.Empty;
            lblEstado.Text = String.Empty;
        }
        
        /// <summary>
        /// Method to hide and show results and button area
        /// </summary>
        /// <param name="b"></param>
        private void ShowResults(bool b)
        {
            divResultados.Visible = b;
            btnCambiar.Visible = b;
        }

        #endregion
    }
}