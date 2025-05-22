using ProyectoF.Views;
namespace ProyectoF

{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ElegirUsuarioPage", typeof(ProyectoF.Views.ElegirUsuarioPage));
            Routing.RegisterRoute("LogInPage", typeof(ProyectoF.Views.LogInPage));
            Routing.RegisterRoute("InicioMaestroPage", typeof(ProyectoF.Views.InicioMaestroPage));
            Routing.RegisterRoute("InicioEstudiantePage", typeof(ProyectoF.Views.InicioEstudiantePage));
            Routing.RegisterRoute("InicioAdminPage", typeof(ProyectoF.Views.InicioAdminPage));
        }
    }
}