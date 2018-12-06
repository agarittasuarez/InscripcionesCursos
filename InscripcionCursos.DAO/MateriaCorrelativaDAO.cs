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
    public class MateriaCorrelativaDAO
    {
        #region Fields

        private string connectionStringName;

        #endregion

        #region Constructors

        public MateriaCorrelativaDAO(string connectionStringName)
        {
            ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

            this.connectionStringName = connectionStringName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Selects all records from the MateriaCorrelativa table by idCarrera and DNI.
        /// </summary>
        public List<MateriaCorrelativa> MateriasCorrelativasSelectByUser(int DNI, int idCarrera)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", DNI),
                new SqlParameter("@idCarrera", idCarrera)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriasCorrelativasSelectByUser", parameters))
            {
                List<MateriaCorrelativa> materiaList = new List<MateriaCorrelativa>();
                while (dataReader.Read())
                {
                    MateriaCorrelativa materia = MapDataReader(dataReader);
                    materiaList.Add(materia);
                }
                SqlConnection.ClearAllPools();
                return materiaList;
            }
        }

        /// <summary>
        /// Selects all records from the MateriaCorrelativa new plan table by idCarrera and DNI.
        /// </summary>
        public List<MateriaCorrelativa> MateriasCorrelativasNuevoPlanSelectByUser(int DNI, int idCarrera)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DNI", DNI),
                new SqlParameter("@idCarrera", idCarrera)
            };

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "MateriasCorrelativasNuevoPlanSelectByUser", parameters))
            {
                List<MateriaCorrelativa> materiaList = new List<MateriaCorrelativa>();
                while (dataReader.Read())
                {
                    MateriaCorrelativa materia = MapDataReader(dataReader);
                    materiaList.Add(materia);
                }
                SqlConnection.ClearAllPools();
                return materiaList;
            }
        }

        /// <summary>
        /// Creates a new instance of the MateriaCorrelativa class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private MateriaCorrelativa MapDataReader(SqlDataReader dataReader)
        {
            MateriaCorrelativa materia = new MateriaCorrelativa();
            materia.IdMateria = dataReader.GetInt32("IdMateria", 0);
            materia.Estado = dataReader.GetString("Estado", "");

            return materia;
        }

        #endregion
    }
}
