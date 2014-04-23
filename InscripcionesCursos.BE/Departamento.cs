using System;

namespace InscripcionesCursos.BE
{
	public class Departamento
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Departamento class.
		/// </summary>
		public Departamento()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Departamento class.
		/// </summary>
		public Departamento(string nombre)
		{
			this.Nombre = nombre;
		}

		/// <summary>
		/// Initializes a new instance of the Departamento class.
		/// </summary>
		public Departamento(int idDepartamento, string nombre)
		{
			this.IdDepartamento = idDepartamento;
			this.Nombre = nombre;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdDepartamento value.
		/// </summary>
		public int IdDepartamento { get; set; }

		/// <summary>
		/// Gets or sets the Nombre value.
		/// </summary>
		public string Nombre { get; set; }

		#endregion
	}
}
