using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Xml;
using System.IO;
using System.Threading;

namespace InscripcionesCursos.Privado.Empleados
{
    public partial class CambioTextos : System.Web.UI.Page
    {
        #region Constants & Variables

        const int UserTypeEmployee = 1;
        const string IcoPlus = "../../img/ico_plus.png";
        const string IcoMinus = "../../img/ico_minus.png";

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.CheckLoggedUser(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);

                if (!Utils.CheckAccountStatus(Session["userEmployee"], UserTypeEmployee))
                    Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlEmployeePasswordChange"]);
                
                if (!IsPostBack)
                    LoadTexts();
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
        /// Event to save texts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ChangeTexts();
        }

        /// <summary>
        /// Event to accept modal popup message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            mpeMessage.Hide();
        }

        #endregion

        #region Methods

        private void LoadTexts()
        {
            try
            {
                lblTitulo.Text = ConfigurationManager.AppSettings["TitleCambioTextos"];
                txtPaginaInicio1.Text = ConfigurationManager.AppSettings["ContentDefaultBodyPart1"];
                txtPaginaInicio2.Text = ConfigurationManager.AppSettings["ContentDefaultBodyPart2"];
                txtInformacionAlumnos.Text = ConfigurationManager.AppSettings["ContentInicioInformes"];
                txtPreInscripcion1.Text = ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart1"];
                txtPreInscripcion2.Text = ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart2"];
                txtPreHistorico.Text = ConfigurationManager.AppSettings["ContentPreHistorialInscripcion"];
                txtPieComprobantePromo.Text = ConfigurationManager.AppSettings["ContentFooterHistoricoEmail"];
                txtPieComprobanteVerano.Text = ConfigurationManager.AppSettings["ContentFooterHistoricoEmailCursoVerano"];
                txtPieComprobanteExamen.Text = ConfigurationManager.AppSettings["ContentFooterHistoricoEmailExamen"];

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["InscriptionHistoricDisable"]))
                {
                    listRBHabilitaImprimirHistorico.SelectedIndex = 1;
                    imgEstadoHistorico.ImageUrl = IcoMinus;
                }
                else
                {
                    listRBHabilitaImprimirHistorico.SelectedIndex = 0;
                    imgEstadoHistorico.ImageUrl = IcoPlus;
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["MaintenanceFlag"]))
                {
                    listRBHabilitaPortal.SelectedIndex = 0;
                    imgEstadoPortal.ImageUrl = IcoPlus;
                }
                else
                {
                    listRBHabilitaPortal.SelectedIndex = 1;
                    imgEstadoPortal.ImageUrl = IcoMinus;
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["InscripcionDisable"]))
                {
                    listRBHabilitaInscripcion.SelectedIndex = 1;
                    imgEstadoInscripcion.ImageUrl = IcoMinus;
                }
                else
                {
                    listRBHabilitaInscripcion.SelectedIndex = 0;
                    imgEstadoInscripcion.ImageUrl = IcoPlus;
                }
            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "LoadTexts", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        private void ChangeTexts()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "AppCustom.config");
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("appSettings");

                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.NodeType != XmlNodeType.Comment)
                    {
                        switch (childNode.Attributes["key"].Value)
                        {
                            case "ContentDefaultBodyPart1":
                                childNode.Attributes["value"].Value = txtPaginaInicio1.Text;
                                break;
                            case "ContentDefaultBodyPart2":
                                childNode.Attributes["value"].Value = txtPaginaInicio2.Text;
                                break;
                            case "ContentPreInscripcionBodyPart1":
                                childNode.Attributes["value"].Value = txtPreInscripcion1.Text;
                                break;
                            case "ContentPreInscripcionBodyPart2":
                                childNode.Attributes["value"].Value = txtPreInscripcion2.Text;
                                break;
                            case "ContentPreHistorialInscripcion":
                                childNode.Attributes["value"].Value = txtPreHistorico.Text;
                                break;
                            case "ContentFooterHistoricoEmail":
                                childNode.Attributes["value"].Value = txtPieComprobantePromo.Text;
                                break;
                            case "ContentFooterHistoricoEmailCursoVerano":
                                childNode.Attributes["value"].Value = txtPieComprobanteVerano.Text;
                                break;
                            case "ContentFooterHistoricoEmailExamen":
                                childNode.Attributes["value"].Value = txtPieComprobanteExamen.Text;
                                break;
                            case "ContentInicioInformes":
                                childNode.Attributes["value"].Value = txtInformacionAlumnos.Text;
                                break;
                            case "InscriptionHistoricDisable":
                                if (listRBHabilitaImprimirHistorico.SelectedIndex == 0)
                                    childNode.Attributes["value"].Value = "false";
                                else
                                    childNode.Attributes["value"].Value = "true";
                                break;
                            case "MaintenanceFlag":
                                if (listRBHabilitaPortal.SelectedIndex == 0)
                                    childNode.Attributes["value"].Value = "true";
                                else
                                    childNode.Attributes["value"].Value = "false";
                                break;
                            case "InscripcionDisable":
                                if (listRBHabilitaInscripcion.SelectedIndex == 0)
                                    childNode.Attributes["value"].Value = "false";
                                else
                                    childNode.Attributes["value"].Value = "true";
                                break;
                        }
                    }
                }
                xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "AppCustom.config");

                HttpRuntime.Close();
                Response.Redirect(Page.ResolveUrl("~") + ConfigurationManager.AppSettings["UrlLogin"]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                LogWriter log = new LogWriter();
                log.WriteLog(ex.Message, "ChangeTexts", Path.GetFileName(Request.PhysicalPath));
                throw ex;
            }
        }

        #endregion
    }
}