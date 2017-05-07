using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class ConsultaInscripcionAgrupada : System.Web.UI.Page
    {
        #region Constants & Variables

        private const int UserTypeEmployee = 1;
        private const int MinUserLevel = 2;
        private const string TipoExamenLibre = "Exámenes Libres";
        private const string TipoPromocion = "Promoción";
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
                if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                if (!Utils.CheckAccountStatus(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeePasswordChange"]);

                if (!Utils.CheckUserProfileLevel(Session["userEmployee"], MinUserLevel))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"]);

                if (!IsPostBack)
                {
                    Session.Remove("user");
                    SetUpPage();
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddTurnos.SelectedIndex != 0 && ddVueltas.SelectedIndex != 0 && ddAgrupaciones.SelectedIndex != 0)
                {
                    var idTipoInscripcion = GetIdTipoInscripcion(ddTurnos.SelectedValue.Substring(ddTurnos.SelectedValue.IndexOf("-") + 2));
                    DrawBarChartInscripciones(Convert.ToDateTime(Convert.ToDateTime(ddTurnos.SelectedValue.Substring(0, ddTurnos.SelectedValue.IndexOf("-")))), idTipoInscripcion, Convert.ToInt32(ddVueltas.SelectedValue), Convert.ToInt32(ddAgrupaciones.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnConsultar_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set up page texts and data
        /// </summary>
        private void SetUpPage()
        {
            try
            {
                ddTurnos.DataSource = ExtractTurnosAndTipoInscripcion(InscripcionDTO.GetAllTurnos(new Inscripcion())).GroupBy(i => i).Select(group => group.Key).ToList();
                ddAgrupaciones.DataSource = GetAgrupaciones();
                ddVueltas.DataSource = GetVueltas();

                ddAgrupaciones.DataBind();
                ddTurnos.DataBind();
                ddVueltas.DataBind();
                ddTurnos.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
                lblSeleccione.Text = ConfigurationManager.AppSettings["LabelConsultaInscripcionesAgrupadasSeleccione"];
                lblTurno.Text = ConfigurationManager.AppSettings["LabelConsultaInscripcionesAgrupadasTurno"];
                lblVuelta.Text = ConfigurationManager.AppSettings["LabelConsultaInscripcionesAgrupadasVuelta"];
                lblAgrupacion.Text = ConfigurationManager.AppSettings["LabelConsultaInscripcionesAgrupadasAgrupacion"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpPage", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Get the id inscription type
        /// </summary>
        /// <param name="tipo">TurnoInscripcion description</param>
        /// <returns>IdInscripcion</returns>
        private string GetIdTipoInscripcion(string tipo)
        {
            switch (tipo)
            {
                case TipoExamenLibre:
                    return "E";
                default:
                    return "P";
            }
        }

        /// <summary>
        /// Get list of minutes
        /// </summary>
        /// <returns></returns>
        private List<String> GetAgrupaciones()
        {
            var minutos = new List<String>();
            
            minutos.Add(ConfigurationManager.AppSettings["ContentComboAgrupacionDefault"]);
            minutos.Add("05");
            minutos.Add("10");
            minutos.Add("15");
            minutos.Add("20");
            minutos.Add("30");
            minutos.Add("60");
            
            return minutos;
        }

        /// <summary>
        /// Get list of vueltas
        /// </summary>
        /// <returns></returns>
        private List<string> GetVueltas()
        {
            var vueltas = new List<string>();

            vueltas.Add(ConfigurationManager.AppSettings["ContentComboVueltaDefault"]);
            vueltas.Add("0");
            vueltas.Add("1");
            vueltas.Add("2");
            vueltas.Add("3");

            return vueltas;
        }

        /// <summary>
        /// Clean oldest and obsolet inscriptions shift
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Draw a bar chart with inscriptions, grouped by specific range
        /// </summary>
        /// <param name="turno"></param>
        /// <param name="tipoInscripcion"></param>
        /// <param name="vuelta"></param>
        /// <param name="agrupacion"></param>
        private void DrawBarChartInscripciones(DateTime turno, string tipoInscripcion, int vuelta, int agrupacion)
        {
            try
            {
                DataTable dt = ConsultaDTO.GetInscripcionAgrupada(turno, tipoInscripcion, vuelta, agrupacion);

                //storing total rows count to loop on each Record  
                string[] XPointMember = new string[dt.Rows.Count];
                int[] YPointMember = new int[dt.Rows.Count];

                for (int count = 0; count < dt.Rows.Count; count++)
                {
                    //storing Values for X axis  
                    XPointMember[count] = dt.Rows[count][1].ToString();
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToInt32(dt.Rows[count][0]);


                }
                //binding chart control  
                barChart.Series[0].Points.DataBindXY(XPointMember, YPointMember);

                //Setting width of line  
                barChart.Series[0].BorderWidth = 10;
                //setting Chart type   
                barChart.Series[0].ChartType = SeriesChartType.Column;
                barChart.Series[0].IsValueShownAsLabel = true;
                barChart.Series[0].LabelForeColor = Color.White;

                barChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //barChart.ChartAreas["ChartArea1"].AxisX.Enabled = AxisEnabled.False;
                barChart.ChartAreas["ChartArea1"].AxisY.Interval = 50;

                //Hide or show chart back GridLines  
                barChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                barChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

                barChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = Color.White;
                barChart.ChartAreas["ChartArea1"].AxisY.LabelStyle.ForeColor = Color.White;

                barChart.Visible = true;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DrawBarChartInscripciones", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }
        
        /// <summary>
        /// Get all inscription shifts and inscription types
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
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

        #endregion
    }
}