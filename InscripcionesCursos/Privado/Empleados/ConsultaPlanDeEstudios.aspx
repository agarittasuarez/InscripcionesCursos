<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaPlanDeEstudios.aspx.cs" Inherits="InscripcionesCursos.Privado.Empleados.ConsultaPlanDeEstudios" %>
<asp:Content ID="TitleContentPlanEstudio" ContentPlaceHolderID="TitleContent" runat="server">
    <%= String.Format(ConfigurationManager.AppSettings["TitleGeneric"], ConfigurationManager.AppSettings["TitlePlanDeEstudio"])%>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="contenedorFormGenerar" style="height:200px;" id="divSearchBox" runat="server">
        <div class="tituloContenido">
            <asp:Label ID="Label1" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitleConsultaPlanEstudio"] %></asp:Label>
        </div>
        <div class="contenedorInput">
            <asp:Label ID="lblDni" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelDNI"] %></asp:Label>
            <asp:TextBox ID="txtDni" runat="server" MaxLength="8"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDni" ID="DniRequired" runat="server" 
                ForeColor="Red" ToolTip="Debe ingresar un DNI válido">*</asp:RequiredFieldValidator>
        </div>
        <div class="errorText">
            <asp:Literal ID="FailureText" runat="server" Visible="false" ></asp:Literal>
        </div>
        <div class="contenedorBotonGenerar">
            <asp:Button ID="btnRequest" runat="server" Text="Buscar" CssClass="blackButton" OnClick="btnRequest_Click"  />
        </div>
        <div id="contentSearchStudent" runat="server" visible="false" class="divResultados">
            <br />
            <div class="resultadosGen">
                <div class="resultadosLinea">
                    <asp:Label CssClass="labelsResultadosGen" ID="lblApellidoNombreResultado" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelApellidoNombre"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtApellidoNombreResultado" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="Label2" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelCarrera"] %></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtCarrera" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEmail" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEmail"]%></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEmail" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                    <asp:Label CssClass="labelsResultadosGen" ID="lblEstadoCuenta" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelEstadoCuenta"]%></asp:Label>
                    <asp:TextBox CssClass="inputsResultadosGen" ID="txtEstadoCuenta" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    
    <div id="contentSearchPlan" runat="server" visible="false">

        <div class="tituloContenido">
            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
        </div>

        <div class="planLine" id="contentCarrera" runat="server" visible="false" >
                <asp:Label CssClass="labelsCambioPass" ID="lblCarrera" runat="server" Text=""><%= ConfigurationManager.AppSettings["LabelCarrera"] %></asp:Label>
                <asp:DropDownList ID="ddlCarrera" runat="server" OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged" AutoPostBack="true" />
        </div>

        <br />
        <div class="tituloContenido">
            <asp:Label ID="lblTituloPlanViejo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitlePlanViejo"] %></asp:Label>
        </div>

        <div class="planLine" style="text-align:center" >
            <div class="planBox">
                <div class="planDescripMatMini"><br />Aprobadas</div>
                <div id="divRef1" runat="server" class="planCodMatAprobMini"><br /></div>
            </div>
            <div class="planBox">
                <div class="planDescripMatMini"><br />Habilitadas para cursar</div>
                <div id="div1" runat="server" class="planCodMatMini"><br /></div>
            </div>
            <div class="planBox">
                <div class="planDescripMatMini"><br />Bloqueadas por correlatividad</div>
                <div id="div2" runat="server" class="planCodMatCorrelativaMini"><br /></div>
            </div>
        </div>

        <div id="contentContador" runat="server" visible="false">
            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4001" runat="server" class="planCodMat">1</div>
                    <div class="planDescripMat"><br />Matemática I</div>
                    <div class="planCarga">8</div>
                </div>
                    <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4002" runat="server" class="planCodMat">2</div>
                    <div class="planDescripMat"><br />Contabilidad Básica</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4003" runat="server" class="planCodMat">3</div>
                    <div class="planDescripMat"><br />Derecho Constitucional</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div class="planCodMat">4</div>
                    <div class="planDescripMat"></div>
                    <div class="planCarga"><br /></div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4004" runat="server" class="planCodMat">5</div>
                    <div class="planDescripMat"><br />Introducción a la Filosofía</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4005" runat="server" class="planCodMat">6</div>
                    <div class="planDescripMat"><br />Historia Económica y Social Contemporánea</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C4006" runat="server" class="planCodMat">7</div>
                    <div class="planDescripMat"><br />Principios de Administración</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1</div>
                    <div id="C4007" runat="server" class="planCodMat">8</div>
                    <div class="planDescripMat"><br />Matemática II</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8</div>
                    <div id="C4008" runat="server" class="planCodMat">9</div>
                    <div class="planDescripMat"><br />Estadística</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">2</div>
                    <div id="C4009" runat="server" class="planCodMat">10</div>
                    <div class="planDescripMat"><br />Técnicas de Valuación</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="C4010" runat="server" class="planCodMat">11</div>
                    <div class="planDescripMat"><br />Derecho Civil</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">5</div>
                    <div id="C4011" runat="server" class="planCodMat">12</div>
                    <div class="planDescripMat"><br />Lógica y Metodología de las Ciencias</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="C4012" runat="server" class="planCodMat">13</div>
                    <div class="planDescripMat"><br />Elementos de Costos</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1-6</div>
                    <div id="C4013" runat="server" class="planCodMat">14</div>
                    <div class="planDescripMat"><br />Introducción a la Economía</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">7</div>
                    <div id="C4014" runat="server" class="planCodMat">15</div>
                    <div class="planDescripMat"><br />Organización y Estructuras</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">12-5</div>
                    <div id="C4015" runat="server" class="planCodMat">16</div>
                    <div class="planDescripMat"><br />Psicosociología de la Organización</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">12-15</div>
                    <div id="C4016" runat="server" class="planCodMat">17</div>
                    <div class="planDescripMat"><br />Procesamiento de Datos</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="C4117" runat="server" class="planCodMat">18</div>
                    <div class="planDescripMat"><br />Costos y Actividades Especiales</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="C4018" runat="server" class="planCodMat">19</div>
                    <div class="planDescripMat"><br />Estados Contables</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCodMat">20</div>
                    <div class="planDescripMat"></div>
                    <div class="planCarga"><br /></div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">2-3-9-12-24-29</div>
                    <div id="C4019" runat="server" class="planCodMat">21</div>
                    <div class="planDescripMat"><br /><br />Finanzas Públicas</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">8</div>
                    <div id="C4020" runat="server" class="planCodMat">22</div>
                    <div class="planDescripMat"><br />Matemática Financiera</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">11</div>
                    <div id="C4021" runat="server" class="planCodMat">23</div>
                    <div class="planDescripMat"><br />Derecho Comercial I</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div id="C4022" runat="server" class="planCodMat">24</div>
                    <div class="planDescripMat"><br />Geografía Económica</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">14</div>
                    <div id="C4023" runat="server" class="planCodMat">25</div>
                    <div class="planDescripMat"><br />Microeconomía</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">17</div>
                    <div id="C4024" runat="server" class="planCodMat">26</div>
                    <div class="planDescripMat"><br />Sistemas de Información</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">18-19-21-23</div>
                    <div id="C4125" runat="server" class="planCodMat">27</div>
                    <div class="planDescripMat"><br />Teoría y Técnica Impositiva I</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">23</div>
                    <div id="C4026" runat="server" class="planCodMat">28</div>
                    <div class="planDescripMat"><br />Derecho Comercial II</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">25</div>
                    <div id="C4027" runat="server" class="planCodMat">29</div>
                    <div class="planDescripMat"><br />Macroeconomía</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">18-19-21-26</div>
                    <div id="C4028" runat="server" class="planCodMat">30</div>
                    <div class="planDescripMat"><br />Administración y Empresas Públicas</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">27</div>
                    <div id="C4129" runat="server" class="planCodMat">31</div>
                    <div class="planDescripMat"><br />Teoría y Técnica Impositiva II</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">11</div>
                    <div id="C4030" runat="server" class="planCodMat">32</div>
                    <div class="planDescripMat"><br />Derecho Laboral y Previsional</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">16-24-29</div>
                    <div id="C4031" runat="server" class="planCodMat">33</div>
                    <div class="planDescripMat"><br />Economía Contemporánea</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">28</div>
                    <div id="C4032" runat="server" class="planCodMat">34</div>
                    <div class="planDescripMat"><br />Derecho Administrativo</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9-31</div>
                    <div id="C4133" runat="server" class="planCodMat">35</div>
                    <div class="planDescripMat"><br />Auditoría</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">18-19-21</div>
                    <div id="C4134" runat="server" class="planCodMat">36</div>
                    <div class="planDescripMat"><br />Contabilidad Pública</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">21-33</div>
                    <div id="C4035" runat="server" class="planCodMat">37</div>
                    <div class="planDescripMat"><br />Estructura Económica Argentina</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">30-35-36</div>
                    <div id="C4136" runat="server" class="planCodMat">38</div>
                    <div class="planDescripMat"><br />Seminario de Práctica Profesional Adm. Contable</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">32-34-35-36</div>
                    <div id="C4137" runat="server" class="planCodMat">39</div>
                    <div class="planDescripMat"><br />Seminario de Práctica Profesional Jurid. Contable</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">22-26</div>
                    <div id="C4038" runat="server" class="planCodMat">40</div>
                    <div class="planDescripMat"><br />Administración Financiera</div>
                    <div class="planCarga">6</div>
                </div>
            </div>
        </div>

        <div id="contentAdministracion" runat="server" visible="false">

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4001" runat="server" class="planCodMat">1</div>
                    <div class="planDescripMat"><br />Matemática I</div>
                    <div class="planCarga">8</div>
                </div>
                    <div class="planBox"> 
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4002" runat="server" class="planCodMat">2</div>
                    <div class="planDescripMat"><br />Contabilidad Básica</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4003" runat="server" class="planCodMat">3</div>
                    <div class="planDescripMat"><br />Derecho Constitucional</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div runat="server" class="planCodMat">4</div>
                    <div class="planDescripMat"></div>
                    <div class="planCarga"><br /></div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4004" runat="server" class="planCodMat">5</div>
                    <div class="planDescripMat"><br />Introducción a la Filosofía</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4005" runat="server" class="planCodMat">6</div>
                    <div class="planDescripMat"><br />Historia Económica y Social Contemporánea</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A4006" runat="server" class="planCodMat">7</div>
                    <div class="planDescripMat"><br />Principios de Administración</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1</div>
                    <div id="A4007" runat="server" class="planCodMat">8</div>
                    <div class="planDescripMat"><br />Matemática II</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8</div>
                    <div id="A4008" runat="server" class="planCodMat">9</div>
                    <div class="planDescripMat"><br />Estadística</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">2</div>
                    <div id="A4009" runat="server" class="planCodMat">10</div>
                    <div class="planDescripMat"><br />Técnicas de Valuación</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="A4010" runat="server" class="planCodMat">11</div>
                    <div class="planDescripMat"><br />Derecho Civil</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">5</div>
                    <div id="A4011" runat="server" class="planCodMat">12</div>
                    <div class="planDescripMat"><br />Lógica y Metodología de las Ciencias</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="A4012" runat="server" class="planCodMat">13</div>
                    <div class="planDescripMat"><br /><br />Elementos de Costos</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1-6</div>
                    <div id="A4013" runat="server" class="planCodMat">14</div>
                    <div class="planDescripMat"><br />Introducción a la Economía</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">7</div>
                    <div id="A4014" runat="server" class="planCodMat">15</div>
                    <div class="planDescripMat"><br />Organización y Estructuras</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">12</div>
                    <div id="A4015" runat="server" class="planCodMat">16</div>
                    <div class="planDescripMat"><br />Psicosociología de la Organización</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">15</div>
                    <div id="A4216" runat="server" class="planCodMat">17</div>
                    <div class="planDescripMat"><br />Procedimientos Administrativos</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">12-15</div>
                    <div id="A4016" runat="server" class="planCodMat">18</div>
                    <div class="planDescripMat"><br />Procesamiento de Datos</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">14</div>
                    <div id="A4023" runat="server" class="planCodMat">19</div>
                    <div class="planDescripMat"><br />Microeconomía</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">11</div>
                    <div id="A4021" runat="server" class="planCodMat">20</div>
                    <div class="planDescripMat"><br />Derecho Comercial I</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">18</div>
                    <div id="A4024" runat="server" class="planCodMat">21</div>
                    <div class="planDescripMat"><br />Sistemas de Información</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">11</div>
                    <div id="A4030" runat="server" class="planCodMat">22</div>
                    <div class="planDescripMat"><br />Derecho Laboral y Previsional</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="A4018" runat="server" class="planCodMat">23</div>
                    <div class="planDescripMat"><br />Estados Contables</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">19</div>
                    <div id="A4027" runat="server" class="planCodMat">24</div>
                    <div class="planDescripMat"><br />Macroeconomía</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">16-21</div>
                    <div id="A4224" runat="server" class="planCodMat">25</div>
                    <div class="planDescripMat"><br />Administración de Personal</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9-13-21</div>
                    <div id="A4225" runat="server" class="planCodMat">26</div>
                    <div class="planDescripMat"><br />Administración de la Producción</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">2-3-9-12-24-28</div>
                    <div id="A4019" runat="server" class="planCodMat">27</div>
                    <div class="planDescripMat"><br />Finanzas Públicas</div>
                    <div class="planCarga">8</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"></div>
                    <div id="A4022" runat="server" class="planCodMat">28</div>
                    <div class="planDescripMat"><br />Geografía Económica</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">21-23-27</div>
                    <div id="A4028" runat="server" class="planCodMat">29</div>
                    <div class="planDescripMat"><br />Administración y Empresas Públicas</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9-21</div>
                    <div id="A4229" runat="server" class="planCodMat">30</div>
                    <div class="planDescripMat"><br />Administración de la Comercialización y Distribución</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">20</div>
                    <div id="A4026" runat="server" class="planCodMat">31</div>
                    <div class="planDescripMat"><br />Derecho Comercial II</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9</div>
                    <div id="A4231" runat="server" class="planCodMat">32</div>
                    <div class="planDescripMat"><br />Investigación Operativa</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">21</div>
                    <div id="A4232" runat="server" class="planCodMat">33</div>
                    <div class="planDescripMat"><br />Teoría y Técnica de la Decisión</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">31</div>
                    <div id="A4032" runat="server" class="planCodMat">34</div>
                    <div class="planDescripMat"><br />Derecho Administrativo</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8</div>
                    <div id="A4020" runat="server" class="planCodMat">35</div>
                    <div class="planDescripMat"><br />Matemática Financiera</div>
                    <div class="planCarga">8</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">16-24-28</div>
                    <div id="A4031" runat="server" class="planCodMat">36</div>
                    <div class="planDescripMat"><br />Economía Contemporánea</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">33</div>
                    <div id="A4236" runat="server" class="planCodMat">37</div>
                    <div class="planDescripMat"><br />Dirección General</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">21-35</div>
                    <div id="A4038" runat="server" class="planCodMat">38</div>
                    <div class="planDescripMat"><br />Administración Financiera</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">33</div>
                    <div id="A4238" runat="server" class="planCodMat">39</div>
                    <div class="planDescripMat"><br />Planeamiento y Evaluación de Proyectos</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">27-36</div>
                    <div id="A4035" runat="server" class="planCodMat">40</div>
                    <div class="planDescripMat"><br />Estructura Económica Argentina</div>
                    <div class="planCarga">6</div>
                </div>
            </div>
        </div>
        <br />

        <div class="tituloContenido">
            <asp:Label ID="lblTituloPlanNuevo" runat="server" Text=""><%= ConfigurationManager.AppSettings["ContentMainTitlePlanNuevo"] %></asp:Label>
        </div>

        <div class="planLine" style="text-align:center" >
            <div class="planBox">
                <div class="planDescripMatMini"><br />Aprobadas</div>
                <div id="div3" runat="server" class="planCodMatAprobMini"><br /></div>
            </div>
            <div class="planBox">
                <div class="planDescripMatMini"><br />Habilitadas para cursar</div>
                <div id="div4" runat="server" class="planCodMatMini"><br /></div>
            </div>
            <div class="planBox">
                <div class="planDescripMatMini"><br />Bloqueadas por correlatividad</div>
                <div id="div5" runat="server" class="planCodMatCorrelativaMini"><br /></div>
            </div>
        </div>

        <div id="contentContadorNuevo" runat="server" visible="false">
            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7002" runat="server" class="planCodMat">1</div>
                    <div class="planDescripMat"><br />Contabilidad Básica</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7003" runat="server" class="planCodMat">2</div>
                    <div class="planDescripMat"><br />Derecho Constitucional</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7006" runat="server" class="planCodMat">3</div>
                    <div class="planDescripMat"><br />Principios de Administración</div>
                    <div class="planCarga">4</div>
                </div>     
            </div>

            <div class="planLine">                
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7001" runat="server" class="planCodMat">4</div>
                    <div class="planDescripMat"><br />Matemática I</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7005" runat="server" class="planCodMat">5</div>
                    <div class="planDescripMat"><br />Historia Económica Contemporánea</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="C7004" runat="server" class="planCodMat">6</div>
                    <div class="planDescripMat"><br />Metodología de las Ciencias Sociales</div>
                    <div class="planCarga">3</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="C7016" runat="server" class="planCodMat">7</div>
                    <div class="planDescripMat"><br />Tecnologías de la Información y la Comunicación</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="C7014" runat="server" class="planCodMat">8</div>
                    <div class="planDescripMat"><br />Organización y Estructuras</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">2</div>
                    <div id="C7010" runat="server" class="planCodMat">9</div>
                    <div class="planDescripMat"><br />Derecho Civil</div>
                    <div class="planCarga">3</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">4</div>
                    <div id="C7007" runat="server" class="planCodMat">10</div>
                    <div class="planDescripMat"><br />Matemática II</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="C7015" runat="server" class="planCodMat">11</div>
                    <div class="planDescripMat"><br />Comportamiento Organizacional</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1</div>
                    <div id="C7009" runat="server" class="planCodMat">12</div>
                    <div class="planDescripMat"><br />Técnicas de Valuación</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">4-5</div>
                    <div id="C7013" runat="server" class="planCodMat">13</div>
                    <div class="planDescripMat"><br />Introducción a la Economía</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">7-8</div>
                    <div id="C7024" runat="server" class="planCodMat">14</div>
                    <div class="planDescripMat"><br />Sistemas de Información</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="C7023" runat="server" class="planCodMat">15</div>
                    <div class="planDescripMat"><br />Microeconomía</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">9</div>
                    <div id="C7032" runat="server" class="planCodMat">16</div>
                    <div class="planDescripMat"><br />Derecho Administrativo</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9</div>
                    <div id="C7021" runat="server" class="planCodMat">17</div>
                    <div class="planDescripMat"><br />Derecho Comercial I</div>
                    <div class="planCarga">3</div>
                </div>                
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="C7008" runat="server" class="planCodMat">18</div>
                    <div class="planDescripMat"><br />Estadística</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">1-8</div>
                    <div id="C7012" runat="server" class="planCodMat">19</div>
                    <div class="planDescripMat"><br />Sistemas de Costos</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="C7020" runat="server" class="planCodMat">20</div>
                    <div class="planDescripMat"><br />Matemática Financiera</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">15</div>
                    <div id="C7027" runat="server" class="planCodMat">21</div>
                    <div class="planDescripMat"><br />Macroeconomía</div>
                    <div class="planCarga">4</div>
                </div>
            </div>
               
            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">17</div>
                    <div id="C7026" runat="server" class="planCodMat">22</div>
                    <div class="planDescripMat"><br />Derecho Comercial II</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">12-14-19</div>
                    <div id="C7018" runat="server" class="planCodMat">23</div>
                    <div class="planDescripMat"><br />Estados Contables</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">17</div>
                    <div id="C7030" runat="server" class="planCodMat">24</div>
                    <div class="planDescripMat"><br />Derecho del Trabajo y la Seguridad Social</div>
                    <div class="planCarga">3</div>
                </div>
            </div>


            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">16-18-21</div>
                    <div id="C7019" runat="server" class="planCodMat">25</div>
                    <div class="planDescripMat"><br /><br />Finanzas Públicas</div>
                    <div class="planCarga">6</div>
                </div>
                 <div class="planBox">
                    <div class="planCorrelativa">12-19</div>
                    <div id="C7117" runat="server" class="planCodMat">26</div>
                    <div class="planDescripMat"><br />Costos para la Toma de Decisiones</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">17-23-25</div>
                    <div id="C7125" runat="server" class="planCodMat">27</div>
                    <div class="planDescripMat"><br />Impuestos I</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">12-25</div>
                    <div id="C7134" runat="server" class="planCodMat">28</div>
                    <div class="planDescripMat"><br />Contabilidad Pública</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">20-26</div>
                    <div id="C7038" runat="server" class="planCodMat">29</div>
                    <div class="planDescripMat"><br />Administración Financiera</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">27</div>
                    <div id="C7129" runat="server" class="planCodMat">30</div>
                    <div class="planDescripMat"><br />Impuestos II</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">21</div>
                    <div id="C7031" runat="server" class="planCodMat">31</div>
                    <div class="planDescripMat"><br />Coyuntura Económica</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8-25</div>
                    <div id="C7028" runat="server" class="planCodMat">32</div>
                    <div class="planDescripMat"><br />Administración del Sector Público</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">11-26-30</div>
                    <div id="C7136" runat="server" class="planCodMat">33</div>
                    <div class="planDescripMat"><br />Seminario de Actuación Prof. en el Ámbito Admin. Contable</div>
                    <div class="planCarga">6+50 PPS</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">22-24-26-27-28</div>
                    <div id="C7137" runat="server" class="planCodMat">34</div>
                    <div class="planDescripMat"><br />Seminario de Actuación Prof. en el Ámbito Jurídico</div>
                    <div class="planCarga">6+50 PPS</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">30</div>
                    <div id="C7133" runat="server" class="planCodMat">35</div>
                    <div class="planDescripMat"><br />Auditoría</div>
                    <div class="planCarga">6</div>
                </div>
            </div>
        </div>

        <div id="contentAdministracionNuevo" runat="server" visible="false">
            <div class="planLine">                
                <div class="planBox"> 
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7002" runat="server" class="planCodMat">1</div>
                    <div class="planDescripMat"><br />Contabilidad Básica</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7003" runat="server" class="planCodMat">2</div>
                    <div class="planDescripMat"><br />Derecho Constitucional</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7006" runat="server" class="planCodMat">3</div>
                    <div class="planDescripMat"><br />Principios de Administración</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7001" runat="server" class="planCodMat">4</div>
                    <div class="planDescripMat"><br />Matemática I</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7005" runat="server" class="planCodMat">5</div>
                    <div class="planDescripMat"><br />Historia Económica Contemporánea</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa"><br /></div>
                    <div id="A7004" runat="server" class="planCodMat">6</div>
                    <div class="planDescripMat"><br />Metodología de las Ciencias Sociales</div>
                    <div class="planCarga">3</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="A7016" runat="server" class="planCodMat">7</div>
                    <div class="planDescripMat"><br />Tecnologías de la Información y la Comunicación</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">3</div>
                    <div id="A7014" runat="server" class="planCodMat">8</div>
                    <div class="planDescripMat"><br />Organización y Estructuras</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">2</div>
                    <div id="A7010" runat="server" class="planCodMat">9</div>
                    <div class="planDescripMat"><br />Derecho Civil</div>
                    <div class="planCarga">3</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">4</div>
                    <div id="A7007" runat="server" class="planCodMat">10</div>
                    <div class="planDescripMat"><br />Matemática II</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">6</div>
                    <div id="A7015" runat="server" class="planCodMat">11</div>
                    <div class="planDescripMat"><br />Comportamiento Organizacional</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">1</div>
                    <div id="A7009" runat="server" class="planCodMat">12</div>
                    <div class="planDescripMat"><br />Técnicas de Valuación</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">4-5</div>
                    <div id="A7013" runat="server" class="planCodMat">13</div>
                    <div class="planDescripMat"><br />Introducción a la Economía</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">7-8</div>
                    <div id="A7024" runat="server" class="planCodMat">14</div>
                    <div class="planDescripMat"><br />Sistemas de Información</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">13</div>
                    <div id="A7023" runat="server" class="planCodMat">15</div>
                    <div class="planDescripMat"><br />Microeconomía</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="A7020" runat="server" class="planCodMat">16</div>
                    <div class="planDescripMat"><br />Matemática Financiera</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">9</div>
                    <div id="A7021" runat="server" class="planCodMat">17</div>
                    <div class="planDescripMat"><br />Derecho Comercial I</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8</div>
                    <div id="A7216" runat="server" class="planCodMat">18</div>
                    <div class="planDescripMat"><br />Procedimientos Administrativos</div>
                    <div class="planCarga">4</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">14-11</div>
                    <div id="A7232" runat="server" class="planCodMat">19</div>
                    <div class="planDescripMat"><br />Administración Estratégica</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">17</div>
                    <div id="A7030" runat="server" class="planCodMat">20</div>
                    <div class="planDescripMat"><br />Derecho del Trabajo y la Seguridad Social</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">10</div>
                    <div id="A7008" runat="server" class="planCodMat">21</div>
                    <div class="planDescripMat"><br />Estadística</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">1-8</div>
                    <div id="A7012" runat="server" class="planCodMat">22</div>
                    <div class="planDescripMat"><br /><br />Sistemas de Costos</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">15</div>
                    <div id="A7027" runat="server" class="planCodMat">23</div>
                    <div class="planDescripMat"><br /><br />Macroeconomía</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">11-14-20</div>
                    <div id="A7224" runat="server" class="planCodMat">24</div>
                    <div class="planDescripMat"><br />Administración de Recursos Humanos</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">9</div>
                    <div id="A7032" runat="server" class="planCodMat">25</div>
                    <div class="planDescripMat"><br />Derecho Administrativo</div>
                    <div class="planCarga">3</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">12-22</div>
                    <div id="A7117" runat="server" class="planCodMat">26</div>
                    <div class="planDescripMat"><br />Costos para la Toma de Decisiones</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">21-23-25</div>
                    <div id="A7019" runat="server" class="planCodMat">27</div>
                    <div class="planDescripMat"><br />Finanzas Públicas</div>
                    <div class="planCarga">6</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">18-19-21</div>
                    <div id="A7229" runat="server" class="planCodMat">28</div>
                    <div class="planDescripMat"><br />Marketing</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">21</div>
                    <div id="A7231" runat="server" class="planCodMat">29</div>
                    <div class="planDescripMat"><br />Investigación Operativa</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">17</div>
                    <div id="A7026" runat="server" class="planCodMat">30</div>
                    <div class="planDescripMat"><br />Derecho Comercial II</div>
                    <div class="planCarga">3</div>
                </div>
            </div>

            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">14-26-29</div>
                    <div id="A7225" runat="server" class="planCodMat">31</div>
                    <div class="planDescripMat"><br />Administración de la Producción</div>
                    <div class="planCarga">6</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">23</div>
                    <div id="A7031" runat="server" class="planCodMat">32</div>
                    <div class="planDescripMat"><br />Coyuntura Económica</div>
                    <div class="planCarga">4</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">16-26</div>
                    <div id="A7038" runat="server" class="planCodMat">33</div>
                    <div class="planDescripMat"><br />Administración Financiera</div>
                    <div class="planCarga">4</div>
                </div>
            </div>
            
            <div class="planLine">
                <div class="planBox">
                    <div class="planCorrelativa">24-28-31-33</div>
                    <div id="A7238" runat="server" class="planCodMat">34</div>
                    <div class="planDescripMat"><br />Seminario de Planeamiento y Evaluación de Proyectos</div>
                    <div class="planCarga">6+50 PPS</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">24-28-31-33</div>
                    <div id="A7236" runat="server" class="planCodMat">35</div>
                    <div class="planDescripMat"><br />Seminario de Dirección General</div>
                    <div class="planCarga">6+50 PPS</div>
                </div>
                <div class="planBox">
                    <div class="planCorrelativa">8-27</div>
                    <div id="A7028" runat="server" class="planCodMat">36</div>
                    <div class="planDescripMat"><br />Administración del Sector Público</div>
                    <div class="planCarga">4</div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />

    </div>
</asp:Content>
