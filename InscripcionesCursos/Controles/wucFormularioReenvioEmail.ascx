<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFormularioReenvioEmail.ascx.cs" Inherits="InscripcionesCursos.wucFormularioReenvioEmail" %>
<div class="contenedorFormGenerar">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleReenvioMailActivacion"] %></asp:Label>
    </div>
    <div class="contenedorInput">
        <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
        <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
    </div>
    <div class="contenedorBotonGenerar">
        <asp:Button ID="btnReenviar" runat="server" Text="Reenviar" CssClass="blackButton" onclick="btnReenviar_Click" />
    </div>
    <div class="errorText">
        <asp:Label ID="lblEstado" runat="server" Text="" ></asp:Label>
    </div>
</div>