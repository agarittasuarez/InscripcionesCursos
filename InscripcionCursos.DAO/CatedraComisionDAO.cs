using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharpCore.Data;
using SharpCore.Extensions;
using SharpCore.Utilities;
using InscripcionesCursos.BE;

namespace InscripcionesCursos.DAO
{
	public class CatedraComisionDAO
    {
        #region Fields

        private string connectionStringName;

		#endregion

		#region Constructors

		public CatedraComisionDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the CatedraComision table.
		/// </summary>
		public void Insert(CatedraComision catedraComision)
		{
			ValidationUtility.ValidateArgument("catedraComision", catedraComision);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", catedraComision.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", catedraComision.TurnoInscripcion),
				new SqlParameter("@IdVuelta", catedraComision.IdVuelta),
				new SqlParameter("@IdMateria", catedraComision.IdMateria),
				new SqlParameter("@CatedraComision", catedraComision.CatedraComisionDescripcion),
				new SqlParameter("@FechaDesde", catedraComision.FechaDesde),
				new SqlParameter("@FechaHasta", catedraComision.FechaHasta),
				new SqlParameter("@Horario", catedraComision.Horario),
				new SqlParameter("@IdSede", catedraComision.IdSede),
				new SqlParameter("@ProfesorNombreApellido", catedraComision.ProfesorNombreApellido),
				new SqlParameter("@ProfesorJerarquia", catedraComision.ProfesorJerarquia),
				new SqlParameter("@ComisionAbierta", catedraComision.ComisionAbierta)
			};

			SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "CatedraComisionInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the CatedraComision table.
		/// </summary>
		public void Update(CatedraComision catedraComision)
		{
			ValidationUtility.ValidateArgument("catedraComision", catedraComision);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCatedraComision", catedraComision.IdCatedraComision),
				new SqlParameter("@IdTipoInscripcion", catedraComision.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", catedraComision.TurnoInscripcion),
				new SqlParameter("@IdVuelta", catedraComision.IdVuelta),
				new SqlParameter("@IdMateria", catedraComision.IdMateria),
				new SqlParameter("@CatedraComision", catedraComision.CatedraComisionDescripcion),
				new SqlParameter("@FechaDesde", catedraComision.FechaDesde),
				new SqlParameter("@FechaHasta", catedraComision.FechaHasta),
				new SqlParameter("@Horario", catedraComision.Horario),
				new SqlParameter("@IdSede", catedraComision.IdSede),
				new SqlParameter("@ProfesorNombreApellido", catedraComision.ProfesorNombreApellido),
				new SqlParameter("@ProfesorJerarquia", catedraComision.ProfesorJerarquia),
				new SqlParameter("@ComisionAbierta", catedraComision.ComisionAbierta)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CatedraComisionUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the CatedraComision table by its primary key.
		/// </summary>
		public void Delete(int idCatedraComision)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCatedraComision", idCatedraComision)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CatedraComisionDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the CatedraComision table by a foreign key.
		/// </summary>
		public void DeleteAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcion(string catedraComision, int idMateria, string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CatedraComision", catedraComision),
				new SqlParameter("@IdMateria", idMateria),
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CatedraComisionDeleteAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the CatedraComision table by a foreign key.
		/// </summary>
		public void DeleteAllByIdMateria(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CatedraComisionDeleteAllByIdMateria", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the CatedraComision table.
		/// </summary>
		public CatedraComision Select(int idCatedraComision)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCatedraComision", idCatedraComision)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelect", parameters))
			{
				if (dataReader.Read())
				{
					var mapper = MapDataReader(dataReader);
                    SqlConnection.ClearAllPools();
                    return mapper;
				}
				else
				{
                    SqlConnection.ClearAllPools();
					return null;
				}
			}
		}

		/// <summary>
		/// Selects a single record from the CatedraComision table.
		/// </summary>
		public string SelectJson(int idCatedraComision)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCatedraComision", idCatedraComision)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the CatedraComision table.
		/// </summary>
		public List<CatedraComision> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAll"))
			{
				List<CatedraComision> catedraComisionList = new List<CatedraComision>();
				while (dataReader.Read())
				{
					CatedraComision catedraComision = MapDataReader(dataReader);
					catedraComisionList.Add(catedraComision);
				}
                SqlConnection.ClearAllPools();
				return catedraComisionList;
			}
		}

		/// <summary>
		/// Selects all records from the CatedraComision table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the CatedraComision table by a foreign key.
		/// </summary>
		public List<CatedraComision> SelectAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcion(string catedraComision, int idMateria, string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CatedraComision", catedraComision),
				new SqlParameter("@IdMateria", idMateria),
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters))
			{
				List<CatedraComision> catedraComisionList = new List<CatedraComision>();
				while (dataReader.Read())
				{
					CatedraComision catedraComisionAll = MapDataReader(dataReader);
                    catedraComisionList.Add(catedraComisionAll);
				}
                SqlConnection.ClearAllPools();
				return catedraComisionList;
			}
		}

		/// <summary>
		/// Selects all records from the CatedraComision table by a foreign key.
		/// </summary>
		public List<CatedraComision> SelectAllByIdMateria(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByIdMateria", parameters))
			{
				List<CatedraComision> catedraComisionList = new List<CatedraComision>();
				while (dataReader.Read())
				{
					CatedraComision catedraComision = MapDataReader(dataReader);
					catedraComisionList.Add(catedraComision);
				}
                SqlConnection.ClearAllPools();
				return catedraComisionList;
			}
		}

		/// <summary>
		/// Selects all records from the CatedraComision table by a foreign key.
		/// </summary>
		public string SelectAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcionJson(string catedraComision, int idMateria, string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CatedraComision", catedraComision),
				new SqlParameter("@IdMateria", idMateria),
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByCatedraComision_IdMateria_IdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the CatedraComision table by a foreign key.
		/// </summary>
		public string SelectAllByIdMateriaJson(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByIdMateria", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

        /// <summary>
        /// Selects all records from the CatedraComision table by a foreign key.
        /// </summary>
        public List<CatedraComision> SelectAllByIdSedeAndFilters(int idSede, int idDepartamento, int idCarrera, int idMateria, DateTime fechaActual)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede),
                new SqlParameter("@IdDepartamento", idDepartamento),
                new SqlParameter("@IdCarrera", idCarrera),
                new SqlParameter("@IdMateria", idMateria),
                new SqlParameter("@FechaActual", fechaActual)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByFilter", parameters))
            {
                List<CatedraComision> catedraComisionList = new List<CatedraComision>();
                while (dataReader.Read())
                {
                    CatedraComision catedraComision = MapDataReader(dataReader);
                    catedraComisionList.Add(catedraComision);
                }
                SqlConnection.ClearAllPools();
                return catedraComisionList;
            }
        }

        /// <summary>
        /// Selects all turnos from the CatedraComision table.
        /// </summary>
        public DataTable SelectAllTurnos()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectTurnos"));
            SqlConnection.ClearAllPools();
            return dt;
        }

        /// <summary>
		/// Selects all records from the CatedraComision table.
		/// </summary>
		public List<CatedraComision> SelectAllByTurnoInscripcion(string turnoInscripcion)
		{
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CatedraComisionSelectAllByTurnoInscripcion", parameters))
            {
                List<CatedraComision> catedraComisionList = new List<CatedraComision>();
                while (dataReader.Read())
                {
                    CatedraComision catedraComisionAll = MapDataReader(dataReader);
                    catedraComisionList.Add(catedraComisionAll);
                }
                SqlConnection.ClearAllPools();
                return catedraComisionList;
            }
		}
        
		/// <summary>
		/// Creates a new instance of the CatedraComision class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private CatedraComision MapDataReader(SqlDataReader dataReader)
		{
			CatedraComision catedraComision = new CatedraComision();
			catedraComision.IdCatedraComision = dataReader.GetInt32("IdCatedraComision", 0);
			catedraComision.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
			catedraComision.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
			catedraComision.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
			catedraComision.IdMateria = dataReader.GetInt32("IdMateria", 0);
			catedraComision.CatedraComisionDescripcion = dataReader.GetString("CatedraComision", String.Empty);
			catedraComision.FechaDesde = dataReader.GetDateTime("FechaDesde", new DateTime(0));
			catedraComision.FechaHasta = dataReader.GetDateTime("FechaHasta", new DateTime(0));
			catedraComision.Horario = dataReader.GetString("Horario", null);
			catedraComision.IdSede = dataReader.GetInt32("IdSede", 0);
            catedraComision.ProfesorNombreApellido = dataReader.GetString("ProfesorNombreApellido", null);
			catedraComision.ProfesorJerarquia = dataReader.GetString("ProfesorJerarquia", null);
			catedraComision.ComisionAbierta = dataReader.GetString("ComisionAbierta", String.Empty);

			return catedraComision;
		}

		#endregion
	}
}
