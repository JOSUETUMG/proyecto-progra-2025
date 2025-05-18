using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

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
        private async void OnSalaPorTemasClicked(object sender, EventArgs e)
        {
            var popup = new CustomPopup();
            await this.ShowPopupAsync(popup);
        }
        public class CustomPopup : Popup
        {
            public CustomPopup()
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
    }
}