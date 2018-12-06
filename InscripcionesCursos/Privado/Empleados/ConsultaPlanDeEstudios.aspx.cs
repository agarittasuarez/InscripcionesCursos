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

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class ConsultaPlanDeEstudios : System.Web.UI.Page
    {
        #region Constants & Variables

        const int IDCARRERACONTADOR = 1;
        const int IDCARRERAADMINISTRADOR = 2;
        const int IDCARRERACONTADORADMIN = 3;
        const string COMBOTEXTFIELDCARRERA = "Nombre";
        const string COMBOVALUEFIELDCARRERA = "IdCarrera";

        const int UserTypeEmployee = 1;
        const int MinUserLevel = 2;

        Usuario user;

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

                    if (!Utils.CheckUserProfileLevel(Session["userEmployee"], MinUserLevel))
                        Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"]);
                }
                Session.Remove("user");
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

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUpControls(Convert.ToInt32(ddlCarrera.SelectedItem.Value));
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            int dni = 0;

            if (int.TryParse(txtDni.Text, out dni))
            {
                FailureText.Visible = false;
                GetStudentPlan(dni);
            }
            else
            {
                FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageLoginDni"];
                FailureText.Visible = true;
                contentSearchStudent.Visible = false;
                contentSearchPlan.Visible = false;
            }

        }

        #endregion

        #region Methods

        private void GetStudentPlan(int dni)
        {
            try
            {
                user = UsuarioDTO.GetUsuario(new Usuario(dni));
                Session.Add("student", user);

                if (user != null)
                {
                    FailureText.Visible = false;

                    if (user.IdCarrera == IDCARRERACONTADORADMIN)
                    {
                        ddlCarrera.DataTextField = COMBOTEXTFIELDCARRERA;
                        ddlCarrera.DataValueField = COMBOVALUEFIELDCARRERA;
                        ddlCarrera.DataSource = CarreraDTO.GetAllCarreras().Where(s => s.IdCarrera != IDCARRERACONTADORADMIN);
                        ddlCarrera.DataBind();
                        contentCarrera.Visible = true;
                        SetUpControls(Convert.ToInt32(ddlCarrera.SelectedItem.Value));
                    }
                    else
                    {
                        contentCarrera.Visible = false;
                        SetUpControls(user.IdCarrera);
                    }

                    txtApellidoNombreResultado.Text = user.ApellidoNombre;
                    txtCarrera.Text = user.Carrera;
                    txtEmail.Text = user.Email;
                    txtEstadoCuenta.Text = user.Estado;
                    contentSearchStudent.Visible = true;
                    contentSearchPlan.Visible = true;
                }
                else
                {
                    FailureText.Text = ConfigurationManager.AppSettings["ErrorMessageDniInexistente"];
                    FailureText.Visible = true;
                    contentSearchStudent.Visible = false;
                    contentSearchPlan.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "GetStudentPlan", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        protected void SetUpControls(int idCarreraSelected)
        {
            HtmlGenericControl containerControl;
            int idCarrera = 0;
            string codCarrera = String.Empty;

            if (idCarreraSelected == IDCARRERACONTADOR)
            {
                contentAdministracion.Visible = false;
                contentAdministracionNuevo.Visible = false;
                contentContador.Visible = true;
                contentContadorNuevo.Visible = true;
                containerControl = contentContador;
                idCarrera = IDCARRERACONTADOR;
                codCarrera = "C";
            }
            else
            {
                contentContador.Visible = false;
                contentContadorNuevo.Visible = false;
                contentAdministracion.Visible = true;
                contentAdministracionNuevo.Visible = true;
                containerControl = contentAdministracion;
                idCarrera = IDCARRERAADMINISTRADOR;
                codCarrera = "A";
            }
            ProcesarMateriasPlan(codCarrera, idCarrera, containerControl);
        }

        protected void ProcesarMateriasPlan(string codCarrera, int idCarrera, HtmlGenericControl containerControl)
        {
            Usuario user = (Usuario)Session["student"];
            List<MateriaCorrelativa> materiasCorrelativasPlanViejo = MateriaCorrelativaDTO.GetMateriasCorrelativasByUser(user.DNI, idCarrera);

            // Procesar los controles de las materias correlativas, plan viejo
            foreach (MateriaCorrelativa materiaCorrelativa in materiasCorrelativasPlanViejo)
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

            List<MateriaCorrelativa> materiasCorrelativasPlanNuevo = MateriaCorrelativaDTO.GetMateriasCorrelativasNuevoPlanByUser(user.DNI, idCarrera);

            // Procesar los controles de las materias correlativas, plan viejo
            foreach (MateriaCorrelativa materiaCorrelativaNuevo in materiasCorrelativasPlanNuevo)
            {
                string cssClass = String.Empty;

                if (materiaCorrelativaNuevo.Estado == "A")
                    cssClass = "planCodMatAprob";
                else if (materiaCorrelativaNuevo.Estado == "B")
                    cssClass = "planCodMatBloqueada";
                else
                    cssClass = "planCodMatHabilitada";

                HtmlGenericControl control = (HtmlGenericControl)containerControl.FindControl(codCarrera + materiaCorrelativaNuevo.IdMateria.ToString());

                if (control != null)
                    control.Attributes["Class"] = cssClass;
            }
        }

        #endregion
        
    }
}