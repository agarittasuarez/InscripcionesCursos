using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class CarreraDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<Carrera> GetAllCarreras()
        {
            try
            {
                CarreraDAO carreraDAO = new CarreraDAO(connectionString);
                return carreraDAO.SelectAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
