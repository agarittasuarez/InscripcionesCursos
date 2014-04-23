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
            SetUp();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to redirect to GeneracionClave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClaves_Click(object sender, EventArgs e)
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
                log.WriteLog(ex.Message, "btnClaves_Click", Path.GetFileName(Request.PhysicalPath));
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
        protected void btnCambioTextos_Click(object sender, EventArgs e)
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
                log.WriteLog(ex.Message, "btnCambioTextos_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to Consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConsultas_Click(object sender, EventArgs e)
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
                log.WriteLog(ex.Message, "btnConsultas_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect to Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProceso_Click(object sender, EventArgs e)
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
                log.WriteLog(ex.Message, "btnProceso_Click", Path.GetFileName(Request.PhysicalPath));
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
                        btnClaves.Text = ConfigurationManager.AppSettings["BotonGenerarClave"];
                        btnInscription.Text = ConfigurationManager.AppSettings["BotonInscribirAlumno"];
                        btnLogout.Text = ConfigurationManager.AppSettings["BotonLogout"];

                        if (coleccionDniResend.IndexOf(loggedUser.DNI.ToString()) != -1)
                        {
                            btnResend.Text = ConfigurationManager.AppSettings["BotonResend"];
                            btnEmailChange.Text = ConfigurationManager.AppSettings["BotonEmailChange"];
                            btnProceso.Text = ConfigurationManager.AppSettings["BotonProceso"];
                            btnResend.Visible = true;
                            btnEmailChange.Visible = true;
                            btnProceso.Visible = true;
                        }

                        if (coleccionDniStatistics.IndexOf(loggedUser.DNI.ToString()) != -1)
                        {
                            btnConsultas.Text = ConfigurationManager.AppSettings["BotonConsultas"];
                            btnCambioTextos.Text = ConfigurationManager.AppSettings["BotonCambioTextos"];
                            btnConsultas.Visible = true;
                            btnCambioTextos.Visible = true;                            
                        }

                        divLoginTools.Visible = true;
                    }
                    else
                    {
                        menuAlumnos.Visible = true;
                        divContent.Attributes.Add("class", "contenidoCentral");
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

        #endregion
    }
}
