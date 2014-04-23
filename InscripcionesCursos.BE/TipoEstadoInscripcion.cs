using System;

namespace InscripcionesCursos.BE
{
	public class TipoEstadoInscripcion
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TipoEstadoInscripcion class.
		/// </summary>
		public TipoEstadoInscripcion()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TipoEstadoInscripcion class.
		/// </summary>
		public TipoEstadoInscripcion(string idEstadoInscripcion, string descripcion)
		{
			this.IdEstadoInscripcion = idEstadoInscripcion;
			this.Descripcion = descripcion;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdEstadoInscripcion value.
		/// </summary>
		public string IdEstadoInscripcion { get; set; }

		/// <summary>
		/// Gets or sets the Descripcion value.
		/// </summary>
		public string Descripcion { get; set; }

		#endregion
	}
}
