using AppViagem.Models;
using AppViagem.Views;
using System.Diagnostics;

namespace AppViagem
{
    public partial class MainPage : ContentPage
    {
        int acrescimos = 0;
        string descicao_acrescimos ="Nenhum";

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, null);
        }
        public void Clear()
        {
            txt_origem.Text = String.Empty;
            txt_destino.Text = String.Empty;
            txt_distancia.Text = String.Empty;
            txt_rendimento.Text = String.Empty;
            txt_combustivel.Text = String.Empty;
            acrescimos = 0;
            descicao_acrescimos = "Nenhum";
            txt_descricao.Text = String.Empty;
        }
        private async void btn_viagens_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ListaViagens());
        }

        private async void btn_pedagios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ListaPedagios());
        }

        private async void btn_listagempedagios_Clicked(object sender, EventArgs e)
        {
            //string[] pedagios = ["oi0", "oi" ];
            //string action = await DisplayActionSheet("Escolha um pedágio", "Cancel", null, pedagios);
            //Debug.WriteLine("Action: " + action);
        }

        private async void btn_calculoviagem_Clicked(object sender, EventArgs e)
        {
            if(txt_origem.Text == String.Empty || txt_origem.Text == " " ||
               txt_destino.Text == String.Empty || txt_origem.Text == " " ||
               txt_distancia.Text == String.Empty || txt_distancia.Text == " " || double.Parse(txt_distancia.Text) <= 0 ||
               txt_rendimento.Text == String.Empty || txt_rendimento.Text == " " || double.Parse(txt_rendimento.Text) <= 0 ||
               txt_combustivel.Text == String.Empty || txt_combustivel.Text == " " || double.Parse(txt_combustivel.Text) <= 0)
            {
                await DisplayAlert("Ops!", $"Foi verificada a ausência de algum dos campos, verifique e tente novamente.", "OK");
                Clear();
            }
            else
            {
                double litragem = double.Parse(txt_distancia.Text) / double.Parse(txt_rendimento.Text);
                double gasto_combustivel = litragem * double.Parse(txt_combustivel.Text);
                double total_viagem = gasto_combustivel + acrescimos;

                Viagem v = new Viagem()
                {
                    OrigemViagem = txt_origem.Text,
                    DestinoViagem = txt_destino.Text,
                    DistanciaViagem = txt_distancia.Text + " Km",
                    RendimentoViagem = txt_rendimento.Text + " Km/L",
                    CombustivelViagem = "R$ " + txt_combustivel.Text,
                    DescViagem = descicao_acrescimos,
                    LitroViagem = litragem+" L",
                    TotalViagem = total_viagem.ToString("C")
                
                };
                await App.Db_viagens.InsertViagem(v);

                await DisplayAlert($"Viagem de {txt_origem.Text} a {txt_destino.Text}", $"O total de sua viagem resultará em: {total_viagem}", "Certo, obrigado(a)s!");
                Clear();
            }
        }

        private void btn_limparpedagios_Clicked(object sender, EventArgs e)
        {
            acrescimos = 0;
            descicao_acrescimos = "Nenhum";
            txt_descricao.Text = String.Empty;
        }
    }

}
