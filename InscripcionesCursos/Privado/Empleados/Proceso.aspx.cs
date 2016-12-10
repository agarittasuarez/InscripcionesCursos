using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Data.SqlTypes;
using System.Text;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace InscripcionesCursos
{
    public partial class Proceso : System.Web.UI.Page
    {
        #region Constants & Variables

        private string fileNameInscripcion = ConfigurationManager.AppSettings["FileNameInscripcion"];
        private string fileNamePadron = String.Format(ConfigurationManager.AppSettings["FileNamePadron"], DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day);
        private const int UserTypeEmployee = 1;
        string coleccionDNIResend = ConfigurationManager.AppSettings["UserEmployeesResend"];
        const string ClaseInicioInscripcion = "FormatoInicioInscripcion";
        const string ClaseCatedra = "FormatoCatedra";
        const string ClaseInscripcion = "FormatoInscripcion";
        const string ClaseCalificacion = "FormatoCalificacion";
        const string ClasePadron = "FormatoPadron";
        const string PadronAalumnos = "PA";
        const string PadronCalificaciones = "PC";
        const string Todos = "*";

        #endregion

        #region Objects

        ServicioImportacion importacion;
        List<TipoInscripcion> listTipoInscripcion;
        List<string> listTurnos;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckAccountStatus(Session["userEmployee"], UserTypeEmployee))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeePasswordChange"]);

                    if (coleccionDNIResend.IndexOf(((Usuario)Session["userEmployee"]).DNI.ToString()) == -1)
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployee"]);

                    SetUpPage();
                    ClearContents(asyncFile as Control);
                    asyncFile.Dispose();
                    Session.Remove("user");
                }
                FillGrid(1);
                FillGrid(2);
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

        /// <summary>
        /// Event to enable TipoInscripcion, TurnoInscripcion and VueltaInscripcion combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboTipoImportacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTipoImportacion.SelectedValue == "PA" || cboTipoImportacion.SelectedValue == "PC")
                {
                    cboTipoInscripcion.Enabled = false;
                    cboTurnoInscripcion.Enabled = false;
                    cboVueltaInscripcion.Enabled = false;
                }
                else
                {
                    cboTipoInscripcion.Enabled = true;
                    cboTurnoInscripcion.Enabled = true;
                    cboVueltaInscripcion.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboTipoImportacion_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to enable TextBox txtFechaProgramada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkProgramar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkProgramar.Checked)
                    txtFechaProgramada.Enabled = true;
                else
                    txtFechaProgramada.Enabled = false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "chkProgramar_CheckedChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to bind TurnoInscripcion combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboTipoInscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTipoInscripcion.SelectedValue == "E")
                {
                    cboTurnoInscripcion.DataSource = ServicioImportacionDTO.GetServicioTurnoInscripcion().Where(turno => turno == ("2/" + DateTime.Now.Year.ToString()) ||
                        turno == ("5/" + DateTime.Now.Year.ToString()) || turno == ("7/" + DateTime.Now.Year.ToString()) ||
                        turno == ("10/" + DateTime.Now.Year.ToString()) || turno == ("12/" + DateTime.Now.Year.ToString())).ToList();
                    cboTurnoInscripcion.DataBind();

                    cboVueltaInscripcion.DataSource = ServicioImportacionDTO.GetServicioVueltaInscripcion().Where(vuelta => vuelta.IdVuelta == 0).ToList();
                    cboVueltaInscripcion.DataBind();
                }
                else
                {
                    cboTurnoInscripcion.DataSource = ServicioImportacionDTO.GetServicioTurnoInscripcion().Where(turno => turno == ("1/" + DateTime.Now.Year.ToString()) ||
                        turno == ("2/" + DateTime.Now.Year.ToString()) || turno == ("3/" + DateTime.Now.Year.ToString())).ToList();
                    cboTurnoInscripcion.DataBind();

                    cboVueltaInscripcion.DataSource = ServicioImportacionDTO.GetServicioVueltaInscripcion();
                    cboVueltaInscripcion.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboTipoInscripcion_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to row data bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridActiveProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    PlaceHolder pHolder = e.Row.FindControl("phButton") as PlaceHolder;
                    Button btnAction = new Button();

                    if (((ServicioImportacion)e.Row.DataItem).ProcesoActivo)
                    {
                        btnAction.CssClass = "icoPlusAction";
                        btnAction.Attributes.Add("title", ConfigurationManager.AppSettings["LabelAltActiveProcess"]);
                    }
                    else
                    {
                        if (!((ServicioImportacion)e.Row.DataItem).ProcesoActivo && ((ServicioImportacion)e.Row.DataItem).LogError != null && ((ServicioImportacion)e.Row.DataItem).LogError.Trim().Length >0)
                        {
                            btnAction.CssClass = "icoMinusAction";
                            btnAction.Attributes.Add("title", ConfigurationManager.AppSettings["LabelAltErrorProcess"]);
                            btnAction.Enabled = false;
                        }
                        else
                        {
                            btnAction.CssClass = "icoCheck";
                            btnAction.Attributes.Add("title", ConfigurationManager.AppSettings["LabelAltFinishProcess"]);
                            btnAction.Enabled = false;
                        }
                    }

                    btnAction.ID = "btnAction";
                    btnAction.CausesValidation = false;
                    btnAction.CommandArgument = e.Row.Cells[0].Text;
                    btnAction.Click += new EventHandler(btnAction_Click);
                    pHolder.Controls.Add(btnAction);

                    //Set Fromat TurnoInscripcion Field
                    if (((ServicioImportacion)e.Row.DataItem).Descripcion == ConfigurationManager.AppSettings["LabelPadronCalificaciones"] ||
                        ((ServicioImportacion)e.Row.DataItem).Descripcion == ConfigurationManager.AppSettings["LabelPadronAlumnos"])
                    {
                        e.Row.Cells[6].Text = ConfigurationManager.AppSettings["LabelNoAplica"];
                        e.Row.Cells[7].Text = ConfigurationManager.AppSettings["LabelNoAplica"];
                        e.Row.Cells[8].Text = ConfigurationManager.AppSettings["LabelNoAplica"];
                    }
                    else
                        e.Row.Cells[7].Text = (Convert.ToDateTime(e.Row.Cells[7].Text)).Month.ToString() + "/" + (Convert.ToDateTime(e.Row.Cells[7].Text)).Year.ToString();
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "gridActiveProcess_RowDataBound", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to deactivate process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAction_Click(object sender, EventArgs e)
        {
            DeactivateProcess(Convert.ToInt32(((Button)sender).CommandArgument));
        }

        /// <summary>
        /// Event to change index paxe of gridActiveProcess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridActiveProcess_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridActiveProcess.PageIndex = e.NewPageIndex;
                FillGrid(1);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "gridActiveProcess_PageIndexChanging", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to save process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e) 
        {
            Save();
            FillGrid(1);
        }

        /// <summary>
        /// Event to enable TipoInscripcionError combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboTipoImportacionError_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTipoInscripcionError.Enabled)
                {
                    cboTipoInscripcionError.Enabled = false;
                    cboTurnoInscripcionError.Enabled = false;
                    cboVueltaInscripcionError.Enabled = false;
                }

                if (cboTipoImportacionError.SelectedIndex != 0)
                {
                    if (cboTipoImportacionError.SelectedValue != PadronAalumnos && cboTipoImportacionError.SelectedValue != PadronCalificaciones
                        && cboTipoImportacionError.SelectedValue != Todos)
                    {
                        importacion = new ServicioImportacion();
                        importacion.IdTipoImportacion = cboTipoImportacionError.SelectedValue;
                        cboTipoInscripcionError.DataSource = ServicioImportacionDTO.GetServicioTipoInscripcionError(importacion);

                        if (cboTipoInscripcionError.DataSource != null)
                        {
                            cboTipoInscripcionError.Enabled = true;
                            cboTipoInscripcionError.DataBind();
                            cboTipoInscripcionError.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboInscripcionDefault"], "0"));

                            cboTipoInscripcionError.SelectedIndex = 0;
                            if (cboTurnoInscripcionError.Items.Count > 0)
                                cboTurnoInscripcionError.SelectedIndex = 0;
                            if (cboVueltaInscripcionError.Items.Count > 0)
                                cboVueltaInscripcionError.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        cboTipoInscripcionError.Enabled = false;
                        cboTurnoInscripcionError.Enabled = false;
                        cboVueltaInscripcionError.Enabled = false;

                        importacion = new ServicioImportacion();
                        importacion.IdTipoImportacion = cboTipoImportacionError.SelectedValue;
                        importacion.TurnoInscripcion = (DateTime)SqlDateTime.Null;

                        gridLogProcess.DataSource = ServicioImportacionDTO.GetErrorProcess(importacion);
                        gridLogProcess.DataBind();
                        //ShowGridLogProcess(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboTipoImportacionError_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to enable TurnoInscripcion combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboTipoInscripcionError_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTurnoInscripcionError.Enabled)
                {
                    cboTurnoInscripcionError.Enabled = false;
                    cboVueltaInscripcionError.Enabled = false;
                }

                if (cboTipoInscripcionError.SelectedIndex != 0)
                {
                    importacion = new ServicioImportacion();
                    importacion.IdTipoImportacion = cboTipoImportacionError.SelectedValue;
                    importacion.IdTipoInscripcion = cboTipoInscripcionError.SelectedValue;
                    importacion.TurnoInscripcion = (DateTime)SqlDateTime.Null;
                    cboTurnoInscripcionError.DataSource = ServicioImportacionDTO.GetServicioTurnoInscripcionError(importacion);

                    if (cboTurnoInscripcionError.DataSource != null)
                    {
                        cboTurnoInscripcionError.Enabled = true;
                        cboTurnoInscripcionError.DataBind();
                        cboTurnoInscripcionError.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));

                        cboTurnoInscripcionError.SelectedIndex = 0;
                        if (cboVueltaInscripcionError.Items.Count > 0)
                            cboVueltaInscripcionError.SelectedIndex = 0;
                        //ShowGridLogProcess(false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboTipoInscripcionError_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to enable TurnoInscripcion combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboTurnoInscripcionError_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboVueltaInscripcionError.Enabled)
                {
                    cboVueltaInscripcionError.Enabled = false;
                }

                if (cboTurnoInscripcionError.SelectedIndex != 0)
                {
                    importacion = new ServicioImportacion();
                    importacion.IdTipoImportacion = cboTipoImportacionError.SelectedValue;
                    importacion.IdTipoInscripcion = cboTipoInscripcionError.SelectedValue;
                    importacion.TurnoInscripcion = Convert.ToDateTime(cboTurnoInscripcionError.SelectedValue);
                    cboVueltaInscripcionError.DataSource = ServicioImportacionDTO.GetServicioVueltaInscripcionError(importacion);

                    if (cboVueltaInscripcionError.DataSource != null)
                    {
                        cboVueltaInscripcionError.Enabled = true;
                        cboVueltaInscripcionError.DataBind();
                        cboVueltaInscripcionError.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboVueltaDefault"], "0"));

                        cboVueltaInscripcionError.SelectedIndex = 0;
                        //ShowGridLogProcess(false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboTurnoInscripcionError_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to execute query by filtered combos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboVueltaInscripcionError_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboVueltaInscripcionError.SelectedIndex != 0)
                {
                    importacion = new ServicioImportacion();
                    importacion.IdTipoImportacion = cboTipoImportacionError.SelectedValue;
                    importacion.IdTipoInscripcion = cboTipoInscripcionError.SelectedValue;
                    importacion.TurnoInscripcion = Convert.ToDateTime(cboTurnoInscripcionError.SelectedValue);
                    importacion.IdVuelta = Convert.ToInt32(cboVueltaInscripcionError.SelectedValue);
                    gridLogProcess.DataSource = ServicioImportacionDTO.GetErrorProcess(importacion);
                    gridLogProcess.DataBind();
                    //ShowGridLogProcess(true);
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "cboVueltaInscripcionError_SelectedIndexChanged", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to change index paxe of gridLogProcess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridLogProcess_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLogProcess.PageIndex = e.NewPageIndex;
            FillGrid(2);
        }

        /// <summary>
        /// Event to row data bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridLogProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    PlaceHolder pHolder = e.Row.FindControl("phErrorDetail") as PlaceHolder;
                    Button btnDetail = new Button();

                    if (e.Row.Cells[1].Text.Length > 130)
                    {
                        btnDetail.CommandArgument = e.Row.Cells[1].Text;
                        e.Row.Cells[1].Text = Utils.TruncateAtWord(e.Row.Cells[1].Text, 120);
                        btnDetail.Click += new EventHandler(btnDetail_Click);
                    }
                    else
                        btnDetail.Enabled = false;

                    btnDetail.CssClass = "icoDetailAction";
                    btnDetail.ID = "btnDetail";
                    btnDetail.CausesValidation = false;
                    pHolder.Controls.Add(btnDetail);
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "gridLogProcess_RowDataBound", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to view detail error process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessagePopUp.Text = ((Button)sender).CommandArgument;
                mpeMessage.Show();
                FillGrid(2);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnDetail_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to accept modal popup message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            mpeMessage.Hide();
        }

        /// <summary>
        /// Event to upload file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void asyncFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            UploadAsyncFile(sender, e);
        }

        /// <summary>
        /// Event to download Padron of Alumnos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExtraerAlumnos_Click(object sender, EventArgs e)
        {
            DownloadExcelFile(fileNamePadron);
        }

        /// <summary>
        /// Event to download Inscripciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExtraerInscripciones_Click(object sender, EventArgs e)
        {
            if (cboInscripciones.SelectedIndex != 0)
                DownloadTxtFiles(1, fileNameInscripcion);
            else
            {
                lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageExtractSelectTurno"];
                mpeMessage.Show();
            }
        }

        /// <summary>
        /// Event to fill the Vuelta Turno drop down list info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboInscripciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInscripciones.SelectedIndex != 0)
            {
                GetVueltasTurno(cboInscripciones.SelectedValue.Split('-')[0]);
                cboInscripcionesVuelta.Enabled = true;
            }
            else
                cboInscripcionesVuelta.Enabled = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to set up combos and texts
        /// </summary>
        private void SetUpPage()
        {
            try
            {
                //Texts
                lblTipoImportacion.Text = ConfigurationManager.AppSettings["LabelTipoImportacion"];
                lblArchivo.Text = ConfigurationManager.AppSettings["LabelArchivo"];
                lblTipoInscripcion.Text = ConfigurationManager.AppSettings["LabelTipoInscripcion"];
                lblTurnoInscripcion.Text = ConfigurationManager.AppSettings["ContentDescripcionComboTurno"];
                lblVueltaInscripcion.Text = ConfigurationManager.AppSettings["LabelVueltaTurno"];
                chkProgramar.Text = ConfigurationManager.AppSettings["LabelCheckProgramarImportacion"];
                lblTipoImportacionError.Text = ConfigurationManager.AppSettings["LabelTipoImportacion"];
                lblTipoInscripcionError.Text = ConfigurationManager.AppSettings["LabelTipoInscripcion"];
                lblTurnoInscripcionError.Text = ConfigurationManager.AppSettings["ContentDescripcionComboTurno"];
                lblIdVueltaError.Text = ConfigurationManager.AppSettings["LabelVueltaTurno"];

                //Combos Iniciar Procesos
                cboTipoImportacion.DataSource = ServicioImportacionDTO.GetServicioTipoImportacion();
                cboTipoImportacion.DataBind();

                listTipoInscripcion = new List<TipoInscripcion>();
                listTipoInscripcion = ServicioImportacionDTO.GetServicioTipoInscripcion();
                listTipoInscripcion = listTipoInscripcion.Where(tipoIns => tipoIns.IdTipoInscripcion == "E" || tipoIns.IdTipoInscripcion == "P").ToList();
                cboTipoInscripcion.DataSource = listTipoInscripcion;
                cboTipoInscripcion.DataBind();

                cboVueltaInscripcion.DataSource = ServicioImportacionDTO.GetServicioVueltaInscripcion();
                cboVueltaInscripcion.DataBind();

                cboTurnoInscripcion.DataSource = ServicioImportacionDTO.GetServicioTurnoInscripcion();
                cboTurnoInscripcion.DataBind();
                this.cboTipoInscripcion_SelectedIndexChanged(new object(), new EventArgs());

                //Combos Log Errores
                cboTipoImportacionError.DataSource = ServicioImportacionDTO.GetServicioTipoImportacionError();
                cboTipoImportacionError.DataBind();
                cboTipoImportacionError.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboImportacionDefault"], "0"));
                cboTipoImportacionError.Items.Insert(cboTipoImportacionError.Items.Count, new ListItem(ConfigurationManager.AppSettings["ContentComboImportacionAll"], "*"));

                txtFechaProgramada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                //Combos Export Inscripciones
                listTurnos = new List<string>();
                //listTurnos = ExtractTurnos(InscripcionDTO.GetAllTurnos(new Inscripcion()));
                listTurnos = ExtractTurnosAndTipoInscripcion(InscripcionDTO.GetAllTurnos(new Inscripcion())).GroupBy(i => i).Select(group => group.Key).ToList();

                cboInscripciones.DataSource = listTurnos;
                cboInscripciones.DataBind();
                cboInscripciones.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
                //cboInscripciones.Items.Insert(1, new ListItem(ConfigurationManager.AppSettings["ContentComboExtractAll"], "*"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpPage", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to save the process instance
        /// </summary>
        private void Save()
        {
            try
            {
                if (asyncFile.HasFile)
                {
                    if (asyncFile.ContentType.ToLower() == ("text/plain"))
                    {
                        importacion = new ServicioImportacion();

                        importacion.ArchivoImportacion = asyncFile.FileName;
                        importacion.FechaAlta = DateTime.Now;
                        importacion.ProcesoActivo = true;
                        importacion.IdTipoImportacion = cboTipoImportacion.SelectedValue;
                        importacion.FechaImportacion = DateTime.Now;
                        importacion.UsuarioImportador = ((Usuario)Session["userEmployee"]).DNI;
                        importacion.FechaProgramadaImportacion = Convert.ToDateTime(DateTime.Parse(txtFechaProgramada.Text).ToString("yyyy-MM-dd HH:mm:ss"));
                        importacion.ClaseFormato = SetClaseFormato(importacion.IdTipoImportacion);

                        if (importacion.IdTipoImportacion != PadronAalumnos && importacion.IdTipoImportacion != PadronCalificaciones)
                        {
                            importacion.IdTipoInscripcion = cboTipoInscripcion.SelectedValue;
                            importacion.TurnoInscripcion = Convert.ToDateTime(cboTurnoInscripcion.SelectedValue);
                            importacion.IdVuelta = Convert.ToInt32(cboVueltaInscripcion.SelectedValue);
                        }
                        else
                            importacion.TurnoInscripcion = (DateTime)SqlDateTime.Null;

                        if (ServicioImportacionDTO.ValidatePredecessor(importacion))
                        {
                            if (ServicioImportacionDTO.ValidateInscriptionOnCourse(importacion))
                            {
                                SetProcess(importacion);
                                FillGrid(1);
                            }
                            else
                            {
                                lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageWarningActiveInscription"];
                                mpeMessage.Show();
                            }
                        }
                        else
                        {
                            lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageProcessInvalidPredecessor"];
                            mpeMessage.Show();
                        }
                    }
                    else
                    {
                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageProcessInvalidFileFormat"];
                        mpeMessage.Show();
                    }
                }
                else
                {
                    lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageProcessNotFile"];
                    mpeMessage.Show();
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "Save", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set process format class
        /// </summary>
        /// <param name="tipo">Tipo de Importacion</param>
        /// <returns></returns>
        private string SetClaseFormato(string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "II":
                        return ClaseInicioInscripcion;
                    case "CC":
                        return ClaseCatedra;
                    case "IN":
                        return ClaseInscripcion;
                    case "PA":
                        return ClasePadron;
                    default:
                        return ClaseCalificacion;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetClaseFormato", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set a new process instance
        /// </summary>
        private void SetProcess(ServicioImportacion importacion)
        {
            try
            {
                ServicioImportacionDTO.InsertNuevoServicio(importacion);

                lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageProcessOk"];
                mpeMessage.Show();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetProcess", Path.GetFileName(Request.PhysicalPath));
                
                asyncFile.BackColor = Color.FromArgb(255, 173, 173);
                lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageProcessError"];
                mpeMessage.Show();
                LimpiarFormulario();
            }
        }

        /// <summary>
        /// Method to clean form data
        /// </summary>
        private void LimpiarFormulario()
        {
            txtFechaProgramada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            ClearContents(asyncFile as Control);
            asyncFile.Dispose();
        }

        /// <summary>
        /// Method to deactivate specific process import
        /// </summary>
        /// <param name="id">Process Id</param>
        private void DeactivateProcess(int id)
        {
            try
            {
                ServicioImportacion proceso = new ServicioImportacion();
                proceso.IdImportacion = id;
                ServicioImportacionDTO.DeactivateImportProcess(proceso);
                FillGrid(1);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DeactivateProcess", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to fill grid
        /// </summary>
        /// <param name="option">Option to select wich grid fill (1: GridActiveProcess/ 2:GridLogProcess)</param>
        private void FillGrid(int option)
        {
            try
            {
                switch (option)
                {
                    case 1:
                        gridActiveProcess.DataSource = ServicioImportacionDTO.GetActiveProcess();
                        gridActiveProcess.Columns[9].Visible = true;
                        gridActiveProcess.Columns[10].Visible = true;
                        gridActiveProcess.DataBind();
                        gridActiveProcess.Columns[9].Visible = false;
                        gridActiveProcess.Columns[10].Visible = false;
                        break;
                    default:
                        importacion = new ServicioImportacion();
                        importacion.IdTipoImportacion = cboTipoImportacionError.Items.Count > 0 ?
                            cboTipoImportacionError.SelectedIndex != 0 ? cboTipoImportacionError.SelectedValue : null : null;
                        importacion.IdTipoInscripcion = cboTipoInscripcionError.Items.Count > 0 ?
                            cboTipoInscripcionError.SelectedIndex != 0 ? cboTipoInscripcionError.SelectedValue : null : null;
                        importacion.TurnoInscripcion = cboTurnoInscripcionError.Items.Count > 0 ?
                            cboTurnoInscripcionError.SelectedIndex != 0 ? Convert.ToDateTime(cboTurnoInscripcionError.SelectedValue) : (DateTime)SqlDateTime.Null : (DateTime)SqlDateTime.Null;
                        importacion.IdVuelta = cboVueltaInscripcionError.Items.Count > 0 ?
                            cboVueltaInscripcionError.SelectedIndex != 0 ? Convert.ToInt32(cboVueltaInscripcionError.SelectedValue) : 0 : 0;
                        gridLogProcess.DataSource = ServicioImportacionDTO.GetErrorProcess(importacion);
                        gridLogProcess.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "FillGrid", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to clear GridLogProcess
        /// </summary>
        private void ShowGridLogProcess(bool show)
        {
            gridLogProcess.Visible = show;
        }

        /// <summary>
        /// Method to upload asynchronous file
        /// </summary>
        /// <param name="arg">AsyncFileUploadEventArgs</param>
        private void UploadAsyncFile(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs arg)
        {
            try
            {
                if (asyncFile.ContentType.ToLower() == ("text/plain"))
                {
                    string strDestPath = ConfigurationManager.AppSettings["PathFileTransfer"] + arg.FileName;
                    asyncFile.SaveAs(strDestPath);
                }
                else
                    return;
            }
            catch(Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "UploadAsyncFile", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to clear asyncFileUpload control
        /// </summary>
        /// <param name="control">Control object</param>
        private void ClearContents(Control control)
        {
            for (var i = 0; i < Session.Keys.Count; i++)
            {
                if (Session.Keys[i].Contains(control.ClientID))
                {
                    Session.Remove(Session.Keys[i]);
                    break;
                }
            }
        }

        /// <summary>
        /// Method to create and download extract txt files
        /// </summary>
        private void DownloadTxtFiles(int option, string fileName)
        {
            try
            {
                string FilePath = ConfigurationManager.AppSettings["FilePath"];

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath + fileName))
                {
                    switch (option)
                    {
                        case 1:
                            sw.WriteLine(ExtractInscripciones());
                            break;
                    }
                    sw.Close();
                }

                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(FilePath + fileName, System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                Response.AddHeader("Content-disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DownloadTxtFiles", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to download all active students in a xls file
        /// </summary>
        private void DownloadExcelFile(string fileName)
        {           
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);

            // Create a dynamic control, populate and render it
            GridView excel = new GridView();
            excel.DataSource = UsuarioDTO.ExportPadron();
            excel.DataBind();
            excel.RenderControl(new HtmlTextWriter(Response.Output));

            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// Method to load the Vuelta of TurnoInscripcion selected
        /// </summary>
        /// <returns></returns>
        private void GetVueltasTurno(string turno)
        {
            try
            {
                cboInscripcionesVuelta.DataSource = InscripcionDTO.GetVueltasByTurnoInscripcion(Convert.ToDateTime(turno));
                cboInscripcionesVuelta.DataBind();

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "TraerVueltasTurno", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to extract off TurnoInscripcion on datatable
        /// </summary>
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

        /// <summary>
        /// Method to download all Inscripciones for the Turno selected
        /// </summary>
        /// <returns></returns>
        private string ExtractInscripciones()
        {
            try
            {
                List<Inscripcion> listInscripciones = new List<Inscripcion>();
                StringBuilder sbLine = new StringBuilder();
                StringBuilder sbFile = new StringBuilder();
                string sFullFecha = String.Empty;
                string sFullDni = String.Empty;

                listInscripciones = InscripcionDTO.GetInscripcionesByTurnoInscripcionIdVuelta(cboInscripciones.SelectedValue.Split('-')[0], Convert.ToInt32(cboVueltaInscripcion.SelectedValue));
                for (int i = 0; i < listInscripciones.Count; i++)
                {
                    sbLine.Append(listInscripciones[i].IdTipoInscripcion.ToString() + ";");
                    sbLine.Append(listInscripciones[i].TurnoInscripcion.ToString("MM/yyyy") + ";");
                    sbLine.Append(listInscripciones[i].IdVuelta.ToString() + ";");
                    sbLine.Append(listInscripciones[i].IdMateria.ToString() + ";");
                    sbLine.Append(listInscripciones[i].CatedraComision + ";");
                    sbLine.Append(listInscripciones[i].DNI.ToString().PadLeft(8, '0') + ";");
                    sbLine.Append(listInscripciones[i].IdEstadoInscripcion + ";");
                    sbLine.Append(listInscripciones[i].OrigenInscripcion.PadLeft(1, ' ') + ";");
                    sbLine.Append(listInscripciones[i].FechaAltaInscripcion.ToString("dd/MM/yyyy") + ";");
                    sbLine.Append(listInscripciones[i].FechaAltaInscripcion.ToString("HH:mm") + ";");
                    sbLine.Append(listInscripciones[i].OrigenModificacion.PadLeft(1, ' ') + ";");
                    sbLine.Append(listInscripciones[i].FechaModificacionInscripcion.ToString("dd/MM/yyyy") != "01/01/0001" ? listInscripciones[i].FechaModificacionInscripcion.ToString("dd/MM/yyyy") + ";" : String.Empty.PadLeft(10, ' ') + ";");
                    sbLine.Append(listInscripciones[i].FechaModificacionInscripcion.ToString("dd/MM/yyyy") != "01/01/0001" ? listInscripciones[i].FechaModificacionInscripcion.ToString("HH:mm") + ";" : String.Empty.PadLeft(5, ' ') + ";");
                    sbLine.Append(listInscripciones[i].DNIEmpleadoAlta.ToString().PadLeft(8, '0') + ";");
                    sbLine.Append(listInscripciones[i].DNIEmpleadoMod.ToString().PadLeft(8, '0'));

                    if (i < listInscripciones.Count - 1)
                        sbLine.AppendLine();

                    sFullDni = String.Empty;
                    sFullFecha = String.Empty;
                }
                return sbLine.ToString();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ExtractInscripciones", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}