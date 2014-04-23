using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InscripcionesCursos.BE
{
    public class BusquedaFiltro
    {
        #region Constructors

        /// <summary>
		/// Initializes a new instance of the Carrera class.
		/// </summary>
		public BusquedaFiltro()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Carrera class.
		/// </summary>
        public BusquedaFiltro(int idDepartamento, int idCarrera, DateTime fechaActual)
		{
            this.IdDepartamento = idDepartamento;
            this.IdCarrera = idCarrera;
            this.FechaActual = fechaActual;
		}

        /// <summary>
        /// Initializes a new instance of the Carrera class.
        /// </summary>
        public BusquedaFiltro(int idDepartamento, int idCarrera, int idCodigoMateria, DateTime fechaActual)
        {
            this.IdDepartamento = idDepartamento;
            this.IdCarrera = idCarrera;
            this.IdMateria = idCodigoMateria;
            this.FechaActual = fechaActual;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IdDepartamento value.
        /// </summary>
        public int IdDepartamento { get; set; }

        /// <summary>
        /// Gets or sets the IdCarrera value.
        /// </summary>
        public int IdCarrera { get; set; }

        /// <summary>
        /// Gets or sets the IdMateria value.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Gets or sets the IdMateria value.
        /// </summary>
        public DateTime FechaActual { get; set; }

        #endregion
    }
}
