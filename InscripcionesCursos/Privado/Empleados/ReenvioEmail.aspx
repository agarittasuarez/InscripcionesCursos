<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReenvioEmail.aspx.cs" Inherits="InscripcionesCursos.ReenvioEmail" %>
<%@ Register src="~/Controles/wucFormularioReenvioEmail.ascx" tagname="FormReenvioEmail" tagprefix="uc1" %>

<asp:Content ID="TitleContentReenvioEmail" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleReenvioEmail"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:FormReenvioEmail ID="formReenvio" runat="server" />
</asp:Content>
