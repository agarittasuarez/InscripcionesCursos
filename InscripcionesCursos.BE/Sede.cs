using System;

namespace InscripcionesCursos.BE
{
	public class Sede
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Sede class.
		/// </summary>
		public Sede()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Sede class.
		/// </summary>
		public Sede(string nombre)
		{
			this.Nombre = nombre;
		}

		/// <summary>
		/// Initializes a new instance of the Sede class.
		/// </summary>
		public Sede(int idSede, string nombre)
		{
			this.IdSede = idSede;
			this.Nombre = nombre;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdSede value.
		/// </summary>
		public int IdSede { get; set; }

		/// <summary>
		/// Gets or sets the Nombre value.
		/// </summary>
		public string Nombre { get; set; }

		#endregion
	}
}
