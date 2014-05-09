using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class InscripcionDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static int InsertInscripcion(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.Insert(inscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteInscriptions(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                inscripcionDAO.DeleteAllByIdTipoInscripcionIdVueltaTurnoInscripcionDNI(inscripcion.IdTipoInscripcion, inscripcion.TurnoInscripcion, inscripcion.DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static void DeleteInscriptionsInTurn(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                inscripcionDAO.DeleteAllByTurnoInscripcionIdVuelta(inscripcion.TurnoInscripcion, inscripcion.IdVuelta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Carro> GetInscriptionsInTurn(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.GetInscriptionsInTurn(inscripcion.IdTipoInscripcion, inscripcion.TurnoInscripcion, inscripcion.IdVuelta, inscripcion.DNI, inscripcion.IdEstadoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetStudentInscriptions(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.SelectHistoricoByDNI(inscripcion.DNI, inscripcion.TurnoInscripcion, inscripcion.IdTipoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetAllTurnos(Inscripcion inscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.SelectAllTurnos(inscripcion.DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Inscripcion> GetInscripcionesByTurnoInscripcionIdVuelta(string turnoInscripcion, int idVuelta)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.SelectAllByTurnoInscripcionIdVuelta(turnoInscripcion, idVuelta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<int> GetVueltasByTurnoInscripcion(DateTime turnoInscripcion)
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                return inscripcionDAO.SelectIdVueltaByTurnoInscripcion(turnoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckEmployeeTest()
        {
            try
            {
                InscripcionDAO inscripcion = new InscripcionDAO(connectionString);
                return inscripcion.CheckEmployeeTest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteEmployeeTestInscription()
        {
            try
            {
                InscripcionDAO inscripcionDAO = new InscripcionDAO(connectionString);
                inscripcionDAO.DeleteEmployeeTestIncription();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
