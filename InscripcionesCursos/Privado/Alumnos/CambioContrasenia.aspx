<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambioContrasenia.aspx.cs" Inherits="InscripcionesCursos.Alumnos.CambioContrasenia" %>
<asp:Content ID="TitleContentCambioPassword" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleCambioPassword"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
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
                <asp:Label CssClass="labelsCambioPass" ID="lblPassword" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPasswordAntigua"]%></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordOld" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordOld" ID="PasswordOldRequired" runat="server" ToolTip="Debe ingresar la contraseña actual" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblPasswordNew" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPasswordNueva"]%></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordNew" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordNew" ID="PasswordNewRequired" runat="server" ToolTip="Debe ingresar una nueva contraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:Label CssClass="labelsWarning" ID="lblWarning" runat="server" Text="La contraseña debe poseer entre 6 y 10 caracteres"></asp:Label>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsCambioPass" ID="lblPasswordRepeat" runat="server" Text="Repetir contraseña Nueva:"><%= ConfigurationManager.AppSettings["LabelPasswordRepite"]%></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordRepeat" runat="server" TextMode="Password" Enabled="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPasswordRepeat" ID="PasswordRepeatRequired" runat="server" ToolTip="Debe repetir la contraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
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
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="blackButton" onclick="btnEnviar_Click" />
        </div>
        <div class="infoCambioPass">
            <p><%= String.Format(ConfigurationManager.AppSettings["ContentInfoStudentCambioPassword"],"<b>","</b>","<b>","</b>")%></p>
        </div>
    </div>
</asp:Content>
