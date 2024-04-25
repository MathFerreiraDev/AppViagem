using AppViagem.Views;
using System.Diagnostics;

namespace AppViagem
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, null);
        }



        private void OnCounterClicked(object sender, EventArgs e)
        {
            
        }

        private async void btn_viagens_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaViagens());
        }

        private async void btn_pedagios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaPedagios());
        }

        private async void btn_listagempedagios_Clicked(object sender, EventArgs e)
        {
            string[] pedagios = ["oi0", "oi" ];
            string action = await DisplayActionSheet("Escolha um pedágio", "Cancel", null, pedagios);
            Debug.WriteLine("Action: " + action);
        }

        private void btn_calculoviagem_Clicked(object sender, EventArgs e)
        {

        }
    }

}
