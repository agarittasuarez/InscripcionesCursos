<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenuNavegacionSimulador.ascx.cs" Inherits="InscripcionesCursos.wucMenuNavegacionSimulador" %>

<div class="menuIzquierdo">
    <asp:Button ID="btnConstancia" runat="server" Text="" CausesValidation="false" Enabled="False" CssClass="menuIzquierdoBotonDisabled" onclick="btnConstancia_Click"/>
    <asp:Button ID="btnPlanes" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnPlanes_Click"/>
    <asp:Button ID="btnRendidas" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnRendidas_Click"/>
    <asp:Button ID="btnInscripciones" runat="server" Text="" CausesValidation="false" Enabled="False" CssClass="menuIzquierdoBotonDisabled" onclick="btnInscripciones_Click"/>
    <asp:Button ID="btnHistorialInscrip" runat="server" Text="" CausesValidation="false" Enabled="true" onclick="btnHistorialInscrip_Click"/>
    <asp:Button ID="btnActualizarDatos" runat="server" Text="" CausesValidation="false" Enabled="false" CssClass="menuIzquierdoBotonDisabled" onclick="btnActualizarDatos_Click"/>
    <asp:Button ID="btnOfertas" runat="server" Text="" CausesValidation="false" Enabled="true" OnClientClick="window.open('http://www.seube.com.ar/lab/ofertaslaborales.asp', 'Ofertas');" />
    <asp:Button ID="btnTalleres" runat="server" Text="" CausesValidation="false" Enabled="true" OnClientClick="window.open('http://www.seube.com.ar/idiomas/cursos/inscripcion1.asp', 'Talleres');" />
    <asp:LinkButton ID="test" runat="server" CausesValidation="false" />
    <div class="cajaEstado">
        <asp:Label ID="lblUser" runat="server" CssClass="menuIzquierdoBotonDisabled" />
        <asp:Label ID="lblEstado" runat="server" CssClass="menuIzquierdoBotonDisabled"/>
        <asp:Button ID="btnBack" runat="server" Text="" CausesValidation="false" onclick="btnBack_Click" />
    </div>
</div>