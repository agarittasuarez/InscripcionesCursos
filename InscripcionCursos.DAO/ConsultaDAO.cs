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
	public class ConsultaDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

        public ConsultaDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Selects average of Claves Generadas, Cuentas Activas and others
		/// </summary>
		public List<List<Double>> GetAVGActivos()
		{
            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ConsultasAlumnosActivos"))
			{
                List<List<double>> returnList = new List<List<double>>();
                List<double> AVGList = new List<double>();
				while (dataReader.Read())
				{
                    AVGList.Add(dataReader.GetDouble(0));
                    AVGList.Add(dataReader.GetDouble(1));
                    AVGList.Add(dataReader.GetDouble(2));
                    returnList.Add(AVGList);
                    AVGList = new List<double>();
                    AVGList.Add(dataReader.GetInt32(3));
                    AVGList.Add(dataReader.GetInt32(4));
                    AVGList.Add(dataReader.GetInt32(5));
                    AVGList.Add(dataReader.GetInt32(6));
                    returnList.Add(AVGList);
				}
                SqlConnection.ClearAllPools();
                return returnList;
			}
		}

        /// <summary>
        /// Selects average of Web and Facu Inscriptions by TurnoInscripcion
        /// </summary>
        public List<List<Double>> GetAVGInscripcionesByTurno(DateTime turnoInscripcion, string tipoInscripcion)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdTipoInscripcion", tipoInscripcion)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ConsultasInscripcionesEstadisticas", parameters))
            {
                List<List<double>> returnList = new List<List<double>>();
                List<double> AVGList = new List<double>();
                while (dataReader.Read())
                {
                    AVGList.Add(dataReader.GetDouble(0));
                    AVGList.Add(dataReader.GetDouble(1));
                    returnList.Add(AVGList);
                    AVGList = new List<double>();
                    AVGList.Add(dataReader.GetInt32(2));
                    AVGList.Add(dataReader.GetInt32(3));
                    AVGList.Add(dataReader.GetInt32(4));
                    AVGList.Add(dataReader.GetInt32(5));
                    returnList.Add(AVGList);
                }
                SqlConnection.ClearAllPools();
                return returnList;
            }
        }

        /// <summary>
        /// Selects average of Contabilidad and Administracion Career
        /// </summary>
        public DataTable GetAVGInscripcionesByCarrera(DateTime turnoInscripcion, string tipoInscripcion)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdTipoInscripcion", tipoInscripcion)
			};

            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ConsultasTotalesPorCarrera", parameters));
            SqlConnection.ClearAllPools();
            return dt;
        }

        /// <summary>
        /// Selects total Inscriptions by Comision
        /// </summary>
        public DataTable GetInscripcionByComision(DateTime turnoInscripcion, string tipoInscripcion)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdTipoInscripcion", tipoInscripcion)
			};

            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ConsultasTotalesInscripcionComision", parameters));
            SqlConnection.ClearAllPools();
            return dt;
        }

        /// <summary>
        /// Selects total Inscriptions grouped by time
        /// </summary>
        public DataTable GetInscripcionAgrupada(DateTime turnoInscripcion, string tipoInscripcion, int vuelta, int agrupacion)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TurnoInscripcion", turnoInscripcion),
                new SqlParameter("@IdTipoInscripcion", tipoInscripcion),
                new SqlParameter("@IdVuelta", vuelta),
                new SqlParameter("@FranjaMinutos", agrupacion)
            };

            DataTable dt = new DataTable();
            dt.Load(SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "ConsultasInscripcionesPorFranja", parameters));
            SqlConnection.ClearAllPools();
            return dt;
        }

        #endregion
    }
}
