<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucEncuesta.ascx.cs" Inherits="InscripcionesCursos.Controles.wucEncuesta" %>
<asp:UpdatePanel runat="server">
    <asp:Repeater runat="server" ID="rptSurvey">
        <ItemTemplate>
            <div>
                <asp:Label ID="lblQuestion" runat="server" text="<%# Eval("Pregunta") %>"/>
                <asp:RadioButton runat="server" ID="rbOption1" GroupName="Options" Text="SI"/>
                <asp:RadioButton runat="server" ID="rbOption2" GroupName="Options" Text="NO" Checked="True"/>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:UpdatePanel>