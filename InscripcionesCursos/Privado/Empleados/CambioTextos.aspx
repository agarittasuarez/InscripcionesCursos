<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambioTextos.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.CambioTextos"  ValidateRequest="false"%>
<asp:Content ID="TitleContentDefault" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleCambioTextos"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tituloContenido">
        <asp:Label ID="lblTitulo" runat="server" Text="" />
    </div>
    <div style="float:left">
        <div class="labelCambioTexto">
            <asp:Label ID="lblPreInscripcion1" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion1"]%></asp:Label>
            <asp:TextBox ID="txtPreInscripcion1" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" />
        </div>
        <br />
        <div class="labelCambioTexto">
            <asp:Label ID="lblPreInscripcion2" runat="server"><%= ConfigurationManager.AppSettings["LabelPreInscripcion2"]%></asp:Label>
            <asp:TextBox ID="txtPreInscripcion2" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" />
        </div>
        <br />
        <div class="labelCambioTexto">
            <asp:Label ID="lblPreHistorico" runat="server"><%= ConfigurationManager.AppSettings["LabelPreHistorico"]%></asp:Label>
            <asp:TextBox ID="txtPreHistorico" runat="server" Width="500px" Height="80px" Wrap="true" TextMode="MultiLine" />
        </div>
        <br />
        <div class="labelRadioButton">
            <asp:Label ID="lblHabilitaHistorico" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaHistorico"]%></asp:Label>
            <div class="radioButton">
                <asp:RadioButtonList id="listRBHabilitaImprimirHistorico" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Habilitar"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <br />
        <div class="labelRadioButton">
            <asp:Label ID="Label1" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaPortalMantenimiento"]%></asp:Label>
            <div class="radioButton">
                <asp:RadioButtonList id="listRBHabilitaPortal" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Habilitar"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <br />
        <div class="labelRadioButton">
            <asp:Label ID="Label2" runat="server"><%= ConfigurationManager.AppSettings["LabelHabilitaInscripcionAlumno"]%></asp:Label>
            <div class="radioButton">
                <asp:RadioButtonList id="listRBHabilitaInscripcion" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Habilitar"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitar"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="fixButtonCambioTextos">
            <div class="contenedorBtnInscripcion">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="blackButtonInscripcion" onclick="btnGuardar_Click" />
            </div> 
        </div>
    </div>
</asp:Content>