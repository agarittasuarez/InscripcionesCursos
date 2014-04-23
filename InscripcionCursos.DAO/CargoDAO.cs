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
	public class CargoDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public CargoDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Cargo table.
		/// </summary>
		public void Insert(Cargo cargo)
		{
			ValidationUtility.ValidateArgument("cargo", cargo);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Cargo", cargo.CargoDescricpion)
			};

			cargo.IdCargo = (int) SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "CargoInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Cargo table.
		/// </summary>
		public void Update(Cargo cargo)
		{
			ValidationUtility.ValidateArgument("cargo", cargo);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", cargo.IdCargo),
				new SqlParameter("@Cargo", cargo.CargoDescricpion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CargoUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Cargo table by its primary key.
		/// </summary>
		public void Delete(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "CargoDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Cargo table.
		/// </summary>
		public Cargo Select(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CargoSelect", parameters))
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
		/// Selects a single record from the Cargo table.
		/// </summary>
		public string SelectJson(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CargoSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Cargo table.
		/// </summary>
		public List<Cargo> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "CargoSelectAll"))
			{
				List<Cargo> CargoList = new List<Cargo>();
				while (dataReader.Read())
				{
					Cargo Cargo = MapDataReader(dataReader);
					CargoList.Add(Cargo);
				}
                SqlConnection.ClearAllPools();
				return CargoList;
			}
		}

		/// <summary>
		/// Selects all records from the Cargo table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "CargoSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the Cargo class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Cargo MapDataReader(SqlDataReader dataReader)
		{
			Cargo Cargo = new Cargo();
			Cargo.IdCargo = dataReader.GetInt32("IdCargo", 0);
			Cargo.CargoDescricpion = dataReader.GetString("Cargo", null);

			return Cargo;
		}

		#endregion
	}
}
