using System;
using InscripcionesCursos.DAO;
using InscripcionesCursos.BE;

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
        public static Usuario ValidateLogin(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.ValidateLogin(user.DNI, user.Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update the first password generated
        /// </summary>
        public static Usuario UpdatePassword(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.UpdateGeneratedPassword(user.DNI, user.Password);
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
        public static void Update(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.Update(user);
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
        public static void UpdateLimitaciones(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.UpdateLimitaciones(user);
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
        public static void UpdateEmail(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.UpdateEmail(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update mandatory
        /// </summary>
        public static bool UpdateMandatoryPasswordEmail(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.UpdateMandatoryPasswordEmail(user.DNI, user.Password, user.Email, user.CodigoActivacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to activate account
        /// </summary>
        public static Usuario ActivateAccount(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.ActivateAccount(user.DNI, user.CodigoActivacion);
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
        public static Usuario GetUsuario(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                return usuarioDAO.Select(user.DNI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to import users
        /// </summary>
        public static void ImportPadron(Usuario user)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connectionString);
                usuarioDAO.ImportPadron(user.DNI, user.ApellidoNombre, user.IdSede, user.Estado, Convert.ToInt32(user.Carrera), user.CuatrimestreAnioIngreso, user.CuatrimestreAnioReincorporacion, user.IdCargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
