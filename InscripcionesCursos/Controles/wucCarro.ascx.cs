using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using AjaxControlToolkit;

namespace InscripcionesCursos
{
    public partial class wucCarro : System.Web.UI.UserControl
    {
        #region Constants & Variables

        string IdEstadoBajaModificacion = ConfigurationManager.AppSettings["IdEstadoBajaModificacion"];
        string IdEstadoAltaInscripcion = ConfigurationManager.AppSettings["IdEstadoAltaInscripcion"];
        string IdTipoInscripcionPromocion = ConfigurationManager.AppSettings["IdTipoInscripcionPromocion"];
        string IdOrigenInscripcionWeb = ConfigurationManager.AppSettings["IdOrigenInscripcionWeb"];
        string IdOrigenInscripcionFacu = ConfigurationManager.AppSettings["IdOrigenInscripcionFacu"];
        StringBuilder scriptingBuilder;
        int idVuelta = 0;

        #endregion

        #region Objects

        List<Carro> listCarro, listPreCarro, deletedCarro, preCarro;
        Carro auxCarro;
        Inscripcion inscripcion;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SetUpCart();
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
        /// Event to delet cart item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridCarro_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteCartItem(e.RowIndex);
        }

        /// <summary>
        /// Evento to hide or disable rows that IdEstadoInscripcion are not A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridCarro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {
                    if (listCarro == null)
                    {
                        listCarro = new List<Carro>();
                        listCarro = (List<Carro>)Session["carro"];
                    }
                    if (listCarro.Count > 0)
                    {
                        if ((listCarro[e.Row.RowIndex].IdEstadoInscripcion != null) && (listCarro[e.Row.RowIndex].IdEstadoInscripcion != String.Empty) && (listCarro[e.Row.RowIndex].IdEstadoInscripcion != IdEstadoAltaInscripcion))
                        {
                            e.Row.Enabled = false;
                            e.Row.CssClass = "filaDeshabilitada";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "GridCarro_RowDataBound", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to suscribe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInscribir_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessagePopUpCart.Text = ConfigurationManager.AppSettings["MessageCartClearInscriptions"];
                mpeMessageCart.Show();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnInscribir_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Event to accept modal popup message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptarCart_Click(object sender, EventArgs e)
        {
            SuscribeStudent();
        }

        /// <summary>
        /// Event to cancel modal popup message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelarCart_Click(object sender, EventArgs e)
        {
            mpeMessageCart.Hide();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to delete a item added in the cart
        /// </summary>
        private void DeleteCartItem(int index)
        {
            try
            {
                Carro objDeleted;
                listCarro = new List<Carro>();
                deletedCarro = new List<Carro>();
                preCarro = (List<Carro>)Session["preCarro"];

                if (Session["idVuelta"] != null)
                    idVuelta = Convert.ToInt32(Session["idVuelta"]);

                if (Session["carro"] != null)
                    listCarro = (List<Carro>)Session["carro"];

                if (Session["deletedCarro"] != null)
                    deletedCarro = (List<Carro>)Session["deletedCarro"];

                if (listCarro[index].IdVuelta != idVuelta)
                {
                    //GUARDO EL ESTADO ANTERIOR DEL OBJETO BORRADO
                    objDeleted = new Carro(listCarro[index].IdTipoInscripcion, listCarro[index].TurnoInscripcion, listCarro[index].IdVuelta, listCarro[index].IdMateria,
                        listCarro[index].CatedraComision, listCarro[index].DNI, listCarro[index].Materia, listCarro[index].Profesor, listCarro[index].Horario, listCarro[index].FechaDesdeHasta,
                        listCarro[index].EstadoDescripcion, listCarro[index].IdEstadoInscripcion);
                    objDeleted.FechaAltaInscripcion = listCarro[index].FechaAltaInscripcion;
                    objDeleted.FechaModificacionInscripcion = listCarro[index].FechaModificacionInscripcion != DateTime.MinValue ? listCarro[index].FechaModificacionInscripcion : (DateTime)SqlDateTime.Null;
                    objDeleted.OrigenInscripcion = listCarro[index].OrigenInscripcion;
                    objDeleted.OrigenModificacion = listCarro[index].OrigenModificacion;
                    objDeleted.DNIEmpleadoAlta = listCarro[index].DNIEmpleadoAlta;
                    objDeleted.DNIEmpleadoMod = listCarro[index].DNIEmpleadoMod;

                    deletedCarro.Add(objDeleted);
                }

                if (listCarro[index].EstadoDescripcion != null)
                {
                    //GUARDO EL ESTADO ACTUAL DEL OBJETO BORRADO, CAMBIO ID ESTADO, FECHA MOD Y ORIGEN MOD
                    objDeleted = new Carro(listCarro[index].IdTipoInscripcion, listCarro[index].TurnoInscripcion, idVuelta, listCarro[index].IdMateria,
                        listCarro[index].CatedraComision, listCarro[index].DNI, listCarro[index].Materia, listCarro[index].Profesor, listCarro[index].Horario, listCarro[index].FechaDesdeHasta,
                        listCarro[index].EstadoDescripcion, IdEstadoBajaModificacion);
                    objDeleted.FechaAltaInscripcion = listCarro[index].FechaAltaInscripcion;
                    objDeleted.FechaModificacionInscripcion = DateTime.Now;
                    objDeleted.OrigenInscripcion = listCarro[index].OrigenInscripcion;
                    objDeleted.DNIEmpleadoAlta = listCarro[index].DNIEmpleadoAlta;
                    objDeleted.DNIEmpleadoMod = listCarro[index].DNIEmpleadoMod;

                    if (Session["userEmployee"] == null)
                        objDeleted.OrigenModificacion = IdOrigenInscripcionWeb;
                    else
                        objDeleted.OrigenModificacion = IdOrigenInscripcionFacu;

                    deletedCarro.Add(objDeleted);
                    Session.Add("deletedCarro", deletedCarro);
                    deletedCarro = null;
                }

                listCarro.RemoveAt(index);

                if (listCarro.Count == 0)
                    headerCart.Visible = true;

                Session.Add("carro", listCarro);
                GridCarro.DataSource = listCarro;
                GridCarro.DataBind();
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DeleteCartItem", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to Suscribe Student
        /// </summary>
        private void SuscribeStudent()
        {
            try
            {
                bool cleanInscriptions = true;
                bool mailStatus = false;
                bool userEmployee = false;
                listCarro = new List<Carro>();
                listPreCarro = new List<Carro>();
                deletedCarro = new List<Carro>();
                listCarro = (List<Carro>)Session["carro"];
                listPreCarro = (List<Carro>)Session["preCarro"];

                if (Session["idVuelta"] != null)
                    idVuelta = Convert.ToInt32(Session["idVuelta"]);

                if (Session["userEmployee"] != null)
                    userEmployee = true;

                using (TransactionScope scope = new TransactionScope())
                {
                    for (int count = 0; count < listCarro.Count; count++)
                    {
                        if (listCarro[count].IdEstadoInscripcion != IdEstadoBajaModificacion)
                        {
                            inscripcion = new Inscripcion();
                            inscripcion.IdTipoInscripcion = listCarro[count].IdTipoInscripcion;
                            inscripcion.TurnoInscripcion = listCarro[count].TurnoInscripcion;
                            inscripcion.IdVuelta = listCarro[count].IdVuelta;
                            inscripcion.IdMateria = listCarro[count].IdMateria;
                            inscripcion.CatedraComision = listCarro[count].CatedraComision;
                            inscripcion.DNI = ((Usuario)Session["user"]).DNI;
                            inscripcion.IdEstadoInscripcion = listCarro[count].IdEstadoInscripcion;

                            //LOGICA PARA ACTUALIZAR Y SETEAR DATOS DE AUDITORIA
                            if (listPreCarro != null)
                            {
                                auxCarro = new Carro();
                                var tagged = listPreCarro.Select((carro, i) => new { Carro = carro, Index = i });
                                auxCarro = (from obj in tagged
                                            where obj.Carro.CatedraComision == listCarro[count].CatedraComision
                                            && obj.Carro.IdMateria == listCarro[count].IdMateria
                                            && obj.Carro.IdVuelta == listCarro[count].IdVuelta
                                            select obj.Carro).SingleOrDefault();
                            }

                            if (auxCarro != null)
                            {
                                if (auxCarro.OrigenInscripcion != String.Empty && auxCarro.OrigenInscripcion != null)
                                    inscripcion.OrigenInscripcion = auxCarro.OrigenInscripcion;
                                else
                                {
                                    if (!userEmployee)
                                        inscripcion.OrigenInscripcion = IdOrigenInscripcionWeb;
                                    else
                                        inscripcion.OrigenInscripcion = IdOrigenInscripcionFacu;
                                }
                                if (auxCarro.IdEstadoInscripcion == IdEstadoBajaModificacion)
                                {
                                    if (!userEmployee)
                                        inscripcion.OrigenModificacion = IdOrigenInscripcionWeb;
                                    else
                                        inscripcion.OrigenModificacion = IdOrigenInscripcionFacu;
                                    inscripcion.FechaModificacionInscripcion = DateTime.Now;
                                }
                                else
                                {
                                    inscripcion.OrigenModificacion = null;
                                    inscripcion.FechaModificacionInscripcion = (DateTime)SqlDateTime.Null;
                                }

                                if (auxCarro.FechaAltaInscripcion != null && auxCarro.FechaAltaInscripcion != DateTime.MinValue && auxCarro.FechaAltaInscripcion != (DateTime)SqlDateTime.Null)
                                    inscripcion.FechaAltaInscripcion = auxCarro.FechaAltaInscripcion;
                                else
                                    inscripcion.FechaAltaInscripcion = DateTime.Now;
                            }
                            else
                            {
                                if (listCarro[count].OrigenInscripcion != String.Empty && listCarro[count].OrigenInscripcion != null)
                                    inscripcion.OrigenInscripcion = listCarro[count].OrigenInscripcion;
                                else
                                {
                                    if (!userEmployee)
                                        inscripcion.OrigenInscripcion = IdOrigenInscripcionWeb;
                                    else
                                        inscripcion.OrigenInscripcion = IdOrigenInscripcionFacu;
                                }

                                if (listCarro[count].OrigenModificacion != String.Empty && listCarro[count].OrigenModificacion != null)
                                    inscripcion.OrigenModificacion = listCarro[count].OrigenModificacion;
                                else
                                    inscripcion.OrigenModificacion = null;

                                if (listCarro[count].FechaAltaInscripcion != null && listCarro[count].FechaAltaInscripcion != DateTime.MinValue && listCarro[count].FechaAltaInscripcion != (DateTime)SqlDateTime.Null)
                                    inscripcion.FechaAltaInscripcion = listCarro[count].FechaAltaInscripcion;
                                else
                                    inscripcion.FechaAltaInscripcion = DateTime.Now;

                                inscripcion.FechaModificacionInscripcion = (DateTime)SqlDateTime.Null;
                            }

                            if (userEmployee)
                            {
                                if (listCarro[count].DNIEmpleadoAlta != 0)
                                    inscripcion.DNIEmpleadoAlta = listCarro[count].DNIEmpleadoAlta;
                                else
                                    inscripcion.DNIEmpleadoAlta = listCarro[count].OrigenInscripcion == IdOrigenInscripcionWeb ? 0 : ((Usuario)Session["userEmployee"]).DNI;
                                if (listPreCarro != null && listPreCarro.Count > 0)
                                    inscripcion = SetDNIEmpleado(inscripcion, listPreCarro, false);
                            }
                            else
                            {
                                if (listPreCarro != null && listPreCarro.Count > 0)
                                    inscripcion = SetDNIEmpleado(inscripcion, listPreCarro, true);

                                if (inscripcion.DNIEmpleadoAlta == 0)
                                    inscripcion.DNIEmpleadoAlta = listCarro[count].DNIEmpleadoAlta;
                                if (inscripcion.DNIEmpleadoMod == 0)
                                    inscripcion.DNIEmpleadoMod = listCarro[count].DNIEmpleadoMod;
                            }

                            if (cleanInscriptions)
                            {
                                InscripcionDTO.DeleteInscriptions(inscripcion);
                                cleanInscriptions = false;
                            }

                            inscripcion.IdInscripcion = InscripcionDTO.InsertInscripcion(inscripcion);
                        }
                    }
                    
                    if ((listPreCarro != null) && (listPreCarro.Count > 0))
                        ChangeInscriptionsBajaMod(listCarro, listPreCarro, 1, cleanInscriptions, userEmployee);

                    if(Session["deletedCarro"] != null)
                        ChangeInscriptionsBajaMod(listCarro, (List<Carro>)Session["deletedCarro"], 2, cleanInscriptions, userEmployee);
                    if(Session["listDuplicated"] != null)
                        ChangeInscriptionsBajaMod(listCarro, (List<Carro>)Session["listDuplicated"], 2, cleanInscriptions, userEmployee);

                    scope.Complete();
                }

                if (listCarro.Count > 0)
                    mailStatus = SendMailInscriptions(listCarro);
                else
                {
                    if (Session["deletedCarro"] != null)
                        mailStatus = SendMailInscriptions((List<Carro>)Session["deletedCarro"]);
                }

                if (mailStatus)
                {
                    if (Session["userEmployee"] == null)
                        Response.Redirect(ConfigurationManager.AppSettings["UrlStudentInscripcion"] + "?result=ok&email=ok");
                    else
                        ToolkitScriptManager.RegisterStartupScript(this.Page, this.GetType(), "jsPrint", scriptingBuilder.ToString(), false);
                }
                else
                {
                    if (Session["userEmployee"] == null)
                        Response.Redirect(ConfigurationManager.AppSettings["UrlStudentInscripcion"] + "?result=ok&email=error");
                    else
                        ToolkitScriptManager.RegisterStartupScript(this.Page, this.GetType(), "jsPrint", scriptingBuilder.ToString(), false);
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "DeleteCartItem", Path.GetFileName(Request.PhysicalPath));
                Response.Redirect(ConfigurationManager.AppSettings["UrlStudentInscripcion"] + "?result=error");
            }
        }

        /// <summary>
        /// Method to Set Employee DNI in the object
        /// </summary>
        private Inscripcion SetDNIEmpleado(Inscripcion inscripcion, List<Carro> listPreCarro, bool altaMod)
        {
            try
            {
                for (int i = 0; i < listPreCarro.Count; i++)
                {
                    if (listPreCarro[i].IdMateria == inscripcion.IdMateria && listPreCarro[i].CatedraComision == inscripcion.CatedraComision)
                    {
                        if (altaMod)
                        {
                            inscripcion.DNIEmpleadoAlta = listPreCarro[i].DNIEmpleadoAlta;
                            inscripcion.DNIEmpleadoMod = listPreCarro[i].DNIEmpleadoMod;
                        }
                        else
                            inscripcion.DNIEmpleadoMod = ((Usuario)Session["userEmployee"]).DNI;
                        break;
                    }
                }

                return inscripcion;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetDNIEmpleado", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }
        
        /// <summary>
        /// Method to compare the original inscriptions and the new inscriptions, except the errased inscriptions and insert with the M state
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        private void ChangeInscriptionsBajaMod(List<Carro> newList, List<Carro> oldList, int actionStatus, bool clean, bool userEmployee)
        {
            try
            {
                List<Carro> listDifference;

                if (actionStatus == 1)
                    listDifference = oldList.Except(newList, new Carro()).ToList();
                else
                    listDifference = oldList;

                for (int i = 0; i < listDifference.Count; i++)
                {
                    inscripcion = new Inscripcion();
                    inscripcion.IdTipoInscripcion = listDifference[i].IdTipoInscripcion;
                    inscripcion.TurnoInscripcion = listDifference[i].TurnoInscripcion;
                    inscripcion.IdVuelta = listDifference[i].IdVuelta;
                    inscripcion.IdMateria = listDifference[i].IdMateria;
                    inscripcion.CatedraComision = listDifference[i].CatedraComision;
                    inscripcion.DNI = ((Usuario)Session["user"]).DNI;

                    if (actionStatus == 1)
                        inscripcion.IdEstadoInscripcion = IdEstadoBajaModificacion;
                    else
                        inscripcion.IdEstadoInscripcion = listDifference[i].IdEstadoInscripcion;

                    inscripcion.OrigenInscripcion = listDifference[i].OrigenInscripcion;

                    if (listDifference[i].OrigenModificacion != null && listDifference[i].OrigenModificacion != String.Empty)
                        inscripcion.OrigenModificacion = listDifference[i].OrigenModificacion;
                    else
                    {
                        if (Session["userEmployee"] == null)
                        {
                            if (actionStatus == 2 && listDifference[i].IdEstadoInscripcion == IdEstadoAltaInscripcion)
                                inscripcion.OrigenModificacion = null;
                            else
                                inscripcion.OrigenModificacion = IdOrigenInscripcionWeb;
                        }
                        else
                        {
                            if (actionStatus == 2 && listDifference[i].IdEstadoInscripcion == IdEstadoAltaInscripcion)
                                inscripcion.OrigenModificacion = null;
                            else
                                inscripcion.OrigenModificacion = IdOrigenInscripcionFacu;
                        }
                    }

                    if (listDifference[i].FechaModificacionInscripcion != null && listDifference[i].FechaModificacionInscripcion != DateTime.MinValue && listDifference[i].FechaModificacionInscripcion != (DateTime)SqlDateTime.Null)
                        inscripcion.FechaModificacionInscripcion = listDifference[i].FechaModificacionInscripcion;
                    else
                    {
                        if (actionStatus == 2 && listDifference[i].IdEstadoInscripcion == IdEstadoAltaInscripcion)
                            inscripcion.FechaModificacionInscripcion = listDifference[i].FechaModificacionInscripcion != DateTime.MinValue ? listDifference[i].FechaModificacionInscripcion : (DateTime)SqlDateTime.Null;
                        else
                            inscripcion.FechaModificacionInscripcion = DateTime.Now;
                    }

                    inscripcion.FechaAltaInscripcion = listDifference[i].FechaAltaInscripcion;
                    inscripcion.DNIEmpleadoAlta = listDifference[i].DNIEmpleadoAlta;

                    if (userEmployee)
                        inscripcion.DNIEmpleadoMod = ((Usuario)Session["userEmployee"]).DNI;
                    else
                        inscripcion.DNIEmpleadoMod = listDifference[i].DNIEmpleadoMod;

                    if (clean)
                    {
                        InscripcionDTO.DeleteInscriptions(inscripcion);
                        clean = false;
                    }

                    InscripcionDTO.InsertInscripcion(inscripcion);
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ChangeInscriptionsBajaMod", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to send the inscription resume
        /// </summary>
        /// <param name="listCarro"></param>
        /// <returns></returns>
        private bool SendMailInscriptions(List<Carro> listCarro)
        {
            try
            {
                string enter = "<br />";
                string boldOpen = "<b>";
                string boldClose = "</b>";
                string subject = ConfigurationManager.AppSettings["MailInscriptionSubject"];
                StringBuilder mailBody = new StringBuilder();
                StringBuilder css = new StringBuilder();
                Dictionary<int, string> months = new Dictionary<int, string>();
                scriptingBuilder = new StringBuilder();

                #region Months Dictionary

                months.Add(1, ConfigurationManager.AppSettings["Enero"]);
                months.Add(2, ConfigurationManager.AppSettings["Febrero"]);
                months.Add(3, ConfigurationManager.AppSettings["Marzo"]);
                months.Add(4, ConfigurationManager.AppSettings["Abril"]);
                months.Add(5, ConfigurationManager.AppSettings["Mayo"]);
                months.Add(6, ConfigurationManager.AppSettings["Junio"]);
                months.Add(7, ConfigurationManager.AppSettings["Julio"]);
                months.Add(8, ConfigurationManager.AppSettings["Agosto"]);
                months.Add(9, ConfigurationManager.AppSettings["Septiembre"]);
                months.Add(10, ConfigurationManager.AppSettings["Octubre"]);
                months.Add(11, ConfigurationManager.AppSettings["Noviembre"]);
                months.Add(12, ConfigurationManager.AppSettings["Diciembre"]);

                #endregion

                string tableStyle = " style=\"font-family: Consolas, Verdana; font-size: 10pt;border-width: 1px; border-spacing: 0px;	border-color: black; border-collapse: collapse;\"";
                string thStyle = " style = \"border-width: 1px; padding: 5px; border-style: solid; border-color: black;\"";
                string tdStyle = " style = \"border-width: 1px;	padding: 5px; border-style: solid; border-color: black;\"";
                string bodyStyle = " style = \"font-family: Consolas, Verdana; font-size: 12pt;\"";


                mailBody.Append("<html><head><title>" + ConfigurationManager.AppSettings["TitlePrintInscripciones"] + "</title>");
                mailBody.Append("</head><body" + bodyStyle + ">");
                mailBody.Append("<div><div style=\"text-align: center; float: left; display: inline\">" + String.Format(ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresion"], enter, enter) + "</div>");
                mailBody.Append("<div style=\"display:inline; float:right;\">" + ConfigurationManager.AppSettings["ContentHeaderHistoricoImpresionFecha"] + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "</div></div><br /><br /><br /><br /><br /><br />");
                mailBody.Append("<div style=\" text-align: center;\">");

                if (((List<InscripcionActiva>)Session["inscripcionesActivas"])[0].IdVuelta == 0)
                    mailBody.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresionExamenVuelta"], enter));
                else
                {
                    if (((List<InscripcionActiva>)Session["inscripcionesActivas"])[0].IdVuelta == 1)
                        mailBody.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion1Vuelta"], enter));
                    else
                        mailBody.Append(String.Format(ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion"], ConfigurationManager.AppSettings["ContentSubtitleHistoricoImpresion2Vuelta"], enter));
                }

                mailBody.Append("</div><br />");

                if (listCarro[0].TurnoInscripcion.Month == 1)
                    mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistorico1Cuatrimestre"], listCarro[0].TurnoInscripcion.Month.ToString() + "/" + listCarro[0].TurnoInscripcion.Year.ToString()) + "</div><br />");
                else
                {
                    if ((listCarro[0].TurnoInscripcion.Month == 2) && (listCarro[0].IdTipoInscripcion == IdTipoInscripcionPromocion))
                        mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistorico2Cuatrimestre"], listCarro[0].TurnoInscripcion.Month.ToString() + "/" + listCarro[0].TurnoInscripcion.Year.ToString()) + "</div><br />");
                    else
                    {
                        if ((listCarro[0].TurnoInscripcion.Month == 2) || (listCarro[0].TurnoInscripcion.Month == 5) || (listCarro[0].TurnoInscripcion.Month == 7) || (listCarro[0].TurnoInscripcion.Month == 10) || (listCarro[0].TurnoInscripcion.Month == 12))
                            mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentDataHistoricoExamen"], months[listCarro[0].TurnoInscripcion.Month] + "/" + listCarro[0].TurnoInscripcion.Year.ToString()) + "</div><br />");
                        else
                            mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData1HistoricoImpresion"], ConfigurationManager.AppSettings["ContentDataHistoricoCursoVerano"], listCarro[0].TurnoInscripcion.Year.ToString()) + "</div><br />");
                    }
                }

                mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData2HistoricoImpresion"], ((Usuario)Session["user"]).ApellidoNombre) + "</div>");
                mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentData3HistoricoImpresion"], ((Usuario)Session["user"]).DNI.ToString()) + "</div><br /><br />");
                mailBody.Append("<div>" + ConfigurationManager.AppSettings["ContentBodyTitleHistoricoImpresion"] + "</div><br />");

                mailBody.Append("<table" + tableStyle + ">");
                mailBody.Append("<tr><th" + thStyle + ">MATERIA</th><th" + thStyle + ">COMISION</th><th" + thStyle + ">PROFESOR</th>");

                if (listCarro[0].IdTipoInscripcion != IdTipoInscripcionPromocion)
                    mailBody.Append("<th" + thStyle + ">FECHA EXAMEN</th>");

                mailBody.Append("<th" + thStyle + ">HORARIO</th></tr>");

                for (int i = 0; i < listCarro.Count; i++)
                {
                    if (listCarro[i].IdEstadoInscripcion == IdEstadoAltaInscripcion)
                    {
                        mailBody.Append("<tr><td" + tdStyle + ">" + listCarro[i].IdMateria.ToString() + " " + listCarro[i].Materia + "</td>");
                        mailBody.Append("<td" + tdStyle + ">" + listCarro[i].CatedraComision + "</td>");
                        mailBody.Append("<td" + tdStyle + ">" + listCarro[i].Profesor + "</td>");

                        if (listCarro[0].IdTipoInscripcion != IdTipoInscripcionPromocion)
                            mailBody.Append("<td" + tdStyle + ">" + listCarro[i].FechaDesdeHasta.ToShortDateString() + "</td>");

                        mailBody.Append("<td" + tdStyle + ">" + listCarro[i].Horario + "</td></tr>");
                    }
                }

                mailBody.Append("</table><br /><br /><br />");

                if (((listCarro[0].TurnoInscripcion.Month == 2) || (listCarro[0].TurnoInscripcion.Month == 5) || (listCarro[0].TurnoInscripcion.Month == 7) || (listCarro[0].TurnoInscripcion.Month == 10) || (listCarro[0].TurnoInscripcion.Month == 12)) && listCarro[0].IdTipoInscripcion != IdTipoInscripcionPromocion)
                    mailBody.Append("<div>" + ConfigurationManager.AppSettings["ContentFooterHistoricoEmailExamen"] + "</div>");
                else
                    if (listCarro[0].TurnoInscripcion.Month == 3)
                        mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentFooterHistoricoEmailCursoVerano"], enter, ConfigurationManager.AppSettings["ContentFooterFechaHistoricoEmailCursoVerano"], enter, boldOpen, boldClose, enter));
                    else
                        mailBody.Append("<div>" + String.Format(ConfigurationManager.AppSettings["ContentFooterHistoricoEmail"], enter, ConfigurationManager.AppSettings["ContentFooterFechaHistoricoEmail"], enter, ConfigurationManager.AppSettings["ContentFooterFecha2HistoricoEmail"], boldOpen, boldClose, enter));

                mailBody.Append("</body></html>");

                //Impresion de comprobante para inscripciones realizadas por empleados de la Facultad
                if (Session["userEmployee"] != null)
                {
                    scriptingBuilder.Append("<script type='text/javascript'>");
                    scriptingBuilder.Append("var win=null;");
                    scriptingBuilder.Append("win = window.open();");
                    scriptingBuilder.Append("self.focus();");
                    scriptingBuilder.Append("win.document.open();");
                    scriptingBuilder.Append("win.document.write('" + HttpUtility.JavaScriptStringEncode(mailBody.ToString()) + "');");
                    scriptingBuilder.Append("win.document.close();");
                    scriptingBuilder.Append("win.print();");
                    scriptingBuilder.Append("win.close();");
                    scriptingBuilder.Append("window.location='" + ConfigurationManager.AppSettings["UrlStudentInscripcion"] + "';");
                    scriptingBuilder.Append("</script>");
                }

                if (Utils.SendMail((Usuario)Session["user"], subject, mailBody.ToString()))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SendMailInscriptions", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to setup texts
        /// </summary>
        /// <param name="listCarro"></param>
        /// <returns></returns>
        private void SetUpCart()
        {
            try
            {
                btnInscribir.Text = ConfigurationManager.AppSettings["BotonInscribir"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SetUpCart", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}