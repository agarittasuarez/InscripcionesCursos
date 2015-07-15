<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SimuladorAlumno.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.SimuladorAlumno" %>
<asp:Content ID="TitleContentSimulador" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleInterfazAlumno"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar" id="divSearchBox" runat="server">
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleInterfazAlumno"] %></asp:Label>
        </div>
        <div class="contenedorInput">
            <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
            <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
                ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
        </div>
        <div class="errorText">
            <asp:Literal ID="FailureText" runat="server" Visible="false" ></asp:Literal>
        </div>
        <div class="contenedorBotonGenerar">
            <asp:Button ID="btnRequest" runat="server" Text="Buscar" CssClass="blackButton" 
                onclick="btnRequest_Click"  />
        </div>
    </div>
</asp:Content>
