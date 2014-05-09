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
	public class AnaliticoDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public AnaliticoDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

        #region Old
        ///// <summary>
        ///// Saves a record to the HistoricoAnalitico table.
        ///// </summary>
        //public void Insert(Analitico analitico)
        //{
        //    ValidationUtility.ValidateArgument("historicoAnalitico", analitico);

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@Carrera", analitico.Carrera),
        //        new SqlParameter("@CatedraComision", analitico.CatedraComision),
        //        new SqlParameter("@DNI", analitico.DNI),
        //        new SqlParameter("@Fecha", analitico.Fecha),
        //        new SqlParameter("@Folio", analitico.Folio),
        //        new SqlParameter("@IdMateria", analitico.IdMateria),
        //        new SqlParameter("@Libro", analitico.Libro),
        //        new SqlParameter("@Materia", analitico.Materia),
        //        new SqlParameter("@Nota", analitico.Nota),
        //        new SqlParameter("@Resolucion", analitico.Resolucion),
        //        new SqlParameter("@SubFolio", analitico.SubFolio),
        //        new SqlParameter("@TipoInscripcion", analitico.TipoInscripcion),
        //        new SqlParameter("@Tomo", analitico.Tomo),
        //        new SqlParameter("@TurnoInscripcion", analitico.TurnoInscripcion),
        //        new SqlParameter("@UltimoIngreso", analitico.UltimoIngreso)
        //    };

        //    SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "AnaliticoInsert", parameters);
        //}

        ///// <summary>
        ///// Selects all records from the HistoricoAnalitico table.
        ///// </summary>
        //public List<Analitico> SelectAll()
        //{
        //    using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "AnaliticoSelectAll"))
        //    {
        //        List<Analitico> analiticoList = new List<Analitico>();
        //        while (dataReader.Read())
        //        {
        //            Analitico analitico = MapDataReader(dataReader);
        //            analiticoList.Add(analitico);
        //        }

        //        return analiticoList;
        //    }
        //}

        ///// <summary>
        ///// Selects all records from the HistoricoAnalitico table.
        ///// </summary>
        //public string SelectAllJson()
        //{
        //    return SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "AnaliticoSelectAll");
        //}

        #endregion

        /// <summary>
        /// Selects all records from the Analitico table.
        /// </summary>
        public List<Analitico> SelectByDNI(int dni)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dni)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "AnaliticoSelectAll", parameters))
            {
                List<Analitico> analiticoList = new List<Analitico>();
                while (dataReader.Read())
                {
                    Analitico analitico = MapDataReader(dataReader);
                    analiticoList.Add(analitico);
                }
                SqlConnection.ClearAllPools();
                return analiticoList;
            }
        }

        /// <summary>
        /// Selects all records from the Analitico table by dni and carrera.
        /// </summary>
        public List<Materia> SelectByDNIAndCarrera(int dni, int idCarrera)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dni),
                new SqlParameter("@idCarrera", idCarrera)
			};

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "AnaliticoSelectByUser", parameters))
            {
                List<Materia> materiasAprobadasList = new List<Materia>();
                while (dataReader.Read())
                {
                    Materia materia = MapDataReaderMateria(dataReader);
                    materiasAprobadasList.Add(materia);
                }
                SqlConnection.ClearAllPools();
                return materiasAprobadasList;
            }
        }

        /// <summary>
        /// Import Notas Analitico
        /// </summary>
        public void ImportNotas(string CatedraComision, string CodigoMovimiento, int DNI, DateTime Fecha, string Folio, string IdTipoInscripcion, int IdMateria, string Libro, double Nota, int Plan, string Resolucion, string SubFolio, string Tomo, DateTime TurnoInscripcion)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
                new SqlParameter("@IdTipoInscripcion",IdTipoInscripcion),
				new SqlParameter("@TurnoInscripcion", TurnoInscripcion),
				new SqlParameter("@IdMateria", IdMateria),
				new SqlParameter("@CatedraComision", CatedraComision),
				new SqlParameter("@DNI", DNI),
				new SqlParameter("@Plan", Plan),
				new SqlParameter("@Fecha", Fecha),
				new SqlParameter("@Nota", Nota),
                new SqlParameter("@Libro", Libro),
                new SqlParameter("@Tomo", Tomo),
                new SqlParameter("@Folio", Folio),
                new SqlParameter("@SubFolio", SubFolio),
                new SqlParameter("@Resolucion", Resolucion),
                new SqlParameter("@CodigoMovimiento", CodigoMovimiento)
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "AnaliticoImportNotas", parameters);
            SqlConnection.ClearAllPools();
        }

		/// <summary>
		/// Creates a new instance of the HistoricoAnalitico class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Analitico MapDataReader(SqlDataReader dataReader)
		{
			Analitico analitico = new Analitico();
            analitico.CatedraComision = dataReader.GetString("CatedraComision", null);
			analitico.DNI = dataReader.GetInt32("DNI", 0);
            analitico.Fecha = dataReader.GetDateTime("Fecha", new DateTime(0));
			analitico.Folio = dataReader.GetString("Folio", null);
			analitico.Libro = dataReader.GetString("Libro", null);
            analitico.IdMateria = dataReader.GetInt32("IdMateria", 0);
			analitico.Materia = dataReader.GetString("Materia", null);
            if (!dataReader.IsDBNull(8))
                analitico.Nota = dataReader.GetDouble(8);
            analitico.Plan = dataReader.GetInt32("Plan", 0);
			analitico.Resolucion = dataReader.GetString("Resolucion", null);
            analitico.SubFolio = dataReader.GetString("SubFolio", null);
			analitico.TipoInscripcion = dataReader.GetString("TipoInscripcion", null);
            analitico.Tomo = dataReader.GetString("Tomo", null);
            analitico.TurnoInscripcion = dataReader.GetDateTime("TurnoInscripcion", new DateTime(0));
            analitico.UltimoIngreso = dataReader.GetDateTime("UltimoIngreso", new DateTime(0));

			return analitico;
		}

        /// <summary>
        /// Creates a new instance of the Materia class and populates it with data from the specified SqlDataReader.
        /// </summary>
        private Materia MapDataReaderMateria(SqlDataReader dataReader)
        {
            Materia materia = new Materia();
            materia.IdMateria = dataReader.GetInt32("IdMateria", 0);

            return materia;
        }

		#endregion
    }
}
