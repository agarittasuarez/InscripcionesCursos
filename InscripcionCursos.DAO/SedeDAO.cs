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
	public class SedeDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public SedeDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Sede table.
		/// </summary>
		public void Insert(Sede sede)
		{
			ValidationUtility.ValidateArgument("sede", sede);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Nombre", sede.Nombre)
			};

			sede.IdSede = (int) SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "SedeInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Sede table.
		/// </summary>
		public void Update(Sede sede)
		{
			ValidationUtility.ValidateArgument("sede", sede);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", sede.IdSede),
				new SqlParameter("@Nombre", sede.Nombre)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "SedeUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Sede table by its primary key.
		/// </summary>
		public void Delete(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "SedeDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Sede table.
		/// </summary>
		public Sede Select(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "SedeSelect", parameters))
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
		/// Selects a single record from the Sede table.
		/// </summary>
		public string SelectJson(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "SedeSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Sede table.
		/// </summary>
		public List<Sede> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "SedeSelectAll"))
			{
				List<Sede> sedeList = new List<Sede>();
				while (dataReader.Read())
				{
					Sede sede = MapDataReader(dataReader);
					sedeList.Add(sede);
				}
                SqlConnection.ClearAllPools();
				return sedeList;
			}
		}

		/// <summary>
		/// Selects all records from the Sede table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "SedeSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the Sede class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Sede MapDataReader(SqlDataReader dataReader)
		{
			Sede sede = new Sede();
			sede.IdSede = dataReader.GetInt32("IdSede", 0);
			sede.Nombre = dataReader.GetString("Nombre", null);

			return sede;
		}

		#endregion
	}
}
