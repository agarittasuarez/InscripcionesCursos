<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaInscripcionAgrupada.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.ConsultaInscripcionAgrupada" %>
<asp:Content ID="TitleContentDefault" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripcionAgrupada"])%>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
    <div style="height: 800px">
        <asp:ScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
        <asp:UpdatePanel ID="updateResultados" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
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

                <div class="legendChartIframe">
                    <asp:Label ID="lblSeleccione" runat="server" />
                </div>
                <br /><br />
                <div class="legendChartIframe">
                    <asp:Label ID="lblTurno" runat="server" />
                    <asp:DropDownList ID="ddTurnos" runat="server" OnSelectedIndexChanged="ddTurnos_SelectedIndexChanged" AutoPostBack="true"/>
                    <asp:Label ID="lblVuelta" runat="server"/>
                    <asp:DropDownList ID="ddVueltas" runat="server" Enabled="false" AutoPostBack="true"/>
                    <asp:Label ID="lblAgrupacion" runat="server"/>
                    <asp:DropDownList ID="ddAgrupaciones" runat="server" Enabled="false" AutoPostBack="true"/>
                </div>
                <br /><br /><br />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="blackButtonInscripcion" onclick="btnConsultar_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConsultar" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:Chart ID="barChart" runat="server" BackColor="#941A63" BackGradientStyle="None"  
            BorderlineWidth="0" Height="440px" Palette="None" PaletteCustomColors="#B20000"
            Width="1004px" BorderlineColor="#004275" Visible="false">  
            <BorderSkin BackColor="Transparent" PageColor="Transparent" SkinStyle="Emboss" />
            <Series>  
                <asp:Series Name="Series1"  YValuesPerPoint="10">  
                </asp:Series>  
            </Series>  
            <ChartAreas>  
                <asp:ChartArea Name="ChartArea1" BackColor="#8B8B8B">  
                </asp:ChartArea>  
            </ChartAreas>  
        </asp:Chart>
    </div>

</asp:Content>
