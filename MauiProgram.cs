using System.Reflection;
using CommunityToolkit.Maui;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;

namespace ProyectoF
{ //grep -ril 'SessionData.TipoUsuario' . grep -ril 'SessionData.TipoUsuario' . | xargs -I {} mv {} rescatados3/
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string resourceName = "ProyectoF.Resources.Raw.firebase-adminsdk.json";
            Assembly assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromStream(stream)
                });
            }
            else
            {
                Console.WriteLine("Error: No se encontró el archivo JSON en los recursos.");
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
