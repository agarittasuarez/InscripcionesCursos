<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreInscripcionCursos.aspx.cs" Inherits="InscripcionesCursos.PreInscripcionCursos" %>
<asp:Content ID="TitleContentPreInscripcion" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= ConfigurationManager.AppSettings["ContentPreInscripcionTitulo"]%></h2>
    <div class="textoAngostoAlumnos">
        <p><%= ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart1"]%></p>
        <p><%= ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart2"]%></p>
    </div>
    <asp:Button ID="btnContinuar" CssClass="blackButtonBig" runat="server" Text="Continuar" onclick="btnContinuar_Click" />
</asp:Content>
