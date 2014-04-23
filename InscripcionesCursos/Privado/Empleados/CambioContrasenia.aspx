<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambioContrasenia.aspx.cs" Inherits="InscripcionesCursos.Empleados.CambioContrasenia" %>
<asp:Content ID="TitleContentCambioPassword" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleCambioPassword"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" class="contenedorFormGenerarEmployee" runat="server">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleCambioPassword"] %></asp:Label>
        </div>
        <div class="resultadosGen">
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtDni" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblApellidoNombre" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombre" runat="server" Enabled="false"></asp:TextBox>
            </div>
<%--            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellido" runat="server" Enabled="false"></asp:TextBox>
            </div>--%>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblPassword" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPasswordAntigua"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordOld" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordOld" ID="PasswordOldRequired" runat="server" ToolTip="Debe ingresar la contraseña actual" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblPasswordNew" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPasswordNueva"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordNew" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordNew" ID="PasswordNewRequired" runat="server" ToolTip="Debe ingresar una nueva contraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:Label CssClass="labelsWarning" ID="lblWarning" runat="server" Text="La contraseña debe tener entre 6 y 10 caracteres"></asp:Label>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblPasswordRepeat" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPasswordRepite"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordRepeat" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordRepeat" ID="PasswordRepeatRequired" runat="server" ToolTip="Debe repetir la contraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="divMessage" runat="server" class="errorText" visible="false">
            <asp:Label ID="FailureText" runat="server"></asp:Label>
            <div id="divLoading" runat="server" visible="false">
                <img src="<%= Page.ResolveUrl("~")%>img/ico_loading.gif" alt="Redirigiendo" />
            </div>
        </div>
        <div class="contenedorBotonActualizarEmployee">
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="blackButton" onclick="btnEnviar_Click" />
        </div>
        <div class="infoCambioPassEmployee">
            <p><%= System.Configuration.ConfigurationManager.AppSettings["ContentInfoEmployeeCambioPassword"]%></p>
        </div>
    </div>
</asp:Content>
