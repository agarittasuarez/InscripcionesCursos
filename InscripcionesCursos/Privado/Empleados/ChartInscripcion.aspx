<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartInscripcion.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.ChartInscripcion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--[if IE 7]><link rel="stylesheet" type="text/css" href="~/Styles/SiteIE7.css" /><![endif]-->
    <!--[if IE 8]><link rel="stylesheet" type="text/css" href="~/Styles/SiteIE8.css" /><![endif]-->
    <!--[if IE 9]><link rel="stylesheet" type="text/css" href="~/Styles/SiteIE8.css" /><![endif]-->
    <!--[if !IE]><!--><link rel="stylesheet" type="text/css" href="~/Styles/Site.css" /><!--<![endif]-->
</head>
<body class="bodyIframe">
    <form id="form1" runat="server">
        <div class="chartColumnaDerecha">
            <asp:Chart id="chartInscripcion" runat="server" Width="350px" BackColor="Transparent" >
                <Titles>
                    <asp:Title Name="titleChartInscripcion" ForeColor="White" TextStyle="Shadow" />
                </Titles>
                <Legends>
                    <asp:Legend Name="legendInscripcion" Alignment="Far" Docking="Bottom" IsTextAutoFit="True" LegendStyle="Row" BackColor="Transparent" ForeColor="White"/>
                </Legends>
                <Series>
                    <asp:Series Name="serieInscripcion" ChartType="Pie" LabelForeColor="White" />
                </Series>
                <Chartareas> 
                    <asp:ChartArea Name="chartAreaInscripcion" BorderWidth="0" BackColor="Transparent" />
                </Chartareas>
            </asp:Chart>
        </div>
        <asp:ScriptManager id="scriptManagerCombo" runat="server" />
        <asp:UpdatePanel ID="updateResultados" runat="server">
            <ContentTemplate>
                <div class="legendChartIframe">
                    <asp:Label ID="lblDescripcion" runat="server" />
                    <asp:DropDownList ID="ddTurnos" runat="server"/>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="blackButtonInscripcion" onclick="btnConsultar_Click" />
                </div>
                <br /><br /><br />
                <div class="legendChartIframe">
                    <asp:Label ID="lblTotalInscriptos" runat="server" CssClass="legendChart"/><br />
                    <asp:Label ID="lblTotalInscriptosWeb" runat="server" CssClass="legendChart"/><br />
                    <asp:Label ID="lblTotalInscriptosFacu" runat="server" CssClass="legendChart"/><br />
                    <asp:Label ID="lblTotalNoInscriptos" runat="server" CssClass="legendChart"/><br />
                    <asp:Label ID="lblMuestreo" runat="server" CssClass="legendChart" /><br />
                </div>
                <br />
                <div class="tableEstadisticas">
                    <asp:GridView ID="gridCarrera" runat="server" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridRowBack" />
                        <Columns>
                            <asp:TemplateField HeaderText="Carrera">
                                <ItemTemplate>
                                    <asp:Label ID="lblCarrera" runat="server" Text='<%#Eval("Carrera")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inscriptos">
                                <ItemTemplate>
                                    <asp:Label ID="lblInscriptos" runat="server" Text='<%#Eval("TotalInscriptos")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Inscriptos">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvgInscriptos" runat="server" Text='<%#Eval("AVGInscriptos") + "%"%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br /><br />
                    <asp:GridView ID="gridComision" runat="server" AutoGenerateColumns="False" 
                        onrowdatabound="gridComision_RowDataBound" >
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridRowBack" />
                        <Columns>
                            <asp:TemplateField HeaderText="Departamento">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartamento" runat="server" Text='<%#Eval("Departamento")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Materia">
                                <ItemTemplate>
                                    <asp:Label ID="lblMateria" runat="server" Text='<%#Eval("IdMateria") + "- " + Eval("Materia")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Catedra/Comision">
                                <ItemTemplate>
                                    <asp:Label ID="lblComision" runat="server" Text='<%#Eval("CatedraComision")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profesor">
                                <ItemTemplate>
                                    <asp:Label ID="lblProfesor" runat="server" Text='<%#Eval("ProfesorNombreApellido")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Horario">
                                <ItemTemplate>
                                    <asp:Label ID="lblHorario" runat="server" Text='<%#Eval("Horario")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inscriptos">
                                <ItemTemplate>
                                    <asp:Label ID="lblInscriptos" runat="server" Text='<%#Eval("CantInscriptos")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConsultar" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
