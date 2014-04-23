<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificacionDatos.aspx.cs" Inherits="InscripcionesCursos.ModificacionDatos" %>
<asp:Content ID="TitleContentModificacionDatos" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleModificacionDatos"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleModificacionDatos"] %></asp:Label>
        </div>
        <div class="resultadosGen">
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtDni" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblApellidoNombre" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombre" runat="server"></asp:TextBox>
            </div>
<%--            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellido" runat="server" Enabled="false"></asp:TextBox>
            </div>--%>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblEmail" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEmail"]%></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtEmail" runat="server" Enabled="true" MaxLength="40"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ID="EmailRequired" runat="server" ToolTip="Debe ingresar una dirección de mail" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" ID="EmailValidate" runat="server" ToolTip="Debe ingresar una dirección de mail válida" ValidationExpression="^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$" ForeColor="Red">*</asp:RegularExpressionValidator>
            </div>
        </div>
        <div id="divMessage" runat="server" class="changePasswordStatus" visible="false">
            <asp:Label ID="FailureText" runat="server"></asp:Label>
        </div>
        <div class="contenedorBotonActualizar">
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="blackButton"/>
        </div>
    </div>
</asp:Content>
