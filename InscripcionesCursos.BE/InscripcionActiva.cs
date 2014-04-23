using System;

namespace InscripcionesCursos.BE
{
	public class InscripcionActiva
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the InscripcionActiva class.
		/// </summary>
		public InscripcionActiva()
		{
		}

		/// <summary>
		/// Initializes a new instance of the InscripcionActiva class.
		/// </summary>
		public InscripcionActiva(string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, DateTime inscripcionFechaDesde, DateTime inscripcionFechaHasta)
		{
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.InscripcionFechaDesde = inscripcionFechaDesde;
			this.InscripcionFechaHasta = inscripcionFechaHasta;
		}

		/// <summary>
		/// Initializes a new instance of the InscripcionActiva class.
		/// </summary>
		public InscripcionActiva(int idInscripcionActiva, string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, DateTime inscripcionFechaDesde, DateTime inscripcionFechaHasta)
		{
			this.IdInscripcionActiva = idInscripcionActiva;
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.InscripcionFechaDesde = inscripcionFechaDesde;
			this.InscripcionFechaHasta = inscripcionFechaHasta;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdInscripcionActiva value.
		/// </summary>
		public int IdInscripcionActiva { get; set; }

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
		/// Gets or sets the InscripcionFechaDesde value.
		/// </summary>
		public DateTime InscripcionFechaDesde { get; set; }

		/// <summary>
		/// Gets or sets the InscripcionFechaHasta value.
		/// </summary>
		public DateTime InscripcionFechaHasta { get; set; }

		#endregion
	}
}
