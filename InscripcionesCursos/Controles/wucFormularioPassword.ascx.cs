using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;

namespace InscripcionesCursos
{
    public partial class wucFormularioPassword : System.Web.UI.UserControl
    {
        #region Objects

        Usuario userPasswordUpdate;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Events

        /// <summary>
        /// Generate Event.
        /// </summary>
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to auto-generate a Password.
        /// </summary>
        private void GeneratePassword()
        {
            try
            {
                string definitions = ConfigurationManager.AppSettings["RandomDefinitions"];
                Random random = new Random();
                StringBuilder passwordReturn = new StringBuilder();

                if (Utils.ValidateDni(txtDni.Text))
                {
                    FailureText.Visible = false;

                    for (int i = 0; i < 10; i++)
                        passwordReturn.Append(definitions.Substring(random.Next(definitions.Length), 1));

                    userPasswordUpdate = new Usuario();
                    userPasswordUpdate.DNI = Convert.ToInt32(txtDni.Text);
                    userPasswordUpdate.Password = passwordReturn.ToString();
                    userPasswordUpdate = UsuarioDTO.UpdatePassword(userPasswordUpdate);

                    if (userPasswordUpdate.DNI != 0)
                    {
                        divResultados.Visible = true;
                        FailureText.Visible = false;
                        txtDniResultado.Text = userPasswordUpdate.DNI.ToString();
                        txtApellidoNombreResultado.Text = userPasswordUpdate.ApellidoNombre;
                        txtPasswordResultado.Attributes.Add("value", userPasswordUpdate.ToString());

                        PrintForm(userPasswordUpdate);
                    }
                    else
                    {
                        FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageDniInexistente"];
                        FailureText.Visible = true;
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
                log.WriteLog(ex.Message, "GeneratePassword", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to print a form transaction.
        /// </summary>
        private void PrintForm(Usuario user)
        {
            try
            {
                string enter = "<br />";
                StringBuilder scriptingBuilder = new StringBuilder();
                StringBuilder dataPrint = new StringBuilder();
                dataPrint.Append("<img src=\"" + Page.ResolveUrl("~") + "img/header_impresion.gif\" alt=\"UNLZ\" />");
                dataPrint.Append("<div><div style=\"display:inline\">Fecha Tramite: " + DateTime.Now.ToShortDateString() + "</div>");
                dataPrint.Append("<div style=\"display:inline; float:right;\">Hora: " + DateTime.Now.ToShortTimeString() + "</div></div><br />");
                dataPrint.Append("<div>DNI: " + user.DNI.ToString() + "</div><div>" + "Apellido y Nombre: " + user.ApellidoNombre + "</div><br /><div style=\"display:inline;\">" + "Contraseña: <div style=\"color:#FFFFFF; font-weight: bold; display:inline;\"/>" + user.Password + "</div><div><br />");
                dataPrint.Append(String.Format(ConfigurationManager.AppSettings["ContentPrintForm"], enter, enter, enter));

                scriptingBuilder.Append("<script type='text/javascript'>");
                scriptingBuilder.Append("var win=null;");
                scriptingBuilder.Append("win = window.open();");
                scriptingBuilder.Append("self.focus();");
                scriptingBuilder.Append("win.document.open();");
                scriptingBuilder.Append("win.document.write('<'+'html'+'><'+'head'+'><'+'style'+'>');");
                scriptingBuilder.Append("win.document.write('body, td { font-family: Verdana; font-size: 10pt;}');");
                scriptingBuilder.Append("win.document.write('<'+'/'+'style'+'><'+'/'+'head'+'><'+'body'+'>');");
                scriptingBuilder.Append("win.document.write('" + HttpUtility.JavaScriptStringEncode(dataPrint.ToString()) + "');");
                scriptingBuilder.Append("win.document.write('<'+'/'+'body'+'><'+'/'+'html'+'>');");
                scriptingBuilder.Append("win.document.close();");
                scriptingBuilder.Append("win.print();");
                scriptingBuilder.Append("win.close();");
                scriptingBuilder.Append("</script>");

                ClientScriptManager script = Page.ClientScript;
                script.RegisterStartupScript(this.GetType(), "jsPrint", scriptingBuilder.ToString());
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "PrintForm", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}