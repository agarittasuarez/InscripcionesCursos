using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DTO;
using System.IO;

namespace InscripcionesCursos
{
    static class Utils
    {
        #region Constants & Variables

        const int mailTypeActivation = 1;
        const int mailTypePasswordRecovery = 2;

        static string enter = "<br />";

        #endregion

        #region Methods

        /// <summary>
        /// Method to check if the user is logged in
        /// </summary>
        /// <param name="session"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static bool CheckLoggedUser(object session, int userType)
        {
            try
            {
                if (session != null)
                {
                    Usuario loggedUser = new Usuario();
                    loggedUser = (Usuario)session;

                    if (loggedUser.IdCargo == userType)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method for check if the user changed the generated password
        /// </summary>
        /// <param name="loggedUser"></param>
        public static bool CheckAccountStatus(object session, int userType)
        {
            try
            {
                Usuario loggedUser = new Usuario();
                loggedUser = (Usuario)session;

                switch (userType)
                {
                    case 1:
                        if ((loggedUser.CambioPrimerLogin))
                            return true;
                        else
                            return false;
                    case 2:
                        if ((loggedUser.CambioPrimerLogin) && (loggedUser.CuentaActivada))
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method for check if the user has the required level for a page
        /// </summary>
        /// <param name="session">Logged user</param>
        /// <param name="minUserLevel">Min level required</param>
        public static bool CheckUserProfileLevel(object session, int minUserLevel = 10)
        {
            try
            {
                Usuario loggedUser = new Usuario();
                loggedUser = (Usuario)session;

                if (loggedUser.IdPerfil <= minUserLevel)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to send activation email
        /// </summary>
        /// <returns></returns>
        public static bool SendMail(Usuario user, int mailType)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                MailSettingsSectionGroup settingsMail = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
                MailMessage message = new MailMessage();
                NetworkCredential credentials = new NetworkCredential(settingsMail.Smtp.From, settingsMail.Smtp.Network.Password);
                
                message.To.Add(user.Email);
                message.From = new MailAddress(settingsMail.Smtp.From);
                switch (mailType)
                {
                    case mailTypeActivation:
                        message.Subject = ConfigurationManager.AppSettings["MailActivationSubject"];
                        message.Body = GenerateActivationMailBody(user);
                        break;
                    case mailTypePasswordRecovery:
                        message.Subject = ConfigurationManager.AppSettings["MailPasswordRecoverSubject"];
                        message.Body = GenerateRecoverMailBody(user);
                        break;
                }
                
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(settingsMail.Smtp.Network.Host, settingsMail.Smtp.Network.Port);
                smtp.UseDefaultCredentials = settingsMail.Smtp.Network.DefaultCredentials;
                smtp.Credentials = credentials;
                smtp.EnableSsl = settingsMail.Smtp.Network.EnableSsl;
                smtp.Send(message);
                
                return true;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SendMail", "Utils.cs");
                return false;
            }           
        }

        /// <summary>
        /// Method to send activation email
        /// </summary>
        /// <returns></returns>
        public static bool SendMail(Usuario user, string subject, string body)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                MailSettingsSectionGroup settingsMail = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
                MailMessage message = new MailMessage();
                NetworkCredential credentials = new NetworkCredential(settingsMail.Smtp.From, settingsMail.Smtp.Network.Password);

                message.To.Add(user.Email);
                message.From = new MailAddress(settingsMail.Smtp.From);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(settingsMail.Smtp.Network.Host, settingsMail.Smtp.Network.Port);
                smtp.UseDefaultCredentials = settingsMail.Smtp.Network.DefaultCredentials;
                smtp.Credentials = credentials;
                smtp.EnableSsl = settingsMail.Smtp.Network.EnableSsl;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "SendMail_Overload", "Utils.cs");
                return false;
            }  
        }

        /// <summary>
        /// Method to create activation email body
        /// </summary>
        /// <param name="user"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GenerateActivationMailBody(Usuario user)
        {
            try
            {
                StringBuilder body = new StringBuilder();
                StringBuilder activationLink = new StringBuilder();
                StringBuilder htmlLink = new StringBuilder();

                activationLink.Append(ConfigurationManager.AppSettings["UrlAcountActivation"] + "?user=");
                activationLink.Append(Cryptography.Encrypt(user.DNI.ToString()) + "&code=" + Cryptography.Encrypt(user.CodigoActivacion.ToString()));
                htmlLink.Append("<a href=\"" + activationLink.ToString() + "\" target=\"_blank\">" + ConfigurationManager.AppSettings["MailActivationClick"] + "</a>");
                                
                body.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                body.Append("<head><title>UNLZ- Activación de Cuenta</title></head>");
                body.Append("<body><img alt=\"UNLZ\" src=\"" + ConfigurationManager.AppSettings["MailBodyHeaderImg"] + "\" />");
                body.Append("<p style=\"font-size: 1em; font-family: Arial, Helvetica, Verdana, sans-serif;\">");
                body.Append(String.Format(ConfigurationManager.AppSettings["MailBodyActivation"], user.ApellidoNombre, enter, enter, htmlLink.ToString(), enter, activationLink.ToString(), enter, enter, enter));
                body.Append("</p></body></html>");

                return body.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to create recovery email body
        /// </summary>
        /// <param name="user"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GenerateRecoverMailBody(Usuario user)
        {
            try
            {
                StringBuilder body = new StringBuilder();
              
                body.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" >");
                body.Append("<head><title>UNLZ- Activación de Cuenta</title></head>");
                body.Append("<body><img alt=\"UNLZ\" src=\"" + ConfigurationManager.AppSettings["MailBodyHeaderImg"] + "\" />");
                body.Append("<p style=\"font-size: 1em; font-family: Arial, Helvetica, Verdana, sans-serif;\">");
                body.Append(String.Format(ConfigurationManager.AppSettings["MailBodyPasswordRecover"], user.ApellidoNombre, enter + enter, enter + enter, user.Password, enter + enter, enter, enter));
                body.Append("</p></body></html>");

                return body.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to generate 4 characters activation code
        /// </summary>
        /// <param name="user"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static int GenerateActivationCode()
        {
            try
            {
                string definitions = ConfigurationManager.AppSettings["RandomDefinitionsActivation"];
                Random random = new Random();
                StringBuilder activationCode = new StringBuilder();

                for (int i = 0; i < 8; i++)
                    activationCode.Append(definitions.Substring(random.Next(definitions.Length), 1));

                return Convert.ToInt32(activationCode.ToString());
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "GenerateActivationCode", "Utils.cs");
                throw ex;
            }
        }

        /// <summary>
        /// Method to validate DNI.
        /// </summary>
        public static bool ValidateDni(string dniValidate)
        {
            int dni;
            bool validDni = true;

            if (dniValidate == string.Empty)
                validDni = false;
            else
            {
                if (!int.TryParse(dniValidate, out dni))
                    validDni = false;
            }
            return validDni;
        }

        /// <summary>
        /// Method to truncate string
        /// </summary>
        public static string TruncateAtWord(string text, int length)
        {
            if (text == null || text.Length < length || text.IndexOf(" ", length) == -1)
                return text;

            return text.Substring(0, text.IndexOf(" ", length)) + "...";
        }

        #endregion
    }
}