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
	public class TipoEstadoInscripcionDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public TipoEstadoInscripcionDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the TipoEstadoInscripcion table.
		/// </summary>
		public void Insert(TipoEstadoInscripcion tipoEstadoInscripcion)
		{
			ValidationUtility.ValidateArgument("tipoEstadoInscripcion", tipoEstadoInscripcion);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", tipoEstadoInscripcion.IdEstadoInscripcion),
				new SqlParameter("@Descripcion", tipoEstadoInscripcion.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the TipoEstadoInscripcion table.
		/// </summary>
		public void Update(TipoEstadoInscripcion tipoEstadoInscripcion)
		{
			ValidationUtility.ValidateArgument("tipoEstadoInscripcion", tipoEstadoInscripcion);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", tipoEstadoInscripcion.IdEstadoInscripcion),
				new SqlParameter("@Descripcion", tipoEstadoInscripcion.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the TipoEstadoInscripcion table by its primary key.
		/// </summary>
		public void Delete(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the TipoEstadoInscripcion table.
		/// </summary>
		public TipoEstadoInscripcion Select(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionSelect", parameters))
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
		/// Selects a single record from the TipoEstadoInscripcion table.
		/// </summary>
		public string SelectJson(string idEstadoInscripcion)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdEstadoInscripcion", idEstadoInscripcion)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the TipoEstadoInscripcion table.
		/// </summary>
		public List<TipoEstadoInscripcion> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionSelectAll"))
			{
				List<TipoEstadoInscripcion> tipoEstadoInscripcionList = new List<TipoEstadoInscripcion>();
				while (dataReader.Read())
				{
					TipoEstadoInscripcion tipoEstadoInscripcion = MapDataReader(dataReader);
					tipoEstadoInscripcionList.Add(tipoEstadoInscripcion);
				}
                SqlConnection.ClearAllPools();
				return tipoEstadoInscripcionList;
			}
		}

		/// <summary>
		/// Selects all records from the TipoEstadoInscripcion table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "TipoEstadoInscripcionSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Creates a new instance of the TipoEstadoInscripcion class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private TipoEstadoInscripcion MapDataReader(SqlDataReader dataReader)
		{
			TipoEstadoInscripcion tipoEstadoInscripcion = new TipoEstadoInscripcion();
			tipoEstadoInscripcion.IdEstadoInscripcion = dataReader.GetString("IdEstadoInscripcion", String.Empty);
			tipoEstadoInscripcion.Descripcion = dataReader.GetString("Descripcion", null);

			return tipoEstadoInscripcion;
		}

		#endregion
	}
}
