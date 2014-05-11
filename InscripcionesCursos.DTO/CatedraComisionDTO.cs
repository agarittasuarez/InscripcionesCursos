using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InscripcionesCursos.BE;
using InscripcionesCursos.DAO;

namespace InscripcionesCursos.DTO
{
    public static class CatedraComisionDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        public static List<CatedraComision> GetCatedraComisionFiltered(Usuario loggedUser, BusquedaFiltro filter, int rol)
        {
            try
            {
                CatedraComisionDAO catedraComisionDAO = new CatedraComisionDAO(connectionString);
                return catedraComisionDAO.SelectAllByIdSedeAndFilters(loggedUser.IdSede, filter.IdDepartamento, filter.IdCarrera, filter.IdMateria, filter.FechaActual, rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CatedraComision> GetAllCatedraComision()
        {
            try
            {
                CatedraComisionDAO catedraComisionDAO = new CatedraComisionDAO(connectionString);
                return catedraComisionDAO.SelectAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetAllTurnos()
        {
            try
            {
                CatedraComisionDAO catedraComisionDAO = new CatedraComisionDAO(connectionString);
                return catedraComisionDAO.SelectAllTurnos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CatedraComision> GetCatedraComisionByTurnoInscripcion(string turnoInscripcion)
        {
            try
            {
                CatedraComisionDAO catedraComisionDAO = new CatedraComisionDAO(connectionString);
                return catedraComisionDAO.SelectAllByTurnoInscripcion(turnoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertCatedraComision(CatedraComision comision)
        {
            try
            {
                CatedraComisionDAO catedraComisionDAO = new CatedraComisionDAO(connectionString);
                catedraComisionDAO.Insert(comision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
