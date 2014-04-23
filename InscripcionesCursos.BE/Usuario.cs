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
		public Usuario(int dNI, string nombreApellido, string email, int idCargo, string password, bool cambioPrimerLogin, bool cuentaActivada, int codigoActivacion, int idSede, string estado, string carrera, string cuatrimestreAnioIngreso, string cuatrimestreAnioReincorporacion)
		{
			this.DNI = dNI;
			this.ApellidoNombre = nombreApellido;
			this.Email = email;
			this.IdCargo = idCargo;
			this.Password = password;
			this.CambioPrimerLogin = cambioPrimerLogin;
			this.CuentaActivada = cuentaActivada;
			this.CodigoActivacion = codigoActivacion;
			this.IdSede = idSede;
            this.Estado = estado;
            this.Carrera = carrera;
            this.CuatrimestreAnioIngreso = cuatrimestreAnioIngreso;
            this.CuatrimestreAnioReincorporacion = cuatrimestreAnioReincorporacion;
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

		#endregion
	}
}
