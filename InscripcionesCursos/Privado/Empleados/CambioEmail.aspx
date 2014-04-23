<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambioEmail.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.CambioEmail" %>
<%@ Register src="~/Controles/wucCambioEmail.ascx" tagname="FormCambioEmail" tagprefix="uc1" %>
<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleGeneracionClave"])%>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:FormCambioEmail ID="formCambioEmail" runat="server" />
</asp:Content>
