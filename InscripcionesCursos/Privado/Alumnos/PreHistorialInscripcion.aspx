<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreHistorialInscripcion.aspx.cs" Inherits="InscripcionesCursos.PreHistorialRegistracion" %>
<asp:Content ID="TitleContentPreHistorial" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleHistorialInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="textoAngostoAlumnos">
        <p><%= ConfigurationManager.AppSettings["ContentPreHistorialInscripcion"]%></p>
    </div>
    <asp:Button ID="btnContinuar" CssClass="blackButtonBig" runat="server" Text="Continuar" onclick="btnContinuar_Click" Visible="true"/>
</asp:Content>
