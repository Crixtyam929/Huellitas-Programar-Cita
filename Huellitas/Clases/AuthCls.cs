using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Huellitas.Models;

namespace Huellitas.Clases
{
    /// <summary>
    /// Clase que gestiona la autenticación y registro de usuarios
    /// </summary>
    public class AuthCls : IDisposable
    {
        private readonly DBHuellitasEntities _db;
        private bool _disposed = false;

        /// <summary>
        /// Constructor de la clase AuthCls
        /// </summary>
        public AuthCls()
        {
            _db = new DBHuellitasEntities();
        }

        /// <summary>
        /// Valida las credenciales de un usuario en el sistema
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Objeto Usuario si las credenciales son válidas, null en caso contrario</returns>
        /// <exception cref="ArgumentNullException">Si el email o password son nulos o vacíos</exception>
        public Usuario ValidarUsuario(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "El correo electrónico es requerido");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "La contraseña es requerida");

            try
            {
                // Normalizar el email para la comparación (convertir a minúsculas)
                email = email.Trim().ToLower();

                // Buscamos al usuario en la base de datos por el correo (email)
                Usuario usuario = _db.Usuarios.FirstOrDefault(x => x.Email.ToLower() == email);

                // Verificamos si el usuario existe y si la contraseña es correcta
                if (usuario != null && usuario.PasswordHash.SequenceEqual(GenerarSHA512ConSalt(password, usuario.PasswordSalt)))
                {
                    return usuario;  // Retornamos el usuario si las credenciales son válidas
                }

                return null;  // Credenciales inválidas
            }
            catch (Exception ex)
            {
                // Aquí sería recomendable usar un sistema de logging más robusto
                Console.WriteLine($"Error al validar usuario: {ex.Message}");
                throw new ApplicationException("Error al validar el usuario", ex);
            }
        }

        /// <summary>
        /// Valida las credenciales de un empleado en el sistema
        /// </summary>
        /// <param name="email">Correo electrónico del empleado</param>
        /// <param name="password">Contraseña del empleado</param>
        /// <returns>Objeto empleado si las credenciales son válidas, null en caso contrario</returns>
        /// <exception cref="ArgumentNullException">Si el email o password son nulos o vacíos</exception>
        public Empleado ValidarEmpleado(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "El correo electrónico es requerido");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "La contraseña es requerida");

            try
            {
                // Normalizar el email para la comparación (convertir a minúsculas)
                email = email.Trim().ToLower();

                // Buscamos al usuario en la base de datos por el correo (email)
                Empleado usuario = _db.Empleadoes.FirstOrDefault(x => x.Email.ToLower() == email);

                // Verificamos si el usuario existe y si la contraseña es correcta
                if (usuario != null && usuario.PasswordHash.SequenceEqual(GenerarSHA512ConSalt(password, usuario.PasswordSalt)))
                {
                    return usuario;  // Retornamos el usuario si las credenciales son válidas
                }

                return null;  // Credenciales inválidas
            }
            catch (Exception ex)
            {
                // Aquí sería recomendable usar un sistema de logging más robusto
                Console.WriteLine($"Error al validar empleado: {ex.Message}");
                throw new ApplicationException("Error al validar el empleado", ex);
            }
        }

        /// <summary>
        /// Genera un hash SHA-512 con salt para una contraseña
        /// </summary>
        /// <param name="password">Contraseña a hashear</param>
        /// <param name="salt">Valor de salt para aumentar la seguridad</param>
        /// <returns>Arreglo de bytes con el hash generado</returns>
        public static byte[] GenerarSHA512ConSalt(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "La contraseña no puede ser nula o vacía");

            if (salt == null || salt.Length == 0)
                throw new ArgumentNullException(nameof(salt), "El salt no puede ser nulo o vacío");

            // Concatenamos la contraseña con el salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            // Copiamos la contraseña en el arreglo saltedPassword
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);

            // Copiamos el salt en el arreglo saltedPassword
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            // Creamos una instancia de SHA-512
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Generamos el hash de la contraseña concatenada con el salt
                return sha512Hash.ComputeHash(saltedPassword);
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario existente
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Nueva contraseña</param>
        /// <returns>Usuario actualizado o null si ocurre un error</returns>
        /// <exception cref="ArgumentNullException">Si email o password son nulos</exception>
        /// <exception cref="InvalidOperationException">Si el usuario no existe</exception>
        public Usuario CambiarPassword(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "El correo electrónico es requerido");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "La contraseña es requerida");

            if (!ValidarFormatoPassword(password))
                throw new ArgumentException("La contraseña no cumple con los requisitos de seguridad", nameof(password));

            try
            {
                // Normalizar el email para la comparación
                email = email.Trim().ToLower();

                // Buscamos al usuario en la base de datos por el correo
                Usuario usuario = _db.Usuarios.FirstOrDefault(x => x.Email.ToLower() == email);

                if (usuario == null)
                    throw new InvalidOperationException($"El usuario con correo {email} no existe");

                // Actualizamos el hash de la contraseña
                usuario.PasswordHash = GenerarSHA512ConSalt(password, usuario.PasswordSalt);
                _db.SaveChanges();

                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar contraseña: {ex.Message}");
                throw new ApplicationException("Error al cambiar la contraseña", ex);
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema
        /// </summary>
        /// <param name="usuarioModel">Modelo con los datos del usuario a registrar</param>
        /// <returns>Usuario registrado o null si ocurre un error</returns>
        /// <exception cref="ArgumentNullException">Si usuarioModel es nulo</exception>
        /// <exception cref="InvalidOperationException">Si ya existe un usuario con el mismo email o cédula</exception>
        public Usuario RegistrarUsuario(UsuarioModel usuarioModel)
        {
            if (usuarioModel == null)
                throw new ArgumentNullException(nameof(usuarioModel), "Los datos del usuario son requeridos");

            ValidarDatosUsuario(usuarioModel);

            try
            {
                // Verificar si ya existe un usuario con el mismo email o cédula
                if (_db.Usuarios.Any(u => u.Email.ToLower() == usuarioModel.Email.ToLower()))
                    throw new InvalidOperationException($"Ya existe un usuario con el correo {usuarioModel.Email}");

                if (_db.Usuarios.Any(u => u.Cedula == usuarioModel.Cedula))
                    throw new InvalidOperationException($"Ya existe un usuario con la cédula {usuarioModel.Cedula}");

                // Generar un nuevo salt
                byte[] salt = GenerarSalt();

                // Crear el hash de la contraseña con el salt
                byte[] passwordHash = GenerarSHA512ConSalt(usuarioModel.Password, salt);

                // Crear un nuevo usuario
                Usuario nuevoUsuario = new Usuario
                {
                    Cedula = usuarioModel.Cedula.Trim(),
                    Nombre = usuarioModel.Nombre.Trim(),
                    Telefono = usuarioModel.Telefono?.Trim(),
                    Direccion = usuarioModel.Direccion?.Trim(),
                    Email = usuarioModel.Email.Trim().ToLower(),
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    FechaRegistro = DateTime.Now
                };

                // Guardar el nuevo usuario en la base de datos
                _db.Usuarios.Add(nuevoUsuario);
                _db.SaveChanges();

                return nuevoUsuario;
            }
            catch (InvalidOperationException)
            {
                // Relanzamos errores de validación específicos sin envolverlos
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                throw new ApplicationException("Error al registrar el usuario", ex);
            }
        }

        /// <summary>
        /// Valida los datos del usuario antes de registrarlo
        /// </summary>
        /// <param name="usuarioModel">Modelo con los datos del usuario a validar</param>
        /// <exception cref="ArgumentException">Si algún dato es inválido</exception>
        private void ValidarDatosUsuario(UsuarioModel usuarioModel)
        {
            if (string.IsNullOrWhiteSpace(usuarioModel.Cedula))
                throw new ArgumentException("La cédula es requerida", nameof(usuarioModel));

            if (string.IsNullOrWhiteSpace(usuarioModel.Nombre))
                throw new ArgumentException("El nombre es requerido", nameof(usuarioModel));

            if (string.IsNullOrWhiteSpace(usuarioModel.Email))
                throw new ArgumentException("El correo electrónico es requerido", nameof(usuarioModel));

            if (string.IsNullOrWhiteSpace(usuarioModel.Password))
                throw new ArgumentException("La contraseña es requerida", nameof(usuarioModel));

            // Validar el formato del email
            if (!ValidarFormatoEmail(usuarioModel.Email))
                throw new ArgumentException("El formato del correo electrónico es inválido", nameof(usuarioModel));

            // Validar la seguridad de la contraseña
            if (!ValidarFormatoPassword(usuarioModel.Password))
                throw new ArgumentException("La contraseña no cumple con los requisitos de seguridad", nameof(usuarioModel));
        }

        /// <summary>
        /// Valida que el formato del email sea válido
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <returns>true si el formato es válido, false en caso contrario</returns>
        private bool ValidarFormatoEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Expresión regular para validar email
                var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida que la contraseña cumpla con los requisitos de seguridad
        /// </summary>
        /// <param name="password">Contraseña a validar</param>
        /// <returns>true si la contraseña es segura, false en caso contrario</returns>
        private bool ValidarFormatoPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Mínimo 8 caracteres, al menos una letra mayúscula, una minúscula y un número
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return regex.IsMatch(password);
        }

        /// <summary>
        /// Genera un nuevo salt aleatorio para el hash de contraseñas
        /// </summary>
        /// <returns>Arreglo de bytes con el salt generado</returns>
        public static byte[] GenerarSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Libera los recursos utilizados por la clase
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera los recursos utilizados por la clase
        /// </summary>
        /// <param name="disposing">Indica si se están liberando recursos administrados</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor de la clase
        /// </summary>
        ~AuthCls()
        {
            Dispose(false);
        }
    }
}