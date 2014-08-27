using System;

namespace InscripcionesCursos.BE
{
    public class MateriaCorrelativa
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MateriaCorrelativa class.
        /// </summary>
        public MateriaCorrelativa()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MateriaCorrelativa class.
        /// </summary>
        public MateriaCorrelativa(int idCarrera, int idMateria, int idMateriaPredecesora, string estado)
        {
            this.IdCarrera = idCarrera;
            this.IdMateria = idMateria;
            this.idMateriaPredecesora = idMateriaPredecesora;
            this.Estado = estado;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IdCarrera value.
        /// </summary>
        public int IdCarrera { get; set; }

        /// <summary>
        /// Gets or sets the IdMateria value.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Gets or sets the idMateriaPredecesora value.
        /// </summary>
        public int idMateriaPredecesora { get; set; }

        /// <summary>
        /// Gets or sets the estado value.
        /// </summary>
        public string Estado { get; set; }

        #endregion
    }
}
