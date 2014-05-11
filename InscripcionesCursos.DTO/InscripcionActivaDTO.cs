using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class InscripcionActivaDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<InscripcionActiva> ValidateInscipcionesActivas(DateTime dateNow, int rol)
        {
            try
            {
                InscripcionActivaDAO inscripcionActivaDAO = new InscripcionActivaDAO(connectionString);
                return inscripcionActivaDAO.ValidateInscripcionesActivas(dateNow, rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertInscripcionActiva(InscripcionActiva inscripcionActiva)
        {
            try
            {
                InscripcionActivaDAO inscripcionActivaDAO = new InscripcionActivaDAO(connectionString);
                inscripcionActivaDAO.Insert(inscripcionActiva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
