<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proceso.aspx.cs" Inherits="InscripcionesCursos.Proceso" %>
<asp:Content ID="TitleContentExtraccionDatos" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleExtraccionDatos"])%>
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
    <asp:UpdatePanel ID="updateProceso" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tabContainer" />
        </Triggers>
        <ContentTemplate>            
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

                <ajaxToolkit:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-fuschia" Height="400px">
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelStart" OnDemandMode="Once" >
                        <HeaderTemplate>
                            <%= System.Configuration.ConfigurationManager.AppSettings["LabelTabIniciarProceso"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="procesoLineInfo">
                                <asp:Label ID="lblTipoImportacion" runat="server" CssClass="procesoLabelInfo" ></asp:Label>
                                <asp:DropDownList ID="cboTipoImportacion" runat="server" 
                                    CausesValidation="True" DataTextField="Descripcion" 
                                    DataValueField="IdTipoImportacion" AutoPostBack="True"
                                    onselectedindexchanged="cboTipoImportacion_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="procesoLineInfo">
                                <table class="tableAsyncFileUpload" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblArchivo" runat="server" CssClass="procesoLabelInfo"></asp:Label>
                                        </td>
                                        <td class="tdFileInput">
                                            <ajaxToolkit:AsyncFileUpload ID="asyncFile" runat="server"
                                                Width="400px" UploaderStyle="Modern" ErrorBackColor="#FFADAD"
                                                UploadingBackColor="#CACEDB" CompleteBackColor="#00FF00" ThrobberID="myThrobber"
                                                onuploadedcomplete="asyncFile_UploadedComplete" OnClientUploadComplete="AssemblyFileUpload_Complete"
                                                PersistFile="True" />
                                        </td>
                                        <td class="tdFileLoading">
                                            <asp:Label ID="myThrobber" runat="server" style="display: none;">
                                                <img src="../../img/ico_loading2.gif" align="middle" alt="loading" />
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="procesoLineInfo">
                                <asp:Label ID="lblTipoInscripcion" runat="server" CssClass="procesoLabelInfo" ></asp:Label>
                                <asp:DropDownList ID="cboTipoInscripcion" runat="server" 
                                    CausesValidation="True" DataTextField="Descripcion"
                                    DataValueField="IdTipoInscripcion" AutoPostBack="True" 
                                    onselectedindexchanged="cboTipoInscripcion_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="procesoLineInfo">
                                <asp:Label ID="lblTurnoInscripcion" runat="server" CssClass="procesoLabelInfo" ></asp:Label>
                                <asp:DropDownList ID="cboTurnoInscripcion" runat="server"
                                    CausesValidation="True" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="procesoLineInfo">
                                <asp:Label ID="lblVueltaInscripcion" runat="server" CssClass="procesoLabelInfo" ></asp:Label>
                                <asp:DropDownList ID="cboVueltaInscripcion" runat="server" 
                                    CausesValidation="True" DataTextField="IdVuelta"
                                    DataValueField="IdVuelta" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="procesoLineInfo">
                                <asp:CheckBox ID="chkProgramar" runat="server" AutoPostBack="True"
                                    Text="Programar Importación Diferida" CssClass="procesoLabelInfo" 
                                    oncheckedchanged="chkProgramar_CheckedChanged" />
                                <asp:TextBox ID="txtFechaProgramada" runat="server" Enabled="False"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="tktFecha" runat="server" 
                                    TargetControlID="txtFechaProgramada" Format="dd/MM/yyyy hh:mm" Enabled="True" />                                    
                            </div>
                            <asp:Button id="btnGuardar" runat="server" Text="Guardar" 
                                ValidationGroup="ArchivoImport" onclick="btnGuardar_Click" CssClass="greenButton" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelActivas" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= System.Configuration.ConfigurationManager.AppSettings["LabelTabProcesoActivo"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="gridProcess">
                                <asp:GridView ID="gridActiveProcess" runat="server" AutoGenerateColumns="False" 
                                    BorderWidth="0px" EmptyDataText="No se encontraron importaciones en la base de datos"
                                    OnPageIndexChanging="gridActiveProcess_PageIndexChanging" OnRowDataBound="gridActiveProcess_RowDataBound"
                                    AllowPaging="true" PageSize="8">
                                    <HeaderStyle CssClass="gridProcessHeader" />
                                    <RowStyle CssClass="gridProcessRowBack" />
                                    <Columns>
                                        <asp:BoundField DataField="IdImportacion" HeaderText="Id Importacion">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ArchivoImportacion" HeaderText="Archivo">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Tipo Importacion">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UsuarioImportador" HeaderText="Usuario">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaProgramadaImportacion" HeaderText="Fecha Programada">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdTipoInscripcion" HeaderText="Tipo Inscripcion">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TurnoInscripcion" HeaderText="Turno Inscripcion">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdVuelta" HeaderText="Vuelta">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProcesoActivo" HeaderText="Activo">                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LogError" HeaderText="Error">                                        
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:PlaceHolder ID="phButton" runat="server">
                                                </asp:PlaceHolder>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="Numeric"/>
                                    <PagerStyle CssClass="gridProcessPager" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelLog" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= System.Configuration.ConfigurationManager.AppSettings["LabelTabLogProceso"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="logErrorComboLine">
                                <asp:Label id="lblTipoImportacionError" runat="server"/>
                                <asp:DropDownList ID="cboTipoImportacionError" runat="server" 
                                    CausesValidation="True" DataTextField="Descripcion" 
                                    DataValueField="IdTipoImportacion" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboTipoImportacionError_SelectedIndexChanged" ></asp:DropDownList>
                                <asp:Label id="lblTipoInscripcionError" runat="server"/>
                                <asp:DropDownList ID="cboTipoInscripcionError" runat="server" 
                                    CausesValidation="True" DataTextField="Descripcion" Enabled="false"
                                    DataValueField="IdTipoInscripcion" AutoPostBack="True" 
                                    OnSelectedIndexChanged="cboTipoInscripcionError_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="logErrorComboLine">
                                <asp:Label id="lblTurnoInscripcionError" runat="server"/>
                                <asp:DropDownList ID="cboTurnoInscripcionError" runat="server" 
                                    CausesValidation="True" AutoPostBack="True" Enabled="false"
                                    OnSelectedIndexChanged="cboTurnoInscripcionError_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Label id="lblIdVueltaError" runat="server"/>
                                <asp:DropDownList ID="cboVueltaInscripcionError" runat="server" 
                                    CausesValidation="True" DataTextField="IdVuelta" Enabled="false"
                                    DataValueField="IdVuelta" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboVueltaInscripcionError_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="gridProcess">
                                <asp:GridView ID="gridLogProcess" runat="server" AutoGenerateColumns="False" 
                                    BorderWidth="0px" EmptyDataText="No hay errores registrados en la base de datos"
                                    OnPageIndexChanging="gridLogProcess_PageIndexChanging"
                                    OnRowDataBound="gridLogProcess_RowDataBound" AllowPaging="true" PageSize="8">
                                    <HeaderStyle CssClass="gridProcessHeader" />
                                    <RowStyle CssClass="gridProcessRowBack" />
                                    <Columns>
                                        <asp:BoundField DataField="IdImportacion" HeaderText="Id Importacion">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LogError" HeaderText="Error Logueado">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="+Info" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:PlaceHolder ID="phErrorDetail" runat="server">
                                                </asp:PlaceHolder>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="Numeric"/>
                                    <PagerStyle CssClass="gridProcessPager" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tabPanelExport" runat="server" OnDemandMode="Always" >
                        <HeaderTemplate>
                            <%= System.Configuration.ConfigurationManager.AppSettings["LabelTabExportFiles"]%>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="float:left">
                                <ajaxToolkit:Accordion
                                    ID="MyAccordion"
                                    runat="Server"
                                    SelectedIndex="0"
                                    HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent"
                                    AutoSize="None"
                                    FadeTransitions="true"
                                    TransitionDuration="250"
                                    FramesPerSecond="40"
                                    RequireOpenedPane="false"
                                    SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <ajaxToolkit:AccordionPane
                                            runat="server"
                                            id="accordPaneAlumnos"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                <asp:Label ID="lblAccordionAlumnos" runat="server">
                                                    <%= System.Configuration.ConfigurationManager.AppSettings["LabelAccordionAlumnos"]%>
                                                </asp:Label>
                                            </Header>
                                            <Content>
                                                    <asp:UpdatePanel ID="upExtractAlumnos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnExtraerAlumnos" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div>
                                                                <asp:Label ID="lblExtraerAlumnos" runat="server" CssClass="labelDescripcion">
                                                                    <%= ConfigurationManager.AppSettings["ContentMainTitleExtraccionAlumnos"]%>
                                                                </asp:Label>
                                                                <asp:Button ID="btnExtraerAlumnos" runat="server" Text="" CssClass="buttonDownload" OnClick="btnExtraerAlumnos_Click" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </Content>
                                        </ajaxToolkit:AccordionPane>

                                        <ajaxToolkit:AccordionPane
                                            runat="server"
                                            id="accordPaneInscripciones"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                <asp:Label ID="lblAccordionInscripciones" runat="server">
                                                    <%= System.Configuration.ConfigurationManager.AppSettings["LabelAccordionInscripciones"]%>
                                                </asp:Label>
                                            </Header>
                                            <Content>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnExtraerInscripciones" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <div>
                                                            <asp:Label ID="lblExtraerInscripciones" runat="server" CssClass="labelDescripcionInscripciones">
                                                                <%= ConfigurationManager.AppSettings["ContentMainTitleExtraccionInscripciones"]%>
                                                            </asp:Label>
                                                            <asp:DropDownList ID="cboInscripciones" runat="server" onselectedindexchanged="cboInscripciones_SelectedIndexChanged" AutoPostBack="True" />
                                                            <asp:DropDownList ID="cboInscripcionesVuelta" runat="server" Enabled="false" />
                                                            <asp:Button ID="btnExtraerInscripciones" runat="server" Text="" CssClass="buttonDownload" OnClick="btnExtraerInscripciones_Click"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </Content>
                                        </ajaxToolkit:AccordionPane>
                                    </Panes>
                                </ajaxToolkit:Accordion>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
