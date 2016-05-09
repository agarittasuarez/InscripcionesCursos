<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionCursos.aspx.cs" Inherits="InscripcionesCursos.InscripcionCursos" %>
<%@ Register src="~/Controles/wucInscripcion.ascx" tagname="Inscripcion" tagprefix="uc1" %>
<asp:Content ID="TitleContentInscripcion" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div  class="contenedorCentro">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="scriptManagerInscripciones" />
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleInscripcion"] %></asp:Label>
        </div>
        <uc1:Inscripcion id="ddInscripciones" runat="server"/>
        <div class="mensajeNoDisponible" id="divNoDisponible" runat="server" visible="false">
            <asp:Label ID="lblNoDisponible" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>