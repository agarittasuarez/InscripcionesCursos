<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCambioEmail.ascx.cs" Inherits="InscripcionesCursos.wucCambioEmail" %>
<div class="contenedorFormGenerar">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleCambioEmail"]%></asp:Label>
    </div>
    <div class="contenedorInput">
        <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
        <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
            ForeColor="Red" ToolTip="Debe ingresar un DNI válido" ValidationGroup="Dni">*</asp:RequiredFieldValidator>
    </div>
    <div class="errorText">
        <asp:Literal ID="FailureText" runat="server" Text="Ingrese un DNI válido" Visible="false" ></asp:Literal>
    </div>
    <div class="contenedorBotonGenerar">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="blackButton" 
            onclick="btnBuscar_Click" CausesValidation="true" ValidationGroup="Dni" />
    </div>
    <div class="contenedorFormGenerarEmployee">
        <div id="divResultados" runat="server" visible="false" class="resultadosGen">
            <div class="separador"></div>
            <div class="resultadosGen">
                <div class="resultadosLinea">
                    <asp:Label CssClass="labelsResultadosGen" ID="lblApellidoNombre" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombre" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="resultadosLinea">
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEmail" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEmail"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEmail" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="resultadosLinea">
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEmailNew" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEmailChange"]%></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEmailChange" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtEmailChange" ID="EmailChangeRequired" runat="server" 
                        ForeColor="Red" ToolTip="Debe ingresar un mail válido" ValidationGroup="EmailChange">*</asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="contenedorBotonActualizarEmployee">
            <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" CssClass="blackButton" onclick="btnCambiar_Click" CausesValidation="true" ValidationGroup="EmailChange" Visible="false"/>
        </div>
        <div class="infoCambioPassEmployee">
            <asp:Label id="lblEstado" runat="server"></asp:Label>
        </div>
    </div>
</div>