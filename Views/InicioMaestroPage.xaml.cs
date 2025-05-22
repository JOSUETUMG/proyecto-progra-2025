using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Media;
using Microsoft.Maui.Media;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Diagnostics;

namespace ProyectoF.Views
{
    public partial class InicioMaestroPage : ContentPage
    {
        public InicioMaestroPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, true);
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        }
        private async void SalaPorTemasClick(object sender, EventArgs e)
        {
            var temaPopup = new ElegirTemaPopup();
            await this.ShowPopupAsync(temaPopup);
        }
        private async void SalaAPartirDeDocumentosClick(object sender, EventArgs e)
        {
            var documentoPopup = new DocumentoPopup();
            await this.ShowPopupAsync(documentoPopup);
        }

        public class ElegirTemaPopup : Popup
        {
            public ElegirTemaPopup()
            {
                Size = new Size(400, 350);
                Color = new Color(0, 0, 0, 102);
                Content = new Border
                {
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(20),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            new Label
                            {
                                Text = "Elige el tema de la sala:",
                                FontSize = 20,
                                HorizontalOptions = LayoutOptions.Center,
                                TextColor = Colors.Black,
                                Margin = new Thickness(0, 0, 0, 20)
                            },

                            new Button
                            {
                                Text = "Matemáticas",
                                BackgroundColor = Color.FromArgb("#4CAF50"),
                                TextColor = Colors.White,
                                CornerRadius = 10,
                                HeightRequest = 50,
                                WidthRequest = 250
                            },

                            new Button
                            {
                                Text = "Ciencia",
                                BackgroundColor = Color.FromArgb("#4CAF50"),
                                TextColor = Colors.White,
                                CornerRadius = 10,
                                HeightRequest = 50,
                                WidthRequest = 250
                            },

                            new Button
                            {
                                Text = "Historia",
                                BackgroundColor = Color.FromArgb("#4CAF50"),
                                TextColor = Colors.White,
                                CornerRadius = 10,
                                HeightRequest = 50,
                                WidthRequest = 250
                            }
                        }
                    }
                };
            }
        }

        public class DocumentoPopup : Popup
        {
            private async Task<string> EscanearDocumento()
            {
                var foto = await MediaPicker.Default.CapturePhotoAsync();
                if (foto != null)
                {
                    using var stream = await foto.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    string base64Imagen = Convert.ToBase64String(imageBytes);
                    return await EnviarImagenACloudVision(base64Imagen);
                }

                return "Error al capturar la imagen";
            }

            public async Task<string> EnviarImagenACloudVision(string base64Imagen)
            {
                var apiKey = "AIzaSyC4IkX2VHcShhfGQgjx8I5ar6JtfHj0juY";
                var url = $"https://vision.googleapis.com/v1/images:annotate?key={apiKey}";

                var jsonPayload = new
                {
                    requests = new[]
                    {
                        new
                        {
                            image = new { content = base64Imagen},
                            features = new[] { new { type = "TEXT_DETECTION"}}
                        }

                    }
                }; 
                
                var httpClient = new HttpClient();
                var contenido = new StringContent(JsonSerializer.Serialize(jsonPayload), Encoding.UTF8, "application/json");
                var respuesta = await httpClient.PostAsync(url, contenido);
                var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();

                return await respuesta.Content.ReadAsStringAsync();
            }

            string ExtraerTextoOCR(string json)
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    var responses = root.GetProperty("responses");

                    if (responses.GetArrayLength() > 0)
                    {
                        var response = responses[0];

                        if (response.TryGetProperty("fullTextAnnotation", out JsonElement fullText))
                        {
                            string textoCompleto = fullText.GetProperty("text").GetString();

                            // Elimina saltos de línea y aplica uno solo después de cada punto.
                            string textoFormateado = textoCompleto
                                .Replace("\n", " ")           // Unifica todo en una sola línea
                                .Replace("\r", " ")           // Por si acaso hay retornos de carro
                                .Replace("  ", " ")           // Elimina espacios dobles
                                .Replace(". ", ".\n");        // Agrega salto de línea solo después de los puntos

                            return textoFormateado.Trim();
                        }
                    }
                }

                return "No se pudo extraer el texto.";
            }








            public class ResultadoOCRPopup : Popup
            {
                public ResultadoOCRPopup(string textoOCR)
                {
                    Size = new Size(400, 350);
                    Color = new Color(0, 0, 0, 102);
                    Content = new Border
                    {
                        BackgroundColor = Colors.White,
                        Padding = new Thickness(20),
                        Content = new VerticalStackLayout
                        {
                            Spacing = 20,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                {
                    new Label
                    {
                        Text = "Texto escaneado:",
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.Black,
                        Margin = new Thickness(0, 0, 0, 10)
                    },
                    new ScrollView
                    {
                        Content = new Label
                        {
                            Text = textoOCR,
                            FontSize = 16,
                            TextColor = Colors.Black
                        },
                        HeightRequest = 200
                    },
                    new Button
                    {
                        Text = "Cerrar",
                        BackgroundColor = Colors.Red,
                        TextColor = Colors.White,
                        CornerRadius = 10,
                        Command = new Command(() => Close())
                    }
                }
                        }
                    };
                }
            }







            public DocumentoPopup()
            {
                Size = new Size(400, 350);
                Color = new Color(0, 0, 0, 102);
                Content = new Border
                {
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(20),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            new Label
                            {
                                Text = "Elige una opción:",
                                FontSize = 20,
                                HorizontalOptions = LayoutOptions.Center,
                                TextColor = Colors.Black,
                                Margin = new Thickness(0, 0, 0, 20)
                            },

                            new Button
                            {
                                Text = "Escanear documento",
                                BackgroundColor = Color.FromArgb("#4CAF50"),
                                TextColor = Colors.White,
                                CornerRadius = 10,
                                HeightRequest = 50,
                                WidthRequest = 250,
                                Command = new Command(async () =>
                                {
                                    var json = await EscanearDocumento();
                                    var resultado = ExtraerTextoOCR(json);
                                    var popup = new ResultadoOCRPopup(resultado);
                                    await Application.Current.MainPage.ShowPopupAsync(popup);
                                })
                            },


                            new Button
                            {
                                Text = "Cargar Documento",
                                BackgroundColor = Color.FromArgb("#4CAF50"),
                                TextColor = Colors.White,
                                CornerRadius = 10,
                                HeightRequest = 50,
                                WidthRequest = 250
                            },

                        }
                    }
                };
            }

        }
    }
}