<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcuMenuExtraccion.ascx.cs" Inherits="InscripcionesCursos.wcuMenuExtraccion" %>
<div class="menuIzquierdo">
    <asp:Button ID="btnCatedras" runat="server" Text="" CausesValidation="false" onclick="btnCatedras_Click"/>
    <asp:Button ID="btnInscripciones" runat="server" Text="" CausesValidation="false" onclick="btnInscripciones_Click"/>
    <asp:Button ID="btnUsuarios" runat="server" Text="" CausesValidation="false" Enabled="false"/>
    <asp:Button ID="btnImportarPadron" runat="server" Text="" CausesValidation="false" onclick="btnImportarPadron_Click"/>
    <asp:Button ID="btnImportarInscripciones" runat="server" Text="" CausesValidation="false" onclick="btnImportarInscripciones_Click" />
    <asp:Button ID="btnImportarComisiones" runat="server" Text="" CausesValidation="false" onclick="btnImportarComisiones_Click"/>
    <asp:Button ID="btnImportarInscripcionActiva" runat="server" Text="" CausesValidation="false" onclick="btnImportarInscripcionActiva_Click" />
    <asp:Button ID="btnImportarAnalitico" runat="server" Text="" CausesValidation="false" onclick="btnImportarAnalitico_Click" />
</div>
<div class="contenedorFormGenerar">
    <asp:ScriptManager id="scriptManagerCombos" runat="server" />
    <asp:UpdatePanel ID="updateCombo" runat="server">
        <ContentTemplate>
            <div id="filtroCatedraComision" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloExtraerCatedras" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleExtraccionCatedras"] %></asp:Label>
                </div>
                <asp:DropDownList ID="ddTurnos" runat="server" ></asp:DropDownList>
                <asp:Button ID="btnExtraerCatedra" runat="server" Text="Extraer" CssClass="blackButtonInscripcion" onclick="btnExtraerCatedra_Click" />
            </div>
            <div id="filtroInscripcion" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloExtraerInscripciones" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleExtraccionInscripciones"] %></asp:Label>
                </div>
                <asp:DropDownList ID="ddInscripciones" runat="server" onselectedindexchanged="ddInscripciones_SelectedIndexChanged" AutoPostBack="true" />
                <asp:DropDownList ID="ddInscripcionesVuelta" runat="server" Enabled="false" />
                <asp:Button ID="btnExtractInscripcion" runat="server" Text="Extraer" 
                    CssClass="blackButtonInscripcion" onclick="btnExtractInscripcion_Click" />
            </div>
            <div id="filtroAlumno" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblExtraerAlumnos" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleExtraccionAlumnos"]%></asp:Label>
                </div>
                <asp:DropDownList ID="ddAlumnos" runat="server" ></asp:DropDownList>
                <asp:Button ID="btnExtraerAlumnos" runat="server" Text="Extraer" CssClass="blackButtonInscripcion" />
            </div>
            <div id="filtroImportarPadron" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloImportarPadron" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleImportarPadronAlumnos"]%></asp:Label>
                </div>
                <asp:FileUpload ID="fuPadron" runat="server" ToolTip="Explorar" />
                <div style="text-align: center">
                    <asp:Button id="btnUpload" runat="server" Text="Subir" onclick="btnUpload_Click" />
                </div>
                <div>
                    <asp:Label ID="lblEstadoImportarPadron" Text="" runat="server" />
                </div>
            </div>
            <div id="filtroImportarInscripciones" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloImportarInscripciones" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleImportarInscripciones"]%></asp:Label>
                </div>
                <asp:FileUpload ID="fuInscripciones" runat="server" ToolTip="Explorar" />
                <div style="text-align: center">
                    <asp:Button id="btnUploadInscripciones" runat="server" Text="Subir" 
                        onclick="btnUploadInscripciones_Click"/>
                </div>
                <div>
                    <asp:Label ID="lblEstadoImportarInscripciones" Text="" runat="server" />
                </div>
            </div>
            <div id="filtroImportarCatedraComision" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloImportarCatedraComision" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleImportarComisiones"]%></asp:Label>
                </div>
                <asp:FileUpload ID="fuComisiones" runat="server" ToolTip="Examinar" />
                <div style="text-align: center">
                    <asp:Button id="btnUploadComisiones" runat="server" Text="Subir" 
                        onclick="btnUploadComisiones_Click" />
                </div>
                <div>
                    <asp:Label ID="lblEstadoImportarComisiones" Text="" runat="server" />
                </div>
            </div>
            <div id="filtroImportarInscripcionActiva" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloImportarInscripcionActiva" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleImportarInscripcionActiva"]%></asp:Label>
                </div>
                <asp:FileUpload ID="fuInscripcionActiva" runat="server" ToolTip="Examinar" />
                <div style="text-align: center">
                    <asp:Button id="btnUploadInscripcionActiva" runat="server" Text="Subir" onclick="btnUploadInscripcionActiva_Click" />
                </div>
                <div>
                    <asp:Label ID="lblEstadImportarInscripcionActiva" Text="" runat="server" />
                </div>
            </div>
            <div id="filtroImportarAnalitico" runat="server" visible="false">
                <div class="tituloContenido">
                    <asp:Label ID="lblTituloImportarAnalitico" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleImportarAnalitico"]%></asp:Label>
                </div>
                <asp:FileUpload ID="fuNotas" runat="server" ToolTip="Examinar" />
                <div style="text-align: center">
                    <asp:Button id="btnUploadAnalitico" runat="server" Text="Subir" onclick="btnUploadAnalitico_Click" />
                </div>
                <div>
                    <asp:Label ID="lblEstadoImportarAnalitico" Text="" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExtraerCatedra" />
            <asp:PostBackTrigger ControlID="btnExtractInscripcion" />
            <asp:PostBackTrigger ControlID="btnExtraerAlumnos" />
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnUploadInscripciones" />
            <asp:PostBackTrigger ControlID="btnUploadComisiones" />
            <asp:PostBackTrigger ControlID="btnUploadAnalitico" />
            <asp:PostBackTrigger ControlID="btnUploadInscripcionActiva" />
        </Triggers>
    </asp:UpdatePanel>
</div>