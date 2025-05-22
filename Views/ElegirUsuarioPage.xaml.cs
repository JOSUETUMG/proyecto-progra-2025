using ProyectoF.Services;
using ProyectoF.Views;

namespace ProyectoF.Views;

public partial class ElegirUsuarioPage : ContentPage
{
    public ElegirUsuarioPage()
    {
        InitializeComponent();
    }

    private async void AdminClick(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Admin";
        await Navigation.PushAsync(new LogInPage());
    }

    private async void MaestroClick(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Maestro";
        await Navigation.PushAsync(new LogInPage());
    }

    private async void EstudianteClick(object sender, EventArgs e)
    {
        SessionData.TipoUsuario = "Estudiante";
        await Navigation.PushAsync(new LogInPage());
    }
}

