using System;

namespace InscripcionesCursos.BE
{
	public class Materia
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Materia class.
		/// </summary>
		public Materia()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Materia class.
		/// </summary>
		public Materia(int idMateria, int idDepartamento, string descripcion)
		{
			this.IdMateria = idMateria;
			this.IdDepartamento = idDepartamento;
			this.Descripcion = descripcion;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdMateria value.
		/// </summary>
		public int IdMateria { get; set; }

		/// <summary>
		/// Gets or sets the IdDepartamento value.
		/// </summary>
		public int IdDepartamento { get; set; }

		/// <summary>
		/// Gets or sets the Descripcion value.
		/// </summary>
		public string Descripcion { get; set; }

		#endregion
	}
}
