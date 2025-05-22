using ProyectoF.Services;
using Microsoft.Maui.Controls;
using ProyectoF.Views;

namespace ProyectoF.Views;

public partial class LogInPage : ContentPage
{
    private readonly FirebaseAuthService _authService = new FirebaseAuthService();

    public LogInPage()
    {
        InitializeComponent();
    }

    private async void AtrasClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ElegirUsuarioPage");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = usuarioEntry.Text;
        string password = contrasenaEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingresa tu correo y contraseña.", "OK");
            return;
        }

        var result = await _authService.SignInUserAsync(email, password);

        if (result.Contains("Inicio de sesión exitoso"))
        {
            await DisplayAlert("Bienvenido", result, "OK");

            switch (SessionData.TipoUsuario)
            {
                case "Admin":
                    await Shell.Current.GoToAsync("//InicioAdminPage");
                    break;
                case "Maestro":
                    await Shell.Current.GoToAsync("//InicioMaestroPage");
                    break;
                case "Estudiante":
                    await Shell.Current.GoToAsync("//InicioEstudiantePage");
                    break;
                default:
                    await DisplayAlert("Error", "Tipo de usuario no reconocido.", "OK");
                    break;
            }
        }
        else
        {
            await DisplayAlert("Error", result, "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string email = usuarioEntry.Text;
        string password = contrasenaEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo y contraseña válidos.", "OK");
            return;
        }

        var result = await _authService.RegisterUserAsync(email, password);

        if (result.Contains("Usuario registrado"))
        {
            await DisplayAlert("Registro exitoso", "El usuario ha sido registrado correctamente.", "OK");
        }
        else
        {
            await DisplayAlert("Error", result, "OK");
        }
    }
}