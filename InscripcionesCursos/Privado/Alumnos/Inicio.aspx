<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="InscripcionesCursos.Inicio" %>
<%@ Register src="~/Controles/wucRelevamientoLimitaciones.ascx" tagname="Relevamiento" tagprefix="uc1" %>
<asp:Content ID="TitleContentInicio" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInicio"])%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="Server" EnablePartialRendering="true" ID="scriptManagerInicio" />
    <div class="textoAngostoAlumnos">
        <uc1:Relevamiento runat="server" ID="ucRelevamiento"/>
        <p><%= ConfigurationManager.AppSettings["ContentInicioInformes"]%></p>
    </div>
</asp:Content>