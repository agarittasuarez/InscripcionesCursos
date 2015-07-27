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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        const int UserTypeStudent = 2;
        string coleccionDniResend = ConfigurationManager.AppSettings["UserEmployeesResend"];
        string coleccionDniExtrac = ConfigurationManager.AppSettings["UserEmployeesExtract"];
        string coleccionDniStatistics = ConfigurationManager.AppSettings["UserEmployeesStatistics"];

        #endregion

        #region Objects

        Usuario loggedUser = new Usuario();

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                SetUp();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to redirect to GeneracionClave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnPassword_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Evento to redirect to ReenvioEmail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResend_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeResendEmail"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnResend_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Evento to redirect Change Email tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEmailChange_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeChangeEmail"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnEmailChange_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to Inscripcion Cursos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInscription_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeInscription"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnInscription_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to CambioTextos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTextsChange_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeChangeTexts"]); 
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnTextsChange_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to Consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQueries_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeConsultas"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnQueries_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeProceso"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnProcess_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to SimuladorAlumno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInterface_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeSimulador"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnInterface_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to close user session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Session.RemoveAll();
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnLogout_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        private void SetUp()
        {
            try
            {
                if (Session["user"] != null && Session["userEmployee"] == null)
                    loggedUser = (Usuario)Session["user"];
                else
                    loggedUser = (Usuario)Session["userEmployee"];

                if (loggedUser != null)
                {
                    if (loggedUser.IdCargo == UserTypeEmployee)
                    {
                        lblUser.Text = String.Format(ConfigurationManager.AppSettings["ContentLoginControl"], Utils.TruncateAtWord(loggedUser.ApellidoNombre, 12));
                        btnManagement.Text = ConfigurationManager.AppSettings["BotonGestionCuentas"];
                        btnTools.Text = ConfigurationManager.AppSettings["BotonTools"];
                        btnPassword.Text = ConfigurationManager.AppSettings["BotonGenerarClave"];
                        btnInscription.Text = ConfigurationManager.AppSettings["BotonInscribirAlumno"];
                        btnLogout.Text = ConfigurationManager.AppSettings["BotonLogout"];
                        btnResend.Text = ConfigurationManager.AppSettings["BotonResend"];
                        btnEmailChange.Text = ConfigurationManager.AppSettings["BotonEmailChange"];
                        btnProcess.Text = ConfigurationManager.AppSettings["BotonProceso"];
                        btnQueries.Text = ConfigurationManager.AppSettings["BotonConsultas"];
                        btnTextsChange.Text = ConfigurationManager.AppSettings["BotonCambioTextos"];
                        btnInterface.Text = ConfigurationManager.AppSettings["BotonInterfazAlumnos"];
                        liPassword.Visible = liInscription.Visible  = true;

                        if (coleccionDniResend.IndexOf(loggedUser.DNI.ToString()) != -1)
                            liTools.Visible = liResend.Visible = liEmailChange.Visible = liProcess.Visible = true;

                        if (coleccionDniStatistics.IndexOf(loggedUser.DNI.ToString()) != -1)
                            liTools.Visible = liQueries.Visible = liTextsChange.Visible = liInterface.Visible = true;
                        
                        divLoginTools.Visible = true;
                        if (Session["user"] != null && Session["userEmployee"] != null)
                            menuSimulador.Visible = true;
                    }
                    else
                    {
                        menuAlumnos.Visible = true;
                        divContent.Attributes.Add("class", "contenidoCentral");

                        if (!loggedUser.LimitacionRelevada)
                            EnableButtons(false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUp", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void EnableButtons(bool enable)
        {
            ((Button)(menuAlumnos.FindControl("btnConstancia"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnPlanes"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnRendidas"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnInscripciones"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnHistorialInscrip"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnActualizarDatos"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnOfertas"))).Enabled = enable;
            ((Button)(menuAlumnos.FindControl("btnTalleres"))).Enabled = enable;
        }

        #endregion
    }
}
