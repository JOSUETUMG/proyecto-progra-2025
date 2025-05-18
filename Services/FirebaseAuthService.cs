using Firebase.Auth;
using System;
using System.Threading.Tasks;

namespace ProyectoF.Services
{
    public class FirebaseAuthService
    {
        private readonly FirebaseAuthProvider _authProvider;

        public FirebaseAuthService()
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBu0ygol_VicpDHtXQal99aF7YnyiVxj5g"));
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            try
            {
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                return "Usuario registrado";
            }
            catch (Exception ex)
            {
                return "Error, no se pudo registrar el usuario";
            }
        }


        public async Task<string> SignInUserAsync(string email, string password)
        {
            try
            {
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
                return "Inicio de sesión exitoso";
            }
            catch (Exception ex)
            {
                return "Error, credenciales inválidas";
            }
        }
    }
}