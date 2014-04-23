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
	public class TipoVueltaDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public TipoVueltaDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the TipoVuelta table.
		/// </summary>
		public void Insert(TipoVuelta tipoVuelta)
		{
			ValidationUtility.ValidateArgument("tipoVuelta", tipoVuelta);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", tipoVuelta.IdVuelta),
				new SqlParameter("@Descripcion", tipoVuelta.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoVueltaInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the TipoVuelta table.
		/// </summary>
		public void Update(TipoVuelta tipoVuelta)
		{
			ValidationUtility.ValidateArgument("tipoVuelta", tipoVuelta);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", tipoVuelta.IdVuelta),
				new SqlParameter("@Descripcion", tipoVuelta.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoVueltaUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the TipoVuelta table by its primary key.
		/// </summary>
		public void Delete(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoVueltaDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the TipoVuelta table.
		/// </summary>
		public TipoVuelta Select(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoVueltaSelect", parameters))
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
		/// Selects a single record from the TipoVuelta table.
		/// </summary>
		public string SelectJson(int idVuelta)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdVuelta", idVuelta)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoVueltaSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the TipoVuelta table.
		/// </summary>
		public List<TipoVuelta> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoVueltaSelectAll"))
			{
				List<TipoVuelta> tipoVueltaList = new List<TipoVuelta>();
				while (dataReader.Read())
				{
					TipoVuelta tipoVuelta = MapDataReader(dataReader);
					tipoVueltaList.Add(tipoVuelta);
				}
                SqlConnection.ClearAllPools();
				return tipoVueltaList;
			}
		}

		/// <summary>
		/// Selects all records from the TipoVuelta table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoVueltaSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the TipoVuelta class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private TipoVuelta MapDataReader(SqlDataReader dataReader)
		{
			TipoVuelta tipoVuelta = new TipoVuelta();
			tipoVuelta.IdVuelta = dataReader.GetInt32("IdVuelta", 0);
			tipoVuelta.Descripcion = dataReader.GetString("Descripcion", null);

			return tipoVuelta;
		}

		#endregion
	}
}
