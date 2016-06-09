<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConstanciaAlumnoRegular.aspx.cs" Inherits="InscripcionesCursos.Privado.Alumnos.ConstanciaAlumnoRegular" %>
<asp:Content ID="TitleContentConstanciaAlumnoRegular" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleConstanciaAlumnoRegular"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleConstanciaAlumnoRegular"] %></asp:Label>
        </div>
        <div id="divDatosConstancia" runat="server">
            <div id="actionForm" runat="server">
                <asp:Label ID="lblDirigidoA" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDirigidoA"]%></asp:Label>
                <input id="inputText" runat="server" clientidmode="Static" type="text" style="height:16px; width:260px" class="preText" value="Ingrese el nombre aquí" onfocus="clearTextbox('Ingrese el nombre aquí');"/>
                <asp:RequiredFieldValidator ID="textRequired" runat="server" ControlToValidate="inputText" ErrorMessage="Debe ingresar un nombre" ForeColor="Red" ToolTip="Debe ingresar un nombre" >*</asp:RequiredFieldValidator>
                <div class="contenedorBtnInscripcion">
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="blackButtonInscripcion" onclick="btnImprimir_Click" />
                </div>
            </div>
            <asp:Label ID="lblEstado" runat="server" Visible="false"><%= ConfigurationManager.AppSettings["ErrorMessageInactiveUser"]%></asp:Label>
        </div>
        <div id="divNoDisponible" runat="server" visible="false">
            <asp:Label ID="lblMsjNoDisponible" runat="server"/>
        </div>
    </div>
</asp:Content>
