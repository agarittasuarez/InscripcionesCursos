using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SharpCore.Data;
using SharpCore.Extensions;
using SharpCore.Utilities;
using InscripcionesCursos.BE;

namespace InscripcionesCursos.DAO
{
	public class InscripcionDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

        #region Objects

        public IDbTransaction inscripcionTrans;

        #endregion

		#region Constructors

		public InscripcionDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
        }

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Inscripcion table.
		/// </summary>
        public int Insert(Inscripcion inscripcion)
        {
            ValidationUtility.ValidateArgument("inscripcion", inscripcion);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", inscripcion.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", inscripcion.TurnoInscripcion),
				new SqlParameter("@IdVuelta", inscripcion.IdVuelta),
				new SqlParameter("@IdMateria", inscripcion.IdMateria),
				new SqlParameter("@CatedraComision", inscripcion.CatedraComision),
				new SqlParameter("@DNI", inscripcion.DNI),
				new SqlParameter("@IdEstadoInscripcion", inscripcion.IdEstadoInscripcion),
                new SqlParameter("@OrigenInscripcion", inscripcion.OrigenInscripcion),
                new SqlParameter("@FechaAltaInscripcion", inscripcion.FechaAltaInscripcion),
                new SqlParameter("@FechaModificacionInscripcion", inscripcion.FechaModificacionInscripcion),
                new SqlParameter("@OrigenModificacion", inscripcion.OrigenModificacion),
                new SqlParameter("@DNIEmpleadoAlta", inscripcion.DNIEmpleadoAlta),
                new SqlParameter("@DNIEmpleadoMod", inscripcion.DNIEmpleadoMod)
			};
            var id = Convert.ToInt32(SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "InscripcionInsert", parameters));
            SqlConnection.ClearAllPools();
            return id;
        }

		/// <summary>
		/// Updates a record in the Inscripcion table.
		/// </summary>
		public void Update(Inscripcion inscripcion)
		{
			ValidationUtility.ValidateArgument("inscripcion", inscripcion);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcion", inscripcion.IdInscripcion),
				new SqlParameter("@IdTipoInscripcion", inscripcion.IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", inscripcion.TurnoInscripcion),
				new SqlParameter("@IdVuelta", inscripcion.IdVuelta),
				new SqlParameter("@IdMateria", inscripcion.IdMateria),
				new SqlParameter("@CatedraComision", inscripcion.CatedraComision),
				new SqlParameter("@DNI", inscripcion.DNI),
				new SqlParameter("@IdEstadoInscripcion", inscripcion.IdEstadoInscripcion),
                new SqlParameter("@OrigenInscripcion", inscripcion.OrigenInscripcion),
                new SqlParameter("@FechaAltaInscripcion", inscripcion.FechaAltaInscripcion),
                new SqlParameter("@FechaModificacionInscripcion", inscripcion.FechaModificacionInscripcion),
                new SqlParameter("@OrigenModificacion", inscripcion.OrigenModificacion),
                new SqlParameter("@DNIEmpleadoAlta", inscripcion.DNIEmpleadoAlta),
                new SqlParameter("@DNIEmpleadoMod", inscripcion.DNIEmpleadoMod)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Inscripcion table by its primary key.
		/// </summary>
		public void Delete(int idInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcion", idInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Inscripcion table by a foreign key.
		/// </summary>
		public void DeleteAllByIdEstadoInscripcion(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDeleteAllByIdEstadoInscripcion", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Inscripcion table by a foreign key.
		/// </summary>
		public void DeleteAllByDNI(int dNI)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dNI)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDeleteAllByDNI", parameters);
            SqlConnection.ClearAllPools();
		}

        /// <summary>
        /// Deletes a record from the Inscripcion table by a foreign key.
        /// </summary>
        public void DeleteAllByIdTipoInscripcionIdVueltaTurnoInscripcionDNI(string idTipoInscripcion, DateTime turnoInscripcion, int dNI)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
                new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
				new SqlParameter("@DNI", dNI)
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDeleteAllByIdTipoInscripcion_IdVuelta_TurnoInscripcion_DNI", parameters);
            SqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Deletes all inscriptions by TurnoInscripcion and IdVuelta.
        /// </summary>
        public void DeleteAllByTurnoInscripcionIdVuelta(DateTime turnoInscripcion, int idVuelta)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdVuelta", idVuelta),
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDeleteAllByTurnoInscripcion_IdVuelta", parameters);
            SqlConnection.ClearAllPools();
        }

		/// <summary>
		/// Selects a single record from the Inscripcion table.
		/// </summary>
		public Inscripcion Select(int idInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcion", idInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelect", parameters))
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
		/// Selects a single record from the Inscripcion table.
		/// </summary>
		public string SelectJson(int idInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdInscripcion", idInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Inscripcion table.
		/// </summary>
		public List<Inscripcion> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAll"))
			{
				List<Inscripcion> inscripcionList = new List<Inscripcion>();
				while (dataReader.Read())
				{
					Inscripcion inscripcion = MapDataReader(dataReader);
					inscripcionList.Add(inscripcion);
				}
                SqlConnection.ClearAllPools();
				return inscripcionList;
			}
		}

		/// <summary>
		/// Selects all records from the Inscripcion table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Inscripcion table by a foreign key.
		/// </summary>
		public List<Inscripcion> SelectAllByIdEstadoInscripcion(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAllByIdEstadoInscripcion", parameters))
			{
				List<Inscripcion> inscripcionList = new List<Inscripcion>();
				while (dataReader.Read())
				{
					Inscripcion inscripcion = MapDataReader(dataReader);
					inscripcionList.Add(inscripcion);
				}
                SqlConnection.ClearAllPools();
				return inscripcionList;
			}
		}

		/// <summary>
		/// Selects all records from the Inscripcion table by a foreign key.
		/// </summary>
		public List<Inscripcion> SelectAllByDNI(int dNI)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dNI)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAllByDNI", parameters))
			{
				List<Inscripcion> inscripcionList = new List<Inscripcion>();
				while (dataReader.Read())
				{
					Inscripcion inscripcion = MapDataReader(dataReader);
					inscripcionList.Add(inscripcion);
				}
                SqlConnection.ClearAllPools();
				return inscripcionList;
			}
		}

		/// <summary>
		/// Selects all records from the Inscripcion table by a foreign key.
		/// </summary>
		public string SelectAllByIdEstadoInscripcionJson(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAllByIdEstadoInscripcion", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Inscripcion table by a foreign key.
		/// </summary>
		public string SelectAllByDNIJson(int dNI)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dNI)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAllByDNI", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

        /// <summary>
        /// Check if the student has more than 1 inscription
        /// </summary>
        public List<Carro> GetInscriptionsInTurn(string idTipoInscripcion, DateTime turnoInscripcion, int idVuelta, int dni, string idEstado)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion),
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdVuelta", idVuelta),
                new SqlParameter("@DNI", dni),
                new SqlParameter("@IdEstadoInscripcion", idEstado)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectByIdTipoInscripcion_TurnoInscripcion_DNI", parameters))
            {
                List<Carro> carroList = new List<Carro>();
                while (dataReader.Read())
                {
                    Carro carro = MapDataReaderCarro(dataReader);
                    carroList.Add(carro);
                }
                SqlConnection.ClearAllPools();
                return carroList;
            }
        }

        /// <summary>
        /// Selects all historic records from the Inscripcion table by DNI, joinning Materia table.
        /// </summary>
        public DataTable SelectHistoricoByDNI(int dNI, DateTime turno, string tipo)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dNI),
                new SqlParameter("@TurnoInscripcion", turno),
                new SqlParameter("@IdTipoInscripcion", tipo)
			};

            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectHistoricoByDNI", parameters));
            SqlConnection.ClearAllPools();
            return dt;
        }

        /// <summary>
        /// Selects all turnos from the Inscripcion table.
        /// </summary>
        public DataTable SelectAllTurnos(int dni)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dni)
            };

            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectTurnos", parameters));
            SqlConnection.ClearAllPools();
            return dt;
        }

        /// <summary>
        /// Selects all records from the Inscripcion table.
        /// </summary>
        public List<Inscripcion> SelectAllByTurnoInscripcionIdVuelta(string turnoInscripcion, int idVuelta)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdVuelta", idVuelta)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionSelectAllByTurnoInscripcion_IdVuelta", parameters))
            {
                List<Inscripcion> inscripcionList = new List<Inscripcion>();
                while (dataReader.Read())
                {
                    Inscripcion inscripcionAll = MapDataReader(dataReader);
                    inscripcionList.Add(inscripcionAll);
                }
                SqlConnection.ClearAllPools();
                return inscripcionList;
            }
        }

        /// <summary>
        /// Select IdVuelta by TurnoInscripcion.
        /// </summary>
        public List<int> SelectIdVueltaByTurnoInscripcion(DateTime turnoInscripcion)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "InscripcionActivaSelectVueltaByTurnoInscripcion", parameters))
            {
                List<int> vueltaList = new List<int>();
                while (dataReader.Read())
                {
                    vueltaList.Add(MapDataReaderVueltas(dataReader));
                }
                SqlConnection.ClearAllPools();
                return vueltaList;
            }
        }

        /// <summary>
        /// Validate if exists any inscription of employee
        /// </summary>
        public bool CheckEmployeeTest()
        {
            var validation = Convert.ToBoolean(SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "InscripcionCheckEmployeeTest"));
            SqlConnection.ClearAllPools();
            return validation;
        }

        /// <summary>
        /// Delete employee test inscriptions
        /// </summary>
        public void DeleteEmployeeTestIncription()
        {
            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "InscripcionDeleteAllByEmployee");
            SqlConnection.ClearAllPools();
        }

		/// <summary>
		/// Creates a new instance of the Inscripcion class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Inscripcion MapDataReader(SqlDataReader dataReader)
		{
			Inscripcion inscripcion = new Inscripcion();
			inscripcion.IdInscripcion = dataReader.GetInt32("IdInscripcion", 0);
			inscripcion.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
			inscripcion.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
			inscripcion.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
			inscripcion.IdMateria = dataReader.GetInt32("IdMateria", 0);
			inscripcion.CatedraComision = dataReader.GetString("CatedraComision", String.Empty);
			inscripcion.DNI = dataReader.GetInt32("DNI", 0);
			inscripcion.IdEstadoInscripcion = dataReader.GetString("IdEstadoInscripcion", String.Empty);
            inscripcion.OrigenInscripcion = dataReader.GetString("OrigenInscripcion", String.Empty);
            inscripcion.FechaAltaInscripcion = dataReader.GetDateTime("FechaAltaInscripcion", new DateTime(0));
            inscripcion.FechaModificacionInscripcion = dataReader.GetDateTime("FechaModificacionInscripcion", new DateTime(0));
            inscripcion.OrigenModificacion = dataReader.GetString("OrigenModificacion", String.Empty);
            inscripcion.DNIEmpleadoAlta = dataReader.GetInt32("DNIEmpleadoAlta", 0);
            inscripcion.DNIEmpleadoMod = dataReader.GetInt32("DNIEmpleadoMod", 0);

			return inscripcion;
		}

        /// <summary>
        /// Creates a integer and populates it with IdVuelta from the specified SqlDataReader.
        /// </summary>
        private int MapDataReaderVueltas(SqlDataReader dataReader)
        {
            int vuelta;
            vuelta = dataReader.GetInt32("IdVuelta", 0);

            return vuelta;
        }

        /// <summary>
        /// Creates a new instance of the Carro class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private Carro MapDataReaderCarro(SqlDataReader dataReader)
        {
            Carro carroReader = new Carro();
            carroReader.CatedraComision = dataReader.GetString("CatedraComision", String.Empty);
            carroReader.DNI = dataReader.GetInt32("DNI", 0);
            carroReader.Horario = dataReader.GetString("Horario", String.Empty);
            carroReader.IdMateria = dataReader.GetInt32("IdMateria", 0);
            carroReader.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
            carroReader.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
            carroReader.Materia = dataReader.GetString("Materia", String.Empty);
            carroReader.Profesor = dataReader.GetString("Profesor", String.Empty);
            carroReader.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
            carroReader.EstadoDescripcion = dataReader.GetString("EstadoDescripcion", String.Empty);
            carroReader.IdEstadoInscripcion = dataReader.GetString("IdEstadoInscripcion", String.Empty);
            carroReader.OrigenInscripcion = dataReader.GetString("OrigenInscripcion", String.Empty);
            carroReader.FechaAltaInscripcion = dataReader.GetDateTime("FechaAltaInscripcion", new DateTime(0));
            carroReader.FechaModificacionInscripcion = dataReader.GetDateTime("FechaModificacionInscripcion", new DateTime(0));
            carroReader.OrigenModificacion = dataReader.GetString("OrigenModificacion", String.Empty);
            carroReader.DNIEmpleadoAlta = dataReader.GetInt32("DNIEmpleadoAlta", 0);
            carroReader.DNIEmpleadoMod = dataReader.GetInt32("DNIEmpleadoMod", 0);
            carroReader.FechaDesdeHasta = dataReader.GetDateTime("FechaDesde", new DateTime(0));

            return carroReader;
        }

		#endregion
    }
}
