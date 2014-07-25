using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCore.Utilities;
using System.Data.SqlClient;
using SharpCore.Data;
using SharpCore.Extensions;
using InscripcionesCursos.BE;
using System.Data;

namespace InscripcionesCursos.DAO
{
    public class ServicioImportacionDAO
    {
        #region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

        public ServicioImportacionDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Select all types of Tipo Importacion
		/// </summary>
        public List<ServicioImportacion> GetServicioTipoImportacion()
		{
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTipoImportacion"))
			{
                List<ServicioImportacion> TipoImportacionList = new List<ServicioImportacion>();
				while (dataReader.Read())
				{
                    ServicioImportacion tipoImportacion = MapDataReaderTipoImportacion(dataReader);
                    TipoImportacionList.Add(tipoImportacion);
				}
                SqlConnection.ClearAllPools();
                return TipoImportacionList;
			}
        }

        /// <summary>
        /// Select all active porcess
        /// </summary>
        public List<ServicioImportacion> GetActiveProcess()
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectImportacionHistorico"))
            {
                List<ServicioImportacion> ActiveProcessList = new List<ServicioImportacion>();
                while (dataReader.Read())
                {
                    ServicioImportacion activeProcess = MapDataReaderActiveProcess(dataReader);
                    ActiveProcessList.Add(activeProcess);
                }
                SqlConnection.ClearAllPools();
                return ActiveProcessList;
            }
        }

        /// <summary>
        /// Select all porcess with errors
        /// </summary>
        public List<ServicioImportacion> GetErrorProcess(string idTipoImportacion, string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoImportacion", idTipoImportacion),
                new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdVuelta", idVuelta),
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectLogError", parameters))
            {
                List<ServicioImportacion> ErrorProcessList = new List<ServicioImportacion>();
                while (dataReader.Read())
                {
                    ServicioImportacion errorProcess = MapDataReaderErrorProcess(dataReader);
                    ErrorProcessList.Add(errorProcess);
                }
                SqlConnection.ClearAllPools();
                return ErrorProcessList;
            }
        }

        /// <summary>
        /// Select all types of Tipo Inscripcion
        /// </summary>
        public List<TipoInscripcion> GetServicioTipoInscripcion()
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTipoInscripcion"))
            {
                List<TipoInscripcion> TipoInscripcionList = new List<TipoInscripcion>();
                while (dataReader.Read())
                {
                    TipoInscripcion tipoInscripcion = MapDataReaderTipoIinscripcion(dataReader);
                    TipoInscripcionList.Add(tipoInscripcion);
                }
                SqlConnection.ClearAllPools();
                return TipoInscripcionList;
            }
        }

        /// <summary>
        /// Select all types of TurnoImportacion
        /// </summary>
        public List<String> GetServicioTurnoInscripcion()
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTurnosInscripcion"))
            {
                List<String> TurnoInscripcionList = new List<String>();
                while (dataReader.Read())
                {
                    string turnoImportacion = MapDataReaderTurnoInscripcion(dataReader);
                    TurnoInscripcionList.Add(turnoImportacion);
                }
                SqlConnection.ClearAllPools();
                return TurnoInscripcionList;
            }
        }

        /// <summary>
        /// Select all types of VueltaInscripcion
        /// </summary>
        public List<TipoVuelta> GetServicioVueltaInscripcion()
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectVueltaInscripcion"))
            {
                List<TipoVuelta> TipoVueltaList = new List<TipoVuelta>();
                while (dataReader.Read())
                {
                    TipoVuelta tipoVuelta = MapDataReaderTipoVuelta(dataReader);
                    TipoVueltaList.Add(tipoVuelta);
                }
                SqlConnection.ClearAllPools();
                return TipoVueltaList;
            }
        }

        /// <summary>
        /// Select all types of Tipo Importacion
        /// </summary>
        public List<ServicioImportacion> GetServicioTipoImportacionError()
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTipoImportacionError"))
            {
                List<ServicioImportacion> TipoImportacionList = new List<ServicioImportacion>();
                while (dataReader.Read())
                {
                    ServicioImportacion tipoImportacion = MapDataReaderTipoImportacion(dataReader);
                    TipoImportacionList.Add(tipoImportacion);
                }
                SqlConnection.ClearAllPools();
                return TipoImportacionList;
            }
        }

        /// <summary>
        /// Select all types of Tipo Inscripcion
        /// </summary>
        public List<TipoInscripcion> GetServicioTipoInscripcionError(string tipo)
        {
            ValidationUtility.ValidateArgument("tipo", tipo);
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoImportacion", tipo),
			};
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTipoInscripcionError", parameters))
            {
                List<TipoInscripcion> TipoInscripcionList = new List<TipoInscripcion>();
                while (dataReader.Read())
                {
                    TipoInscripcion tipoInscripcion = MapDataReaderTipoIinscripcion(dataReader);
                    TipoInscripcionList.Add(tipoInscripcion);
                }
                SqlConnection.ClearAllPools();
                return TipoInscripcionList;
            }
        }

        /// <summary>
        /// Select all types of TurnoImportacion
        /// </summary>
        public List<String> GetServicioTurnoInscripcionError(string idTipoImportacion, string idTipoInscripcion)
        {
            ValidationUtility.ValidateArgument("idTipoImportacion", idTipoImportacion);
            ValidationUtility.ValidateArgument("idTipoInscripcion", idTipoInscripcion);
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoImportacion", idTipoImportacion),
                new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectTurnoInscripcionError", parameters))
            {
                List<String> TurnoInscripcionList = new List<String>();
                while (dataReader.Read())
                {
                    string turnoImportacion = MapDataReaderTurnoInscripcion(dataReader);
                    TurnoInscripcionList.Add(turnoImportacion);
                }
                SqlConnection.ClearAllPools();
                return TurnoInscripcionList;
            }
        }

        /// <summary>
        /// Select all types of VueltaInscripcion
        /// </summary>
        public List<TipoVuelta> GetServicioVueltaInscripcionError(string idTipoImportacion, string idTipoInscripcion, DateTime turnoInscripcion)
        {
            ValidationUtility.ValidateArgument("idTipoImportacion", idTipoImportacion);
            ValidationUtility.ValidateArgument("idTipoInscripcion", idTipoInscripcion);
            ValidationUtility.ValidateArgument("turnoInscripcion", turnoInscripcion);
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoImportacion", idTipoImportacion),
                new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ServicioSelectVueltaInscripcionError", parameters))
            {
                List<TipoVuelta> TipoVueltaList = new List<TipoVuelta>();
                while (dataReader.Read())
                {
                    TipoVuelta tipoVuelta = MapDataReaderTipoVuelta(dataReader);
                    TipoVueltaList.Add(tipoVuelta);
                }
                SqlConnection.ClearAllPools();
                return TipoVueltaList;
            }
        }

        /// <summary>
        /// Saves a record to the ServicioImportacion table.
        /// </summary>
        public int InsertServicio(ServicioImportacion service)
        {
            ValidationUtility.ValidateArgument("service", service);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProcesoActivo", service.ProcesoActivo),
				new SqlParameter("@ArchivoImportacion", service.ArchivoImportacion),
				new SqlParameter("@IdTipoImportacion", service.IdTipoImportacion),
				new SqlParameter("@FechaImportacion", service.FechaImportacion),
				new SqlParameter("@UsuarioImportador", service.UsuarioImportador),
				new SqlParameter("@FechaAlta", service.FechaAlta),
				new SqlParameter("@FechaProgramadaImportacion", service.FechaProgramadaImportacion),
                new SqlParameter("@LogError", service.LogError),
                new SqlParameter("@IdTipoInscripcion", service.IdTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", service.TurnoInscripcion),
                new SqlParameter("@IdVuelta", service.IdVuelta),
                new SqlParameter("@ClaseFormato", service.ClaseFormato),
			};
            var id = Convert.ToInt32(SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "ServicioInsert", parameters));
            SqlConnection.ClearAllPools();
            return id;
        }

        /// <summary>
        /// Update statuss Process ServicioImportacion table.
        /// </summary>
        public void DeactivateImportProcess(ServicioImportacion service)
        {
            ValidationUtility.ValidateArgument("service", service);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdImportacion", service.IdImportacion),
			};
            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "ServicioBajaImportacion", parameters);
        }

        /// <summary>
        /// Validate the predecessor steps
        /// </summary>
        public bool ValidatePredecessor(ServicioImportacion service)
        {
            ValidationUtility.ValidateArgument("service", service);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoImportacion", service.IdTipoImportacion),
                new SqlParameter("@IdTipoInscripcion", service.IdTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", service.TurnoInscripcion),
                new SqlParameter("@IdVuelta", service.IdVuelta),
			};
            var validation = Convert.ToBoolean(SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "ServicioValidatePredecessor", parameters));
            SqlConnection.ClearAllPools();
            return validation;
        }

        /// <summary>
        /// Validate if there inscriptions on course
        /// </summary>
        public bool ValidateInscriptionOnCourse(ServicioImportacion service)
        {
            ValidationUtility.ValidateArgument("service", service);

            SqlParameter[] parameters = new SqlParameter[]
			{
                new SqlParameter("@IdTipoImportacion", service.IdTipoImportacion),
                new SqlParameter("@IdTipoInscripcion", service.IdTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", service.TurnoInscripcion),
                new SqlParameter("@IdVuelta", service.IdVuelta),
			};
            var validation = Convert.ToBoolean(SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "ServicioValidateInscriptionOnCourse", parameters));
            SqlConnection.ClearAllPools();
            return validation;
        }

        /// <summary>
        /// Creates a new instance of the ServicioImportacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private ServicioImportacion MapDataReaderTipoImportacion(SqlDataReader dataReader)
        {
            ServicioImportacion tipoImportacion = new ServicioImportacion();
            tipoImportacion.IdTipoImportacion = dataReader.GetString("IdTipoImportacion", String.Empty);
            tipoImportacion.Descripcion = dataReader.GetString("Descripcion", String.Empty);

            return tipoImportacion;
        }

        /// <summary>
        /// Creates a new instance of the TipoImportacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private TipoInscripcion MapDataReaderTipoIinscripcion(SqlDataReader dataReader)
        {
            TipoInscripcion tipoInscripcion = new TipoInscripcion();
            tipoInscripcion.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
            tipoInscripcion.Descripcion = dataReader.GetString("Descripcion", String.Empty);

            return tipoInscripcion;
        }

        /// <summary>
        /// Creates a new instance of the ServicioImportacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private String MapDataReaderTurnoInscripcion(SqlDataReader dataReader)
        {
            string turnoInscripcion;
            turnoInscripcion = dataReader.GetString("TurnoInscripcion", String.Empty);

            return turnoInscripcion;
        }

        /// <summary>
        /// Creates a new instance of the TipoVuelta class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private TipoVuelta MapDataReaderTipoVuelta(SqlDataReader dataReader)
        {
            TipoVuelta tipoVuelta = new TipoVuelta();
            tipoVuelta.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
            tipoVuelta.Descripcion = dataReader.GetString("Descripcion", String.Empty);

            return tipoVuelta;
        }

        /// <summary>
        /// Creates a new instance of the ServicioImportacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private ServicioImportacion MapDataReaderActiveProcess(SqlDataReader dataReader)
        {
            ServicioImportacion process = new ServicioImportacion();
            process.ArchivoImportacion = dataReader.GetString("ArchivoImportacion", String.Empty);
            process.FechaAlta = dataReader.GetDateTime("FechaAlta", new DateTime(0));
            process.FechaProgramadaImportacion = dataReader.GetDateTime("FechaProgramadaImportacion", new DateTime(0));
            process.IdImportacion = dataReader.GetInt32("IdImportacion", 0);
            process.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
            process.ProcesoActivo = dataReader.GetBoolean("ProcesoActivo", false);
            process.Descripcion = dataReader.GetString("TipoImportacionDescripcion", String.Empty);
            process.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
            process.UsuarioImportador = dataReader.GetInt32("UsuarioImportador", 0);
            process.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
            process.LogError = dataReader.GetString("LogError", String.Empty);

            return process;
        }

        /// <summary>
        /// Creates a new instance of the ServicioImportacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private ServicioImportacion MapDataReaderErrorProcess(SqlDataReader dataReader)
        {
            ServicioImportacion process = new ServicioImportacion();
            process.IdImportacion = dataReader.GetInt32("IdImportacion", 0);
            process.LogError = dataReader.GetString("LogError", String.Empty);

            return process;
        }

        #endregion
    }
}