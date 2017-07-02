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
	public class InscripcionActivaDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public InscripcionActivaDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the InscripcionActiva table.
		/// </summary>
		public void Insert(InscripcionActiva inscripcionActiva)
		{
			ValidationUtility.ValidateArgument("inscripcionActiva", inscripcionActiva);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", inscripcionActiva.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", inscripcionActiva.TurnoInscripcion),
				new SqlParameter("@IdVuelta", inscripcionActiva.IdVuelta),
				new SqlParameter("@InscripcionFechaDesde", inscripcionActiva.InscripcionFechaDesde),
				new SqlParameter("@InscripcionFechaHasta", inscripcionActiva.InscripcionFechaHasta),
                new SqlParameter("@IdSede", inscripcionActiva.IdSede)
            };

			SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the InscripcionActiva table.
		/// </summary>
		public void Update(InscripcionActiva inscripcionActiva)
		{
			ValidationUtility.ValidateArgument("inscripcionActiva", inscripcionActiva);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcionActiva", inscripcionActiva.IdInscripcionActiva),
				new SqlParameter("@IdTipoInscripcion", inscripcionActiva.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", inscripcionActiva.TurnoInscripcion),
				new SqlParameter("@IdVuelta", inscripcionActiva.IdVuelta),
				new SqlParameter("@InscripcionFechaDesde", inscripcionActiva.InscripcionFechaDesde),
				new SqlParameter("@InscripcionFechaHasta", inscripcionActiva.InscripcionFechaHasta),
                new SqlParameter("@IdSede", inscripcionActiva.IdSede)
            };

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the InscripcionActiva table by its primary key.
		/// </summary>
		public void Delete(int idInscripcionActiva)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcionActiva", idInscripcionActiva)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the InscripcionActiva table by a foreign key.
		/// </summary>
		public void DeleteAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion(string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaDeleteAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the InscripcionActiva table by a foreign key.
		/// </summary>
		public void DeleteAllByIdTipoInscripcion(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaDeleteAllByIdTipoInscripcion", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the InscripcionActiva table by a foreign key.
		/// </summary>
		public void DeleteAllByIdVuelta(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaDeleteAllByIdVuelta", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the InscripcionActiva table.
		/// </summary>
		public InscripcionActiva Select(int idInscripcionActiva)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcionActiva", idInscripcionActiva)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelect", parameters))
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
		/// Selects a single record from the InscripcionActiva table.
		/// </summary>
		public string SelectJson(int idInscripcionActiva)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcionActiva", idInscripcionActiva)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table.
		/// </summary>
		public List<InscripcionActiva> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAll"))
			{
				List<InscripcionActiva> inscripcionActivaList = new List<InscripcionActiva>();
				while (dataReader.Read())
				{
					InscripcionActiva inscripcionActiva = MapDataReader(dataReader);
					inscripcionActivaList.Add(inscripcionActiva);
				}
                SqlConnection.ClearAllPools();
				return inscripcionActivaList;
			}
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public List<InscripcionActiva> SelectAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion(string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters))
			{
				List<InscripcionActiva> inscripcionActivaList = new List<InscripcionActiva>();
				while (dataReader.Read())
				{
					InscripcionActiva inscripcionActiva = MapDataReader(dataReader);
					inscripcionActivaList.Add(inscripcionActiva);
				}
                SqlConnection.ClearAllPools();
				return inscripcionActivaList;
			}
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public List<InscripcionActiva> SelectAllByIdTipoInscripcion(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdTipoInscripcion", parameters))
			{
				List<InscripcionActiva> inscripcionActivaList = new List<InscripcionActiva>();
				while (dataReader.Read())
				{
					InscripcionActiva inscripcionActiva = MapDataReader(dataReader);
					inscripcionActivaList.Add(inscripcionActiva);
				}
                SqlConnection.ClearAllPools();
				return inscripcionActivaList;
			}
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public List<InscripcionActiva> SelectAllByIdVuelta(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdVuelta", parameters))
			{
				List<InscripcionActiva> inscripcionActivaList = new List<InscripcionActiva>();
				while (dataReader.Read())
				{
					InscripcionActiva inscripcionActiva = MapDataReader(dataReader);
					inscripcionActivaList.Add(inscripcionActiva);
				}
                SqlConnection.ClearAllPools();
				return inscripcionActivaList;
			}
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public string SelectAllByIdTipoInscripcion_IdVuelta_TurnoInscripcionJson(string idTipoInscripcion, int idVuelta, DateTime turnoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
				new SqlParameter("@IdVuelta", idVuelta),
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
            };

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public string SelectAllByIdTipoInscripcionJson(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdTipoInscripcion", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the InscripcionActiva table by a foreign key.
		/// </summary>
		public string SelectAllByIdVueltaJson(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectAllByIdVuelta", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

        /// <summary>
        /// Selects all records from the InscripcionActiva table by Fecha
        /// </summary>
        /// <param name="dateNow"></param>
        /// <returns></returns>
        public List<InscripcionActiva> ValidateInscripcionesActivas(DateTime dateNow, int rol, int idSede)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@FechaActual", dateNow),
                new SqlParameter("@IdCargo", rol),
                new SqlParameter("@IdSede", idSede)
            };

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaValidateFecha", parameters))
            {
                List<InscripcionActiva> inscripcionActivaList = new List<InscripcionActiva>();
                while (dataReader.Read())
                {
                    InscripcionActiva inscripcionActiva = MapDataReader(dataReader);
                    inscripcionActivaList.Add(inscripcionActiva);
                }
                SqlConnection.ClearAllPools();
                return inscripcionActivaList;
            }
        }

		/// <summary>
		/// Creates a new instance of the InscripcionActiva class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private InscripcionActiva MapDataReader(SqlDataReader dataReader)
		{
			InscripcionActiva inscripcionActiva = new InscripcionActiva();
			inscripcionActiva.IdInscripcionActiva = dataReader.GetInt32("IdInscripcionActiva", 0);
			inscripcionActiva.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
			inscripcionActiva.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
			inscripcionActiva.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
			inscripcionActiva.InscripcionFechaDesde = dataReader.GetDateTime("InscripcionFechaDesde", new DateTime(0));
			inscripcionActiva.InscripcionFechaHasta = dataReader.GetDateTime("InscripcionFechaHasta", new DateTime(0));
            inscripcionActiva.IdSede = dataReader.GetInt32("IdSede", 1);

            return inscripcionActiva;
		}

		#endregion
    }
}
