using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Threading;

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class InscripcionCursos : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        const string InscriptionTypeExam = "E";
        const string InscriptionTypeCourse = "P";
        const int InscriptionMonthSummerCourse = 3;
        const int MinUserLevel = 3;
        string IdEstadoBajaModificacion = ConfigurationManager.AppSettings["IdEstadoBajaModificacion"];
        string IdEstadoBajaSorteo = ConfigurationManager.AppSettings["IdEstadoBajaSorteo"];
        string IdEstadoBajaErrorInscripcion = ConfigurationManager.AppSettings["IdEstadoBajaErrorInscripcion"];
        string IdEstadoBajaReglamentacion = ConfigurationManager.AppSettings["IdEstadoBajaReglamentacion"];
        string IdEstadoBajaAprobada = ConfigurationManager.AppSettings["IdEstadoBajaAprobada"];
        string IdEstadoAltaInscripcion = ConfigurationManager.AppSettings["IdEstadoAltaInscripcion"];

        #endregion

        #region Objects

        Usuario user;
        Inscripcion inscripcion;
        List<InscripcionActiva> listInscripcionesActivas;
        List<Carro> listCarroAlta, listCarroBaja;

        #endregion

        #region PageLoad

        public void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckUserProfileLevel(Session["userEmployee"], MinUserLevel))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"]);

                    Session.Remove("user");
                }

                if (InscripcionDTO.CheckEmployeeTest())
                    btnClean.Enabled = true;

                FailureText.Visible = false;
                divNoDisponible.Visible = false;
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

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                ddInscripciones.RefreshControl();

                RemoveSessionVars();
                user = ValidateStudent(Convert.ToInt32(txtDni.Text));

                if (user != null && user.DNI != 0)
                {
                    SetStudentData();
                    Session.Remove("user");
                    Session.Add("user", user);
                    listInscripcionesActivas = new List<InscripcionActiva>();
                    listInscripcionesActivas = InscripcionActivaDTO.ValidateInscipcionesActivas(DateTime.Now, 1, ((Usuario)Session["user"]).IdSede);
                    Session.Add("inscripcionesActivas", listInscripcionesActivas);

                    if (listInscripcionesActivas.Count != 0)
                    {
                        inscripcion = new Inscripcion();
                        inscripcion.IdTipoInscripcion = listInscripcionesActivas[0].IdTipoInscripcion;
                        inscripcion.TurnoInscripcion = listInscripcionesActivas[0].TurnoInscripcion;
                        inscripcion.IdVuelta = listInscripcionesActivas[0].IdVuelta;
                        inscripcion.DNI = ((Usuario)Session["user"]).DNI;
                        Session.Add("idVuelta", inscripcion.IdVuelta);

                        //Seteamos el estado en A, para que traiga las inscripciones en alta
                        inscripcion.IdEstadoInscripcion = IdEstadoAltaInscripcion;

                        listCarroAlta = new List<Carro>();
                        listCarroBaja = new List<Carro>();
                        listCarroAlta = InscripcionDTO.GetInscriptionsInTurn(inscripcion);

                        SetCartLimit();
                        ddInscripciones.Visible = true;

                        if (listCarroAlta.Count > 0)
                        {
                            //Seteamos el estado en M, para que traiga las inscripciones en baja
                            inscripcion.IdEstadoInscripcion = IdEstadoBajaModificacion;
                            listCarroBaja = InscripcionDTO.GetInscriptionsInTurn(inscripcion);

                            Session.Add("carro", RemoveDuplicateItemsDifRounds(listCarroAlta, listCarroBaja));
                            Session.Add("preCarro", listCarroBaja);

                            if ((listCarroAlta != null) && (listCarroAlta.Count != 0))
                            {
                                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).DataSource = listCarroAlta;
                                setGridCartHeaders();
                                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).DataBind();
                                ShowEmptyCartHeader(false);
                            }
                        }
                        else
                        {
                            ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).DataSource = null;
                            ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).DataBind();
                            ShowEmptyCartHeader(true);
                        }
                    }
                    else
                        SetMessage(ConfigurationManager.AppSettings["ContentInscripcionActivaNoDisponible"], 2);
                }
                else
                    SetMessage(ConfigurationManager.AppSettings["ErrorMessageDniInexistente"], 1);

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnRequest_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        protected void btnClean_Click(object sender, EventArgs e)
        {
            try
            {
                InscripcionDTO.DeleteEmployeeTestInscription();
                btnClean.Enabled = false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnClean_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to set up the warning message of unavailable inscription
        /// </summary>
        private void SetMessage(string message, int type)
        {
            switch (type)
            {
                case 1:
                    FailureText.Visible = true;
                    FailureText.Text = message;
                    break;
                case 2:
                    ddInscripciones.Visible = false;
                    divNoDisponible.Visible = true;
                    lblNoDisponible.Text = message;
                    break;
            }
        }

        /// <summary>
        /// Method to validate inscription type and turn, and then set the cart items limit
        /// </summary>
        private void SetCartLimit()
        {
            try
            {
                int addSpaceCart = 0;

                for (int i = 0; i < listCarroAlta.Count; i++)
                {
                    if ((listCarroAlta[i].IdEstadoInscripcion == IdEstadoBajaAprobada) || (listCarroAlta[i].IdEstadoInscripcion == IdEstadoBajaErrorInscripcion)
                        || (listCarroAlta[i].IdEstadoInscripcion == IdEstadoBajaModificacion) || (listCarroAlta[i].IdEstadoInscripcion == IdEstadoBajaReglamentacion)
                        || (listCarroAlta[i].IdEstadoInscripcion == IdEstadoBajaSorteo))
                        addSpaceCart++;
                }


                if (listInscripcionesActivas[0].IdTipoInscripcion == InscriptionTypeExam)
                    Session.Add("cartLimit", ConfigurationManager.AppSettings["MaxInscriptionExamen"]);
                else
                {
                    if (listInscripcionesActivas[0].IdTipoInscripcion == InscriptionTypeCourse)
                    {
                        if (listInscripcionesActivas[0].TurnoInscripcion.Month == InscriptionMonthSummerCourse)
                            Session.Add("cartLimit", Convert.ToInt32(ConfigurationManager.AppSettings["MaxInscriptionCursoVerano"]) + addSpaceCart);
                        else
                            Session.Add("cartLimit", Convert.ToInt32(ConfigurationManager.AppSettings["MaxInscriptionCuatrimestral"]) + addSpaceCart);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetCartLimit", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Set grid headers
        /// </summary>
        private void setGridCartHeaders()
        {
            try
            {
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[0].HeaderText = ConfigurationManager.AppSettings["ContentHeaderMateria"];
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[1].HeaderText = ConfigurationManager.AppSettings["ContentHeaderCatedraComision"];
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[2].HeaderText = ConfigurationManager.AppSettings["ContentHeaderProfesor"];
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[3].HeaderText = ConfigurationManager.AppSettings["ContentHeaderHorario"];
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[4].HeaderText = ConfigurationManager.AppSettings["ContentHeaderEstadoInscripcion"];
                ((GridView)ddInscripciones.FindControl("wucCarro").FindControl("GridCarro")).Columns[5].HeaderText = ConfigurationManager.AppSettings["ContentHeaderEliminar"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "setGridCartHeaders", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to show or hide the empty cart header
        /// </summary>
        /// <param name="state"></param>
        private void ShowEmptyCartHeader(bool state)
        {
            try
            {
                ((ddInscripciones.FindControl("wucCarro").FindControl("headerCart"))).Visible = state;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ShowEmptyCartHeader", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to validate if student exists
        /// </summary>
        /// <param name="state"></param>
        private Usuario ValidateStudent(int dni)
        {
            try
            {
                return UsuarioDTO.GetUsuario(new Usuario(dni));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ValidateStudent", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to load student info
        /// </summary>
        /// <param name="state"></param>
        private void SetStudentData()
        {
            try
            {
                divResultados.Visible = true;
                txtApellidoNombreResultado.Text = user.ApellidoNombre.ToUpper();
                txtCarrera.Text = user.Carrera != null ? user.Carrera.ToUpper() : String.Empty;
                txtEmail.Text = user.Email != null ? user.Email.ToUpper() : String.Empty;

                if (user.Password != null && user.CambioPrimerLogin && user.CuentaActivada)
                {
                    txtEstadoCuenta.Text = ConfigurationManager.AppSettings["ContentCuentaActivada"].ToUpper();
                    txtEstadoCuenta.BackColor = Color.FromArgb(112, 219, 147);
                }
                else
                {
                    if (user.Password != null && user.CambioPrimerLogin && !user.CuentaActivada)
                    {
                        txtEstadoCuenta.Text = ConfigurationManager.AppSettings["ContentCuentaNoActivada"].ToUpper();
                        txtEstadoCuenta.BackColor = Color.FromArgb(238, 238, 0);
                    }
                    else
                    {
                        if (user.Password != null && !user.CambioPrimerLogin)
                        {
                            txtEstadoCuenta.Text = ConfigurationManager.AppSettings["ContentCuentaTramitada"].ToUpper();
                            txtEstadoCuenta.BackColor = Color.FromArgb(238, 238, 0);
                        }
                        else
                        {
                            txtEstadoCuenta.Text = ConfigurationManager.AppSettings["ContentCuentaNoTramitada"].ToUpper();
                            txtEstadoCuenta.BackColor = Color.FromArgb(255, 99, 71);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetStudentData", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to remove var session of inscription data
        /// </summary>
        /// <returns></returns>
        private void RemoveSessionVars()
        {
            Session.Remove("carro");
            Session.Remove("preCarro");
            Session.Remove("deletedCarro");
            Session.Remove("idVuelta");
            Session.Remove("listDuplicated");
        }

        /// <summary>
        /// Method to remove duplicate items of distinct rounds
        /// </summary>
        /// <returns></returns>
        private List<Carro> RemoveDuplicateItemsDifRounds(List<Carro> altas, List<Carro> bajas)
        {
            try
            {
                List<Carro> listDuplicated = new List<Carro>();
                //BORRO LAS MATERIAS QUE TENGO EN ESTADO "A" EN VUELTA 1, Y EN ESTADO "M" EN VUELTA 2; "M" EN VUELTA 1 Y "A" EN VUELTA 2
                for (int i = 0; i < altas.Count; i++)
                {
                    for (int x = 0; x < bajas.Count; x++)
                    {
                        //VALIDO QUE SEAN LAS MISMAS COMISIONES DE LAS MATERIAS
                        if (altas[i].CatedraComision == bajas[x].CatedraComision)
                        {
                            //VALIDO QUE SEA EL MISMO TURNO DE LAS MATERIAS
                            if (altas[i].TurnoInscripcion == bajas[x].TurnoInscripcion)
                            {
                                //VALIDO QUE SEA LA MISMA MATERIA
                                if (altas[i].IdMateria == bajas[x].IdMateria)
                                {
                                    //VALIDO QUE LA FECHA DE MODIFICACION DE LA BAJA SEA MAYOR A LA DE ALTA (QUIERE DECIR QUE SE DIO
                                    //DE BAJA EN VUELTA 2)
                                    if (altas[i].FechaModificacionInscripcion < bajas[x].FechaModificacionInscripcion)
                                    {
                                        //VALIDO QUE LA VUELTA DEL ALTA SEA MENOR A LA VUELTA DE BAJA, POR SI SE DIO DE BAJA EN 1ERA,
                                        //Y LUEGO SE DIO DE ALTA EN 2DA
                                        if (altas[i].IdVuelta < bajas[x].IdVuelta)
                                        {
                                            listDuplicated.Add(altas[i]);
                                            altas.RemoveAt(i);
                                            if (i > 0) i--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //BORRO LAS MATERIAS QUE TENGO EN ESTADO A EN VUELTA 1, LUEGO LAS BAJE EN VUELTA 2, Y LAS VOLVI A DAR DE ALTA EN VUELTA 2
                for (int a = 0; a < altas.Count; a++)
                {
                    for (int b = 1; b < altas.Count; b++)
                    {
                        if (altas[a].IdMateria == altas[b].IdMateria && altas[a].CatedraComision == altas[b].CatedraComision
                            && altas[a].TurnoInscripcion == altas[b].TurnoInscripcion)
                        {
                            if (altas[a].IdVuelta > altas[b].IdVuelta)
                            {
                                listDuplicated.Add(altas[b]);
                                altas.RemoveAt(b);
                                a--;
                                b--;
                            }
                        }
                    }
                }
                if (listDuplicated.Count > 0)
                    Session.Add("listDuplicated", listDuplicated);

                return altas;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "RemoveDuplicateItemsDifRounds", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}