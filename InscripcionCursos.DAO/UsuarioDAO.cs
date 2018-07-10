using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SharpCore.Data;
using SharpCore.Extensions;
using SharpCore.Utilities;
using InscripcionesCursos.BE;

namespace InscripcionesCursos.DAO
{
	public class UsuarioDAO
	{
		#region Fields

		private string connectionStringName;

		#endregion

		#region Constructors

		public UsuarioDAO(string connectionStringName)
		{
			ValidationUtility.ValidateArgument("connectionStringName", connectionStringName);

			this.connectionStringName = connectionStringName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Saves a record to the Usuario table.
		/// </summary>
		public void Insert(Usuario usuario)
		{
			ValidationUtility.ValidateArgument("usuario", usuario);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
				new SqlParameter("@ApellidoNombre", usuario.ApellidoNombre),
				new SqlParameter("@Email", usuario.Email),
				new SqlParameter("@IdCargo", usuario.IdCargo),
                new SqlParameter("@IdPerfil", usuario.IdPerfil),
                new SqlParameter("@Password", usuario.Password),
				new SqlParameter("@CambioPrimerLogin", usuario.CambioPrimerLogin),
				new SqlParameter("@CuentaActivada", usuario.CuentaActivada),
				new SqlParameter("@CodigoActivacion", usuario.CodigoActivacion),
				new SqlParameter("@IdSede", usuario.IdSede),
                new SqlParameter("@IdEstado", usuario.Estado),
                new SqlParameter("@IdCarrera", Convert.ToInt32(usuario.Carrera)),
                new SqlParameter("@CuatrimestreAnioIngreso", usuario.CuatrimestreAnioIngreso),
                new SqlParameter("@CuatrimestreAnioReincorporacion", usuario.CuatrimestreAnioReincorporacion),
                new SqlParameter("@LimitacionRelevada", usuario.LimitacionRelevada),
				new SqlParameter("@Limitacion", usuario.Limitacion),
				new SqlParameter("@LimitacionVision", usuario.LimitacionVision),
                new SqlParameter("@Lentes", usuario.Lentes),
				new SqlParameter("@LimitacionAudicion", usuario.LimitacionAudicion),
                new SqlParameter("@Audifonos", usuario.Audifonos),
				new SqlParameter("@LimitacionMotriz", usuario.LimitacionMotriz),
				new SqlParameter("@LimitacionAgarre", usuario.LimitacionAgarre),
				new SqlParameter("@LimitacionHabla", usuario.LimitacionHabla),
                new SqlParameter("@Dislexia", usuario.Dislexia),
				new SqlParameter("@LimitacionOtra", usuario.LimitacionOtra),
                new SqlParameter("@Domicilio", usuario.Domicilio),
                new SqlParameter("@Localidad", usuario.Localidad),
                new SqlParameter("@CP", usuario.CP),
                new SqlParameter("@Celular", usuario.Celular)
            };

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioInsert", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Updates a record in the Usuario table.
		/// </summary>
		public void Update(Usuario usuario)
		{
			ValidationUtility.ValidateArgument("usuario", usuario);

			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
				new SqlParameter("@ApellidoNombre", usuario.ApellidoNombre),
				new SqlParameter("@Email", usuario.Email),
				new SqlParameter("@IdCargo", usuario.IdCargo),
                new SqlParameter("@IdPerfil", usuario.IdPerfil),
                new SqlParameter("@Password", usuario.Password),
				new SqlParameter("@CambioPrimerLogin", usuario.CambioPrimerLogin),
				new SqlParameter("@CuentaActivada", usuario.CuentaActivada),
				new SqlParameter("@CodigoActivacion", usuario.CodigoActivacion),
				new SqlParameter("@IdSede", usuario.IdSede),
                new SqlParameter("@IdEstado", usuario.Estado),
                //new SqlParameter("@IdCarrera", Convert.ToInt32(usuario.Carrera)),
                new SqlParameter("@CuatrimestreAnioIngreso", usuario.CuatrimestreAnioIngreso),
                new SqlParameter("@CuatrimestreAnioReincorporacion", usuario.CuatrimestreAnioReincorporacion),
                new SqlParameter("@LimitacionRelevada", usuario.LimitacionRelevada),
				new SqlParameter("@Limitacion", usuario.Limitacion),
				new SqlParameter("@LimitacionVision", usuario.LimitacionVision),
                new SqlParameter("@Lentes", usuario.Lentes),
				new SqlParameter("@LimitacionAudicion", usuario.LimitacionAudicion),
                new SqlParameter("@Audifonos", usuario.Audifonos),
				new SqlParameter("@LimitacionMotriz", usuario.LimitacionMotriz),
				new SqlParameter("@LimitacionAgarre", usuario.LimitacionAgarre),
				new SqlParameter("@LimitacionHabla", usuario.LimitacionHabla),
                new SqlParameter("@Dislexia", usuario.Dislexia),
				new SqlParameter("@LimitacionOtra", usuario.LimitacionOtra),
                new SqlParameter("@Domicilio", usuario.Domicilio),
                new SqlParameter("@Localidad", usuario.Localidad),
                new SqlParameter("@CP", usuario.CP),
                new SqlParameter("@Celular", usuario.Celular)
            };

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioUpdate", parameters);
            SqlConnection.ClearAllPools();
		}

        public void UpdateLimitaciones(Usuario usuario)
        {
            ValidationUtility.ValidateArgument("usuario", usuario);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
                new SqlParameter("@LimitacionRelevada", usuario.LimitacionRelevada),
				new SqlParameter("@Limitacion", usuario.Limitacion),
				new SqlParameter("@LimitacionVision", usuario.LimitacionVision),
                new SqlParameter("@Lentes", usuario.Lentes),
				new SqlParameter("@LimitacionAudicion", usuario.LimitacionAudicion),
                new SqlParameter("@Audifonos", usuario.Audifonos),
				new SqlParameter("@LimitacionMotriz", usuario.LimitacionMotriz),
				new SqlParameter("@LimitacionAgarre", usuario.LimitacionAgarre),
				new SqlParameter("@LimitacionHabla", usuario.LimitacionHabla),
                new SqlParameter("@Dislexia", usuario.Dislexia),
				new SqlParameter("@LimitacionOtra", usuario.LimitacionOtra),
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioUpdateLimitaciones", parameters);
            SqlConnection.ClearAllPools();
        }

		/// <summary>
		/// Deletes a record from the Usuario table by its primary key.
		/// </summary>
		public void Delete(Usuario user)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", user.DNI)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioDelete", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Usuario table by a foreign key.
		/// </summary>
		public void DeleteAllByIdCargo(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioDeleteAllByIdCargo", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Deletes a record from the Usuario table by a foreign key.
		/// </summary>
		public void DeleteAllByIdSede(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioDeleteAllByIdSede", parameters);
            SqlConnection.ClearAllPools();
		}

		/// <summary>
		/// Selects a single record from the Usuario table.
		/// </summary>
		public Usuario Select(Usuario usuario)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioSelect", parameters))
			{
				if (dataReader.Read())
				{
					var mapper = MapDataReader(dataReader);
                    SqlConnection.ClearAllPools();
                    return mapper;
				}
				else
				{
                    SqlConnection.ClearAllPools();
					return null;
				}
			}
		}

		/// <summary>
		/// Selects a single record from the Usuario table.
		/// </summary>
		public string SelectJson(Usuario usuario)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "UsuarioSelect", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Usuario table.
		/// </summary>
		public List<Usuario> SelectAll()
		{
			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAll"))
			{
				List<Usuario> UsuarioList = new List<Usuario>();
				while (dataReader.Read())
				{
					Usuario Usuario = MapDataReader(dataReader);
					UsuarioList.Add(Usuario);
				}
                SqlConnection.ClearAllPools();
				return UsuarioList;
			}
		}

		/// <summary>
		/// Selects all records from the Usuario table.
		/// </summary>
		public string SelectAllJson()
		{
			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAll");
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Usuario table by a foreign key.
		/// </summary>
		public List<Usuario> SelectAllByIdCargo(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAllByIdCargo", parameters))
			{
				List<Usuario> UsuarioList = new List<Usuario>();
				while (dataReader.Read())
				{
					Usuario Usuario = MapDataReader(dataReader);
					UsuarioList.Add(Usuario);
				}
                SqlConnection.ClearAllPools();
				return UsuarioList;
			}
		}

		/// <summary>
		/// Selects all records from the Usuario table by a foreign key.
		/// </summary>
		public List<Usuario> SelectAllByIdSede(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAllByIdSede", parameters))
			{
				List<Usuario> UsuarioList = new List<Usuario>();
				while (dataReader.Read())
				{
					Usuario Usuario = MapDataReader(dataReader);
					UsuarioList.Add(Usuario);
				}
                SqlConnection.ClearAllPools();
				return UsuarioList;
			}
		}

		/// <summary>
		/// Selects all records from the Usuario table by a foreign key.
		/// </summary>
		public string SelectAllByIdCargoJson(int idCargo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdCargo", idCargo)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAllByIdCargo", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}

		/// <summary>
		/// Selects all records from the Usuario table by a foreign key.
		/// </summary>
		public string SelectAllByIdSedeJson(int idSede)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IdSede", idSede)
			};

			var json = SqlClientUtility.ExecuteJson(connectionStringName, CommandType.StoredProcedure, "UsuarioSelectAllByIdSede", parameters);
            SqlConnection.ClearAllPools();
            return json;
		}
		
		 /// <summary>
        /// Select user for Login.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>Usuario</returns>
        public Usuario ValidateLogin(Usuario usuario)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
                new SqlParameter("@Password", usuario.Password)
			};

            var user = new Usuario();

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioValidateLogin", parameters))
            {
                while (dataReader.Read())
                {
                    user = MapDataReader(dataReader);
                }
                SqlConnection.ClearAllPools();
                return user;
            }
        }

        /// <summary>
        /// Update field Password with random alphanumeric characters.
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Usuario UpdateGeneratedPassword(Usuario usuario)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
                new SqlParameter("@Password", usuario.Password)
			};

            Usuario user = new Usuario();

            using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioUpdateGeneratedPassword", parameters))
            {
                while (dataReader.Read())
                {
                    user = MapDataReader(dataReader);
                }
                SqlConnection.ClearAllPools();
                return user;
            }
        }

        /// <summary>
        /// Update password and change first access bit.
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="codigoActivacion"></param>
        /// <returns></returns>
        public bool UpdateMandatoryPasswordEmail(Usuario usuario)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
                new SqlParameter("@Password", usuario.Password),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@CodigoActivacion", usuario.CodigoActivacion)
			};

            Object objScalar = SqlClientUtility.ExecuteScalar(connectionStringName, CommandType.StoredProcedure, "UsuarioUpdateMandatoryPassword", parameters);
            SqlConnection.ClearAllPools();

            if (Convert.ToInt32(objScalar) == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Updates user email.
        /// </summary>
        public void UpdateEmail(Usuario usuario)
        {
            ValidationUtility.ValidateArgument("usuario", usuario);

            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
				new SqlParameter("@Email", usuario.Email)
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioUpdateEmail", parameters);
            SqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Activate user account
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="codigoActivacion"></param>
        /// <returns>Usuario</returns>
        public Usuario ActivateAccount(Usuario usuario)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
			    {
				    new SqlParameter("@DNI", usuario.DNI),
                    new SqlParameter("@CodigoActivacion", usuario.CodigoActivacion)
			    };

                Usuario user = new Usuario();

                using (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connectionStringName, CommandType.StoredProcedure, "UsuarioActivateAccount", parameters))
                {
                    while (dataReader.Read())
                    {
                        user = MapDataReader(dataReader);
                    }
                    SqlConnection.ClearAllPools();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Import users
        /// </summary>
        public void ImportPadron(Usuario usuario)
        {
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", usuario.DNI),
				new SqlParameter("@ApellidoNombre", usuario.ApellidoNombre),
				new SqlParameter("@IdSede", usuario.IdSede),
				new SqlParameter("@IdEstado", usuario.Estado),
				new SqlParameter("@IdCarrera", Convert.ToInt32(usuario.Carrera)),
				new SqlParameter("@CuatrimestreAnioIngreso", usuario.CuatrimestreAnioIngreso),
				new SqlParameter("@CuatrimestreAnioReincorporacion", usuario.CuatrimestreAnioReincorporacion),
                new SqlParameter("@IdCargo", usuario.IdCargo),
                new SqlParameter("@LimitacionRelevada", usuario.LimitacionRelevada),
				new SqlParameter("@Limitacion", usuario.Limitacion),
				new SqlParameter("@LimitacionVision", usuario.LimitacionVision),
                new SqlParameter("@Lentes", usuario.Lentes),
				new SqlParameter("@LimitacionAudicion", usuario.LimitacionAudicion),
                new SqlParameter("@Audifonos", usuario.Audifonos),
				new SqlParameter("@LimitacionMotriz", usuario.LimitacionMotriz),
				new SqlParameter("@LimitacionAgarre", usuario.LimitacionAgarre),
				new SqlParameter("@LimitacionHabla", usuario.LimitacionHabla),
                new SqlParameter("@Dislexia", usuario.Dislexia),
				new SqlParameter("@LimitacionOtra", usuario.LimitacionOtra)
			};

			SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioImportPadron", parameters);
            SqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Deactivate account
        /// </summary>
        /// <param name="dni"></param>
        public void DeactivateAccount(int dni)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNI", dni)
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioDeactivateAccount", parameters);
            SqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Transfer info from old dni to new dni, and delete old dni
        /// </summary>
        /// <param name="dniOld">DNI from</param>
        /// <param name="dniNew">DNI to</param>
        public void TransferData(int dniOld, int dniNew)
        {
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DNIOld", dniOld),
                new SqlParameter("@DNINew", dniNew)
			};

            SqlClientUtility.ExecuteNonQuery(connectionStringName, CommandType.StoredProcedure, "UsuarioTransferData", parameters);
            SqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Export students
        /// </summary>
        /// <returns>List of students</returns>
        public DataTable ExportPadron()
        {
            using (DataTable dt = SqlClientUtility.ExecuteDataTable(connectionStringName, CommandType.StoredProcedure, "UsuarioExportPadron"))
            {
                SqlConnection.ClearAllPools();
                return dt;
            }
        }

		/// <summary>
		/// Creates a new instance of the Usuario class and populates it with data from the specified SqlDataReader.
		/// </summary>
		private Usuario MapDataReader(SqlDataReader dataReader)
		{
		    var Usuario = new Usuario
		    {
		        DNI = dataReader.GetInt32("DNI", 0),
		        ApellidoNombre = dataReader.GetString("ApellidoNombre", null),
		        Email = dataReader.GetString("Email", null),
		        IdCargo = dataReader.GetInt32("IdCargo", 0),
                IdPerfil = dataReader.GetInt32("IdPerfil", 0),
		        Password = dataReader.GetString("Password", null),
		        CambioPrimerLogin = dataReader.GetBoolean("CambioPrimerLogin", false),
		        CuentaActivada = dataReader.GetBoolean("CuentaActivada", false),
		        CodigoActivacion = dataReader.GetInt32("CodigoActivacion", 0),
		        IdSede = dataReader.GetInt32("IdSede", 0),
		        Estado = dataReader.GetString("Descripcion", null),
		        Carrera = dataReader.GetString("Nombre", null),
		        CuatrimestreAnioIngreso = dataReader.GetString("CuatrimestreAnioIngreso", null),
		        CuatrimestreAnioReincorporacion = dataReader.GetString("CuatrimestreAnioReincorporacion", null),
		        LimitacionRelevada = dataReader.GetBoolean("LimitacionRelevada", false),
                Limitacion = dataReader.GetString("Limitacion", null),
                LimitacionVision = dataReader.GetString("LimitacionVision", null),
                Lentes = dataReader.GetString("Lentes", null),
                LimitacionAudicion = dataReader.GetString("LimitacionAudicion", null),
                Audifonos = dataReader.GetString("Audifonos", null),
                LimitacionMotriz = dataReader.GetString("LimitacionMotriz", null),
                LimitacionHabla = dataReader.GetString("LimitacionHabla", null),
                Dislexia = dataReader.GetString("Dislexia", null),
                LimitacionAgarre = dataReader.GetString("LimitacionAgarre", null),
		        LimitacionOtra = dataReader.GetString("LimitacionOtra", null),
                Domicilio = dataReader.GetString("Domicilio", null),
                Localidad = dataReader.GetString("Localidad", null),
                CP = dataReader.GetString("CP", null),
                Celular = dataReader.GetString("Celular", null),
                IdCarrera = dataReader.GetInt32("IdCarrera", 0)
            };

		    return Usuario;
		}

		#endregion
    }
}
