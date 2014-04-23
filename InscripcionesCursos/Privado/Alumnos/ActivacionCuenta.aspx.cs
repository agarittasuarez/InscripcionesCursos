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
    public partial class ActivacionCuenta : System.Web.UI.Page
    {
        #region Objects

        Usuario userActivate;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["user"] != null) && (Request.QueryString["code"] != null))
                {
                    userActivate = new Usuario();
                    userActivate.DNI = Convert.ToInt32(Cryptography.Decrypt(Request.QueryString["user"].ToString()));
                    userActivate.CodigoActivacion = Convert.ToInt32(Cryptography.Decrypt(Request.QueryString["code"].ToString()));

                    userActivate = UsuarioDTO.ActivateAccount(userActivate);
                    if (userActivate.DNI != 0)
                    {
                        Session.Add("user", userActivate);
                        InfoText.Text = String.Format(ConfigurationManager.AppSettings["ContentActivationAccountSuccesMessage"], userActivate.ApellidoNombre);
                        divLoading.Visible = true;
                        Response.AddHeader("REFRESH", "5;URL=" + Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudent"]);
                    }
                    else
                        InfoText.Text = ConfigurationManager.AppSettings["ContentActivationAccountError"];
                }
                else
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
    }
}