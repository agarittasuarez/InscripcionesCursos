<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucInscripcion.ascx.cs" Inherits="InscripcionesCursos.wucInscripcion" %>
<%@ Register src="wucCarro.ascx" tagname="Carro" tagprefix="uc1" %>
<div class="contenedorFormInscripciones">
    <asp:UpdatePanel ID="updateCombos" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Label ID="invisibleTarget" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="mpeMessage" runat="server"
                OkControlID="btnAceptar" TargetControlID="invisibleTarget"
                PopupControlID="pnMessage" BehaviorID="pnMessage"
	            PopupDragHandleControlID="PopupHeader" Drag="true" 
	            BackgroundCssClass="modalPopUpBG">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel id="pnMessage" style="display: none" runat="server" CssClass="modalPopUp">
                <div class="modalPopUpBody">
                    <asp:Label ID="lblMessagePopUp" runat="server" />
                </div>
                <div id="buttonContainer" class="modalPopUpButtons" runat="server">
                    <input id="btnAceptar" type="button" value="Aceptar"/>
		        </div>
            </asp:Panel>
            <div class="displayCombos">
                <asp:DropDownList ID="comboDepartamento" runat="server" 
                onselectedindexchanged="comboDepartamento_SelectedIndexChanged" AutoPostBack="true" >
                </asp:DropDownList>
                <asp:DropDownList ID="comboCarrera" runat="server" Enabled="false" 
                    AutoPostBack="true" 
                    onselectedindexchanged="comboCarrera_SelectedIndexChanged"  >
                </asp:DropDownList>
                <asp:DropDownList ID="comboMateria" runat="server" Enabled="false" 
                    AutoPostBack="true" onselectedindexchanged="comboMateria_SelectedIndexChanged" >
                </asp:DropDownList>
            </div>
            <div id="divResultados" runat="server" visible="false">
                <div class="separador"></div>
                <div class="gridResultados">
                    <asp:GridView ID="GridResultados" runat="server" AutoGenerateColumns="False" BorderWidth="0" onrowdatabound="GridResultados_RowDataBound">
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridRowBack" />
                        <Columns>
                            <asp:BoundField DataField="CatedraComisionDescripcion">
                            </asp:BoundField>
                            <asp:BoundField DataField="ProfesorNombreApellido">
                            </asp:BoundField>
                            <asp:BoundField DataField="Horario">
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="check" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="contenedorBtnInscripcion">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="blackButtonInscripcion" onclick="btnAgregar_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="contenedorCarro">
    <uc1:Carro id="wucCarro" runat="server" />
</div>
<div>
    <asp:Label ID="lblEstado" runat="server" Text="" Visible="false"></asp:Label>
</div>