using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using InscripcionesCursos.BE;
using System.IO;
using System.Threading;

using InscripcionesCursos.DTO; 

namespace InscripcionesCursos.Privado.Alumnos
{
    public partial class PlanDeEstudios : System.Web.UI.Page
    {
        #region Constants & Variables

        const int USERTYPESTUDENT = 2;
        const int IDCARRERACONTADOR = 1;
        const int IDCARRERAADMINISTRADOR = 2;
        const int IDCARRERACONTADORADMIN = 3;
        const string COMBOTEXTFIELDCARRERA = "Nombre";
        const string COMBOVALUEFIELDCARRERA = "IdCarrera";

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
       
            try
            {
                if (!IsPostBack)
                {
                    if (!Utils.CheckLoggedUser(Session["user"], USERTYPESTUDENT))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                    if (!Utils.CheckAccountStatus(Session["user"], USERTYPESTUDENT))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudentPasswordChange"]);

                    lblTitulo.Text = String.Format(ConfigurationManager.AppSettings["ContentMainTitlePlanEstudio"], ((Usuario)(Session["user"])).Carrera);

                    if (((Usuario)(Session["user"])).IdCarrera == IDCARRERACONTADORADMIN)
                    {
                        ddlCarrera.DataTextField = COMBOTEXTFIELDCARRERA;
                        ddlCarrera.DataValueField = COMBOVALUEFIELDCARRERA;
                        ddlCarrera.DataSource = CarreraDTO.GetAllCarreras().Where(s => s.IdCarrera != IDCARRERACONTADORADMIN);
                        ddlCarrera.DataBind();
                        contentCarrera.Visible = true;
                        SetUpControls(Convert.ToInt32(ddlCarrera.SelectedItem.Value));
                    }
                    else
                        SetUpControls(((Usuario)(Session["user"])).IdCarrera);

                    #region Old
                    //#region Procesar materias aprobadas

                    ////Usuario user = (Usuario)Session["user"];
                    //List<Materia> materiasAprobadas = AnaliticoDTO.GetMateriasAprobadas(user.DNI, idCarrera);

                    //// Procesar los controles de las materias aprobadas seteando el class = "planCodMatAprob"
                    //foreach (Materia materiaAprobada in materiasAprobadas)
                    //{
                    //    HtmlGenericControl control = (HtmlGenericControl)containerControl.FindControl(codCarrera + materiaAprobada.IdMateria.ToString());
                    //    control.Attributes["Class"] = "planCodMatAprob";
                    //}

                    //#endregion
                    #endregion

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

        protected void SetUpControls(int idCarreraSelected)
        {
            HtmlGenericControl containerControl;
            int idCarrera = 0;
            string codCarrera = String.Empty;

            if (idCarreraSelected == IDCARRERACONTADOR)
            {
                contentAdministracion.Visible = false;
                contentContador.Visible = true;
                containerControl = contentContador;
                idCarrera = IDCARRERACONTADOR;
                codCarrera = "C";
            }
            else
            {
                contentContador.Visible = false;
                contentAdministracion.Visible = true;
                containerControl = contentAdministracion;
                idCarrera = IDCARRERAADMINISTRADOR;
                codCarrera = "A";
            }
            ProcesarMaterias(codCarrera, idCarrera, containerControl);
        }

        protected void ProcesarMaterias(string codCarrera, int idCarrera, HtmlGenericControl containerControl)
        {
            Usuario user = (Usuario)Session["user"];
            List<MateriaCorrelativa> materiasCorrelativas = MateriaCorrelativaDTO.GetMateriasCorrelativasByUser(user.DNI, idCarrera);

            // Procesar los controles de las materias correlativas
            foreach (MateriaCorrelativa materiaCorrelativa in materiasCorrelativas)
            {
                string cssClass = String.Empty;

                if (materiaCorrelativa.Estado == "A")
                    cssClass = "planCodMatAprob";
                else if (materiaCorrelativa.Estado == "B")
                    cssClass = "planCodMatBloqueada";
                else
                    cssClass = "planCodMatHabilitada";

                HtmlGenericControl control = (HtmlGenericControl)containerControl.FindControl(codCarrera + materiaCorrelativa.IdMateria.ToString());

                if (control != null)
                    control.Attributes["Class"] = cssClass;
            }
        }

        #endregion

        #region Events
        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUpControls(Convert.ToInt32(ddlCarrera.SelectedItem.Value));
        }

        #endregion
    }
}