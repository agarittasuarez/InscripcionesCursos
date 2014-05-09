<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionCursos.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.InscripcionCursos" %>
<%@ Register src="~/Controles/wucInscripcion.ascx" tagname="Inscripcion" tagprefix="uc1" %>
<asp:Content ID="TitleContentInscripcion" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleInscripcion"] %></asp:Label>
    </div>
    <div style="width:720; text-align:center">
        <div class="contenedorInput">
            <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
            <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
                ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
        </div>
        <div class="errorText">
            <asp:Literal ID="FailureText" runat="server" Visible="false" ></asp:Literal>
        </div>
        <div class="contenedorBotonBuscar">
            <asp:Button ID="btnRequest" runat="server" Text="Buscar" CssClass="blackButton" onclick="btnRequest_Click" />
            <asp:Button ID="btnClean" runat="server" Text="Limpiar" CssClass="blackButton" onclick="btnClean_Click" Enabled="false" CausesValidation="false" />
        </div>
        <div id="divResultados" runat="server" visible="false" class="divResultados">
            <br />
            <div class="resultadosGen">
                <div class="resultadosLinea">
                    <asp:Label CssClass="labelsResultadosGen" ID="lblApellidoNombreResultado" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombreResultado" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="lblCarrera" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelCarrera"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtCarrera" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEmail" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEmail"]%></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEmail" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEstadoCuenta" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEstadoCuenta"]%></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEstadoCuenta" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="contenedorComisiones">
            <uc1:Inscripcion id="ddInscripciones" runat="server" Visible="false"/>
            <div class="mensajeNoDisponible" id="divNoDisponible" runat="server" visible="false">
                <asp:Label ID="lblNoDisponible" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>