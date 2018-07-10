using System;

namespace InscripcionesCursos.BE
{
	public class Usuario
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the UsuarioDTO class.
		/// </summary>
		public Usuario()
		{
		}

        /// <summary>
        /// Initializes a new instance of the UsuarioDTO class.
        /// </summary>
        public Usuario(int dNI)
        {
            this.DNI = dNI;
        }

		/// <summary>
		/// Initializes a new instance of the UsuarioDTO class.
		/// </summary>
		public Usuario(int dNI, string nombreApellido, string email, int idCargo, int idPerfil, string password,
            bool cambioPrimerLogin, bool cuentaActivada, int codigoActivacion, int idSede, string estado,
            string carrera, int idCarrera, string cuatrimestreAnioIngreso, string cuatrimestreAnioReincorporacion,
            bool limitacionRelevada, string limitacion, string limitacionVision, string lentes, string limitacionAudicion,
            string audifonos, string limitacionMotriz, string limitacionHabla, string limitacionAgarre,
            string dislexia, string limitacionOtra, string domicilio, string localidad, string cp, string celular)
		{
			this.DNI = dNI;
			this.ApellidoNombre = nombreApellido;
			this.Email = email;
			this.IdCargo = idCargo;
            this.IdPerfil = idPerfil;
			this.Password = password;
			this.CambioPrimerLogin = cambioPrimerLogin;
			this.CuentaActivada = cuentaActivada;
			this.CodigoActivacion = codigoActivacion;
			this.IdSede = idSede;
            this.Estado = estado;
            this.Carrera = carrera;
            this.CuatrimestreAnioIngreso = cuatrimestreAnioIngreso;
            this.CuatrimestreAnioReincorporacion = cuatrimestreAnioReincorporacion;
		    this.LimitacionRelevada = limitacionRelevada;
		    this.Limitacion = limitacion;
		    this.LimitacionVision = limitacionVision;
            this.Lentes = lentes;
		    this.LimitacionAudicion = limitacionAudicion;
            this.Audifonos = audifonos;
		    this.LimitacionMotriz = limitacionMotriz;
		    this.LimitacionHabla = limitacionHabla;
		    this.LimitacionAgarre = limitacionAgarre;
            this.Dislexia = dislexia;
		    this.LimitacionOtra = limitacionOtra;
            this.Domicilio = domicilio;
            this.Localidad = localidad;
            this.CP = cp;
            this.Celular = celular;
            this.IdCarrera = idCarrera;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the DNI value.
		/// </summary>
		public int DNI { get; set; }

		/// <summary>
		/// Gets or sets the NombreApellido value.
		/// </summary>
		public string ApellidoNombre { get; set; }

		/// <summary>
		/// Gets or sets the Email value.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the IdCargo value.
		/// </summary>
		public int IdCargo { get; set; }

        /// <summary>
        /// Gets or sets the IdCargo value.
        /// </summary>
        public int IdPerfil { get; set; }

        /// <summary>
        /// Gets or sets the Password value.
        /// </summary>
        public string Password { get; set; }

		/// <summary>
		/// Gets or sets the CambioPrimerLogin value.
		/// </summary>
		public bool CambioPrimerLogin { get; set; }

		/// <summary>
		/// Gets or sets the CuentaActivada value.
		/// </summary>
		public bool CuentaActivada { get; set; }

		/// <summary>
		/// Gets or sets the CodigoActivacion value.
		/// </summary>
		public int CodigoActivacion { get; set; }

		/// <summary>
		/// Gets or sets the IdSede value.
		/// </summary>
		public int IdSede { get; set; }

        /// <summary>
        /// Gets or sets the Estado value.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Gets or sets the IdCarrera value.
        /// </summary>
        public int IdCarrera { get; set; }

        /// <summary>
        /// Gets or sets the Carrera value.
        /// </summary>
        public string Carrera { get; set; }

        /// <summary>
        /// Gets or sets the CuatrimestreAnioIngreso value.
        /// </summary>
        public string CuatrimestreAnioIngreso { get; set; }

        /// <summary>
        /// Gets or sets the CuatrimestreAnioReincorporacion value.
        /// </summary>
        public string CuatrimestreAnioReincorporacion { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionRelevada value.
        /// </summary>
        public bool LimitacionRelevada { get; set; }

        /// <summary>
        /// Gets or sets the Limitacion value.
        /// </summary>
        public string Limitacion { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionVision value.
        /// </summary>
        public string LimitacionVision { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionVision value.
        /// </summary>
        public string Lentes { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionAudicion value.
        /// </summary>
        public string LimitacionAudicion { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionAudicion value.
        /// </summary>
        public string Audifonos { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionMotriz value.
        /// </summary>
        public string LimitacionMotriz { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionAgarre value.
        /// </summary>
        public string LimitacionAgarre { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionHabla value.
        /// </summary>
        public string LimitacionHabla { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionHabla value.
        /// </summary>
        public string Dislexia { get; set; }

        /// <summary>
        /// Gets or sets the LimitacionOtra value.
        /// </summary>
        public string LimitacionOtra { get; set; }

        /// <summary>
        /// Gets or sets the Domicilio value.
        /// </summary>
        public string Domicilio { get; set; }

        /// <summary>
        /// Gets or sets the Localidad value.
        /// </summary>
        public string Localidad { get; set; }

        /// <summary>
        /// Gets or sets the CP value.
        /// </summary>
        public string CP { get; set; }

        /// <summary>
        /// Gets or sets the Celular value.
        /// </summary>
        public string Celular { get; set; }

        #endregion
    }
}
