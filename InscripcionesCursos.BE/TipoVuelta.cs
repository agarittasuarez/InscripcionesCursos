using System;

namespace InscripcionesCursos.BE
{
	public class TipoVuelta
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoVuelta class.
		/// </summary>
		public TipoVuelta()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TipoVuelta class.
		/// </summary>
		public TipoVuelta(int idVuelta, string descripcion)
		{
			this.IdVuelta = idVuelta;
			this.Descripcion = descripcion;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdVuelta value.
		/// </summary>
		public int IdVuelta { get; set; }

		/// <summary>
		/// Gets or sets the Descripcion value.
		/// </summary>
		public string Descripcion { get; set; }

		#endregion
	}
}
