using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using InscripcionesCursos.BE;
using System.IO;
using System.Threading;

namespace InscripcionesCursos.Privado.Alumnos
{
    public partial class ConstanciaAlumnoRegular : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;
        const string PrimerCuatrimestre = "1/";
        const string SegundoCuatrimestre = "2/";

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Convert.ToBoolean(ConfigurationManager.AppSettings["ConstanciaRegularidadDisable"]))
                    {
                        if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                            Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                        if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                            Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                        if ((((Usuario)Session["user"]).Estado.IndexOf("Activo") == -1) || (((Usuario)Session["user"]).CuatrimestreAnioIngreso == null))
                        {
                            lblEstado.Visible = true;
                            actionForm.Visible = false;
                        }
                    }
                    else
                    {
                        divDatosConstancia.Visible = false;
                        divNoDisponible.Visible = true;
                        lblMsjNoDisponible.Text = ConfigurationManager.AppSettings["ContentHistorialInscripcionNoDisponible"];
                    }
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

        #region Events

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintConstancia(inputText.Value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to to print regular constancy
        /// </summary>
        private void PrintConstancia(string text)
        {
            try
            {
                string enter = "<br />";
                string condicionDesde = string.Empty;
                string condicionHasta = string.Empty;
                string yearHeader = string.Empty;
                StringBuilder scriptingBuilder = new StringBuilder();
                StringBuilder dataPrint = new StringBuilder();
                StringBuilder css = new StringBuilder();
                Usuario user = (Usuario)Session["user"];
                Dictionary<int, string> months = new Dictionary<int, string>();
                Dictionary<int, string> years = new Dictionary<int, string>();

                #region Months Dictionary

                months.Add(1, ConfigurationManager.AppSettings["Enero"]);
                months.Add(2, ConfigurationManager.AppSettings["Febrero"]);
                months.Add(3, ConfigurationManager.AppSettings["Marzo"]);
                months.Add(4, ConfigurationManager.AppSettings["Abril"]);
                months.Add(5, ConfigurationManager.AppSettings["Mayo"]);
                months.Add(6, ConfigurationManager.AppSettings["Junio"]);
                months.Add(7, ConfigurationManager.AppSettings["Julio"]);
                months.Add(8, ConfigurationManager.AppSettings["Agosto"]);
                months.Add(9, ConfigurationManager.AppSettings["Septiembre"]);
                months.Add(10, ConfigurationManager.AppSettings["Octubre"]);
                months.Add(11, ConfigurationManager.AppSettings["Noviembre"]);
                months.Add(12, ConfigurationManager.AppSettings["Diciembre"]);

                #endregion

                if (text != ConfigurationManager.AppSettings["DefaultText"])
                {
                    //Validate if the student has reincorporation or not
                    if ((user.CuatrimestreAnioIngreso.Trim().IndexOf(PrimerCuatrimestre) != -1) && (user.CuatrimestreAnioReincorporacion == null || (user.CuatrimestreAnioReincorporacion.Trim() == string.Empty)))
                    {
                        if (DateTime.Now.Month <= 3)
                        {
                            condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdePrimerCuatrimestre"] + (DateTime.Now.Year - 1).ToString();
                            condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaPrimerCuatrimestre"] + DateTime.Now.Year.ToString();
                        }
                        else
                        {
                            condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdePrimerCuatrimestre"] + DateTime.Now.Year.ToString();
                            condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaPrimerCuatrimestre"] + (DateTime.Now.Year + 1).ToString();
                        }
                    }
                    else
                    {
                        if ((user.CuatrimestreAnioIngreso.Trim().IndexOf(SegundoCuatrimestre) != -1) && (user.CuatrimestreAnioReincorporacion == null || (user.CuatrimestreAnioReincorporacion.Trim() == string.Empty)))
                        {
                            if (DateTime.Now.Month <= 7)
                            {
                                condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdeSegundoCuatrimestre"] + (DateTime.Now.Year - 1).ToString();
                                condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaSegundoCuatrimestre"] + DateTime.Now.Year.ToString();
                            }
                            else
                            {
                                condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdeSegundoCuatrimestre"] + DateTime.Now.Year.ToString();
                                condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaSegundoCuatrimestre"] + (DateTime.Now.Year + 1).ToString();
                            }
                        }
                        else
                        {
                            if (user.CuatrimestreAnioReincorporacion.Trim().IndexOf(PrimerCuatrimestre) != -1)
                            {
                                if (DateTime.Now.Month <= 3)
                                {
                                    condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdePrimerCuatrimestre"] + (DateTime.Now.Year - 1).ToString();
                                    condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaPrimerCuatrimestre"] + DateTime.Now.Year.ToString();
                                }
                                else
                                {
                                    condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdePrimerCuatrimestre"] + DateTime.Now.Year.ToString();
                                    condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaPrimerCuatrimestre"] + (DateTime.Now.Year + 1).ToString();
                                }
                            }
                            else
                            {
                                if (DateTime.Now.Month <= 7)
                                {
                                    condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdeSegundoCuatrimestre"] + (DateTime.Now.Year - 1).ToString();
                                    condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaSegundoCuatrimestre"] + DateTime.Now.Year.ToString();
                                }
                                else
                                {
                                    condicionDesde = ConfigurationManager.AppSettings["DiaDesde"] + ConfigurationManager.AppSettings["MesDesdeSegundoCuatrimestre"] + DateTime.Now.Year.ToString();
                                    condicionHasta = ConfigurationManager.AppSettings["DiaHasta"] + ConfigurationManager.AppSettings["MesHastaSegundoCuatrimestre"] + (DateTime.Now.Year + 1).ToString();
                                }
                            }
                        }
                    }

                    for (int x = 2013; x <= 2030; x++)
                        years.Add(x, ConfigurationManager.AppSettings[x.ToString()]);

                    css.Append("<style type=\"text/css\">table {font-size: 10pt;border-collapse: collapse;}");
                    css.Append("body { font-family: Courier; font-size: 12pt; font-style: italic;}</style>");

                    dataPrint.Append("<html>");
                    dataPrint.Append("<head><title>" + String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleConstanciaAlumnoRegular"]) + "</title>");
                    dataPrint.Append(css.ToString() + "</head>");
                    dataPrint.Append("<body>");

                    if (DateTime.Now.Month <= 3)
                        yearHeader = (DateTime.Now.Year - 1).ToString();
                    else
                        yearHeader = DateTime.Now.Year.ToString();

                    dataPrint.Append("<div><div style=\"text-align: center; float: left; display: inline\">" + String.Format(ConfigurationManager.AppSettings["ContentHeaderConstanciaImpresion"], enter, enter, yearHeader) + "</div></div>");
                    dataPrint.Append(enter + enter + enter + enter + enter + enter);
                    dataPrint.Append("<p style=\"text-indent:50px\">" + String.Format(ConfigurationManager.AppSettings["ContentBodyConstanciaImpresionPart1"], user.ApellidoNombre, user.DNI.ToString(), user.DNI.ToString(), condicionDesde, condicionHasta) + "</p>");
                    dataPrint.Append("<p style=\"text-indent:50px\">" + String.Format(ConfigurationManager.AppSettings["ContentBodyConstanciaImpresionPart2"], enter, text, DateTime.Now.Day.ToString(), months[DateTime.Now.Month], years[DateTime.Now.Year]) + "</p>");
                    dataPrint.Append(enter + enter);
                    dataPrint.Append("<p style=\"text-indent:50px\">" + ConfigurationManager.AppSettings["ContentFooterConstanciaImpresion"] + "</p>");
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
                }
                else
                    textRequired.IsValid = false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "PrintConstancia", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}