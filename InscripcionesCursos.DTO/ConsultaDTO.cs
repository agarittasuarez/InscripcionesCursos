using System;
using System.Collections.Generic;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;
using System.Data;

namespace InscripcionesCursos.DTO
{
	public static class ConsultaDTO
	{
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<List<Double>> GetAVGActivos()
        {
            try
            {
                ConsultaDAO consultaDAO = new ConsultaDAO(connectionString);
                return consultaDAO.GetAVGActivos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<List<Double>> GetAVGInscripcionesByTurno(DateTime tunoInscripcion, string tipoInscripcion)
        {
            try
            {
                ConsultaDAO consultaDAO = new ConsultaDAO(connectionString);
                return consultaDAO.GetAVGInscripcionesByTurno(tunoInscripcion, tipoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetAVGInscripcionesByCarrera(DateTime tunoInscripcion, string tipoInscripcion)
        {
            try
            {
                ConsultaDAO consultaDAO = new ConsultaDAO(connectionString);
                return consultaDAO.GetAVGInscripcionesByCarrera(tunoInscripcion, tipoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetInscripcionByComision(DateTime tunoInscripcion, string tipoInscripcion)
        {
            try
            {
                ConsultaDAO consultaDAO = new ConsultaDAO(connectionString);
                return consultaDAO.GetInscripcionByComision(tunoInscripcion, tipoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetInscripcionAgrupada(DateTime tunoInscripcion, string tipoInscripcion, int vuelta, int agrupacion)
        {
            try
            {
                ConsultaDAO consultaDAO = new ConsultaDAO(connectionString);
                return consultaDAO.GetInscripcionAgrupada(tunoInscripcion, tipoInscripcion, vuelta, agrupacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
