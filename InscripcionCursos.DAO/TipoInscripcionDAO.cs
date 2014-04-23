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
	public class TipoInscripcionDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public TipoInscripcionDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the TipoInscripcion table.
		/// </summary>
		public void Insert(TipoInscripcion tipoInscripcion)
		{
			ValidationUtility.ValidateArgument("tipoInscripcion", tipoInscripcion);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", tipoInscripcion.IdTipoInscripcion),
				new SqlParameter("@Descripcion", tipoInscripcion.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the TipoInscripcion table.
		/// </summary>
		public void Update(TipoInscripcion tipoInscripcion)
		{
			ValidationUtility.ValidateArgument("tipoInscripcion", tipoInscripcion);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", tipoInscripcion.IdTipoInscripcion),
				new SqlParameter("@Descripcion", tipoInscripcion.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the TipoInscripcion table by its primary key.
		/// </summary>
		public void Delete(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the TipoInscripcion table.
		/// </summary>
		public TipoInscripcion Select(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionSelect", parameters))
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
		/// Selects a single record from the TipoInscripcion table.
		/// </summary>
		public string SelectJson(string idTipoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdTipoInscripcion", idTipoInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the TipoInscripcion table.
		/// </summary>
		public List<TipoInscripcion> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionSelectAll"))
			{
				List<TipoInscripcion> tipoInscripcionList = new List<TipoInscripcion>();
				while (dataReader.Read())
				{
					TipoInscripcion tipoInscripcion = MapDataReader(dataReader);
					tipoInscripcionList.Add(tipoInscripcion);
				}
                SqlConnection.ClearAllPools();
				return tipoInscripcionList;
			}
		}

		/// <summary>
		/// Selects all records from the TipoInscripcion table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoInscripcionSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the TipoInscripcion class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private TipoInscripcion MapDataReader(SqlDataReader dataReader)
		{
			TipoInscripcion tipoInscripcion = new TipoInscripcion();
			tipoInscripcion.IdTipoInscripcion = dataReader.GetString("IdTipoInscripcion", String.Empty);
			tipoInscripcion.Descripcion = dataReader.GetString("Descripcion", null);

			return tipoInscripcion;
		}

		#endregion
	}
}
