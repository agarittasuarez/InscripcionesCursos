using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;

namespace InscripcionesCursos
{
    public partial class wucInscripcion : System.Web.UI.UserControl
    {
        #region Constants & Variables

        const string COMBOTEXTFIELDDEPARTAMENTO = "Nombre";
        const string COMBOVALUEFIELDDEPARTAMENTO = "IdDepartamento";
        const string COMBOTEXTFIELDCARRERA = "Nombre";
        const string COMBOVALUEFIELDCARRERA = "IdCarrera";
        const string COMBOTEXTFIELDMATERIA = "Descripcion";
        const string COMBOVALUEFIELDMATERIA = "IdMateria";
        const string CATEDRACOMISIONCERRADA = "N";
        const string InscriptionTypeExam = "E";

        string IdEstadoBajaModificacion = ConfigurationManager.AppSettings["IdEstadoBajaModificacion"];
        string IdEstadoBajaErrorInscripcion = ConfigurationManager.AppSettings["IdEstadoBajaErrorInscripcion"];
        string IdEstadoInscripto = ConfigurationManager.AppSettings["IdEstadoAltaInscripcion"];
        string IdEstadoBajaSorteo = ConfigurationManager.AppSettings["IdEstadoBajaSorteo"];
        string IdEstadoBajaReglamentacion = ConfigurationManager.AppSettings["IdEstadoBajaReglamentacion"];
        string IdEstadoBajaAprobada = ConfigurationManager.AppSettings["IdEstadoBajaAprobada"];
        
        string resultProcessOk = "ok";
        string resultProcessError = "error";

        int maxSelection = Convert.ToInt32(ConfigurationManager.AppSettings["MaxInscriptionSelection"]);
        int maxInscriptions;
        int rol;

        #endregion

        #region Objects

        Usuario loggedUser;
        BusquedaFiltro filtro;
        List<CatedraComision> catedras;
        Carro carro;
        List<Carro> listCarro;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["result"] != null)
                    {
                        if ((Request.QueryString["result"].ToString() == resultProcessOk) && (Request.QueryString["email"].ToString() == resultProcessOk))
                        {
                            if (Session["userEmployee"] == null)
                            {
                                lblEstado.Text = ConfigurationManager.AppSettings["ContentEstadoInscripcionAndEmailOK"];
                                lblEstado.Visible = true;
                            }
                        }
                        else
                        {
                            if ((Request.QueryString["result"].ToString() == resultProcessOk) && (Request.QueryString["email"].ToString() == resultProcessError))
                            {
                                if (Session["userEmployee"] == null)
                                {
                                    lblEstado.Text = ConfigurationManager.AppSettings["ContentEstadoInscripcionOkEmailError"];
                                    lblEstado.Visible = true;
                                }
                            }
                            else
                            {
                                lblEstado.Text = ConfigurationManager.AppSettings["ContentEstadoInscripcionError"];
                                lblEstado.Visible = true;
                            }
                        }
                    }

                    FillComboDeptoAndCarrera();
                    SetFechaExamenColumn();

                    listCarro = new List<Carro>();
                    listCarro = (List<Carro>)Session["carro"];

                    if ((listCarro != null) && (listCarro.Count != 0))
                    {
                        ((GridView)wucCarro.FindControl("GridCarro")).DataSource = listCarro;
                        setGridCartHeaders();
                        ((GridView)wucCarro.FindControl("GridCarro")).DataBind();
                        ShowEmptyCartHeader(false);
                    }
                }
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
        /// Event to enable Carrera combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void comboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCarrera.Enabled)
            {
                comboCarrera.SelectedIndex = 0;
                comboMateria.SelectedIndex = 0;
                comboMateria.Enabled = false;
            }
            comboCarrera.Enabled = true;
            SetEstado(false);
        }

        /// <summary>
        /// Event to enable Materia combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void comboCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMateria.Enabled = true;
            SetEstado(false);
            FillComboMateria();
        }


        /// <summary>
        /// Event to search user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void comboMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchComboSelection();
        }

        /// <summary>
        /// Event to add items to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int cantSelection = CheckMaxSelection();

            if (cantSelection == maxSelection)
                AddToCart();
            else
            {
                if (cantSelection == 0)
                {
                    lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMinSelection"];
                    mpeMessage.Show();
                }
                else
                {
                    lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMaxSelection"];
                    mpeMessage.Show();
                }
            }
        }

        /// <summary>
        /// Event to check if a catedra or comision is open, and disable his checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                if (catedras[e.Row.RowIndex].ComisionAbierta == CATEDRACOMISIONCERRADA)
                    e.Row.Visible = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fill Combo Departamento
        /// </summary>
        private void FillComboDeptoAndCarrera()
        {
            try
            {
                //Fill Combo Departamento Data
                comboDepartamento.DataTextField = COMBOTEXTFIELDDEPARTAMENTO;
                comboDepartamento.DataValueField = COMBOVALUEFIELDDEPARTAMENTO;
                comboDepartamento.DataSource = DepartamentoDTO.GetAllDepartamentos();
                comboDepartamento.DataBind();

                //Fill Combo Carrera Data
                comboCarrera.DataTextField = COMBOTEXTFIELDCARRERA;
                comboCarrera.DataValueField = COMBOVALUEFIELDCARRERA;
                comboCarrera.DataSource = CarreraDTO.GetAllCarreras();
                comboCarrera.DataBind();

                //Set Default Option
                comboDepartamento.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboDepartamentoDefault"], "0"));
                comboCarrera.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboCarreraDefault"], "0"));
                comboMateria.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboMateriaDefault"], "0"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "FillComboDeptoAndCarrera", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Fill Combo Materia
        /// </summary>
        private void FillComboMateria()
        {
            try
            {
                loggedUser = new Usuario();
                loggedUser = (Usuario)Session["user"];
                rol = Session["userEmployee"] != null ? 1 : 2;
                filtro = new BusquedaFiltro(Convert.ToInt32(comboDepartamento.SelectedValue), Convert.ToInt32(comboCarrera.SelectedValue), DateTime.Now);
                
                comboMateria.DataTextField = COMBOTEXTFIELDMATERIA;
                comboMateria.DataValueField = COMBOVALUEFIELDMATERIA;
                comboMateria.DataSource = MateriaDTO.GetMateriasBySedeAndFilters(loggedUser, filtro, rol).Select(p => new { IdMateria = p.IdMateria, Descripcion = p.IdMateria.ToString() + "- " + p.Descripcion, IdDepartamento = p.IdDepartamento });
                comboMateria.DataBind();
                comboMateria.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["ContentComboMateriaDefault"], "0"));
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "FillComboMateria", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to search the user filtered data
        /// </summary>
        private void SearchComboSelection()
        {
            try
            {
                if (comboMateria.SelectedIndex != 0)
                {
                    Session.Remove("catedras");
                    loggedUser = new Usuario();
                    loggedUser = (Usuario)Session["user"];
                    SetEstado(false);

                    rol = Session["userEmployee"] != null ? 1 : 2;

                    filtro = new BusquedaFiltro(Convert.ToInt32(comboDepartamento.SelectedValue), Convert.ToInt32(comboCarrera.SelectedValue), Convert.ToInt32(comboMateria.SelectedValue), DateTime.Now);
                    catedras = new List<CatedraComision>(CatedraComisionDTO.GetCatedraComisionFiltered(loggedUser, filtro, rol));

                    Session.Add("catedras", catedras);
                    GridResultados.DataSource = catedras;

                    if (GridResultados.DataSource != null)
                    {
                        setGridCatedrasHeaders();
                        divResultados.Visible = true;
                        GridResultados.DataBind();
                    }
                }
                else
                    SetEstado(false);
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SearchComboSelection", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Set grid headers
        /// </summary>
        private void setGridCatedrasHeaders()
        {
            try
            {
                GridResultados.Columns[0].HeaderText = ConfigurationManager.AppSettings["ContentHeaderCatedraComision"];
                GridResultados.Columns[1].HeaderText = ConfigurationManager.AppSettings["ContentHeaderProfesor"];
                GridResultados.Columns[2].HeaderText = ConfigurationManager.AppSettings["ContentHeaderFechaExamen"];
                GridResultados.Columns[3].HeaderText = ConfigurationManager.AppSettings["ContentHeaderHorario"];
                GridResultados.Columns[4].HeaderText = ConfigurationManager.AppSettings["ContentHeaderSeleccion"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "setGridCatedrasHeaders", Path.GetFileName(Request.PhysicalPath));
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
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[0].HeaderText = ConfigurationManager.AppSettings["ContentHeaderMateria"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[1].HeaderText = ConfigurationManager.AppSettings["ContentHeaderCatedraComision"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[2].HeaderText = ConfigurationManager.AppSettings["ContentHeaderProfesor"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[3].HeaderText = ConfigurationManager.AppSettings["ContentHeaderFechaExamen"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[4].HeaderText = ConfigurationManager.AppSettings["ContentHeaderHorario"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[5].HeaderText = ConfigurationManager.AppSettings["ContentHeaderEstadoInscripcion"];
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[6].HeaderText = ConfigurationManager.AppSettings["ContentHeaderEliminar"];
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "setGridCartHeaders", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void SetFechaExamenColumn()
        {
            if (((List<InscripcionActiva>)(Session["inscripcionesActivas"]))[0].IdTipoInscripcion != InscriptionTypeExam)
                ((GridView)(wucCarro.FindControl("GridCarro"))).Columns[3].Visible = false;
        }

        /// <summary>
        /// Method to check maximum selection
        /// </summary>
        private int CheckMaxSelection()
        {
            try
            {
                int cantSelection = 0;

                for (int count = 0; count < GridResultados.Rows.Count; count++)
                {
                    if (((CheckBox)GridResultados.Rows[count].FindControl("check")).Checked)
                    {
                        cantSelection++;
                    }
                }
                return cantSelection;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "CheckMaxSelection", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to add matters to cart
        /// </summary>
        private void AddToCart()
        {
            try
            {
                ShowEmptyCartHeader(false);
                catedras = new List<CatedraComision>();
                catedras = (List<CatedraComision>)Session["catedras"];
                listCarro = new List<Carro>();

                if (maxInscriptions == 0)
                {
                    if (Session["cartLimit"] != null)
                        maxInscriptions = Convert.ToInt32(Session["cartLimit"]);
                }

                if ((List<Carro>)Session["carro"] != null)
                    listCarro = (List<Carro>)Session["carro"];

                if (listCarro.Count < maxInscriptions)
                {
                    if (((Usuario)Session["user"]).Carrera != comboCarrera.SelectedItem.Text)
                    {
                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["MessageCareer"];
                        mpeMessage.Show();
                    }
                    for (int count = 0; count < GridResultados.Rows.Count; count++)
                    {
                        if (((CheckBox)GridResultados.Rows[count].FindControl("check")).Checked)
                        {
                            carro = new Carro();
                            carro.IdTipoInscripcion = catedras[count].IdTipoInscripcion;
                            carro.TurnoInscripcion = catedras[count].TurnoInscripcion;
                            carro.IdVuelta = catedras[count].IdVuelta;
                            carro.IdMateria = catedras[count].IdMateria;
                            carro.CatedraComision = catedras[count].CatedraComisionDescripcion;
                            carro.Materia = comboMateria.SelectedItem.Text.Substring(6);
                            carro.Horario = catedras[count].Horario;
                            carro.Profesor = catedras[count].ProfesorNombreApellido;
                            carro.IdEstadoInscripcion = IdEstadoInscripto;
                            carro.FechaDesdeHasta = catedras[count].FechaDesde;

                            #region OLD VALIDATION
                            //if (listCarro.Find(delegate(Carro c) { if ((c.IdMateria == carro.IdMateria) && (c.IdEstadoInscripcion != IdEstadoInscripto)) return false; else if (c.IdMateria == carro.IdMateria) return true; else return false; }) == null)
                            //{
                            //    listCarro.Add(carro);
                            //    Session.Add("carro", listCarro);
                            //    ((GridView)wucCarro.FindControl("GridCarro")).DataSource = listCarro;
                            //    setGridCartHeaders();
                            //    ((GridView)wucCarro.FindControl("GridCarro")).DataBind();
                            //}
                            //else
                            //{
                            //    lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMatterExistInCart"];
                            //    mpeMessage.Show();
                            //}
                            #endregion

                            #region NEW VALIDATION

                            bool flag;
                            if (listCarro.Count > 0)
                                flag = false;
                            else
                                flag = true;

                            foreach (Carro selected in listCarro)
                            {
                                if (carro.IdMateria == selected.IdMateria && carro.CatedraComision != selected.CatedraComision
                                    && carro.IdVuelta != selected.IdVuelta && selected.IdEstadoInscripcion == IdEstadoBajaAprobada)
                                {
                                    flag = false;
                                    break;
                                }
                                else if (carro.IdMateria == selected.IdMateria && carro.CatedraComision == selected.CatedraComision
                                    && carro.IdVuelta != selected.IdVuelta && (selected.IdEstadoInscripcion == IdEstadoBajaReglamentacion
                                    || selected.IdEstadoInscripcion == IdEstadoBajaSorteo))
                                {
                                    flag = false;
                                    break;
                                }
                                else if (carro.IdMateria == selected.IdMateria && selected.IdEstadoInscripcion == IdEstadoInscripto)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                    flag = true;
                            }
                            #endregion

                            if (flag)
                            {
                                listCarro.Add(carro);
                                Session.Add("carro", listCarro);
                                ((GridView)wucCarro.FindControl("GridCarro")).DataSource = listCarro;
                                setGridCartHeaders();
                                ((GridView)wucCarro.FindControl("GridCarro")).DataBind();
                            }
                            else
                            {
                                foreach (Carro item in listCarro)
                                {
                                    if (item.IdMateria == carro.IdMateria && item.CatedraComision == carro.CatedraComision && item.IdEstadoInscripcion == IdEstadoBajaReglamentacion)
                                    {
                                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMatterBajaReglamentacion"];
                                        mpeMessage.Show();
                                    }
                                    else if (item.IdMateria == carro.IdMateria && item.CatedraComision == carro.CatedraComision && item.IdEstadoInscripcion == IdEstadoBajaSorteo)
                                    {
                                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMatterBajaSorteo"];
                                        mpeMessage.Show();
                                    }
                                    else if (item.IdMateria == carro.IdMateria && item.IdEstadoInscripcion == IdEstadoBajaAprobada)
                                    {
                                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMatterBajaAprobada"];
                                        mpeMessage.Show();
                                    }
                                    else if (item.IdMateria == carro.IdMateria && item.IdEstadoInscripcion == IdEstadoInscripto)
                                    {
                                        lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageMatterExistInCart"];
                                        mpeMessage.Show();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMessagePopUp.Text = ConfigurationManager.AppSettings["ErrorMessageCartAddLimit"];
                    mpeMessage.Show();
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "AddToCart", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Method to set label statuss
        /// </summary>
        private void SetEstado(bool flag)
        {
            divResultados.Visible = flag;
        }

        /// <summary>
        /// Method to show or hide the empty cart header
        /// </summary>
        /// <param name="state"></param>
        private void ShowEmptyCartHeader(bool state)
        {
            try
            {
                ((HtmlGenericControl)(wucCarro.FindControl("headerCart"))).Visible = state;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ShowEmptyCartHeader", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Public Method to refresh control (only used by employee)
        /// </summary>
        /// <param name="state"></param>
        public void RefreshControl()
        {
            comboCarrera.ClearSelection();
            comboMateria.ClearSelection();
            comboDepartamento.ClearSelection();

            comboMateria.Enabled = false;
            comboCarrera.Enabled = false;
            GridResultados.DataSource = null;
            GridResultados.DataBind();
            divResultados.Visible = false;
        }

        #endregion
    }
}