using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class HistorialInscripcion : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;
        const string CarreraContador = "CONTADOR PÚBLICO";
        const string CarreraLicAdmin = "LICENCIATURA EN ADMINISTRACIÓN";
        const string ComboTextFieldTurno = "TurnoInscripcion";
        string IdEstadoBajaModificacion = ConfigurationManager.AppSettings["IdEstadoBajaModificacion"];
        string IdEstadoAltaInscripcion = ConfigurationManager.AppSettings["IdEstadoAltaInscripcion"];
        string IdTipoInscripcionPromocion = ConfigurationManager.AppSettings["IdTipoInscripcionPromocion"];

        #endregion

        #region Objects

        Inscripcion historicoInscripcion;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionHistoricDisable"]))
                    {
                        if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                            Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                        if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                            Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                        FillComboTurnoInscripcion();
                    }
                    else
                    {
                        divResultados.Visible = false;
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

        protected void ddTurnosInscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTurnosInscripcion.SelectedIndex != 0)
            {
                GetStudentInscriptions(Convert.ToDateTime(Convert.ToDateTime(ddTurnosInscripcion.SelectedValue.Substring(0, ddTurnosInscripcion.SelectedValue.IndexOf("-")))), ddTurnosInscripcion.SelectedValue.Substring(ddTurnosInscripcion.SelectedValue.IndexOf("-") + 2));
                showGrid(true);
            }
            else
            {
                showGrid(false);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PrintHistory((Usuario)Session["user"], (DataTable)Session["inscripciones"]);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnImprimir_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        //Method to fill the Turnos drop down list
        /// </summary>
        private void FillComboTurnoInscripcion()
        {
            try
            {
                ddTurnosInscripcion.DataSource = ExtractTurnosAndTipoInscripcion(InscripcionDTO.GetAllTurnos(new Inscripcion(((Usuario)Session["user"]).DNI))).GroupBy(i => i).Select(group => group.Key).ToList();
                ddTurnosInscripcion.DataBind();

                ddTurnosInscripcion.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "FillComboTurnoInscripcion", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        //Method to concatenate the Turnos drop down list and Tipo Inscripcion
        /// </summary>
        private List<string> ExtractTurnosAndTipoInscripcion(DataTable dataTable)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    list.Add(dataTable.Rows[i]["TurnoInscripcionBreve"].ToString() + "- " + dataTable.Rows[i]["Descripcion"].ToString());

                return list;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ExtractTurnosAndTipoInscripcion", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to Get all the inscriptions of students
        /// </summary>
        private void GetStudentInscriptions(DateTime turno, string tipoInscripcion)
        {
            try
            {
                historicoInscripcion = new Inscripcion();
                historicoInscripcion.DNI = ((Usuario)Session["user"]).DNI;
                historicoInscripcion.TurnoInscripcion = turno;
                historicoInscripcion.IdTipoInscripcion = GetIdTipoInscripcion(tipoInscripcion);

                GridResultados.DataSource = RemoveDuplicates(InscripcionDTO.GetStudentInscriptions(historicoInscripcion));
                Session.Add("inscripciones", GridResultados.DataSource);
                setHeaders();
                GridResultados.DataBind();

                if (GridResultados.Rows.Count == 0)
                    btnImprimir.Visible = false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "GetStudentInscriptions", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private string GetIdTipoInscripcion(string tipoInscripcion)
        {
            switch (tipoInscripcion)
            {
                case "Exámenes Libres":
                    return "E";
                default:
                    return "P";
            }                
        }

        /// <summary>
        /// Method to set the GridView Headers texts
        /// </summary>
        private void setHeaders()
        {
            try
            {
                GridResultados.Columns[0].HeaderText = ConfigurationManager.AppSettings["ContentHeaderTurno"];
                GridResultados.Columns[1].HeaderText = ConfigurationManager.AppSettings["ContentHeaderCatedraComision"];
                GridResultados.Columns[2].HeaderText = ConfigurationManager.AppSettings["ContentHeaderCodigoMateria"];
                GridResultados.Columns[3].HeaderText = ConfigurationManager.AppSettings["ContentHeaderMateria"];
                GridResultados.Columns[4].HeaderText = ConfigurationManager.AppSettings["ContentHeaderEstadoInscripcion"];
                GridResultados.Columns[5].HeaderText = ConfigurationManager.AppSettings["ContentHeaderTipoInscripcion"];
                GridResultados.Columns[6].HeaderText = ConfigurationManager.AppSettings["ContentHeaderVuelta"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "setHeaders", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set the GridView visibility
        /// </summary>
        private void showGrid(bool estado)
        {
            divGrid.Visible = estado;
        }

        /// <summary>
        /// Method to print inscription history.
        /// </summary>
        private void PrintHistory(Usuario user, DataTable inscripciones)
        {
            try
            {
                if (user != null && inscripciones != null)
                {
                    string enter = "<br />";

                    StringBuilder scriptingBuilder = new StringBuilder();
                    StringBuilder dataPrint = new StringBuilder();
                    StringBuilder css = new StringBuilder();
                    Dictionary<int, string> months = new Dictionary<int, string>();

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

                    css.Append("<style type=\"text/css\">table {font-family: Consolas, Verdana; font-size: 10pt;border-width: 1px; border-spacing: 0px;	border-style: none;	border-color: black; border-collapse: collapse}");
                    css.Append("table th {border-width: 1px; padding: 5px; border-style: solid; border-color: black}");
                    css.Append("table td {border-width: 1px;	padding: 5px; border-style: solid; border-color: black;}");
                    css.Append("body { font-family: Consolas, Verdana; font-size: 12pt;}</style>");

                    dataPrint.Append("<div><div style=\"text-align: center; float: left; display: inline\">" + String.Format(ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresion"], enter, enter) + "</div>");
                    dataPrint.Append("<div style=\"display:inline; float:right;\">" + ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresionFecha"] + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "</div></div><br /><br /><br /><br /><br /><br />");
                    dataPrint.Append("<div style=\" text-align: center;\">");

                    if (Convert.ToInt32(inscripciones.Rows[0]["IdVuelta"]) == 0)
                        dataPrint.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresionExamenVuelta"], enter));
                    else
                    {
                        if (Convert.ToInt32(inscripciones.Rows[0]["IdVuelta"]) == 1)
                            dataPrint.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion1Vuelta"], enter));
                        else
                            dataPrint.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion2Vuelta"], enter));
                    }

                    dataPrint.Append("</div><br />");

                    if (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 1)
                        dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistorico1Cuatrimestre"], inscripciones.Rows[0]["TurnoInscripcion"]) + "</div><br />");
                    else
                    {
                        if ((Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 2) && (inscripciones.Rows[0]["IdTipoInscripcion"].ToString() == IdTipoInscripcionPromocion))
                            dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistorico2Cuatrimestre"], inscripciones.Rows[0]["TurnoInscripcion"]) + "</div><br />");
                        else
                        {
                            if ((Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 2) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 5) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 7) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 10))
                                dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentDataHistoricoExamen"], months[Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month] + "/" + Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Year.ToString()) + "</div><br />");
                            else
                                dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistoricoCursoVerano"], inscripciones.Rows[0]["TurnoInscripcion"]) + "</div><br />");
                        }
                    }

                    dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData2HistoricoImpresion"], user.ApellidoNombre) + "</div>");
                    dataPrint.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData3HistoricoImpresion"], user.DNI.ToString()) + "</div><br /><br />");
                    dataPrint.Append("<div>" + ConfigurationManager.AppSettings["ContentBodyTitleHistoricoImpresion"] + "</div><br />");

                    dataPrint.Append("<table><tr><th>MATERIA</th><th>COMISION</th><th>PROFESOR</th><th>HORARIO</th></tr>");
                    for (int i = 0; i < inscripciones.Rows.Count; i++)
                    {
                        dataPrint.Append("<tr><td>" + inscripciones.Rows[i]["IdMateria"].ToString() + " " + inscripciones.Rows[i]["MateriaDescripcion"].ToString() + "</td>");
                        dataPrint.Append("<td>" + inscripciones.Rows[i]["CatedraComisionDescripcion"].ToString() + "</td>");
                        dataPrint.Append("<td>" + inscripciones.Rows[i]["Profesor"].ToString() + "</td>");
                        dataPrint.Append("<td>" + inscripciones.Rows[i]["Horario"].ToString() + "</td></tr>");
                    }
                    dataPrint.Append("</table><br /><br /><br />");

                    if ((Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 2) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 5) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 7) || (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 10))
                        dataPrint.Append("<div>" + HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["ContentFooterHistoricoEmailExamen"]).ToString() + "</div>");
                    else
                    {
                        if (Convert.ToDateTime(inscripciones.Rows[0]["TurnoInscripcion"]).Month == 3)
                            dataPrint.Append("<div>" + HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["ContentFooterHistoricoEmailCursoVerano"]).ToString() + "</div>");
                        else
                            dataPrint.Append("<div>" + HttpUtility.HtmlDecode(ConfigurationManager.AppSettings["ContentFooterHistoricoEmail"]).ToString());
                    }                        

                    scriptingBuilder.Append("<script type='text/javascript'>");
                    scriptingBuilder.Append("var win=null;");
                    scriptingBuilder.Append("win = window.open();");
                    scriptingBuilder.Append("self.focus();");
                    scriptingBuilder.Append("win.document.open();");
                    scriptingBuilder.Append("win.document.write('<'+'html'+'><'+'head'+'><'+'title'+'>" + ConfigurationManager.AppSettings["TitlePrintInscripciones"] + "<'+'/'+'title'+'>');");
                    scriptingBuilder.Append("win.document.write('" + css.ToString() + "');");
                    scriptingBuilder.Append("win.document.write('<'+'/'+'head'+'><'+'body'+'>');");
                    scriptingBuilder.Append("win.document.write('" + HttpUtility.JavaScriptStringEncode(dataPrint.ToString()) + "');");
                    scriptingBuilder.Append("win.document.write('<'+'/'+'body'+'><'+'/'+'html'+'>');");
                    scriptingBuilder.Append("win.document.close();");
                    scriptingBuilder.Append("win.print();");
                    scriptingBuilder.Append("win.close();");
                    scriptingBuilder.Append("</script>");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrint", scriptingBuilder.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "PrintHistory", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to remove duplicated inscriptions items
        /// </summary>
        /// <returns></returns>
        private DataTable RemoveDuplicates(DataTable inscripciones)
        {
            try
            {
                List<DataRow> lDataRowBaja = new List<DataRow>();
                List<DataRow> lDataRowAlta = new List<DataRow>();

                for (int i = 0; i < inscripciones.Rows.Count; i++)
                {
                    if (inscripciones.Rows[i]["EstadoInscripcion"].ToString().IndexOf("Baja") != -1)
                        lDataRowBaja.Add(inscripciones.Rows[i]);
                    else
                        lDataRowAlta.Add(inscripciones.Rows[i]);
                }

                return RemoveBaja(lDataRowAlta, lDataRowBaja, inscripciones.Clone());
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "RemoveDuplicates", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to remove modified inscriptions items
        /// </summary>
        /// <returns></returns>
        private DataTable RemoveBaja(List<DataRow> altas, List<DataRow> bajas, DataTable returnTable)
        {
            try
            {
                List<DataRow> finalList = new List<DataRow>();

                //BORRO LAS MATERIAS QUE TENGO EN ESTADO A EN VUELTA 1, Y EN ESTADO M EN VUELTA 2
                for (int i = 0; i < altas.Count; i++)
                {
                    for (int x = 0; x < bajas.Count; x++)
                    {
                        if (altas[i]["CatedraComisionDescripcion"].ToString() == bajas[x]["CatedraComisionDescripcion"].ToString()
                            && altas[i]["IdMateria"].ToString() == bajas[x]["IdMateria"].ToString())
                        {
                            if (Convert.ToInt32(altas[i]["IdVuelta"]) < Convert.ToInt32(bajas[x]["IdVuelta"]))
                            {
                                altas.RemoveAt(i);
                                if (i > 0)
                                    i--;
                            }
                            else
                            {
                                if (bajas[x]["EstadoInscripcion"].ToString().IndexOf("Modificación") == -1)
                                {
                                    altas.RemoveAt(i);
                                    if (i > 0)
                                        i--;
                                }
                                else
                                {
                                    bajas.RemoveAt(x);
                                    if (x > 0)
                                        x--;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < altas.Count; i++)
                    returnTable.ImportRow(altas[i]);

                return returnTable;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "RemoveBaja", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}