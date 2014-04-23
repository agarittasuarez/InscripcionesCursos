using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class Consultas : System.Web.UI.Page
    {
        #region Constants & Variables

        private const int UserTypeEmployee = 1;
        private const string ComboTextFieldTurno = "TurnoInscripcion";
        private const string ComboValueFieldTurno = "TurnoInscripcion";

        string coleccionDniStatistics = ConfigurationManager.AppSettings["UserEmployeesStatistics"];

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

                if (coleccionDniStatistics.IndexOf(((Usuario)Session["userEmployee"]).DNI.ToString()) == -1)
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployee"]);

                if (!IsPostBack)
                {
                    SetUpPage();
                    DrawChartPadron();
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

        private void DrawChartPadron()
        {
            try
            {
                List<List<double>> cuentasValues = new List<List<double>>(ConsultaDTO.GetAVGActivos());
                chartConsulta.Titles["titleChartPadron"].Text = ConfigurationManager.AppSettings["ChartPadronTitle"];
                string[] xValues = { ConfigurationManager.AppSettings["ChartPadronLegendClave"], ConfigurationManager.AppSettings["ChartPadronLegendCuenta"], ConfigurationManager.AppSettings["ChartPadronLegendSinTamitar"] };
                chartConsulta.Series["seriePadron"].Points.DataBindXY(xValues, cuentasValues.ElementAt(0));
                chartConsulta.Series["seriePadron"].Points[0].Color = Color.Yellow;
                chartConsulta.Series["seriePadron"].Points[1].Color = Color.CadetBlue;
                chartConsulta.Series["seriePadron"].Points[2].Color = Color.DarkSalmon;
                chartConsulta.Series["seriePadron"].ChartType = SeriesChartType.Pie;
                chartConsulta.Series["seriePadron"]["PieLabelStyle"] = "Outside";
                chartConsulta.Series["seriePadron"].Label = "#PERCENT{P2}";
                chartConsulta.Series["seriePadron"].LegendText = "#VALX";
                chartConsulta.ChartAreas["chartAreaPadron"].Area3DStyle.Enable3D = true;
                chartConsulta.Legends["legendPadron"].Enabled = true;

                SetLegendsChartPadron(cuentasValues.ElementAt(0), cuentasValues.ElementAt(1));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DrawChartPadron", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void SetLegendsChartPadron(List<double> listAVG, List<double> listTotal)
        {
            try
            {
                lblTotalAlumnos.Text = String.Format(ConfigurationManager.AppSettings["TotalAlumnos"], listTotal.ElementAt(3).ToString());
                lblCuentasTramitadas.Text = String.Format(ConfigurationManager.AppSettings["TotalCuentasTramitadas"], listTotal.ElementAt(0).ToString(), listAVG.ElementAt(0).ToString());
                lblCuentasActivadas.Text = String.Format(ConfigurationManager.AppSettings["TotalCuentasActivadas"], listTotal.ElementAt(1).ToString(), listAVG.ElementAt(1).ToString());
                lblSinTramitar.Text = String.Format(ConfigurationManager.AppSettings["TotalSinTramitar"], listTotal.ElementAt(2).ToString(), listAVG.ElementAt(2).ToString());
                lblMuestreo.Text = ConfigurationManager.AppSettings["MuestreoEstadisticas"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetLegendsChartPadron", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void SetUpPage()
        {
            try
            {
                lblTitulo.Text = ConfigurationManager.AppSettings["ContentMainTitleConsultas"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpPage", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

    }
}