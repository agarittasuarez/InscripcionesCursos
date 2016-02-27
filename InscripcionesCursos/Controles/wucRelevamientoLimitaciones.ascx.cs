using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;

namespace InscripcionesCursos.Controles
{
    public partial class wucRelevamientoLimitaciones : System.Web.UI.UserControl
    {
        #region Constants

        private const string SiValue = "S";
        private const string NoValue = "N";

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                SetUpControl();
        }

        #endregion

        #region Events

        protected void btnEnviar_OnClick(object sender, EventArgs e)
        {
            var user = (Usuario)Session["user"];

            user.Limitacion = rbOption1.Checked ? SiValue : NoValue;
            user.LimitacionVision = rbOption3.Checked ? SiValue : NoValue;
            user.Lentes = rbOption5.Checked ? SiValue : NoValue;
            user.LimitacionAudicion = rbOption7.Checked ? SiValue : NoValue;
            user.Audifonos = rbOption9.Checked ? SiValue : NoValue;
            user.LimitacionMotriz = rbOption11.Checked ? SiValue : NoValue;
            user.LimitacionAgarre = rbOption13.Checked ? SiValue : NoValue;
            user.LimitacionHabla = rbOption15.Checked ? SiValue : NoValue;
            user.Dislexia = rbOption17.Checked ? SiValue : NoValue;
            user.LimitacionOtra = txtOtras.Text;
            user.LimitacionRelevada = true;

            UsuarioDTO.UpdateLimitaciones(user);
            Session.Add("user", user);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void rbOption_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbOption1.Checked)
            {
                rbOption3.Enabled = true;
                rbOption4.Enabled = true;                
                rbOption7.Enabled = true;
                rbOption8.Enabled = true;                
                rbOption11.Enabled = true;
                rbOption12.Enabled = true;
                rbOption13.Enabled = true;
                rbOption14.Enabled = true;
                rbOption15.Enabled = true;
                rbOption16.Enabled = true;
                txtOtras.Enabled = true;
            }
            else
            {
                rbOption4.Checked = rbOption6.Checked = rbOption8.Checked = rbOption10.Checked = rbOption12.Checked = 
                    rbOption14.Checked = rbOption16.Checked = rbOption18.Checked = true;
                rbOption3.Checked = rbOption5.Checked = rbOption7.Checked = rbOption9.Checked = rbOption11.Checked = 
                    rbOption13.Checked = rbOption15.Checked = rbOption17.Checked = false;
                rbOption3.Enabled = rbOption4.Enabled = rbOption5.Enabled = rbOption6.Enabled = rbOption7.Enabled = 
                    rbOption8.Enabled = rbOption9.Enabled = rbOption10.Enabled = rbOption11.Enabled = 
                    rbOption12.Enabled = rbOption13.Enabled = rbOption14.Enabled = rbOption15.Enabled = 
                    rbOption16.Enabled = rbOption17.Enabled = rbOption18.Enabled = txtOtras.Enabled = false;
                txtOtras.Text = string.Empty;
            }
        }

        protected void rbOptionVision_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbOption3.Checked)
            {
                rbOption5.Enabled = true;
                rbOption6.Enabled = true;
            }
            else
            {
                rbOption5.Checked = false;
                rbOption6.Checked = true;
                rbOption5.Enabled = rbOption6.Enabled = false;
            }
        }

        protected void rbOptionAudicion_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbOption7.Checked)
            {
                rbOption9.Enabled = true;
                rbOption10.Enabled = true;
            }
            else
            {
                rbOption9.Checked = false;
                rbOption10.Checked = true;
                rbOption9.Enabled = rbOption10.Enabled = false;
            }
        }

        protected void rbOptionHabla_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbOption15.Checked)
            {
                rbOption17.Enabled = true;
                rbOption18.Enabled = true;
            }
            else
            {
                rbOption17.Checked = false;
                rbOption18.Checked = true;
                rbOption9.Enabled = rbOption10.Enabled = false;
            }
        }

        #endregion

        #region Methods

        public void ShowEnviarButton(bool show)
        {
            btnEnviar.Visible = show;
        }

        public void ShowControlTitle(bool show)
        {
            lblTitulo.Visible = false;
        }

        private void SetUpControl()
        {
            
            var loggedUser = (Usuario)Session["user"];
            if (loggedUser.LimitacionRelevada)
            {
                if (loggedUser.Limitacion == SiValue)
                {
                    rbOption1.Checked = true;

                    if (loggedUser.LimitacionVision == SiValue)
                        rbOption3.Checked = true;
                    else
                        rbOption4.Checked = true;

                    if (loggedUser.Lentes == SiValue)
                        rbOption5.Checked = true;
                    else
                        rbOption6.Checked = true;

                    if (loggedUser.LimitacionAudicion == SiValue)
                        rbOption7.Checked = true;
                    else
                        rbOption8.Checked = true;

                    if (loggedUser.Audifonos == SiValue)
                        rbOption9.Checked = true;
                    else
                        rbOption10.Checked = true;

                    if (loggedUser.LimitacionMotriz == SiValue)
                        rbOption11.Checked = true;
                    else
                        rbOption12.Checked = true;

                    if (loggedUser.LimitacionAgarre == SiValue)
                        rbOption13.Checked = true;
                    else
                        rbOption14.Checked = true;

                    if (loggedUser.LimitacionHabla == SiValue)
                        rbOption15.Checked = true;
                    else
                        rbOption16.Checked = true;

                    if (loggedUser.Dislexia == SiValue)
                        rbOption17.Checked = true;
                    else
                        rbOption18.Checked = true;

                    if (loggedUser.LimitacionOtra != string.Empty)
                        txtOtras.Text = loggedUser.LimitacionOtra;

                }
                else
                {
                    rbOption2.Checked = true;
                    rbOption3.Enabled = rbOption4.Enabled = rbOption5.Enabled = rbOption6.Enabled = rbOption7.Enabled =
                        rbOption8.Enabled = rbOption9.Enabled = rbOption10.Enabled = rbOption11.Enabled = 
                        rbOption12.Enabled = rbOption13.Enabled = rbOption14.Enabled = rbOption15.Enabled = 
                        rbOption16.Enabled = rbOption17.Enabled = rbOption18.Enabled = false;
                }
            }
            else
            {
                rbOption2.Checked = rbOption4.Checked = rbOption6.Checked = rbOption8.Checked = rbOption10.Checked =
                    rbOption12.Checked = rbOption14.Checked = rbOption16.Checked = rbOption18.Checked = true;

                rbOption3.Enabled = rbOption4.Enabled = rbOption5.Enabled = rbOption6.Enabled = rbOption7.Enabled =
                    rbOption8.Enabled = rbOption9.Enabled = rbOption10.Enabled = rbOption11.Enabled =
                    rbOption12.Enabled = rbOption13.Enabled = rbOption14.Enabled = rbOption15.Enabled =
                    rbOption16.Enabled = rbOption17.Enabled = rbOption18.Enabled = txtOtras.Enabled = false;
            }
        }

        #endregion
    }
}