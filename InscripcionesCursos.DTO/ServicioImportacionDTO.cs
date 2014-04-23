using System;
using InscripcionesCursos.DAO;
using InscripcionesCursos.BE;
using System.Collections.Generic;

namespace InscripcionesCursos.DTO
{
    public static class ServicioImportacionDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        /// <summary>
        /// Method to get all import types.
        /// </summary>
        public static List<ServicioImportacion> GetServicioTipoImportacion()
        {
            try
            {
                ServicioImportacionDAO tipoImportacionDAO = new ServicioImportacionDAO(connectionString);
                return tipoImportacionDAO.GetServicioTipoImportacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all active process.
        /// </summary>
        public static List<ServicioImportacion> GetActiveProcess()
        {
            try
            {
                ServicioImportacionDAO activeProcessDAO = new ServicioImportacionDAO(connectionString);
                return activeProcessDAO.GetActiveProcess();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all process with errors.
        /// </summary>
        public static List<ServicioImportacion> GetErrorProcess(ServicioImportacion importacion)
        {
            try
            {
                ServicioImportacionDAO errorProcessDAO = new ServicioImportacionDAO(connectionString);
                return errorProcessDAO.GetErrorProcess(importacion.IdTipoImportacion, importacion.IdTipoInscripcion, importacion.TurnoInscripcion, importacion.IdVuelta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all inscription types.
        /// </summary>
        public static List<TipoInscripcion> GetServicioTipoInscripcion()
        {
            try
            {
                ServicioImportacionDAO tipoInscripcionDAO = new ServicioImportacionDAO(connectionString);
                return tipoInscripcionDAO.GetServicioTipoInscripcion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all inscription types.
        /// </summary>
        public static List<String> GetServicioTurnoInscripcion()
        {
            try
            {
                ServicioImportacionDAO turnoInscripcionDAO = new ServicioImportacionDAO(connectionString);
                return turnoInscripcionDAO.GetServicioTurnoInscripcion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all VueltaTurnos types.
        /// </summary>
        public static List<TipoVuelta> GetServicioVueltaInscripcion()
        {
            try
            {
                ServicioImportacionDAO tipoVueltaDAO = new ServicioImportacionDAO(connectionString);
                return tipoVueltaDAO.GetServicioVueltaInscripcion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all import types.
        /// </summary>
        public static List<ServicioImportacion> GetServicioTipoImportacionError()
        {
            try
            {
                ServicioImportacionDAO tipoImportacionDAO = new ServicioImportacionDAO(connectionString);
                return tipoImportacionDAO.GetServicioTipoImportacionError();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all inscription types.
        /// </summary>
        public static List<TipoInscripcion> GetServicioTipoInscripcionError(ServicioImportacion importacion)
        {
            try
            {
                ServicioImportacionDAO tipoInscripcionDAO = new ServicioImportacionDAO(connectionString);
                return tipoInscripcionDAO.GetServicioTipoInscripcionError(importacion.IdTipoImportacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all inscription types.
        /// </summary>
        public static List<String> GetServicioTurnoInscripcionError(ServicioImportacion importacion)
        {
            try
            {
                ServicioImportacionDAO turnoInscripcionDAO = new ServicioImportacionDAO(connectionString);
                return turnoInscripcionDAO.GetServicioTurnoInscripcionError(importacion.IdTipoImportacion, importacion.IdTipoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all VueltaTurnos types.
        /// </summary>
        public static List<TipoVuelta> GetServicioVueltaInscripcionError(ServicioImportacion importacion)
        {
            try
            {
                ServicioImportacionDAO tipoVueltaDAO = new ServicioImportacionDAO(connectionString);
                return tipoVueltaDAO.GetServicioVueltaInscripcionError(importacion.IdTipoImportacion, importacion.IdTipoInscripcion, importacion.TurnoInscripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to insert new process register.
        /// </summary>
        public static void InsertNuevoServicio(ServicioImportacion service)
        {
            try
            {
                ServicioImportacionDAO importService = new ServicioImportacionDAO(connectionString);
                importService.InsertServicio(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update statuss process.
        /// </summary>
        public static void DeactivateImportProcess(ServicioImportacion service)
        {
            try
            {
                ServicioImportacionDAO process = new ServicioImportacionDAO(connectionString);
                process.DeactivateImportProcess(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to insert new process register.
        /// </summary>
        public static bool ValidatePredecessor(ServicioImportacion service)
        {
            try
            {
                ServicioImportacionDAO importService = new ServicioImportacionDAO(connectionString);
                return importService.ValidatePredecessor(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to validate if there inscriptions on course
        /// </summary>
        public static bool ValidateInscriptionOnCourse(ServicioImportacion service)
        {
            try
            {
                ServicioImportacionDAO importService = new ServicioImportacionDAO(connectionString);
                return importService.ValidateInscriptionOnCourse(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
