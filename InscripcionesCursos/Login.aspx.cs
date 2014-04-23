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
    public partial class Login : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        const int UserTypeStudent = 2;

        #endregion

        #region Objects

        Usuario loggedUser;

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["MaintenanceFlag"]))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlMaintenance"]);
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
        /// Login Authenticate Event.
        /// </summary>
        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                int dni;
                bool incorrectData = false;
                Usuario user = new Usuario();
                loggedUser = new Usuario();

                if (login.UserName == string.Empty)
                {
                    login.FailureText = ConfigurationManager.AppSettings["ErrorMessageLoginDni"];
                    e.Authenticated = false;
                    incorrectData = true; 
                }
                else
                {
                    if (!int.TryParse(login.UserName, out dni))
                    {
                        login.FailureText = ConfigurationManager.AppSettings["ErrorMessageLoginDni"];
                        e.Authenticated = false;
                        incorrectData = true;
                    }
                    #region Old Validate DNI Min Length
                    //else
                    //{
                    //    if (login.UserName.Length < 8)
                    //    {
                    //        login.FailureText = ConfigurationManager.AppSettings["ErrorMessageLoginDni"];
                    //        e.Authenticated = false;
                    //        incorrectData = true; 
                    //    }
                    //}
                    #endregion
                }

                if (login.Password == string.Empty)
                {
                    login.FailureText = login.FailureText + "<br>" + ConfigurationManager.AppSettings["ErrorMessageLoginPassword"];
                    e.Authenticated = false;
                    incorrectData = true; 
                }
                else
                {
                    if (login.Password.Length < 6)
                    {
                        login.FailureText = login.FailureText + "<br>" + ConfigurationManager.AppSettings["ErrorMessageLoginPassword"];
                        e.Authenticated = false;
                        incorrectData = true; 
                    }
                }

                if (!incorrectData)
                {
                    user.DNI = Convert.ToInt32(login.UserName);
                    user.Password = login.Password;

                    loggedUser = UsuarioDTO.ValidateLogin(user);


                    if (loggedUser.DNI != 0)
                    {
                        if (loggedUser.IdCargo == 2)
                            Session.Add("user", loggedUser);
                        else
                            Session.Add("userEmployee", loggedUser);
                        e.Authenticated = true;
                    }
                    else
                    {
                        login.FailureText = ConfigurationManager.AppSettings["ErrorMessageLoginCredentials"];
                        e.Authenticated = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "Login_Authenticate", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Login LoggedIn redirect Event.
        /// </summary>
        protected void Login_LoggedIn(object sender, EventArgs e)
        {
            try
            {
                switch (loggedUser.IdCargo)
                {
                    case UserTypeEmployee:
                        login.DestinationPageUrl = Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeeGenerarClaves"];
                        break;
                    case UserTypeStudent:
                        login.DestinationPageUrl = Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlStudent"];
                        break;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "Login_LoggedIn", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        /// <summary>
        /// Evento to redirect Password recovery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlPasswordRecovery"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "btnForgotPassword_Click", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}
