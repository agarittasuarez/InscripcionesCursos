<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mantenimiento.aspx.cs" Inherits="InscripcionesCursos.Mantenimiento" %>
<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleNoDisponible"])%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div class="contenedorFormGenerar">
        <h3>
            <asp:Label ID="lblErrorGenerico" runat="server">
                <%= ConfigurationManager.AppSettings["ContentErrorNoDisponible"]%>
            </asp:Label>
        </h3>
    </div>
</asp:Content>
