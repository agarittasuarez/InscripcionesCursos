<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuestionarioDatosPersonales.aspx.cs" Inherits="InscripcionesCursos.Privado.Alumnos.CuestionarioDatosPersonales" %>
<%@ Register src="~/Controles/wucRelevamientoDatosPersonales.ascx" tagname="RelevamientoDatosPersonales" tagprefix="uc1" %>
<asp:Content ID="TitleContentCuestionarioDatosPersonales" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleCuestionarioDatosPersonales"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="Server" EnablePartialRendering="true" ID="scriptManagerInicio"  />
    <div class="textoAngostoAlumnos">
       <uc1:RelevamientoDatosPersonales ID="relevamientoDatos" runat="server"/>
    </div>
</asp:Content>
