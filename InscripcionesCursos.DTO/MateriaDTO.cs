using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class MateriaDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<Materia> GetMateriasBySedeAndFilters(Usuario loggedUser, BusquedaFiltro filter)
        {
            try
            {
                MateriaDAO materiasDAO = new MateriaDAO(connectionString);
                return materiasDAO.SelectAllBySedeAndFilters(loggedUser.IdSede, filter.IdDepartamento, filter.IdCarrera, filter.FechaActual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
