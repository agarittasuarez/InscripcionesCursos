using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.IO;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Data.SqlTypes;
using System.Data;

namespace InscripcionesCursos
{
    public partial class wcuMenuExtraccion : System.Web.UI.UserControl
    {
        #region Constants&Variables

        private const string ComboTextFieldTurno = "TurnoInscripcion";
        private const string ComboValueFieldTurno = "TurnoInscripcion";
        private const string C_FILE_DIRECTORY = "ImportFiles\\";
        private const string C_FILE_TYPE = "text/plain";
        private const string IdTipoInscripcionPromocion = "P";
        private const int IdCargoStudent = 2;
        private const string IdMovimientoBaja = "B";
        private const string IdMovimientoCambio = "C";

        private Stream sFile;
        private StreamReader srReadFile;

        private string line;
        private string fileNameCatedraComision = ConfigurationManager.AppSettings["FileNameCatedraComision"];
        private string fileNameInscripcion = ConfigurationManager.AppSettings["FileNameInscripcion"];
        private string fileNamePadronAlumnos = ConfigurationManager.AppSettings["FileNamePadronAlumno"];
        private bool changedAccount = false;

        #endregion

        #region Objects

        private Inscripcion inscripcion;
        private Usuario alumno;
        private CatedraComision comision;
        private Analitico analitico;
        private InscripcionActiva inscripActiva;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SetUpTexts();
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

        #region LeftMenuButtons

        protected void btnCatedras_Click(object sender, EventArgs e)
        {
            SetCatedrasFrame();
        }

        protected void btnInscripciones_Click(object sender, EventArgs e)
        {
            SetInscripcionesFrame();
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            filtroImportarPadron.Visible = false;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = true;
            filtroImportarInscripciones.Visible = false;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;
        }

        protected void btnImportarPadron_Click(object sender, EventArgs e)
        {
            filtroImportarPadron.Visible = true;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarInscripciones.Visible = false;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;
            lblEstadoImportarPadron.Text = null;
        }

        protected void btnImportarInscripciones_Click(object sender, EventArgs e)
        {
            filtroImportarInscripciones.Visible = true;
            filtroImportarPadron.Visible = false;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;
            lblEstadoImportarInscripciones.Text = null;
        }

        protected void btnImportarComisiones_Click(object sender, EventArgs e)
        {
            filtroImportarCatedraComision.Visible = true;
            filtroImportarInscripciones.Visible = false;
            filtroImportarPadron.Visible = false;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;
            lblEstadoImportarComisiones.Text = null;
        }

        protected void btnImportarInscripcionActiva_Click(object sender, EventArgs e)
        {
            filtroImportarCatedraComision.Visible = false;
            filtroImportarInscripciones.Visible = false;
            filtroImportarPadron.Visible = false;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarAnalitico.Visible = false;
            lblEstadoImportarComisiones.Text = null;
            filtroImportarInscripcionActiva.Visible = true;
        }

        protected void btnImportarAnalitico_Click(object sender, EventArgs e)
        {
            filtroImportarAnalitico.Visible = true;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarInscripciones.Visible = false;
            filtroImportarPadron.Visible = false;
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;
            lblEstadoImportarAnalitico.Text = null;
        }

        #endregion

        #region FrameButtons

        protected void btnExtraerAlumnos_Click(object sender, EventArgs e)
        {
            CreateExtractFile(3, fileNamePadronAlumnos);
        }

        protected void btnExtraerCatedra_Click(object sender, EventArgs e)
        {
            if (ddTurnos.SelectedIndex != 0)
                CreateExtractFile(1, fileNameCatedraComision);
        }

        protected void btnExtractInscripcion_Click(object sender, EventArgs e)
        {
            if (ddInscripciones.SelectedIndex != 0)
                CreateExtractFile(2, fileNameInscripcion);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadPadronAlumnos();
        }

        protected void btnUploadInscripciones_Click(object sender, EventArgs e)
        {
            UploadInscripciones();
        }

        protected void btnUploadComisiones_Click(object sender, EventArgs e)
        {
            UploadComisiones();
        }

        protected void btnUploadAnalitico_Click(object sender, EventArgs e)
        {
            UploadNotas();
        }

        protected void btnUploadInscripcionActiva_Click(object sender, EventArgs e)
        {
            UploadInscripcionActiva();
        }

        protected void ddInscripciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddInscripciones.SelectedIndex != 0)
            {
                TraerVueltasTurno(ddInscripciones.SelectedValue);
                ddInscripcionesVuelta.Enabled = true;
            }
            else
                ddInscripcionesVuelta.Enabled = false;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Method to set up button texts
        /// </summary>
        private void SetUpTexts()
        {
            try
            {
                btnCatedras.Text = ConfigurationManager.AppSettings["BotonExtraerCatedras"];
                btnInscripciones.Text = ConfigurationManager.AppSettings["BotonExtraerInscripciones"];
                btnUsuarios.Text = ConfigurationManager.AppSettings["BotonExtraerAlumnos"];
                btnImportarPadron.Text = ConfigurationManager.AppSettings["BotonImportarPadronAlumnos"];
                btnImportarInscripciones.Text = ConfigurationManager.AppSettings["BotonImportarInscripciones"];
                btnImportarComisiones.Text = ConfigurationManager.AppSettings["BotonImportarComisiones"];
                btnImportarAnalitico.Text = ConfigurationManager.AppSettings["BotonImportarAnalitico"];
                btnImportarInscripcionActiva.Text = ConfigurationManager.AppSettings["BotonImportarInscripcionActiva"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to set up the Catedras Frame controls
        /// </summary>
        private void SetCatedrasFrame()
        {
            filtroCatedraComision.Visible = true;
            filtroInscripcion.Visible = false;
            filtroAlumno.Visible = false;
            filtroImportarPadron.Visible = false;
            filtroImportarInscripciones.Visible = false;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;

            try
            {
                ddTurnos.DataTextField = ComboTextFieldTurno;
                ddTurnos.DataValueField = ComboValueFieldTurno;
                ddTurnos.DataSource = CatedraComisionDTO.GetAllTurnos();
                ddTurnos.DataBind();

                ddTurnos.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
                ddTurnos.Items.Insert(1, new ListItem(ConfigurationManager.AppSettings["ContentComboExtractAll"], "*"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetCatedrasFrame", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set up the Inscripciones Frame controls
        /// </summary>
        private void SetInscripcionesFrame()
        {
            List<string> listTurnos = new List<string>();
            filtroCatedraComision.Visible = false;
            filtroInscripcion.Visible = true;
            filtroAlumno.Visible = false;
            filtroImportarPadron.Visible = false;
            filtroImportarInscripciones.Visible = false;
            filtroImportarCatedraComision.Visible = false;
            filtroImportarAnalitico.Visible = false;
            filtroImportarInscripcionActiva.Visible = false;

            try
            {
                listTurnos = ExtractTurnos(InscripcionDTO.GetAllTurnos(new Inscripcion()));

                ddInscripciones.DataSource = listTurnos;
                ddInscripciones.DataBind();

                ddInscripciones.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboTurnoDefault"], "0"));
                ddInscripciones.Items.Insert(1, new ListItem(ConfigurationManager.AppSettings["ContentComboExtractAll"], "*"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetInscripcionesFrame", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to create and download extract file catedras
        /// </summary>
        private void CreateExtractFile(int option, string fileName)
        {
            try
            {
                string FilePath = ConfigurationManager.AppSettings["FilePath"];

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath + fileName))
                {
                    switch (option)
                    {
                        case 1:
                            sw.WriteLine(ExtractCatedrasComisiones());
                            break;
                        case 2:
                            sw.WriteLine(ExtractInscripciones());
                            break;
                        case 3:
                            sw.WriteLine(ExtractPadronAlumnos());
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
                log.WriteLog(ex.Message, "CreateExtractFile", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to extract all Catedras / Comisiones for the Turno selected
        /// </summary>
        /// <returns></returns>
        private string ExtractCatedrasComisiones()
        {
            try
            {
                List<CatedraComision> listCatedras = new List<CatedraComision>();
                StringBuilder sbLine = new StringBuilder();

                listCatedras = CatedraComisionDTO.GetCatedraComisionByTurnoInscripcion(ddTurnos.SelectedValue);
                for (int i = 0; i < listCatedras.Count; i++)
                {
                    sbLine.Append(listCatedras[i].IdTipoInscripcion.ToString() + ";");
                    sbLine.Append(listCatedras[i].TurnoInscripcion.ToShortDateString() + ";");
                    sbLine.Append(listCatedras[i].IdVuelta.ToString() + ";");
                    sbLine.Append(listCatedras[i].IdMateria.ToString() + ";");
                    sbLine.Append(listCatedras[i].CatedraComisionDescripcion + ";");

                    if (listCatedras[i].FechaDesde.Year != 1)
                        sbLine.Append(listCatedras[i].FechaDesde.ToShortDateString() + ";");
                    else
                        sbLine.Append(" ;");
                    if (listCatedras[i].FechaHasta.Year != 1)
                        sbLine.Append(listCatedras[i].FechaHasta.ToShortDateString() + ";");
                    else
                        sbLine.Append(" ;");

                    sbLine.Append(listCatedras[i].Horario + ";");
                    sbLine.Append(listCatedras[i].IdSede.ToString() + ";");
                    sbLine.Append(listCatedras[i].ProfesorNombreApellido + ";");
                    sbLine.Append(listCatedras[i].ProfesorJerarquia + ";");
                    sbLine.Append(listCatedras[i].ComisionAbierta);
                    sbLine.AppendLine("");
                }
                return sbLine.ToString();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ExtractCatedrasComisiones", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to extract all Inscripciones for the Turno selected
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

                listInscripciones = InscripcionDTO.GetInscripcionesByTurnoInscripcionIdVuelta(ddInscripciones.SelectedValue, Convert.ToInt32(ddInscripcionesVuelta.SelectedValue));
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

        /// <summary>
        /// Method to extract all students
        /// </summary>
        /// <returns></returns>
        private string ExtractPadronAlumnos()
        {
            try
            {
                var sbLine = new StringBuilder();
                var sbFile = new StringBuilder();

                var listAlumnos = UsuarioDTO.GetAllUsuario(IdCargoStudent);
                for (int i = 0; i < listAlumnos.Count; i++)
                {
                    sbLine.Append(listAlumnos[i].DNI.ToString().PadLeft(8, '0') + ";");
                    sbLine.Append(listAlumnos[i].ApellidoNombre + ";");
                    sbLine.Append(listAlumnos[i].IdSede + ";");
                    sbLine.Append(listAlumnos[i].Estado + ";");
                    sbLine.Append((listAlumnos[i].Carrera != null ? listAlumnos[i].Carrera.PadLeft(2, '0') : "  ") + ";");
                    sbLine.Append((listAlumnos[i].CuatrimestreAnioIngreso ?? "      ") + ";");
                    sbLine.Append((listAlumnos[i].CuatrimestreAnioReincorporacion ?? "      ") + ";");
                    sbLine.Append((listAlumnos[i].Email ?? "          ") + ";");
                    sbLine.Append((listAlumnos[i].Limitacion ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionVision ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionAudicion ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionMotriz ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionAgarre ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionHabla ?? " ") + ";");
                    sbLine.Append((listAlumnos[i].LimitacionOtra ?? " ") + ";");
                    sbLine.Append(" ;");
                    sbLine.Append("        ;");

                    if (i < listAlumnos.Count - 1)
                        sbLine.AppendLine();
                }
                return sbLine.ToString();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ExtractPadronAlumnos", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to upload the Padron Alumnos file
        /// </summary>
        /// <returns></returns>
        private void UploadPadronAlumnos()
        {
            if (fuPadron.HasFile)
            {
                int count = 0;
                try
                {
                    if (fuPadron.PostedFile.ContentType == C_FILE_TYPE)
                    {
                        
                        string filename = Path.GetFileName(fuPadron.FileName);
                        fuPadron.SaveAs(Server.MapPath("~/") + C_FILE_DIRECTORY + filename);
                        sFile = fuPadron.PostedFile.InputStream;
                        srReadFile = new StreamReader(sFile);

                        while ((line = srReadFile.ReadLine()) != null)
                        {
                            string[] tmpArray = line.Split(Convert.ToChar(";"));
                            alumno = new Usuario();

                            alumno.DNI = Convert.ToInt32(tmpArray[0].Trim());
                            alumno.ApellidoNombre = tmpArray[1].Trim().Replace('�', 'Ñ');
                            alumno.IdSede = Convert.ToInt32(tmpArray[2].Trim());
                            alumno.Estado = tmpArray[3].Trim();
                            alumno.Carrera = tmpArray[4].Trim();
                            alumno.CuatrimestreAnioIngreso = tmpArray[5].Trim().Length != 0 ? tmpArray[5].Trim() : null;
                            alumno.CuatrimestreAnioReincorporacion = tmpArray[6].Trim().Length != 0 ? tmpArray[6].Trim() : null;
                            alumno.IdCargo = 2;
                            alumno.LimitacionRelevada = tmpArray[8].Trim().Length > 0;
                            alumno.Limitacion = tmpArray[8].Trim().Length != 0 ? tmpArray[8].Trim() : null;
                            alumno.LimitacionVision = tmpArray[9].Trim().Length != 0 ? tmpArray[9].Trim() : null;
                            alumno.LimitacionAudicion = tmpArray[10].Trim().Length != 0 ? tmpArray[10].Trim() : null;
                            alumno.LimitacionMotriz = tmpArray[11].Trim().Length != 0 ? tmpArray[11].Trim() : null;
                            alumno.LimitacionAgarre = tmpArray[12].Trim().Length != 0 ? tmpArray[12].Trim() : null;
                            alumno.LimitacionHabla = tmpArray[13].Trim().Length != 0 ? tmpArray[13].Trim() : null;
                            alumno.LimitacionOtra = tmpArray[14].Trim().Length != 0 ? tmpArray[14].Trim() : null;

                            if (tmpArray[15].Trim() != string.Empty)
                            {
                                switch(tmpArray[15].Trim().ToUpper())
                                {
                                    case IdMovimientoBaja:
                                        UsuarioDTO.DeactivateAccount(Convert.ToInt32(tmpArray[0]));
                                        changedAccount = true;
                                        break;
                                    case IdMovimientoCambio:
                                        if (tmpArray[16].Trim() != string.Empty)
                                            UsuarioDTO.TransferData(Convert.ToInt32(tmpArray[0].Trim()), Convert.ToInt32(tmpArray[16].Trim()));
                                        changedAccount = true;
                                        break;
                                }
                            }
                            else
                                UsuarioDTO.ImportPadron(alumno);

                            count++;
                        }
                        lblEstadoImportarPadron.Text = "Se ha importado el padron correctamente. Total de Registros procesados: " + count.ToString();
                    }
                    else
                        lblEstadoImportarPadron.Text = "Formato de archivo invalido (unicamente .txt)";
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter();
                    log.WriteLog(ex.Message, "UploadPadronAlumnos", Path.GetFileName(Request.PhysicalPath));
                    lblEstadoImportarPadron.Text = "No se pudo subir el archivo. Ocurrio el siguiente error en el registro " + count + ": " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Method to upload the Inscripciones file
        /// </summary>
        /// <returns></returns>
        private void UploadInscripciones()
        {
            if (fuInscripciones.HasFile)
            {
                try
                {
                    bool cleanInscriptions = true;
                    int count = 0;

                    StringBuilder dniError = new StringBuilder();
                    if (fuInscripciones.PostedFile.ContentType == C_FILE_TYPE)
                    {
                        string filename = Path.GetFileName(fuInscripciones.FileName);
                        fuInscripciones.SaveAs(Server.MapPath("~/") + C_FILE_DIRECTORY + filename);
                        sFile = fuInscripciones.PostedFile.InputStream;
                        srReadFile = new StreamReader(sFile);

                        while ((line = srReadFile.ReadLine()) != null)
                        {
                            string[] tmpArray = line.Split(Convert.ToChar(";"));
                            inscripcion = new Inscripcion();

                            if (!ValidateStudentsInPadron(Convert.ToInt32(tmpArray[5])))
                            {
                                Usuario missedUser = new Usuario(Convert.ToInt32(tmpArray[5]), "Sin Datos", null, 2, null, false, false, -1, -1, null,
                                    null, null, null, false, null, null, null, null, null, null, null);
                                UsuarioDTO.ImportPadron(missedUser);

                                if (dniError.ToString().IndexOf(tmpArray[5]) == -1)
                                    dniError.Append(tmpArray[5] + "<br/>");
                            }

                            inscripcion.IdTipoInscripcion = tmpArray[0];
                            inscripcion.TurnoInscripcion = Convert.ToDateTime(tmpArray[1]);
                            inscripcion.IdVuelta = Convert.ToInt32(tmpArray[2]);
                            inscripcion.IdMateria = Convert.ToInt32(tmpArray[3]);
                            inscripcion.CatedraComision = tmpArray[4];
                            inscripcion.DNI = Convert.ToInt32(tmpArray[5]);
                            inscripcion.IdEstadoInscripcion = tmpArray[6];
                            inscripcion.OrigenInscripcion = tmpArray[7];
                            if (tmpArray[8].Trim() != String.Empty)
                                inscripcion.FechaAltaInscripcion = Convert.ToDateTime(tmpArray[8] + " " + tmpArray[9]);
                            else
                                inscripcion.FechaAltaInscripcion = (DateTime)SqlDateTime.Null;

                            if (tmpArray[10].Trim() != String.Empty)
                                inscripcion.OrigenModificacion = tmpArray[10];
                            else
                                inscripcion.OrigenModificacion = null;
                            if (tmpArray[11].Trim() != String.Empty)
                                inscripcion.FechaModificacionInscripcion = Convert.ToDateTime(tmpArray[11] + " " + tmpArray[12]);
                            else
                                inscripcion.FechaModificacionInscripcion = (DateTime)SqlDateTime.Null;

                            inscripcion.DNIEmpleadoAlta = tmpArray[13].Trim() != String.Empty ? Convert.ToInt32(tmpArray[13]) : 0;
                            inscripcion.DNIEmpleadoMod = tmpArray[14].Trim() != String.Empty ? Convert.ToInt32(tmpArray[14]) : 0;

                            if (cleanInscriptions)
                            {
                                InscripcionDTO.DeleteInscriptionsInTurn(inscripcion);
                                cleanInscriptions = false;
                            }
                            InscripcionDTO.InsertInscripcion(inscripcion);
                            count++;
                        }
                        if (dniError.Length > 0)
                            lblEstadoImportarInscripciones.Text = "Se han importado las inscripciones correctamente, y se dieron parcialmnete de alta algunos alumnos. <br/> Listado DNI: <br/>"
                                + dniError.ToString() + "<br/><br/>Total de Registros procesados: " + count.ToString();
                        else
                            lblEstadoImportarInscripciones.Text = "Se han importado las inscripciones correctamente";
                    }
                    else
                        lblEstadoImportarInscripciones.Text = "Formato de archivo invalido (unicamente .txt)";
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter();
                    log.WriteLog(ex.Message, "UploadInscripciones", Path.GetFileName(Request.PhysicalPath));
                    lblEstadoImportarInscripciones.Text = "No se pudo subir el archivo. En el registro -->" + inscripcion.IdTipoInscripcion 
                        + "; " + inscripcion.TurnoInscripcion.ToString("MM/yyyy") + "; " + inscripcion.IdVuelta.ToString() + "; " + inscripcion.IdMateria.ToString()
                        + "; " + inscripcion.CatedraComision + "; " + inscripcion.DNI.ToString() + "<-- ocurrio el siguiente error: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Method to upload the Catedra/Comisiones file
        /// </summary>
        /// <returns></returns>
        private void UploadComisiones()
        {
            if (fuComisiones.HasFile)
            {
                try
                {
                    int count = 0;
                    if (fuComisiones.PostedFile.ContentType == C_FILE_TYPE)
                    {
                        string filename = Path.GetFileName(fuComisiones.FileName);
                        fuComisiones.SaveAs(Server.MapPath("~/") + C_FILE_DIRECTORY + filename);
                        sFile = fuComisiones.PostedFile.InputStream;
                        srReadFile = new StreamReader(sFile);

                        while ((line = srReadFile.ReadLine()) != null)
                        {
                            string[] tmpArray = line.Split(Convert.ToChar(";"));

                            comision = new CatedraComision();

                            comision.IdTipoInscripcion = tmpArray[0];
                            comision.TurnoInscripcion = Convert.ToDateTime(tmpArray[1]);
                            comision.IdVuelta = Convert.ToInt32(tmpArray[2]);
                            comision.IdMateria = Convert.ToInt32(tmpArray[3]);
                            if (comision.IdTipoInscripcion == IdTipoInscripcionPromocion)
                                comision.CatedraComisionDescripcion = tmpArray[4].PadLeft(3, '0');
                            else
                                comision.CatedraComisionDescripcion = tmpArray[4].PadLeft(3, ' ');
                            comision.FechaDesde = tmpArray[5].Trim() != String.Empty ? Convert.ToDateTime(tmpArray[5]) : (DateTime)SqlDateTime.Null;
                            comision.FechaHasta = tmpArray[6].Trim() != String.Empty ? Convert.ToDateTime(tmpArray[6]) : (DateTime)SqlDateTime.Null;
                            comision.Horario = tmpArray[7];
                            comision.IdSede = Convert.ToInt32(tmpArray[8]);



                            comision.ProfesorNombreApellido = tmpArray[9].Replace('�', 'Ñ');
                            comision.ProfesorJerarquia = tmpArray[10];
                            comision.ComisionAbierta = tmpArray[11];

                            CatedraComisionDTO.InsertCatedraComision(comision);
                            count++;
                        }
                        lblEstadoImportarComisiones.Text = "Se han importado las Catedras/Comisiones correctamente. Total de registros porcesados: " + count.ToString();
                    }
                    else
                        lblEstadoImportarComisiones.Text = "Formato de archivo invalido (unicamente .txt)";
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter();
                    log.WriteLog(ex.Message, "UploadComisiones", Path.GetFileName(Request.PhysicalPath));

                    lblEstadoImportarComisiones.Text = "No se pudo subir el archivo. En el registro -->" + comision.IdTipoInscripcion
                        + "; " + comision.TurnoInscripcion.ToString("MM/yyyy") + "; " + comision.IdVuelta.ToString() + "; " + comision.IdMateria.ToString()
                        + "; " + comision.CatedraComisionDescripcion + "; " + "<-- ocurrio el siguiente error: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Method to upload the Notas file
        /// </summary>
        /// <returns></returns>
        private void UploadNotas()
        {
            if (fuNotas.HasFile)
            {
                try
                {
                    StringBuilder dniError = new StringBuilder();
                    int count = 0;
                    if (fuNotas.PostedFile.ContentType == C_FILE_TYPE)
                    {
                        string filename = Path.GetFileName(fuNotas.FileName);
                        fuNotas.SaveAs(Server.MapPath("~/") + C_FILE_DIRECTORY + filename);
                        sFile = fuNotas.PostedFile.InputStream;
                        srReadFile = new StreamReader(sFile);

                        while ((line = srReadFile.ReadLine()) != null)
                        {
                            string[] tmpArray = line.Split(Convert.ToChar(";"));
                            analitico = new Analitico();

                            if (ValidateStudentsInPadron(Convert.ToInt32(tmpArray[4])))
                            {
                                analitico.CatedraComision = tmpArray[3];
                                analitico.CodigoMovimiento = tmpArray[13];
                                analitico.DNI = Convert.ToInt32(tmpArray[4]);
                                analitico.Fecha = tmpArray[6].Trim().Length > 0 ? Convert.ToDateTime(tmpArray[6]) : (DateTime)SqlDateTime.Null;
                                analitico.Folio = tmpArray[10];
                                analitico.IdMateria = Convert.ToInt32(tmpArray[2]);
                                analitico.IdTipoInscripcion = tmpArray[0];
                                analitico.Libro = tmpArray[8];
                                analitico.Nota = tmpArray[7].Trim().Length > 0 ? Convert.ToDouble(tmpArray[7]) : -1;
                                analitico.Plan = tmpArray[5].Trim().Length > 0 ? Convert.ToInt32(tmpArray[5]) : -1;
                                analitico.Resolucion = tmpArray[12];
                                analitico.SubFolio = tmpArray[11];
                                analitico.Tomo = tmpArray[9];
                                analitico.TurnoInscripcion = tmpArray[1].Trim().Length > 0 ? Convert.ToDateTime(tmpArray[1]) : (DateTime)SqlDateTime.Null;
                                //analitico.UltimoIngreso = "";

                                AnaliticoDTO.ImportNotas(analitico);
                                count++;
                            }
                            else
                            {
                                if (dniError.ToString().IndexOf(tmpArray[5]) == -1)
                                    dniError.Append(tmpArray[5] + "<br/>");
                            }
                        }
                        if (dniError.Length > 0)
                            lblEstadoImportarAnalitico.Text = "Se han importado parcialmente las notas, ya que hay alumnos que no se encuentran en el padron. <br/> Listado DNI: <br/>" + dniError.ToString();
                        else
                            lblEstadoImportarAnalitico.Text = "Se han importado las notas correctamente. Total de registros procesados: " + count.ToString();
                    }
                    else
                        lblEstadoImportarAnalitico.Text = "Formato de archivo invalido (unicamente .txt)";
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter();
                    log.WriteLog(ex.Message, "UploadNotas", Path.GetFileName(Request.PhysicalPath));
                    lblEstadoImportarAnalitico.Text = "No se pudo subir el archivo. Ocurrio el siguiente error: " + ex.Message + "<br />" + analitico.IdMateria + "- " + analitico.DNI + "- " + analitico.CatedraComision + "- " + analitico.Nota + "- " + analitico.IdTipoInscripcion + "- " + analitico.Fecha;
                }
            }
        }

        /// <summary>
        /// Method to upload the InscripcionActiva file
        /// </summary>
        /// <returns></returns>
        private void UploadInscripcionActiva()
        {
            if (fuInscripcionActiva.HasFile)
            {
                try
                {
                    int count = 0;
                    if (fuInscripcionActiva.PostedFile.ContentType == C_FILE_TYPE)
                    {
                        string filename = Path.GetFileName(fuInscripcionActiva.FileName);
                        fuInscripcionActiva.SaveAs(Server.MapPath("~/") + C_FILE_DIRECTORY + filename);
                        sFile = fuInscripcionActiva.PostedFile.InputStream;
                        srReadFile = new StreamReader(sFile);

                        while ((line = srReadFile.ReadLine()) != null)
                        {
                            string[] tmpArray = line.Split(Convert.ToChar(";"));
                            inscripActiva = new InscripcionActiva();

                            inscripActiva.IdTipoInscripcion = tmpArray[0];
                            inscripActiva.TurnoInscripcion = Convert.ToDateTime(tmpArray[1]);
                            inscripActiva.IdVuelta = Convert.ToInt32(tmpArray[2]);
                            inscripActiva.InscripcionFechaDesde = Convert.ToDateTime(tmpArray[3]);
                            inscripActiva.InscripcionFechaHasta = Convert.ToDateTime(tmpArray[4]);

                            InscripcionActivaDTO.InsertInscripcionActiva(inscripActiva);
                            count++;
                        }
                        lblEstadImportarInscripcionActiva.Text = "Se han importado correctamente las Inscripciones Activas. Total de registros procesados: " + count.ToString();
                    }
                    else
                        lblEstadImportarInscripcionActiva.Text = "Formato de archivo invalido (unicamente .txt)";
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter();
                    log.WriteLog(ex.Message, "UploadInscripcionActiva", Path.GetFileName(Request.PhysicalPath));
                    lblEstadImportarInscripcionActiva.Text = "No se pudo subir el archivo. Ocurrio el siguiente error: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Method to load the Vuelta of TurnoInscripcion selected
        /// </summary>
        /// <returns></returns>
        private void TraerVueltasTurno(string turno)
        {
            try
            {
                ddInscripcionesVuelta.DataSource = InscripcionDTO.GetVueltasByTurnoInscripcion(Convert.ToDateTime(ddInscripciones.SelectedValue));
                ddInscripcionesVuelta.DataBind();

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
        private List<string> ExtractTurnos(DataTable dataTable)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    list.Add(dataTable.Rows[i]["TurnoInscripcionBreve"].ToString());

                return list;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ExtractTurnos", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to validate if the dni exists in the padron
        /// </summary>
        /// <returns></returns>
        private bool ValidateStudentsInPadron(int dni)
        {
            try
            {
                Usuario userInscrip = new Usuario(dni);
                userInscrip = UsuarioDTO.GetUsuario(userInscrip);

                if (userInscrip != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ValidateStudentsInPadron", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}