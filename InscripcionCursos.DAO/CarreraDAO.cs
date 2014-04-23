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
	public class CarreraDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public CarreraDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Carrera table.
		/// </summary>
		public void Insert(Carrera carrera)
		{
			ValidationUtility.ValidateArgument("carrera", carrera);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Nombre", carrera.Nombre)
			};

			carrera.IdCarrera = (int) SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "CarreraInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Carrera table.
		/// </summary>
		public void Update(Carrera carrera)
		{
			ValidationUtility.ValidateArgument("carrera", carrera);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", carrera.IdCarrera),
				new SqlParameter("@Nombre", carrera.Nombre)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CarreraUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Carrera table by its primary key.
		/// </summary>
		public void Delete(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CarreraDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Carrera table.
		/// </summary>
		public Carrera Select(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CarreraSelect", parameters))
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
		/// Selects a single record from the Carrera table.
		/// </summary>
		public string SelectJson(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CarreraSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Carrera table.
		/// </summary>
		public List<Carrera> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CarreraSelectAll"))
			{
				List<Carrera> carreraList = new List<Carrera>();
				while (dataReader.Read())
				{
					Carrera carrera = MapDataReader(dataReader);
					carreraList.Add(carrera);
				}
                SqlConnection.ClearAllPools();
				return carreraList;
			}
		}

		/// <summary>
		/// Selects all records from the Carrera table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CarreraSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

        /// <summary>
        /// Selects all Materia for user PlanCarrera.
        /// </summary>
        public List<PlanCarrera> GetPlan(Usuario user)
        {
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CarreraSelectAll"))
            {
                List<PlanCarrera> planList = new List<PlanCarrera>();
                while (dataReader.Read())
                {
                    PlanCarrera materia = MapDataReaderPlan(dataReader);
                    planList.Add(materia);
                }
                SqlConnection.ClearAllPools();
                return planList;
            }
        }

		/// <summary>
		/// Creates a new instance of the Carrera class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Carrera MapDataReader(SqlDataReader dataReader)
		{
			Carrera carrera = new Carrera();
			carrera.IdCarrera = dataReader.GetInt32("IdCarrera", 0);
			carrera.Nombre = dataReader.GetString("Nombre", null);

			return carrera;
		}

        /// <summary>
        /// Creates a new instance of the Carrera class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private PlanCarrera MapDataReaderPlan(SqlDataReader dataReader)
        {
            PlanCarrera planCarrera = new PlanCarrera();

            return planCarrera;
        }

		#endregion
	}
}
