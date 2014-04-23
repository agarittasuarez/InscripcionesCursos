<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InscripcionesCursos.Default" %>
<asp:Content ID="TitleContentDefault" ContentPlaceHolderID="TitleContent" runat="server">
        <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleDefault"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= ConfigurationManager.AppSettings["ContentDefaultTitulo"]%></h2>
    <div class="textoAngosto">
        <p><%= ConfigurationManager.AppSettings["ContentDefaultBodyPart1"]%></p>
        <p><%= ConfigurationManager.AppSettings["ContentDefaultBodyPart2"]%></p>
    </div>
    <asp:Button ID="btnContinuar" CssClass="blackButtonBig" runat="server" 
    Text="Continuar" onclick="btnContinuar_Click" />
</asp:Content>
