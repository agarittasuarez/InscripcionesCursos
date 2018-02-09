using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.Configuration;

namespace InscripcionesCursos.Controles
{
    public partial class wucRelevamientoDatosPersonales : System.Web.UI.UserControl
    {
        #region Constants

        //Response.Redirect(ConfigurationManager.AppSettings["UrlStudentInscripcion"]);

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Events

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            var user = (Usuario)Session["user"];

            user.Domicilio = txtDomicilio.Text.Trim();
            user.Localidad = txtLocalidad.Text.Trim();
            user.CP = txtCP.Text.Trim();
            user.Celular = txtCaracteristica.Text.Trim().Replace("_", "") + txtCelular.Text.Trim();

            UsuarioDTO.Update(user);
            Session.Add("user", user);
            Response.Redirect(ConfigurationManager.AppSettings["UrlStudentInscripcion"]);
        }

        #endregion

        #region Methods



        #endregion
    }
}