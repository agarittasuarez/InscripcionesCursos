<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenuNavegacion.ascx.cs" Inherits="InscripcionesCursos.wucMenuNavegacion" %>

<div class="menuIzquierdo">
    <asp:Button ID="btnConstancia" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnConstancia_Click"/>
    <asp:Button ID="btnPlanes" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnPlanes_Click"/>
    <asp:Button ID="btnRendidas" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnRendidas_Click"/>
    <asp:Button ID="btnInscripciones" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnInscripciones_Click"/>
    <asp:Button ID="btnHistorialInscrip" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnHistorialInscrip_Click"/>
    <asp:Button ID="btnActualizarDatos" runat="server" Text="" CausesValidation="false" Enabled="false" onclick="btnActualizarDatos_Click"/>
    <asp:LinkButton ID="test" runat="server" CausesValidation="false" />
    <div class="cajaEstado">
        <asp:Label ID="lblUser" runat="server" />
        <asp:Label ID="lblEstado" runat="server" />
        <asp:Button ID="btnLogout" runat="server" Text="" CausesValidation="false" onclick="btnLogout_Click" />
    </div>
</div>