using ProyectoF.Services;
using ProyectoF.Views;

namespace ProyectoF.Views;

public partial class ElegirUsuarioPage : ContentPage
{
    public ElegirUsuarioPage()
    {
        InitializeComponent();
    }

    private async void OnAdminClicked(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Admin";
        await Navigation.PushAsync(new LogInPage());
    }

    private async void OnMaestroClicked(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Maestro";
        await Navigation.PushAsync(new LogInPage());
    }

    private async void OnEstudianteClicked(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Estudiante";
        await Navigation.PushAsync(new LogInPage());
    }
}

