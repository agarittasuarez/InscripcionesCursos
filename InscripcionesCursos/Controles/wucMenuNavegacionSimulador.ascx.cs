using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.BE;
using System.IO;
using System.Threading;

namespace InscripcionesCursos
{
    public partial class wucMenuNavegacionSimulador : System.Web.UI.UserControl
    {
        #region Constants

        private const string rootPath = "Privado/Alumnos/";

        #endregion

        #region Objects

        Usuario loggedUser;

        #endregion

        #region Properties

        public EventHandler BtnBackClick;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            SetUpTexts();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to redirect Constancia Alumno Regular
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConstancia_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentConstanciaAlumnoRegular"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnConstancia_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect Plan de Estudio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlanes_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentPlanEstudio"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnPlanes_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect Inscripciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInscripciones_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentPreInscripcion"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnInscripciones_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect Historial Inscripciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHistorialInscrip_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentPreHistorial"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnHistorialInscrip_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect Modificacion Datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentModificacionDatos"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnActualizarDatos_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to redirect Materias Rendidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRendidas_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(@"~/" + rootPath + ConfigurationManager.AppSettings["UrlStudentRendidas"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnRendidas_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Evento to Back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("user");
                Session["isSimulador"] = false;
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeSimulador"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnBack_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to set up button texts
        /// </summary>
        private void SetUpTexts()
        {
            try
            {
                btnActualizarDatos.Text = ConfigurationManager.AppSettings["BotonActualizarDatos"];
                btnConstancia.Text = ConfigurationManager.AppSettings["BotonConstancia"];
                btnHistorialInscrip.Text = ConfigurationManager.AppSettings["BotonHistorico"];
                btnInscripciones.Text = ConfigurationManager.AppSettings["BotonInscripciones"];
                btnPlanes.Text = ConfigurationManager.AppSettings["BotonPlanes"];
                btnRendidas.Text = ConfigurationManager.AppSettings["BotonRendidas"];
                btnBack.Text = ConfigurationManager.AppSettings["BotonVolver"];
                btnOfertas.Text = ConfigurationManager.AppSettings["BotonOfertas"];
                btnTalleres.Text = ConfigurationManager.AppSettings["BotonTalleres"];

                if (Session["user"] != null)
                {
                    loggedUser = (Usuario)Session["user"];
                    lblUser.Text = loggedUser.ApellidoNombre;
                    lblEstado.Text = ConfigurationManager.AppSettings["LabelEstado"] + loggedUser.Estado;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpTexts", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        public void LabelUsuario(Usuario value)
        {
            lblUser.Text = value.ApellidoNombre;
            lblEstado.Text = value.Estado;
        }

        #endregion
    }
}