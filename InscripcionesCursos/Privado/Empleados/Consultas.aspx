<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="InscripcionesCursos.Consultas" %>

<asp:Content ID="TitleContentConsultas" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleConsultas"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text="" />
    </div>
    <div class="legendChartIframe">
        <asp:Label ID="lblTotalAlumnos" runat="server" CssClass="legendChart"/><br />
        <asp:Label ID="lblCuentasTramitadas" runat="server" CssClass="legendChart"/><br />
        <asp:Label ID="lblCuentasActivadas" runat="server" CssClass="legendChart"/><br />
        <asp:Label ID="lblSinTramitar" runat="server" CssClass="legendChart"/><br />
        <asp:Label ID="lblMuestreo" runat="server" CssClass="legendChart" /><br />
    </div>
    <div class="chartColumnaDerecha">
        <asp:Chart id="chartConsulta" runat="server" Width="350px" BackColor="Transparent" >
            <Titles>
                <asp:Title Name="titleChartPadron" ForeColor="White" TextStyle="Shadow" />
            </Titles>
            <Legends>
                <asp:Legend Name="legendPadron" Alignment="Far" Docking="Bottom" IsTextAutoFit="False" LegendStyle="Row" BackColor="Transparent" ForeColor="White"/>
            </Legends>
            <Series>
                <asp:Series Name="seriePadron" ChartType="Pie" LabelForeColor="White" />
            </Series>
            <Chartareas>
                <asp:ChartArea Name="chartAreaPadron" BorderWidth="0" BackColor="Transparent"/>
            </Chartareas>
        </asp:Chart>
    </div>
    <div>
        <iframe src="ChartInscripcion.aspx" runat="server" scrolling="no" frameborder="0" id="iframeChartInscripcion" class="contenedorIframe" allowTransparency="true"/>
    </div>
</asp:Content>
