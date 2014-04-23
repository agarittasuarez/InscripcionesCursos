<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorGenerico.aspx.cs" Inherits="InscripcionesCursos.ErrorGenerico" %>
<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleError"])%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <asp:Label ID="lblErrorGenerico" runat="server">
            <%= String.Format(ConfigurationManager.AppSettings["ContentErrorMessageGeneric"], "<a href=\"mailto:" + ConfigurationManager.AppSettings["MailToContact"] + "\">" + ConfigurationManager.AppSettings["MailToContact"] + "</a>")%>
        </asp:Label>
    </div>
</asp:Content>
