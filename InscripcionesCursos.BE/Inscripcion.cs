using System;

namespace InscripcionesCursos.BE
{
	public class Inscripcion
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Inscripcion class.
		/// </summary>
		public Inscripcion()
		{
		}

        /// <summary>
        /// Initializes a new instance of the Inscripcion class.
        /// </summary>
        public Inscripcion(int dNi)
        {
            this.DNI = dNi;
        }

		/// <summary>
		/// Initializes a new instance of the Inscripcion class.
		/// </summary>
		public Inscripcion(string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int idMateria, string catedraComision, int dNI,
            string idEstadoInscripcion)
		{
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.IdMateria = idMateria;
			this.CatedraComision = catedraComision;
			this.DNI = dNI;
			this.IdEstadoInscripcion = idEstadoInscripcion;
		}

		/// <summary>
		/// Initializes a new instance of the Inscripcion class.
		/// </summary>
		public Inscripcion(int idInscripcion, string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int idMateria, string catedraComision,
            int dNI, string idEstadoInscripcion, string origenInscripcion, DateTime fechaAltaInscripcion, DateTime fechaModificacionInscripcion,
            string origenModificacion, int dniEmpleadoAlta, int dniEmpleadoMod)
		{
			this.IdInscripcion = idInscripcion;
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.IdMateria = idMateria;
			this.CatedraComision = catedraComision;
			this.DNI = dNI;
			this.IdEstadoInscripcion = idEstadoInscripcion;
            this.OrigenInscripcion = origenInscripcion;
            this.FechaAltaInscripcion = fechaAltaInscripcion;
            this.FechaModificacionInscripcion = fechaModificacionInscripcion;
            this.OrigenModificacion = origenModificacion;
            this.DNIEmpleadoAlta = dniEmpleadoAlta;
            this.DNIEmpleadoMod = dniEmpleadoMod;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdInscripcion value.
		/// </summary>
		public int IdInscripcion { get; set; }

		/// <summary>
		/// Gets or sets the IdTipoInscripcion value.
		/// </summary>
		public string IdTipoInscripcion { get; set; }

		/// <summary>
		/// Gets or sets the TurnoInscripcion value.
		/// </summary>
		public DateTime TurnoInscripcion { get; set; }

		/// <summary>
		/// Gets or sets the IdVuelta value.
		/// </summary>
		public int IdVuelta { get; set; }

		/// <summary>
		/// Gets or sets the IdMateria value.
		/// </summary>
		public int IdMateria { get; set; }

		/// <summary>
		/// Gets or sets the CatedraComision value.
		/// </summary>
		public string CatedraComision { get; set; }

		/// <summary>
		/// Gets or sets the DNI value.
		/// </summary>
		public int DNI { get; set; }

		/// <summary>
		/// Gets or sets the IdEstadoInscripcion value.
		/// </summary>
		public string IdEstadoInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public string OrigenInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public DateTime FechaAltaInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public DateTime FechaModificacionInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public string OrigenModificacion { get; set; }

        /// <summary>
        /// Gets or sets the Empleado DNI if Alta
        /// </summary>
        public int DNIEmpleadoAlta { get; set; }

        /// <summary>
        /// Gets or sets the Empleado DNI if Mod.
        /// </summary>
        public int DNIEmpleadoMod { get; set; }

		#endregion
	}
}
