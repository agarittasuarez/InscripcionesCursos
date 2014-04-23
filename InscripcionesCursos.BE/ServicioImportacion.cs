using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InscripcionesCursos.BE
{
    public class ServicioImportacion
    {
        #region Constructor

        /// <summary>
		/// Initializes a new instance of the ServicioImportacion class.
		/// </summary>
		public ServicioImportacion()
		{
		}

		/// <summary>
        /// Initializes a new instance of the ServicioImportacion class.
		/// </summary>
		public ServicioImportacion(bool procesoActivo, string archivoImportacion, string idTipoImportacion, string descripcion, DateTime fechaImportacion, int usuarioImportador, DateTime fechaAlta, DateTime fechaProgramada, string logError, string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, string claseFormato)
		{
            this.ProcesoActivo = procesoActivo;
            this.ArchivoImportacion = archivoImportacion;
            this.IdTipoImportacion = idTipoImportacion;
            this.Descripcion = descripcion;
            this.FechaImportacion = fechaImportacion;
            this.UsuarioImportador = usuarioImportador;
            this.FechaAlta = fechaAlta;
            this.FechaProgramadaImportacion = fechaProgramada;
            this.LogError = logError;
            this.IdTipoInscripcion = idTipoInscripcion;
            this.TurnoInscripcion = turnoInscripcion;
            this.IdVuelta = idVuelta;
            this.ClaseFormato = claseFormato;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the IdImportacion value.
        /// </summary>
        public int IdImportacion { get; set; }

        /// <summary>
        /// Gets or sets the ProcesoActivo value.
        /// </summary>
        public bool ProcesoActivo { get; set; }

        /// <summary>
        /// Gets or sets the ArchivoImportacion value.
        /// </summary>
        public string ArchivoImportacion { get; set; }

        /// <summary>
        /// Gets or sets the IdTipoImportacion value.
        /// </summary>
        public string IdTipoImportacion { get; set; }

        /// <summary>
        /// Gets or sets the Descripcion value.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Gets or sets the FechaImportacion value.
        /// </summary>
        public DateTime FechaImportacion { get; set; }

        /// <summary>
        /// Gets or sets the UsuarioImportador value.
        /// </summary>
        public int UsuarioImportador { get; set; }

        /// <summary>
        /// Gets or sets the FechaAlta value.
        /// </summary>
        public DateTime FechaAlta { get; set; }

        /// <summary>
        /// Gets or sets the FechaProgramadaImportacion value.
        /// </summary>
        public DateTime FechaProgramadaImportacion { get; set; }

        /// <summary>
        /// Gets or sets the LogError value.
        /// </summary>
        public string LogError { get; set; }

        /// <summary>
        /// Gets or sets the IdTipoInscripcion value.
        /// </summary>
        public string IdTipoInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the TurnoInscripcion value.
        /// </summary>
        public DateTime TurnoInscripcion { get; set; }

        /// <summary>
        /// Gets or sets the IdVuelta value.
        /// </summary>
        public int IdVuelta { get; set; }

        /// <summary>
        /// Gets or sets the ClaseFormato value.
        /// </summary>
        public string ClaseFormato { get; set; }

        #endregion

    }
}
