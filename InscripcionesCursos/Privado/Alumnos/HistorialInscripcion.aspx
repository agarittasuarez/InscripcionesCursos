<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialInscripcion.aspx.cs" Inherits="InscripcionesCursos.HistorialInscripcion" %>
<asp:Content ID="TitleContentHistoricoInscripcion" runat="server" ContentPlaceHolderID="TitleContent">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleHistorialInscripciones"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedorFormGenerar">
        <asp:ScriptManager id="scriptManagerCombos" runat="server" />
        <asp:UpdatePanel ID="updateCombo" runat="server">
            <ContentTemplate>
                <div class="tituloContenido">
                    <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleHistorico"] %></asp:Label>
                </div>
                <div id="divResultados" runat="server">
                    <div id="filtroTurno" runat="server">
                        <asp:Label ID="lblTextSeleccion" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelSeleccionTurno"]%></asp:Label>
                        <asp:DropDownList ID="ddTurnosInscripcion" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddTurnosInscripcion_SelectedIndexChanged" ></asp:DropDownList>
                    </div>
                    <br />
                    <div id="divGrid" class="gridResultados" runat="server" visible="false">
                        <asp:GridView ID="GridResultados" runat="server" AutoGenerateColumns="False" BorderWidth="0" EmptyDataText="No se encontraron inscripciones" >
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridRowBack" />
                            <Columns>
                                <asp:BoundField DataField="TurnoInscripcion">
                                </asp:BoundField>
                                <asp:BoundField DataField="CatedraComisionDescripcion">
                                </asp:BoundField>
                                <asp:BoundField DataField="IdMateria">
                                </asp:BoundField>
                                <asp:BoundField DataField="MateriaDescripcion">
                                </asp:BoundField>
                                <asp:BoundField DataField="EstadoInscripcion">
                                </asp:BoundField>
                                <asp:BoundField DataField="TipoInscripcionDescripcion">
                                </asp:BoundField>
                                <asp:BoundField DataField="TipoVueltaDescripcion">
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <div class="contenedorBtnInscripcion">
                            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="blackButtonInscripcion" onclick="btnImprimir_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
