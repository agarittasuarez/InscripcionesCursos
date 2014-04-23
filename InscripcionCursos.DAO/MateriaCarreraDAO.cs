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
	public class MateriaCarreraDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public MateriaCarreraDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the MateriaCarrera table.
		/// </summary>
		public void Insert(MateriaCarrera materiaCarrera)
		{
			ValidationUtility.ValidateArgument("materiaCarrera", materiaCarrera);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", materiaCarrera.IdMateria),
				new SqlParameter("@IdCarrera", materiaCarrera.IdCarrera)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the MateriaCarrera table by its primary key.
		/// </summary>
		public void Delete(int idMateria, int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria),
				new SqlParameter("@IdCarrera", idCarrera)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the MateriaCarrera table by a foreign key.
		/// </summary>
		public void DeleteAllByIdCarrera(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraDeleteAllByIdCarrera", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the MateriaCarrera table by a foreign key.
		/// </summary>
		public void DeleteAllByIdMateria(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraDeleteAllByIdMateria", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects all records from the MateriaCarrera table by a foreign key.
		/// </summary>
		public List<MateriaCarrera> SelectAllByIdCarrera(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraSelectAllByIdCarrera", parameters))
			{
				List<MateriaCarrera> materiaCarreraList = new List<MateriaCarrera>();
				while (dataReader.Read())
				{
					MateriaCarrera materiaCarrera = MapDataReader(dataReader);
					materiaCarreraList.Add(materiaCarrera);
				}
                SqlConnection.ClearAllPools();
				return materiaCarreraList;
			}
		}

		/// <summary>
		/// Selects all records from the MateriaCarrera table by a foreign key.
		/// </summary>
		public List<MateriaCarrera> SelectAllByIdMateria(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraSelectAllByIdMateria", parameters))
			{
				List<MateriaCarrera> materiaCarreraList = new List<MateriaCarrera>();
				while (dataReader.Read())
				{
					MateriaCarrera materiaCarrera = MapDataReader(dataReader);
					materiaCarreraList.Add(materiaCarrera);
				}
                SqlConnection.ClearAllPools();
				return materiaCarreraList;
			}
		}

		/// <summary>
		/// Selects all records from the MateriaCarrera table by a foreign key.
		/// </summary>
		public string SelectAllByIdCarreraJson(int idCarrera)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCarrera", idCarrera)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraSelectAllByIdCarrera", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the MateriaCarrera table by a foreign key.
		/// </summary>
		public string SelectAllByIdMateriaJson(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "MateriaCarreraSelectAllByIdMateria", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the MateriaCarrera class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private MateriaCarrera MapDataReader(SqlDataReader dataReader)
		{
			MateriaCarrera materiaCarrera = new MateriaCarrera();
			materiaCarrera.IdMateria = dataReader.GetInt32("IdMateria", 0);
			materiaCarrera.IdCarrera = dataReader.GetInt32("IdCarrera", 0);

			return materiaCarrera;
		}

		#endregion
	}
}
