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
    public partial class InscripcionCursos : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeStudent = 2;
        const string InscriptionTypeExam = "E";
        const string InscriptionTypeCourse = "P";
        const int InscriptionMonthSummerCourse = 3;
        string IdEstadoBajaModificacion = ConfigurationManager.AppSettings["IdEstadoBajaModificacion"];
        string IdEstadoBajaSorteo = ConfigurationManager.AppSettings["IdEstadoBajaSorteo"];
        string IdEstadoBajaErrorInscripcion = ConfigurationManager.AppSettings["IdEstadoBajaErrorInscripcion"];
        string IdEstadoBajaReglamentacion = ConfigurationManager.AppSettings["IdEstadoBajaReglamentacion"];
        string IdEstadoBajaAprobada = ConfigurationManager.AppSettings["IdEstadoBajaAprobada"];
        string IdEstadoAltaInscripcion = ConfigurationManager.AppSettings["IdEstadoAltaInscripcion"];

        #endregion

        #region Objects

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
                    RemoveSessionVars();
                    if (!Utils.CheckLoggedUser(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckAccountStatus(Session["user"], UserTypeStudent))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                    listInscripcionesActivas = new List<InscripcionActiva>();
                    listInscripcionesActivas = InscripcionActivaDTO.ValidateInscipcionesActivas(DateTime.Now);
                    Session.Add("inscripcionesActivas", listInscripcionesActivas);

                    if (listInscripcionesActivas.Count != 0)
                    {
                        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["InscripcionDisable"]))
                        {
                            inscripcion = new Inscripcion();
                            inscripcion.IdTipoInscripcion = listInscripcionesActivas[0].IdTipoInscripcion;
                            inscripcion.TurnoInscripcion = listInscripcionesActivas[0].TurnoInscripcion;
                            inscripcion.IdVuelta = listInscripcionesActivas[0].IdVuelta;
                            inscripcion.DNI = ((Usuario)Session["user"]).DNI;
                            Session.Add("idVuelta", inscripcion.IdVuelta);

                            //Seteamos el estado en A, para que traiga todas las inscripciones excepto las baja por modificacion
                            inscripcion.IdEstadoInscripcion = IdEstadoAltaInscripcion;

                            listCarroAlta = new List<Carro>();
                            listCarroBaja = new List<Carro>();
                            listCarroAlta = InscripcionDTO.GetInscriptionsInTurn(inscripcion);

                            if (listCarroAlta.Count > 0)
                            {
                                //Seteamos el estado en M, para que traiga las inscripciones en baja por modificacion
                                inscripcion.IdEstadoInscripcion = IdEstadoBajaModificacion;
                                listCarroBaja = InscripcionDTO.GetInscriptionsInTurn(inscripcion);

                                Session.Add("carro", RemoveDuplicateItemsDifRounds(listCarroAlta, listCarroBaja));
                                Session.Add("preCarro", listCarroBaja);
                            }
                            SetCartLimit();
                        }
                        else
                            SetMessage(ConfigurationManager.AppSettings["ContentInscripcionProcesandoDatos"]);
                    }
                    else
                        SetMessage(ConfigurationManager.AppSettings["ContentInscripcionActivaNoDisponible"]);
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

        /// <summary>
        /// Method to set up the warning message of unavailable inscription
        /// </summary>
        private void SetMessage(string message)
        {
            ddInscripciones.Visible = false;
            divNoDisponible.Visible = true;
            lblNoDisponible.Text = message;
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
                                            if(i>0) i--;
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