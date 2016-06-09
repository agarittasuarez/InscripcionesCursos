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

                <ajaxToolkit:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-fuschia" Height="420px">
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelStartPage" OnDemandMode="Once">
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosPaginaInicio"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="fixRichTextArea">
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblTextoInicio1" runat="server"><%= ConfigurationManager.AppSettings["LabelPaginaInicio1"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPaginaInicio1" runat="server" Width="540px" Height="150px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="htmlExtenderTxtPaginaInicio1" TargetControlID="txtPaginaInicio1" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblTextoInicio2" runat="server"><%= ConfigurationManager.AppSettings["LabelPaginaInicio2"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPaginaInicio2" runat="server" Width="540px" Height="150px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="htmlExtenderTxtPaginaInicio2" TargetControlID="txtPaginaInicio2" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelInicioInforme" OnDemandMode="Once">
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosPaginaInicioAlumnos"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="fixRichTextArea">
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblTextoInformacionAlumnos" runat="server"><%= ConfigurationManager.AppSettings["LabelInformacionAlumnos"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtInformacionAlumnos" runat="server" Width="540px" Height="150px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender4" TargetControlID="txtInformacionAlumnos" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                                <ajaxToolkit:JustifyCenter />
                                                <ajaxToolkit:JustifyFull />
                                                <ajaxToolkit:JustifyRight />
                                                <ajaxToolkit:JustifyLeft />
                                                <ajaxToolkit:CreateLink />
                                                <ajaxToolkit:InsertImage />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelInscriptPage" OnDemandMode="Once" >
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosPaginaInscripcion"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="fixRichTextArea">
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreInscripcion1" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion1"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPreInscripcion1" runat="server" Width="540px" Height="120px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="htmlEditExt1" TargetControlID="txtPreInscripcion1" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreInscripcion2" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion2"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPreInscripcion2" runat="server" Width="540px" Height="120px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="htmlEditExt2" TargetControlID="txtPreInscripcion2" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPreHistorico" runat="server"><%= ConfigurationManager.AppSettings["LabelPreHistorico"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPreHistorico" runat="server" Width="540px" Height="120px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="htmlEditExt3" TargetControlID="txtPreHistorico" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelVouchers" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= ConfigurationManager.AppSettings["LabelTabTextosComprobantes"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="fixRichTextArea">
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPieComprobantePromo" runat="server"><%= ConfigurationManager.AppSettings["LabelPieComprobantePromo"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPieComprobantePromo" runat="server" Width="540px" Height="125px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="txtPieComprobantePromo" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPieComprobanteVerano" runat="server"><%= ConfigurationManager.AppSettings["LabelPieComprobanteVerano"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPieComprobanteVerano" runat="server" Width="540px" Height="125px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender2" TargetControlID="txtPieComprobanteVerano" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="labelCambioTexto">
                                    <asp:Label ID="lblPieComprobanteExamen" runat="server"><%= ConfigurationManager.AppSettings["LabelPieComprobanteExamen"]%></asp:Label>
                                    <div class="fixRichTextArea">
                                        <asp:TextBox ID="txtPieComprobanteExamen" runat="server" Width="540px" Height="125px" Wrap="true" TextMode="MultiLine" style="resize:none" />
                                        <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender3" TargetControlID="txtPieComprobanteExamen" DisplaySourceTab="true" runat="server">
                                            <Toolbar>
                                                <ajaxToolkit:Bold />
                                                <ajaxToolkit:Italic />
                                                <ajaxToolkit:Underline />
                                            </Toolbar>
                                        </ajaxToolkit:HtmlEditorExtender>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelComponents" runat="server" OnDemandMode="Always" >
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
                                <br />
                                <div class="labelRadioButton">
                                    <asp:Label ID="lblHabilitarConstanciaRegularidad" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaConstanciaRegularidad"]%></asp:Label>
                                    <div class="radioButton">
                                        <asp:RadioButtonList id="listRBHabilitaConstancia" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Habilitar"></asp:ListItem>
                                            <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="labelRadioButton">
                                        <asp:Image ID="imgEstadoConstancia" runat="server" />
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