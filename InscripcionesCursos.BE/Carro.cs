using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InscripcionesCursos.BE
{
    public class Carro : IEqualityComparer<Carro>
    {
        #region Constructors

		/// <summary>
		/// Initializes a new instance of the Inscripcion class.
		/// </summary>
		public Carro()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Inscripcion class.
		/// </summary>
        public Carro(string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int idMateria, string catedraComision, int dNI, string materia, string profesor, string horario, DateTime fechaDesdeHasta, string estadoDescripcion, string idEstadoInscripcion)
		{
			this.IdTipoInscripcion = idTipoInscripcion;
			this.TurnoInscripcion = turnoInscripcion;
			this.IdVuelta = idVuelta;
			this.IdMateria = idMateria;
			this.CatedraComision = catedraComision;
			this.DNI = dNI;
            this.Materia = materia;
            this.Profesor = profesor;
            this.Horario = horario;
            this.FechaDesdeHasta = fechaDesdeHasta;
            this.EstadoDescripcion = estadoDescripcion;
            this.IdEstadoInscripcion = idEstadoInscripcion;
		}

		#endregion

		#region Properties

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
        public string Materia { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public string Profesor { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public string Horario { get; set; }

        /// <summary>
        /// Gets or sets the IdEstadoInscripcion value.
        /// </summary>
        public DateTime FechaDesdeHasta { get; set; }

        /// <summary>
        /// Gets or sets the Description of Estado value.
        /// </summary>
        public string EstadoDescripcion { get; set; }

        /// <summary>
        /// Gets or set the Id of the EstadInscripcion
        /// </summary>
        public string IdEstadoInscripcion { get; set; }

        /// <summary>
        /// Gets or set the OrigenInscripcion
        /// </summary>
        public string OrigenInscripcion { get; set; }

        /// <summary>
        /// Gets or set the FechaAltaInscripcion
        /// </summary>
        public DateTime FechaAltaInscripcion { get; set; }

        /// <summary>
        /// Gets or set the FechaModificacionInscripcion
        /// </summary>
        public DateTime FechaModificacionInscripcion { get; set; }

        /// <summary>
        /// Gets or set the OrigenModificacion
        /// </summary>
        public string OrigenModificacion { get; set; }

        /// <summary>
        /// Gets or sets the DNI value.
        /// </summary>
        public int DNIEmpleadoAlta { get; set; }

        /// <summary>
        /// Gets or sets the DNI value.
        /// </summary>
        public int DNIEmpleadoMod { get; set; }

        public bool Equals(Carro x, Carro y)
        {
            if (object.ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;
            if ((x.IdMateria == y.IdMateria) && (x.CatedraComision == y.CatedraComision) && (x.IdVuelta == y.IdVuelta))
                return true;
            else
                return false;
        }

        public int GetHashCode(Carro obj)
        {
            return obj.IdMateria.GetHashCode();
        }

		#endregion
    }
}
