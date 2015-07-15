<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucRelevamientoLimitaciones.ascx.cs" Inherits="InscripcionesCursos.Controles.wucRelevamientoLimitaciones" %>
<asp:UpdatePanel ID="upRelevamiento" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleCuestionarioLimitaciones"]%></asp:Label>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion1" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacion") %></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption1" GroupName="Options1" Text="SI" OnCheckedChanged="rbOption_OnCheckedChanged" AutoPostBack="True"/>
                <asp:RadioButton runat="server" ID="rbOption2" GroupName="Options1" Text="NO" OnCheckedChanged="rbOption_OnCheckedChanged" AutoPostBack="True"/>
            </div>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion2" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionVision")%></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption3" GroupName="Options2" Text="SI" />
                <asp:RadioButton runat="server" ID="rbOption4" GroupName="Options2" Text="NO" />
            </div>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion3" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionAudicion")%></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption5" GroupName="Options3" Text="SI" />
                <asp:RadioButton runat="server" ID="rbOption6" GroupName="Options3" Text="NO" />
            </div>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion4" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionMotriz")%></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption7" GroupName="Options4" Text="SI" />
                <asp:RadioButton runat="server" ID="rbOption8" GroupName="Options4" Text="NO" />
            </div>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion5" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionAgarre")%></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption9" GroupName="Options5" Text="SI" />
                <asp:RadioButton runat="server" ID="rbOption10" GroupName="Options5" Text="NO" />
            </div>
        </div>
        <div class="contenedorCuestionario">
            <div class="contenedorPregunta">
                <asp:Label ID="lblQuestion6" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionHabla")%></asp:Label>
            </div>
            <div class="contenedorRadioButton">
                <asp:RadioButton runat="server" ID="rbOption11" GroupName="Options6" Text="SI" />
                <asp:RadioButton runat="server" ID="rbOption12" GroupName="Options6" Text="NO" />
            </div>
        </div>
        <div class="contenedorCuestionario">
            <asp:Label ID="lblQuestion7" runat="server"><%= ConfigurationManager.AppSettings.Get("LabelPreguntaLimitacionOtra")%></asp:Label>
            <div class="contenedorRadioButton">
                <asp:TextBox id="txtOtras" runat="server" TextMode="MultiLine" MaxLength="300" 
                    Width="480px" Height="50px" Wrap="True" style="resize:none" >
                </asp:TextBox>
            </div>
        </div>
        <div class="contenedorBtnInscripcion">
            <asp:Button id="btnEnviar" runat="server" Text="Enviar" CssClass="blackButtonInscripcion" OnClick="btnEnviar_OnClick" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>