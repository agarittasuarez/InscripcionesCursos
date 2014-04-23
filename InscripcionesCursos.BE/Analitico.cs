using System;

namespace InscripcionesCursos.BE
{
	public class Analitico
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HistoricoAnalitico class.
		/// </summary>
		public Analitico()
		{
		}

		/// <summary>
		/// Initializes a new instance of the HistoricoAnalitico class.
		/// </summary>
        public Analitico(string catedraComision, int dNI, DateTime fecha, string folio, string libro, int idMateria, string materia, double nota, int plan, string resolucion, string subFolio, string tipoInscripcion, string tomo, DateTime turnoInscripcion, DateTime ultimoIngreso)
		{
			this.CatedraComision = catedraComision;
            this.DNI = dNI;
			this.Fecha = fecha;
			this.Folio = folio;
            this.IdMateria = idMateria;
			this.Libro = libro;
			this.Materia = materia;
			this.Nota = nota;
            this.Plan = plan;
			this.Resolucion = resolucion;
			this.SubFolio = subFolio;
			this.TipoInscripcion = tipoInscripcion;
			this.Tomo = tomo;
            this.TurnoInscripcion = turnoInscripcion;
            this.UltimoIngreso = ultimoIngreso;
		}

		#endregion

		#region Properties
		/// <summary>
        /// Gets or sets the TipoInscripcion value.
		/// </summary>
		public string TipoInscripcion { get; set; }

		/// <summary>
        /// Gets or sets the TurnoInscripcion value.
		/// </summary>
		public DateTime TurnoInscripcion { get; set; }

		/// <summary>
        /// Gets or sets the Materia value.
		/// </summary>
		public string Materia { get; set; }

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
        /// Gets or sets the Carrera value.
		/// </summary>
		public int Plan { get; set; }

        /// <summary>
        /// Gets or sets the Fecha value.
        /// </summary>
        public DateTime Fecha { get; set; }

		/// <summary>
        /// Gets or sets the Nota value.
		/// </summary>
		public double Nota { get; set; }

		/// <summary>
		/// Gets or sets the Libro value.
		/// </summary>
		public string Libro { get; set; }

		/// <summary>
		/// Gets or sets the Tomo value.
		/// </summary>
		public string Tomo { get; set; }

		/// <summary>
		/// Gets or sets the Folio value.
		/// </summary>
		public string Folio { get; set; }

        /// <summary>
        /// Gets or sets the SubFolio value.
        /// </summary>
        public string SubFolio { get; set; }

		/// <summary>
        /// Gets or sets the Resolucion value.
		/// </summary>
		public string Resolucion { get; set; }

        /// <summary>
        /// Gets or sets the UltimoIngreso value.
        /// </summary>
        public DateTime UltimoIngreso { get; set; }

        /// <summary>
        /// Gets or sets the CodigoMovimiento value.
        /// </summary>
        public string CodigoMovimiento { get; set; }

        /// <summary>
        /// Gets or sets the IdTipoInscripcion value.
        /// </summary>
        public string IdTipoInscripcion { get; set; }

		#endregion
	}
}
