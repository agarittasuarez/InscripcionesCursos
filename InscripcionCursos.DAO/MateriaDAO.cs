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
	public class MateriaDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public MateriaDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Materia table.
		/// </summary>
		public void Insert(Materia materia)
		{
			ValidationUtility.ValidateArgument("materia", materia);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", materia.IdMateria),
				new SqlParameter("@IdDepartamento", materia.IdDepartamento),
				new SqlParameter("@Descripcion", materia.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Materia table.
		/// </summary>
		public void Update(Materia materia)
		{
			ValidationUtility.ValidateArgument("materia", materia);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", materia.IdMateria),
				new SqlParameter("@IdDepartamento", materia.IdDepartamento),
				new SqlParameter("@Descripcion", materia.Descripcion)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Materia table by its primary key.
		/// </summary>
		public void Delete(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Materia table by a foreign key.
		/// </summary>
		public void DeleteAllByIdDepartamento(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "MateriaDeleteAllByIdDepartamento", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Materia table.
		/// </summary>
		public Materia Select(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaSelect", parameters))
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
		/// Selects a single record from the Materia table.
		/// </summary>
		public string SelectJson(int idMateria)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdMateria", idMateria)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "MateriaSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Materia table.
		/// </summary>
		public List<Materia> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaSelectAll"))
			{
				List<Materia> materiaList = new List<Materia>();
				while (dataReader.Read())
				{
					Materia materia = MapDataReader(dataReader);
					materiaList.Add(materia);
				}
                SqlConnection.ClearAllPools();
				return materiaList;
			}
		}

		/// <summary>
		/// Selects all records from the Materia table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "MateriaSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Materia table by a foreign key.
		/// </summary>
		public List<Materia> SelectAllByIdDepartamento(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaSelectAllByIdDepartamento", parameters))
			{
				List<Materia> materiaList = new List<Materia>();
				while (dataReader.Read())
				{
					Materia materia = MapDataReader(dataReader);
					materiaList.Add(materia);
				}
                SqlConnection.ClearAllPools();
				return materiaList;
			}
		}

		/// <summary>
		/// Selects all records from the Materia table by a foreign key.
		/// </summary>
		public string SelectAllByIdDepartamentoJson(int idDepartamento)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdDepartamento", idDepartamento)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "MateriaSelectAllByIdDepartamento", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

        /// <summary>
        /// Selects all records from the Materia table by a foreign key.
        /// </summary>
        public List<Materia> SelectAllBySedeAndFilters(int sede, int departamento, int carrera, DateTime fechaActual, int rol)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", sede),
                new SqlParameter("@IdDepartamento", departamento),
                new SqlParameter("@IdCarrera", carrera),
                new SqlParameter("@FechaActual", fechaActual),
                new SqlParameter("@IdCargo", rol)

			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriaSelectAllBySedeAndFilters", parameters))
            {
                List<Materia> materiaList = new List<Materia>();
                while (dataReader.Read())
                {
                    Materia materia = MapDataReader(dataReader);
                    materiaList.Add(materia);
                }
                SqlConnection.ClearAllPools();
                return materiaList;
            }
        }

		/// <summary>
		/// Creates a new instance of the Materia class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Materia MapDataReader(SqlDataReader dataReader)
		{
			Materia materia = new Materia();
			materia.IdMateria = dataReader.GetInt32("IdMateria", 0);
			materia.IdDepartamento = dataReader.GetInt32("IdDepartamento", 0);
			materia.Descripcion = dataReader.GetString("Descripcion", null);

			return materia;
		}

		#endregion
	}
}
