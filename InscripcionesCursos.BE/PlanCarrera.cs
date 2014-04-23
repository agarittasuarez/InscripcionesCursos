using System;

namespace InscripcionesCursos.BE
{
	public class PlanCarrera
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Materia class.
		/// </summary>
		public PlanCarrera()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Materia class.
		/// </summary>
		public PlanCarrera(int idMateriaPlan, string descripcionMateria, string correlatividad, int cargaHoraria, bool aprobada)
		{
            this.IdMateriaPlan = idMateriaPlan;
            this.DescripcionMateria = descripcionMateria;
            this.Correlatividad = correlatividad;
            this.CargaHoraria = cargaHoraria;
            this.Aprobada = aprobada;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the IdMateriaPlan value.
		/// </summary>
		public int IdMateriaPlan { get; set; }

		/// <summary>
		/// Gets or sets the DescripcionMateria value.
		/// </summary>
		public string DescripcionMateria { get; set; }

		/// <summary>
		/// Gets or sets the Correlatividad value.
		/// </summary>
		public string Correlatividad { get; set; }

        /// <summary>
        /// Gets or sets the CargaHoraria value.
        /// </summary>
        public int CargaHoraria { get; set; }

        /// <summary>
        /// Gets or sets the Aprobada value.
        /// </summary>
        public bool Aprobada { get; set; }

		#endregion
	}
}
