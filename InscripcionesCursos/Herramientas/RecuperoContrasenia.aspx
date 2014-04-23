<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperoContrasenia.aspx.cs" Inherits="InscripcionesCursos.RecuperoContrasenia" %>
<asp:Content ID="TitleContentRecupero" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleRecuperoPassword"])%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleRecuperoPassword"]%></asp:Label>
        </div>
        <div class="contenedorInput">
            <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
            <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
                ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
        </div>
        <div class="errorText">
            <asp:Label ID="lblEstado" runat="server" Text="" ></asp:Label>
        </div>
        <div class="contenedorBotonesRecuperar">
            <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" CssClass="blackButton" onclick="btnRecuperar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="blackButton" onclick="btnCancelar_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>
