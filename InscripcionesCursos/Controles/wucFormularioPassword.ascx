<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFormularioPassword.ascx.cs" Inherits="InscripcionesCursos.wucFormularioPassword" %>
<div class="contenedorFormGenerar">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleGeneracionClaves"] %></asp:Label>
    </div>
    <div class="contenedorInput">
        <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
        <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
            ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
    </div>
    <div class="errorText">
        <asp:Literal ID="FailureText" runat="server" Text="Ingrese un DNI válido" Visible="false" ></asp:Literal>
    </div>
    <div class="contenedorBotonGenerar">
        <asp:Button ID="btnGenerar" runat="server" Text="Generar" CssClass="blackButton" onclick="btnGenerar_Click" />
    </div>
    <div id="divResultados" runat="server" visible="false" class="divResultados">
        <div class="separador"></div>
        <div class="resultadosGen">
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsResultadosGen" ID="lblDniResultado" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtDniResultado" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsResultadosGen" ID="lblApellidoNombreResultado" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombreResultado" runat="server" Enabled="false"></asp:TextBox>
            </div>
<%--            <div class="resultadosLinea">
                <asp:Label CssClass="labelsResultadosGen" ID="lblApellidoResultado" runat="server" Text="Apellido:"></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoResultado" runat="server" Enabled="false"></asp:TextBox>
            </div>--%>
            <div class="resultadosLinea">
                <asp:Label CssClass="labelsResultadosGen" ID="lblPasswordResultado" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelPassword"] %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtPasswordResultado" runat="server" TextMode="Password" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
