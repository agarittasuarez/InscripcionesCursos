<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivacionCuenta.aspx.cs" Inherits="InscripcionesCursos.ActivacionCuenta" %>
<asp:Content ID="TitleContentActivacion" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleActivacionCuenta"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleActivacionCuenta"] %></asp:Label>
        </div>
        <asp:Label ID="InfoText" runat="server"></asp:Label>
        <div id="divLoading" runat="server" visible="false">
            <img src="<%= Page.ResolveUrl("~")%>img/ico_loading.gif" alt="Redirigiendo" />
        </div>
    </div>
</asp:Content>
