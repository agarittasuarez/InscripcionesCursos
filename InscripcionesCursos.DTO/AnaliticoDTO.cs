using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class AnaliticoDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<Analitico> GetAnalitic(Usuario user)
        {
            try
            {
                AnaliticoDAO analiticoDAO = new AnaliticoDAO(connectionString);
                return analiticoDAO.SelectByDNI(user.DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to import users
        /// </summary>
        public static void ImportNotas(Analitico analitico)
        {
            try
            {
                AnaliticoDAO analiticoDAO = new AnaliticoDAO(connectionString);
                analiticoDAO.ImportNotas(analitico.CatedraComision, analitico.CodigoMovimiento, analitico.DNI, analitico.Fecha, analitico.Folio, analitico.IdTipoInscripcion, analitico.IdMateria, analitico.Libro, analitico.Nota, analitico.Plan, analitico.Resolucion, analitico.SubFolio, analitico.Tomo, analitico.TurnoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene las materias aprobadas del alumno segun DNI y carrera
        /// </summary>
        public static List<Materia> GetMateriasAprobadas(int DNI, int idCarrera)
        {
            try
            {
                AnaliticoDAO analiticoDAO = new AnaliticoDAO(connectionString);
                return analiticoDAO.SelectByDNIAndCarrera(DNI, idCarrera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
