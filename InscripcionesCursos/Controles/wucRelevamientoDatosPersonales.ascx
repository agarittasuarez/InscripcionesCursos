<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucRelevamientoDatosPersonales.ascx.cs" Inherits="InscripcionesCursos.Controles.wucRelevamientoDatosPersonales" %>
<asp:UpdatePanel ID="upRelevamiento" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleCuestionarioDatosPersonales"]%></asp:Label>
        </div>
        <div style="text-align:left; margin:15px 10px">
            <asp:Label ID="lblAviso" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelAviso") %></asp:Label>
        </div>
        <div class="resultadosGen">
            <div class="resultadosLinea">
                <asp:Label ID="lblDomicilio" runat="server" CssClass="labelsCambioPass"><%= ConfigurationManager.AppSettings.Get("LabelDomicilio") %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtDomicilio" runat="server" MaxLength="100"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label ID="lblLocalidad" runat="server" CssClass="labelsCambioPass"><%= ConfigurationManager.AppSettings.Get("LabelLocalidad") %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtLocalidad" runat="server" MaxLength="50"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label ID="lblCP" runat="server" CssClass="labelsCambioPass"><%= ConfigurationManager.AppSettings.Get("LabelCP") %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtCP" runat="server" MaxLength="8"></asp:TextBox>
            </div>
            <div class="resultadosLinea">
                <asp:Label ID="lblCelular" runat="server" CssClass="labelsCambioPass"><%= ConfigurationManager.AppSettings.Get("LabelCelular") %></asp:Label>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtCaracteristica" runat="server" Width="30px" MaxLength="4" ValidationGroup="vgPhone1"></asp:TextBox>
                <asp:TextBox CssClass="inputsResultadosGen" ID="txtCelular" runat="server" Width="70px" MaxLength="8" ValidationGroup="vgPhone"></asp:TextBox>
            </div>
            <div>
                <em><span ID="lblLegend1" class="smallLegend">Caracteristica-nro. (ej: 11-60256132)</span></em>
            </div>
            <div class="resultadosLinea">
                <ajaxToolkit:MaskedEditExtender ID="mskCaracteristica" runat="server"
                       AcceptNegative="None"
                       TargetControlID="txtCaracteristica"
                       ClearMaskOnLostFocus ="false"
                       MaskType="Number"
                       Mask="9999"
                       AutoComplete="false"
                       InputDirection="RightToLeft">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="mskValidatorCaracteristica" runat="server"
                    ControlExtender="mskCaracteristica"
                    ControlToValidate="txtCaracteristica" 
                    IsValidEmpty="False"
                    ForeColor="Red"
                    Font-Bold="true"
                    Font-Size="1em"
                    EmptyValueMessage="La caracteristica es obligatoria"
                    Display="Dynamic"
                    ValidationGroup="vgPhone1"/>
            </div>
             <div class="resultadosLinea">
                <ajaxToolkit:MaskedEditExtender ID="mskCelular" runat="server"
                       AcceptNegative="None"
                       TargetControlID="txtCelular"
                       ClearMaskOnLostFocus ="false"
                       MaskType="Number"
                       Mask="99999999"
                       AutoComplete="false"
                       InputDirection="RightToLeft">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="mskValidatorCellphone" runat="server"
                    ControlExtender="mskCelular"
                    ControlToValidate="txtCelular" 
                    IsValidEmpty="False"
                    ForeColor="Red"
                    Font-Bold="true"
                    Font-Size="1em"
                    EmptyValueMessage="El teléfono celular es obligatorio"
                    Display="Dynamic" 
                    ValidationGroup="vgPhone"/>
            </div>
            <div class="contenedorBtnInscripcion">
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="blackButtonInscripcion" OnClick="btnEnviar_Click" ValidationGroup="vgPhone" CausesValidation="true"/>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>