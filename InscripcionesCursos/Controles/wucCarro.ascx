<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCarro.ascx.cs" Inherits="InscripcionesCursos.wucCarro" %>
<script type="text/javascript">
    function Loading() {
        $('#imgProcessing').css('display', '');
    }
</script>
<asp:UpdatePanel ID="updateCarro" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Label ID="invisibleTargetCart" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeMessageCart" runat="server"
            TargetControlID="invisibleTargetCart"
            PopupControlID="pnMessageCart" BehaviorID="pnMessageCart"
	        PopupDragHandleControlID="PopupHeaderCart" Drag="true" 
	        BackgroundCssClass="modalPopUpBG">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel id="pnMessageCart" style="display: none" runat="server" CssClass="modalPopUp">
            <div class="modalPopUpBody">
                <asp:Label ID="lblMessagePopUpCart" runat="server" />
            </div>
            <img src="/../img/ico_loading.gif" id="imgProcessing" align="middle" alt="loading" style="display:none" />
            <div id="buttonContainerCart" class="modalPopUpButtonsLoading" runat="server">
                <asp:Button ID="btnAceptarCart" Text="Aceptar" runat="server" OnClick="btnAceptarCart_Click" OnClientClick="Loading();" />
                <asp:Button ID="btnCancelarCart" Text="Cancelar" runat="server" OnClick="btnCancelarCart_Click" />
		    </div>
        </asp:Panel>
        <div class="gridResultados" id="divCarro" runat="server">
            <div class="tituloContenidoCarro">
                <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleMiCarro"] %></asp:Label>
            </div>
            <div class="emptyGridHeaders" id="headerCart" runat="server">
                <table cellspacing="0" style="border-width:0px;border-collapse:collapse;" rules="all">
                    <tbody>
                        <tr class="gridHeader">
		                    <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderMateria"]%></th>
                            <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderCatedraComision"]%></th>
                            <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderProfesor"]%></th>
                            <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderHorario"]%></th>
                            <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderEstadoInscripcion"]%></th>
                            <th scope="col"><%= ConfigurationManager.AppSettings["ContentHeaderEliminar"] %></th>
	                    </tr>
                    </tbody>
                </table>
            </div>
            <asp:GridView ID="GridCarro" runat="server" AutoGenerateColumns="False" 
                BorderWidth="0px" onrowdeleting="GridCarro_RowDeleting" 
                onrowdatabound="GridCarro_RowDataBound">
                <HeaderStyle CssClass="gridHeader" />
                <RowStyle CssClass="gridRowBack" />
                <Columns>
                    <asp:BoundField DataField="Materia">
                    </asp:BoundField>
                    <asp:BoundField DataField="CatedraComision">
                    </asp:BoundField>
                    <asp:BoundField DataField="Profesor">
                    </asp:BoundField>
                    <asp:BoundField DataField="Horario">
                    </asp:BoundField>
                    <asp:BoundField DataField="EstadoDescripcion">
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="False" 
                                CommandName="Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Para completar la eliminación debe clickear en Confirmar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="contenedorBtnInscripcion">
                <asp:Button ID="btnInscribir" runat="server" Text="" CssClass="blackButtonInscripcion" onclick="btnInscribir_Click" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>