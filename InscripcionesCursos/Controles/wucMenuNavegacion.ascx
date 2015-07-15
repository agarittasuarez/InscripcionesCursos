<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenuNavegacion.ascx.cs" Inherits="InscripcionesCursos.wucMenuNavegacion" %>

<div class="menuIzquierdo">
    <asp:Button ID="btnConstancia" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnConstancia_Click"/>
    <asp:Button ID="btnPlanes" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnPlanes_Click"/>
    <asp:Button ID="btnRendidas" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnRendidas_Click"/>
    <asp:Button ID="btnInscripciones" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnInscripciones_Click"/>
    <asp:Button ID="btnHistorialInscrip" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnHistorialInscrip_Click"/>
    <asp:Button ID="btnActualizarDatos" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnActualizarDatos_Click"/>
    <asp:Button ID="btnOfertas" runat="server" Text="" CausesValidation="false" Enabled="true" OnClientClick="window.open('http://www.seube.com.ar/lab/ofertaslaborales.asp', 'Ofertas');" />
    <asp:Button ID="btnTalleres" runat="server" Text="" CausesValidation="false" Enabled="true" OnClientClick="window.open('http://www.seube.com.ar/idiomas/cursos/inscripcion1.asp', 'Talleres');" />
    <div class="cajaEstado">
        <asp:Label ID="lblUser" runat="server" />
        <asp:Label ID="lblEstado" runat="server" />
        <asp:Button ID="btnLogout" runat="server" Text="" CausesValidation="false" onclick="btnLogout_Click" />
    </div>
</div>