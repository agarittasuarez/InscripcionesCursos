<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InscripcionesCursos.Login" %>

<asp:Content ID="TitleContentLogin" runat="server" ContentPlaceHolderID="TitleContent">
        <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitleLogin"])%>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="loginBox">
        <asp:Login ID="login" runat="server" LoginButtonText="Entrar" 
            UserNameLabelText="DNI:" PasswordLabelText="Constraseña:" DisplayRememberMe="False"
            PasswordRequiredErrorMessage="Deber ingresar una contraseña alfanumerica válida" 
            UserNameRequiredErrorMessage="Debe ingresar un DNI válido" 
            RememberMeSet="True" Font-Bold="True" FailureTextStyle-ForeColor="Red" 
            FailureText="Error de login. Verifique que sus credenciales sean válidas" 
            onauthenticate="Login_Authenticate" onloggedin="Login_LoggedIn">
            <LabelStyle CssClass="loginTexts" />
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
<%--                                <tr>
                                    <td align="center" class="loginTitle" colspan="2">
                                        Acceso Inscripciones Cursos</td>
                                </tr>--%>
                                <tr>
                                    <td align="right" class="loginTexts">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" MaxLength="8" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="Debe ingresar un DNI válido" 
                                            ForeColor="Red" ToolTip="Debe ingresar un DNI válido" >*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="loginTexts">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"><%= ConfigurationManager.AppSettings["LabelPassword"] %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" 
                                            ErrorMessage="Debe ingresar una contraseña alfanumerica válida" 
                                            ForeColor="Red" ToolTip="Deber ingresar una contraseña alfanumerica válida" >*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5" style="color:Red; width:280px">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Visible="true"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="4">
                                        <div class="contenedorOlvido">
                                            <asp:LinkButton ID="btnForgotPassword" runat="server" Text="Olvido de contraseña" onclick="btnForgotPassword_Click" CausesValidation="false" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="blackButton" Text="Entrar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <LoginButtonStyle CssClass="loginBoton" />
            <TitleTextStyle CssClass="loginTitle" />        
        </asp:Login>
        
    </div>
</asp:Content>
