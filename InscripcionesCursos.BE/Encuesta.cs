using System;
using System.Collections.Generic;

namespace InscripcionesCursos.BE
{
	public class Encuesta
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Encuesta class.
		/// </summary>
		public Encuesta()
		{
		}

		#endregion

		#region Properties
		/// <summary>
        /// Gets or sets the Id value.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
        /// Gets or sets the NombreEncuesta value.
		/// </summary>
		public string NombreEncuesta { get; set; }

		/// <summary>
        /// Gets or sets the Preguntas value.
		/// </summary>
		public List<string> Preguntas { get; set; }

        /// <summary>
        /// Gets or sets the Activa value.
        /// </summary>
        public bool Activa { get; set; }

		#endregion
	}
}
