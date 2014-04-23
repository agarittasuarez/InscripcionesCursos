<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GeneracionClave.aspx.cs" Inherits="InscripcionesCursos.GeneracionClave" %>
<%@ Register src="~/Controles/wucFormularioPassword.ascx" tagname="FormPassword" tagprefix="uc1" %>

<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleGeneracionClave"])%>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:FormPassword ID="formGeneracion" runat="server" />
</asp:Content>