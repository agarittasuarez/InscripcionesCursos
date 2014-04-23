using System;

namespace InscripcionesCursos.BE
{
	public class Carrera
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Carrera class.
		/// </summary>
		public Carrera()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Carrera class.
		/// </summary>
		public Carrera(string nombre)
		{
			this.Nombre = nombre;
		}

		/// <summary>
		/// Initializes a new instance of the Carrera class.
		/// </summary>
		public Carrera(int idCarrera, string nombre)
		{
			this.IdCarrera = idCarrera;
			this.Nombre = nombre;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdCarrera value.
		/// </summary>
		public int IdCarrera { get; set; }

		/// <summary>
		/// Gets or sets the Nombre value.
		/// </summary>
		public string Nombre { get; set; }

		#endregion
	}
}
