<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaInscripcionAgrupada.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.ConsultaInscripcionAgrupada" %>
<asp:Content ID="TitleContentDefault" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripcionAgrupada"])%>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <asp:UpdatePanel ID="updateResultados" runat="server">
        <ContentTemplate>
            <div class="legendChartIframe">
                <asp:Label ID="lblSeleccione" runat="server" />
            </div>
            <br /><br />
            <div class="legendChartIframe">
                <asp:Label ID="lblTurno" runat="server" />
                <asp:DropDownList ID="ddTurnos" runat="server"/>
                <asp:Label ID="lblVuelta" runat="server" />
                <asp:DropDownList ID="ddVueltas" runat="server"/>
                <asp:Label ID="lblAgrupacion" runat="server"/>
                <asp:DropDownList ID="ddAgrupaciones" runat="server"/>
            </div>
            <br /><br /><br />
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="blackButtonInscripcion" onclick="btnConsultar_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConsultar" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Chart ID="barChart" runat="server" BackColor="#941A63" BackGradientStyle="None"  
        BorderlineWidth="0" Height="640px" Palette="None" PaletteCustomColors="#B20000"
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

</asp:Content>
