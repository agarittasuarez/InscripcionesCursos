using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Globalization;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class MateriasRendidas : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;

        #endregion

        #region Objects


        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                    PrintRenderedMatters((Usuario)Session["user"], AnaliticoDTO.GetAnalitic((Usuario)Session["user"]));
                }
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
        /// Method to print analitic.
        /// </summary>
        private void PrintRenderedMatters(Usuario user, List<Analitico> analitico)
        {
            try
            {
                if (user != null && analitico != null)
                {
                    if (analitico.Count > 0)
                    {
                        string enter = "<br />";
                        StringBuilder scriptingBuilder = new StringBuilder();
                        StringBuilder dataPrint = new StringBuilder();
                        StringBuilder css = new StringBuilder();
                        Dictionary<double, string> notas = new Dictionary<double, string>();
                        notas.Add(1.00, ConfigurationManager.AppSettings["NotaUno"]);
                        notas.Add(2.00, ConfigurationManager.AppSettings["NotaDos"]);
                        notas.Add(3.00, ConfigurationManager.AppSettings["NotaTres"]);
                        notas.Add(4.00, ConfigurationManager.AppSettings["NotaCuatro"]);
                        notas.Add(5.00, ConfigurationManager.AppSettings["NotaCinco"]);
                        notas.Add(6.00, ConfigurationManager.AppSettings["NotaSeis"]);
                        notas.Add(7.00, ConfigurationManager.AppSettings["NotaSiete"]);
                        notas.Add(8.00, ConfigurationManager.AppSettings["NotaOcho"]);
                        notas.Add(9.00, ConfigurationManager.AppSettings["NotaNueve"]);
                        notas.Add(10.00, ConfigurationManager.AppSettings["NotaDiez"]);


                        css.Append("<style type=\"text/css\">table {font-family: Consolas, Verdana; font-size: 10pt;border-collapse: collapse;}");
                        css.Append("body { font-family: Consolas, Verdana; font-size: 12pt;}</style>");

                        dataPrint.Append("<html>");
                        dataPrint.Append("<head><title>" + String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleMateriasRendidas"]) + "</title>");
                        dataPrint.Append(css.ToString() + "</head>");
                        dataPrint.Append("<body>");

                        dataPrint.Append("<div><div style=\"text-align: center; float: left; display: inline\">" + String.Format(ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresion"], enter, enter) + "</div>");
                        dataPrint.Append("<div style=\"display:inline; float:right;\">" + ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresionFecha"] + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "</div></div><br /><br /><br /><br /><br /><br />");

                        dataPrint.Append("<div style=\"text-align: center\"><table style=\"text-align: center\" border=\"1\" width=\"1024px\">");
                        dataPrint.Append("<tr><td colspan=\"12\">");
                        dataPrint.Append(user.ApellidoNombre + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + user.DNI.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ConfigurationManager.AppSettings["ContentAnaliticoUltimoIngreso"] + "&nbsp;&nbsp;&nbsp;" + ConfigurationManager.AppSettings["ContentAnaliticoSitucion"] + user.Estado);
                        dataPrint.Append("</td></tr><tr>");
                        dataPrint.Append("<td colspan=\"12\">" + ConfigurationManager.AppSettings["ContentAnaliticoTitulo"] + "</td></tr>");
                        dataPrint.Append("<tr><td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderCodigo"] + "</td><td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderMateria"] + "</td>");
                        dataPrint.Append("<td colspan=\"5\"><table style=\"text-align: center\" border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
                        dataPrint.Append("<tr><td colspan=\"2\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderCalificacion"] + "</td>");
                        dataPrint.Append("<td colspan=\"3\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderFecha"] + "</td>");
                        dataPrint.Append("</tr><tr><td style=\"border-top:1px solid; border-right:1px solid\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderNumero"] + "</td>");
                        dataPrint.Append("<td style=\"border-top:1px solid; border-right:1px solid\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderLetras"] + "</td>");
                        dataPrint.Append("<td style=\"border-top:1px solid; border-right:1px solid\"><div style=\"width:16px\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderDia"] + "</div></td>");
                        dataPrint.Append("<td style=\"border-top:1px solid; border-right:1px solid\"><div style=\"width:16px\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderMes"] + "</div></td>");
                        dataPrint.Append("<td style=\"border-top:1px solid\"><div style=\"width:22px\">" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderAnio"] + "</div></td>");
                        dataPrint.Append("</tr></table></td>");
                        dataPrint.Append("<td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderLibro"] + "</td>");
                        dataPrint.Append("<td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderTomo"] + "</td>");
                        dataPrint.Append("<td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderFolio"] + "</td>");
                        dataPrint.Append("<td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderSubFolio"] + "</td>");
                        dataPrint.Append("<td>" + ConfigurationManager.AppSettings["ContentAnaliticoHeaderCondicion"] + "</td></tr>");

                        for (int i = 0; i < analitico.Count; i++)
                        {
                            dataPrint.Append("<tr>");
                            dataPrint.Append("<td>" + analitico[i].Plan.ToString() + analitico[i].IdMateria.ToString() + "</td>");
                            dataPrint.Append("<td>" + analitico[i].Materia + "</td>");
                            dataPrint.Append("<td><div style=\"width:40px\">");
                            if (analitico[i].Nota != 0.0)
                                dataPrint.Append(analitico[i].Nota.ToString());
                            dataPrint.Append("</div></td>");
                            dataPrint.Append("<td><div style=\"width:40px\">");
                            if (analitico[i].Nota != 0.0)
                                dataPrint.Append(notas[analitico[i].Nota]);
                            dataPrint.Append("</div></td>");
                            dataPrint.Append("<td><div style=\"width:13px\">" + analitico[i].Fecha.Day.ToString() + "</div></td>");
                            dataPrint.Append("<td><div style=\"width:13px\">" + analitico[i].Fecha.Month.ToString() + "</div></td>");
                            dataPrint.Append("<td><div style=\"width:20px\">" + analitico[i].Fecha.Year.ToString() + "</div></td>");
                            dataPrint.Append("<td>" + analitico[i].Libro + "</td>");
                            dataPrint.Append("<td>" + analitico[i].Tomo + "</td>");
                            dataPrint.Append("<td>" + analitico[i].Folio + "</td>");
                            dataPrint.Append("<td>" + analitico[i].SubFolio + "</td>");
                            dataPrint.Append("<td>" + analitico[i].TipoInscripcion + "</td>");
                            dataPrint.Append("</tr>");
                        }
                        dataPrint.Append("</table></div>");
                        dataPrint.Append(enter + enter + "<div>" + ConfigurationManager.AppSettings["ContentAnaliticoFooter1"] + "</div>");
                        dataPrint.Append("<div>" + ConfigurationManager.AppSettings["ContentAnaliticoFooter2"] + "</div>");
                        dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentAnaliticoFooter3"], DateTime.Now.Day.ToString(), String.Format("{0:MMMM}", DateTime.Now).ToString(), DateTime.Now.Year.ToString()) + "</div>");
                        dataPrint.Append("</body></html>");

                        scriptingBuilder.Append("<script type='text/javascript'>");
                        scriptingBuilder.Append("var win=null;");
                        scriptingBuilder.Append("win = window.open();");
                        scriptingBuilder.Append("self.focus();");
                        scriptingBuilder.Append("win.document.open();");
                        scriptingBuilder.Append("win.document.write('" + HttpUtility.JavaScriptStringEncode(dataPrint.ToString()) + "');");
                        scriptingBuilder.Append("win.document.close();");
                        scriptingBuilder.Append("win.print();");
                        scriptingBuilder.Append("win.close();");
                        scriptingBuilder.Append("</script>");

                        ClientScriptManager script = Page.ClientScript;
                        script.RegisterStartupScript(this.GetType(), "jsPrint", scriptingBuilder.ToString());
                        lblEstado.Text = ConfigurationManager.AppSettings["ContentConMateriasRendidas"];
                    }
                    else
                        lblEstado.Text = ConfigurationManager.AppSettings["ContentSinMateriasRendidas"];
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "PrintRenderedMatters", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}