using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class DepartamentoDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<Departamento> GetAllDepartamentos()
        {
            try
            {
                DepartamentoDAO departamentoDAO = new DepartamentoDAO(connectionString);
                return departamentoDAO.SelectAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
