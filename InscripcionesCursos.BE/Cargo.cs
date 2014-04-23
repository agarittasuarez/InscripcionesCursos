using System;

namespace InscripcionesCursos.BE
{
	public class Cargo
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CargoDTO class.
		/// </summary>
		public Cargo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the CargoDTO class.
		/// </summary>
		public Cargo(string cargo)
		{
			this.CargoDescricpion = cargo;
		}

		/// <summary>
		/// Initializes a new instance of the CargoDTO class.
		/// </summary>
		public Cargo(int idCargo, string cargo)
		{
			this.IdCargo = idCargo;
			this.CargoDescricpion = cargo;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the IdCargo value.
		/// </summary>
		public int IdCargo { get; set; }

		/// <summary>
		/// Gets or sets the Cargo value.
		/// </summary>
        public string CargoDescricpion { get; set; }

		#endregion
	}
}
