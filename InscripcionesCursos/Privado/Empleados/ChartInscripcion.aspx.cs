using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.DTO;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using InscripcionesCursos.BE;
using System.Data;
using System.Text;
using System.IO;

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class ChartInscripcion : System.Web.UI.Page
    {
        #region Constants & Variables

        private const int UserTypeEmployee = 1;
        private const string IdTipoInscripcion = "P";
        private const string ObsoletDate = "01/07/2013";
        private const string ExceptObsoletDate = "01/02/2013";
        private const string ExceptObsoletDateCursoVerano = "01/03/2013";

        string coleccionDniExtract = ConfigurationManager.AppSettings["UserEmployeesExtract"];

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    SetUpPage();
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddTurnos.SelectedIndex != 0)
                {
                    DrawChartInscripciones(Convert.ToDateTime(Convert.ToDateTime(ddTurnos.SelectedValue.Substring(0, ddTurnos.SelectedValue.IndexOf("-")))), ddTurnos.SelectedValue.Substring(ddTurnos.SelectedValue.IndexOf("-") + 2));
                    DrawInscripcionesCarrera(Convert.ToDateTime(Convert.ToDateTime(ddTurnos.SelectedValue.Substring(0, ddTurnos.SelectedValue.IndexOf("-")))), ddTurnos.SelectedValue.Substring(ddTurnos.SelectedValue.IndexOf("-") + 2));
                    DrawInscripcionesComision(Convert.ToDateTime(Convert.ToDateTime(ddTurnos.SelectedValue.Substring(0, ddTurnos.SelectedValue.IndexOf("-")))), ddTurnos.SelectedValue.Substring(ddTurnos.SelectedValue.IndexOf("-") + 2));
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnConsultar_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        protected void gridComision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label titleLabelDepto = (Label)e.Row.FindControl("lblDepartamento");
                    string strvalDep = ((Label)(titleLabelDepto)).Text;
                    string titleDepto = (string)ViewState["Departamento"];
                    if (titleDepto == strvalDep)
                    {
                        titleLabelDepto.Visible = false;
                        titleLabelDepto.Text = string.Empty;
                    }
                    else
                    {
                        titleDepto = strvalDep;
                        ViewState["Departamento"] = titleDepto;
                        titleLabelDepto.Visible = true;
                        titleLabelDepto.Text = titleDepto;
                    }

                    Label titleLabel = (Label)e.Row.FindControl("lblMateria");
                    string strval = ((Label)(titleLabel)).Text;
                    string title = (string)ViewState["Materia"];
                    if (title == strval)
                    {
                        titleLabel.Visible = false;
                        titleLabel.Text = string.Empty;
                    }
                    else
                    {
                        title = strval;
                        ViewState["Materia"] = title;
                        titleLabel.Visible = true;
                        titleLabel.Text = title;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "gridComision_RowDataBound", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        private void SetUpPage()
        {
            try
            {
                ddTurnos.DataSource = CleanObsoletTurnos(ExtractTurnosAndTipoInscripcion(InscripcionDTO.GetAllTurnos(new Inscripcion())));

                ddTurnos.DataBind();
                ddTurnos.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
                lblDescripcion.Text = ConfigurationManager.AppSettings["ContentDescripcionComboTurno"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpPage", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private List<string> CleanObsoletTurnos(List<string> list)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (Convert.ToDateTime(list.ElementAt(i).Substring(0, list.ElementAt(i).IndexOf("-"))) < Convert.ToDateTime(ObsoletDate))
                    {
                        if (Convert.ToDateTime(list.ElementAt(i).Substring(0, list.ElementAt(i).IndexOf("-"))) != Convert.ToDateTime(ExceptObsoletDate) &&
                            Convert.ToDateTime(list.ElementAt(i).Substring(0, list.ElementAt(i).IndexOf("-"))) != Convert.ToDateTime(ExceptObsoletDateCursoVerano))
                        {
                            list.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            if (list.ElementAt(i).Substring(list.ElementAt(i).IndexOf("-") + 2) != IdTipoInscripcion)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "CleanObsoletTurnos", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void DrawChartInscripciones(DateTime turno, string tipoInscripcion)
        {
            try
            {
                List<List<double>> inscripcionesValues = new List<List<double>>(ConsultaDTO.GetAVGInscripcionesByTurno(turno, tipoInscripcion));
                chartInscripcion.Titles["titleChartInscripcion"].Text = ConfigurationManager.AppSettings["ChartInscripcionesTitle"];
                string[] xValues = { ConfigurationManager.AppSettings["ChartInscripcionesLegendWeb"], ConfigurationManager.AppSettings["ChartInscripcionesLegendFacu"] };
                chartInscripcion.Series["serieInscripcion"].Points.DataBindXY(xValues, inscripcionesValues.ElementAt(0));
                chartInscripcion.Series["serieInscripcion"].Points[0].Color = Color.YellowGreen;
                chartInscripcion.Series["serieInscripcion"].Points[1].Color = Color.Tomato;
                chartInscripcion.Series["serieInscripcion"].ChartType = SeriesChartType.Pie;
                chartInscripcion.Series["serieInscripcion"]["PieLabelStyle"] = "Outside";
                chartInscripcion.Series["serieInscripcion"].Label = "#PERCENT{P2}";
                chartInscripcion.Series["serieInscripcion"].LegendText = "#VALX";
                chartInscripcion.ChartAreas["chartAreaInscripcion"].Area3DStyle.Enable3D = true;
                chartInscripcion.Legends["legendInscripcion"].Enabled = true;

                SetLegendsChartInscripcion(inscripcionesValues.ElementAt(0), inscripcionesValues.ElementAt(1));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DrawChartInscripciones", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void DrawInscripcionesCarrera(DateTime turno, string tipoInscripcion)
        {
            try
            {
                gridCarrera.DataSource = ConsultaDTO.GetAVGInscripcionesByCarrera(turno, tipoInscripcion);
                gridCarrera.DataBind();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DrawInscripcionesCarrera", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void DrawInscripcionesComision(DateTime turno, string tipoInscripcion)
        {
            try
            {
                gridComision.DataSource = ConsultaDTO.GetInscripcionByComision(turno, tipoInscripcion);
                gridComision.DataBind();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DrawInscripcionesComision", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

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

        private void SetLegendsChartInscripcion(List<double> listAVG, List<double> listTotal)
        {
            try
            {
                lblTotalInscriptos.Text = String.Format(ConfigurationManager.AppSettings["TotalInscriptos"], listTotal.ElementAt(2).ToString());
                lblTotalInscriptosWeb.Text = String.Format(ConfigurationManager.AppSettings["TotalInscriptosWeb"], listTotal.ElementAt(0).ToString(), listAVG.ElementAt(0).ToString());
                lblTotalInscriptosFacu.Text = String.Format(ConfigurationManager.AppSettings["TotalInscriptosFacu"], listTotal.ElementAt(1).ToString(), listAVG.ElementAt(1).ToString());
                lblTotalNoInscriptos.Text = String.Format(ConfigurationManager.AppSettings["TotalNoInscriptos"], listTotal.ElementAt(3).ToString());
                lblMuestreo.Text = ConfigurationManager.AppSettings["MuestreoEstadisticas"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetLegendsChartInscripcion", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}