<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="InscripcionesCursos.Inicio" %>
<asp:Content ID="TitleContentInicio" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInicio"])%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="textoAngostoAlumnos">
        <p><%= ConfigurationManager.AppSettings["ContentInicioInformes"]%></p>
    </div>
</asp:Content>