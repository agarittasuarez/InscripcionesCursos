using System;
using System.Collections.Generic;
using InscripcionesCursos.DAO;
using InscripcionesCursos.BE;
using System.Data;

namespace InscripcionesCursos.DTO
{
    public static class UsuarioDTO
    {
        #region Constants

        const string connectionString = "InscripcionesCursos";

        #endregion

        #region Methods

        /// <summary>
        /// Method to validate login credentials.
        /// </summary>
        public static Usuario ValidateLogin(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.ValidateLogin(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the first password generated
        /// </summary>
        public static Usuario UpdatePassword(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.UpdateGeneratedPassword(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the user data
        /// </summary>
        /// <param name="user"></param>
        public static void Update(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.Update(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the user limitations
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateLimitaciones(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.UpdateLimitaciones(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the user data
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateEmail(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.UpdateEmail(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update mandatory
        /// </summary>
        public static bool UpdateMandatoryPasswordEmail(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.UpdateMandatoryPasswordEmail(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to activate account
        /// </summary>
        public static Usuario ActivateAccount(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.ActivateAccount(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get user data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Usuario GetUsuario(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.Select(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to get all users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Usuario> GetAllUsuario(int idCargo)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.SelectAllByIdCargo(idCargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to import users
        /// </summary>
        public static void ImportPadron(Usuario usuario)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.ImportPadron(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to deactivate a student account
        /// </summary>
        /// <param name="dni"></param>
        public static void DeactivateAccount(int dni)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.DeactivateAccount(dni);
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
        }

        /// <summary>
        /// Method to transfer data from old dni to new dni
        /// </summary>
        /// <param name="dniOld">DNI from</param>
        /// <param name="dniNew">DNI to</param>
        public static void TransferData(int dniOld, int dniNew)
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.TransferData(dniOld, dniNew);
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
        }

        /// <summary>
        /// Method to get all students who have activated account
        /// </summary>
        /// <returns></returns>
        public static DataTable ExportPadron()
        {
            try
            {
                var usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.ExportPadron();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
