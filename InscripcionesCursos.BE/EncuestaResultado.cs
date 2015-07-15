using System;
using System.Collections.Generic;

namespace InscripcionesCursos.BE
{
	public class EncuestaResultado
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Encuesta class.
		/// </summary>
        public EncuestaResultado()
		{
		}

		#endregion

		#region Properties
		/// <summary>
        /// Gets or sets the Id value.
		/// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the IdEncuesta value.
        /// </summary>
        public int IdEncuesta { get; set; }

		/// <summary>
        /// Gets or sets the DNI value.
		/// </summary>
		public int DNI { get; set; }

		/// <summary>
        /// Gets or sets the Completa value.
		/// </summary>
		public bool Completa { get; set; }

        /// <summary>
        /// Gets or sets the Activa value.
        /// </summary>
        public bool Activa { get; set; }

		#endregion
	}
}
