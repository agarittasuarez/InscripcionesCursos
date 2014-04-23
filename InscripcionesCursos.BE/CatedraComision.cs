using System;

namespace InscripcionesCursos.BE
{
	public class CatedraComision
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CatedraComision class.
		/// </summary>
		public CatedraComision()
		{
		}

		/// <summary>
		/// Initializes a new instance of the CatedraComision class.
		/// </summary>
        public CatedraComision(string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int idMateria, string catedraComision, DateTime fechaDesde, DateTime fechaHasta, string horario, int idSede, string profesorNombreApellido, string profesorJerarquia, string comisionAbierta)
		{
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.IdMateria = idMateria;
			this.CatedraComisionDescripcion = catedraComision;
			this.FechaDesde = fechaDesde;
			this.FechaHasta = fechaHasta;
			this.Horario = horario;
			this.IdSede = idSede;
            this.ProfesorNombreApellido = profesorNombreApellido;
			this.ProfesorJerarquia = profesorJerarquia;
			this.ComisionAbierta = comisionAbierta;
		}

		/// <summary>
		/// Initializes a new instance of the CatedraComision class.
		/// </summary>
        public CatedraComision(int idCatedraComision, string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int idMateria, string catedraComision, DateTime fechaDesde, DateTime fechaHasta, string horario, int idSede, string profesorNombreApellido, string profesorJerarquia, string comisionAbierta)
		{
			this.IdCatedraComision = idCatedraComision;
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.IdMateria = idMateria;
			this.CatedraComisionDescripcion = catedraComision;
			this.FechaDesde = fechaDesde;
			this.FechaHasta = fechaHasta;
			this.Horario = horario;
			this.IdSede = idSede;
            this.ProfesorNombreApellido = profesorNombreApellido;
			this.ProfesorJerarquia = profesorJerarquia;
			this.ComisionAbierta = comisionAbierta;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdCatedraComision value.
		/// </summary>
		public int IdCatedraComision { get; set; }

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
		public string CatedraComisionDescripcion { get; set; }

		/// <summary>
		/// Gets or sets the FechaDesde value.
		/// </summary>
		public DateTime FechaDesde { get; set; }

		/// <summary>
		/// Gets or sets the FechaHasta value.
		/// </summary>
		public DateTime FechaHasta { get; set; }

		/// <summary>
		/// Gets or sets the Horario value.
		/// </summary>
		public string Horario { get; set; }

		/// <summary>
		/// Gets or sets the IdSede value.
		/// </summary>
		public int IdSede { get; set; }

		/// <summary>
		/// Gets or sets the ProfesorNomreApellid value.
		/// </summary>
        public string ProfesorNombreApellido { get; set; }

		/// <summary>
		/// Gets or sets the ProfesorJerarquia value.
		/// </summary>
		public string ProfesorJerarquia { get; set; }

		/// <summary>
		/// Gets or sets the ComisionAbierta value.
		/// </summary>
		public string ComisionAbierta { get; set; }

		#endregion
	}
}
