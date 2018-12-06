using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class MateriaCorrelativaDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<MateriaCorrelativa> GetMateriasCorrelativasByUser(int DNI, int idCarrera)
        {
            try
            {
                MateriaCorrelativaDAO materiasDAO = new MateriaCorrelativaDAO(connectionString);
                return materiasDAO.MateriasCorrelativasSelectByUser(DNI, idCarrera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MateriaCorrelativa> GetMateriasCorrelativasNuevoPlanByUser(int DNI, int idCarrera)
        {
            try
            {
                MateriaCorrelativaDAO materiasDAO = new MateriaCorrelativaDAO(connectionString);
                return materiasDAO.MateriasCorrelativasNuevoPlanSelectByUser(DNI, idCarrera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
