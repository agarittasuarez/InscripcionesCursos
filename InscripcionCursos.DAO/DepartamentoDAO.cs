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
	public class DepartamentoDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public DepartamentoDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Departamento table.
		/// </summary>
		public void Insert(Departamento departamento)
		{
			ValidationUtility.ValidateArgument("departamento", departamento);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Nombre", departamento.Nombre)
			};

			departamento.IdDepartamento = (int) SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "DepartamentoInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Departamento table.
		/// </summary>
		public void Update(Departamento departamento)
		{
			ValidationUtility.ValidateArgument("departamento", departamento);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", departamento.IdDepartamento),
				new SqlParameter("@Nombre", departamento.Nombre)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "DepartamentoUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Departamento table by its primary key.
		/// </summary>
		public void Delete(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "DepartamentoDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Departamento table.
		/// </summary>
		public Departamento Select(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "DepartamentoSelect", parameters))
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
		/// Selects a single record from the Departamento table.
		/// </summary>
		public string SelectJson(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "DepartamentoSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Departamento table.
		/// </summary>
		public List<Departamento> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "DepartamentoSelectAll"))
			{
				List<Departamento> departamentoList = new List<Departamento>();
				while (dataReader.Read())
				{
					Departamento departamento = MapDataReader(dataReader);
					departamentoList.Add(departamento);
				}
                SqlConnection.ClearAllPools();
				return departamentoList;
			}
		}

		/// <summary>
		/// Selects all records from the Departamento table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "DepartamentoSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the Departamento class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Departamento MapDataReader(SqlDataReader dataReader)
		{
			Departamento departamento = new Departamento();
			departamento.IdDepartamento = dataReader.GetInt32("IdDepartamento", 0);
			departamento.Nombre = dataReader.GetString("Nombre", null);

			return departamento;
		}

		#endregion
	}
}
