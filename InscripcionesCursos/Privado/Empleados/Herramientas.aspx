<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Herramientas.aspx.cs" Inherits="InscripcionesCursos.Herramientas" %>
<%@ Register src="~/Controles/wcuMenuExtraccion.ascx" tagname="ExtraccionDatos" tagprefix="uc1" %>

<asp:Content ID="TitleContentExtraccionDatos" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleExtraccionDatos"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ExtraccionDatos id="Extraccion" runat="server"/>
</asp:Content>
