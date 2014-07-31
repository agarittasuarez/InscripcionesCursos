<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambioTextos.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.CambioTextos"  ValidateRequest="false"%>
<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleCambioTextos"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <script type="text/javascript">
        var HighlightAnimations = {};
        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function AssemblyFileUpload_Complete(sender, args) {
            var filename = args.get_fileName();
            var ext = filename.substring(filename.lastIndexOf(".") + 1);

            if (ext.toLowerCase() != 'txt') {
                $('#MainContent_lblMessagePopUp').text('<%= System.Configuration.ConfigurationManager.AppSettings["MessageProcessInvalidFileFormat"] %>');
                $('#MainContent_tabContainer_tabPanelStart_asyncFile_ctl04').css('background-color', '#FFADAD');
                $find('pnMessage').show();
            }
            else
                $('#MainContent_tabContainer_tabPanelStart_asyncFile_ctl04').css('background-color', '#00FF00');
        }
    </script>


    <asp:UpdatePanel ID="updateProceso" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="tituloContenido">
                <asp:Label ID="lblTitulo" runat="server" Text="" />
            </div>          
            <div class="divTabContainer">
                <asp:Label ID="invisibleTarget" runat="server" Style="display: none" />

                <ajaxToolkit:ModalPopupExtender ID="mpeMessage" runat="server"
                    TargetControlID="invisibleTarget"
                    PopupControlID="pnMessage" BehaviorID="pnMessage"
	                PopupDragHandleControlID="PopupHeader" Drag="true" 
	                BackgroundCssClass="modalPopUpBG">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel id="pnMessage" style="display: none" runat="server" CssClass="modalPopUp">
                    <div class="modalPopUpBody">
                        <asp:Label ID="lblMessagePopUp" runat="server" />
                    </div>
                    <div id="buttonContainer" class="modalPopUpButtons" runat="server">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
		            </div>
                </asp:Panel>

                <ajaxToolkit:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-fuschia" Height="380px">
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelStart" OnDemandMode="Once" >
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosPortal"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="float:left">
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreInscripcion1" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion1"]%></asp:Label>
                                    <asp:TextBox ID="txtPreInscripcion1" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreInscripcion2" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion2"]%></asp:Label>
                                    <asp:TextBox ID="txtPreInscripcion2" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreHistorico" runat="server"><%= ConfigurationManager.AppSettings["LabelPreHistorico"]%></asp:Label>
                                    <asp:TextBox ID="txtPreHistorico" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelActivas" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosComprobantes"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelLog" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabPaginas"]%>
                        </HeaderTemplate>
                        <ContentTemplate>   
                            <div style="float:left">
                                <div class="labelRadioButton">
                                    <asp:Label ID="lblHabilitaHistorico" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaHistorico"]%></asp:Label>
                                    <div class="radioButton">
                                        <asp:RadioButtonList id="listRBHabilitaImprimirHistorico" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Habilitar"></asp:ListItem>
                                            <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="labelRadioButton">
                                        <asp:Image ID="imgEstadoHistorico" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="labelRadioButton">
                                    <asp:Label ID="lblHabilitarPortal" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaPortalMantenimiento"]%></asp:Label>
                                    <div class="radioButton">
                                        <asp:RadioButtonList id="listRBHabilitaPortal" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Habilitar"></asp:ListItem>
                                            <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="labelRadioButton">
                                        <asp:Image ID="imgEstadoPortal" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="labelRadioButton">
                                    <asp:Label ID="lblHabilitarInscripcion" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaInscripcionAlumno"]%></asp:Label>
                                    <div class="radioButton">
                                        <asp:RadioButtonList id="listRBHabilitaInscripcion" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Habilitar"></asp:ListItem>
                                            <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="labelRadioButton">
                                        <asp:Image ID="imgEstadoInscripcion" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <div class="contenedorBtnInscripcion">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="blackButtonInscripcion" onclick="btnGuardar_Click" />
                </div> 
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>