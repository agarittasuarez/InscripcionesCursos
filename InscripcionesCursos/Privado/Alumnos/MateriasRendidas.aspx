<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MateriasRendidas.aspx.cs" Inherits="InscripcionesCursos.MateriasRendidas" %>
<asp:Content ID="TitleContentMateriasRendidas" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleMateriasRendidas"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleMateriasRendidas"] %></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
