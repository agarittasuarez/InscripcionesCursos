<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreInscripcionCursos.aspx.cs" Inherits="InscripcionesCursos.PreInscripcionCursos" %>
<asp:Content ID="TitleContentPreInscripcion" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= ConfigurationManager.AppSettings["ContentPreInscripcionTitulo"]%></h2>
    <div class="textoAngostoAlumnos">
        <%
            string styleOpenBold = "<b>";
            string styleCloseBold = "</b>";
            string styleOpenItalic = "<i>";
            string styleCloseItalic = "</i>";
            string enter = "<br/>";
        %>
        <p><%= String.Format(ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart1"], styleOpenBold + styleOpenItalic, styleCloseItalic + styleCloseBold)%></p>
        <p><%= String.Format(ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart2"], enter)%></p>
        <p><%= ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart4"]%></p>
        <p><%= String.Format(ConfigurationManager.AppSettings["ContentPreInscripcionBodyPart3"], styleOpenBold, styleCloseBold)%></p>
    </div>
    <asp:Button ID="btnContinuar" CssClass="blackButtonBig" runat="server" Text="Continuar" onclick="btnContinuar_Click" />
</asp:Content>
