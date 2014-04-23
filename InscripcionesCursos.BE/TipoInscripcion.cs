using System;

namespace InscripcionesCursos.BE
{
	public class TipoInscripcion
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoInscripcion class.
		/// </summary>
		public TipoInscripcion()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TipoInscripcion class.
		/// </summary>
		public TipoInscripcion(string idTipoInscripcion, string descripcion)
		{
			this.IdTipoInscripcion = idTipoInscripcion;
			this.Descripcion = descripcion;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdTipoInscripcion value.
		/// </summary>
		public string IdTipoInscripcion { get; set; }

		/// <summary>
		/// Gets or sets the Descripcion value.
		/// </summary>
		public string Descripcion { get; set; }

		#endregion
	}
}
