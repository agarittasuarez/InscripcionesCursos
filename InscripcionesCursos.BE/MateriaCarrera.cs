using System;

namespace InscripcionesCursos.BE
{
	public class MateriaCarrera
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MateriaCarrera class.
		/// </summary>
		public MateriaCarrera()
		{
		}

		/// <summary>
		/// Initializes a new instance of the MateriaCarrera class.
		/// </summary>
		public MateriaCarrera(int idMateria, int idCarrera)
		{
			this.IdMateria = idMateria;
			this.IdCarrera = idCarrera;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdMateria value.
		/// </summary>
		public int IdMateria { get; set; }

		/// <summary>
		/// Gets or sets the IdCarrera value.
		/// </summary>
		public int IdCarrera { get; set; }

		#endregion
	}
}
