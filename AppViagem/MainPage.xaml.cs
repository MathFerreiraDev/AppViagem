using AppViagem.Models;
using AppViagem.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AppViagem
{
    public partial class MainPage : ContentPage
    {
        double acrescimos = 0;
        string descricao_acrescimos ="";

        List<Pedagio> listaPedagios = new List<Pedagio>();


        public MainPage()
        {
            InitializeComponent();
           
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, null);
            
        }

        protected async override void OnAppearing()
        {
            //if (listViagens.Count == 0)
            //{
            listaPedagios.Clear();

            List<Pedagio> tmp = await App.Db_pedagios.SelectAllPedagios();
            foreach (Pedagio p in tmp)
            {
                listaPedagios.Add(p);
            }

            
            //}
        }

        public void Clear()
        {
            txt_origem.Text = String.Empty;
            txt_destino.Text = String.Empty;
            txt_distancia.Text = String.Empty;
            txt_rendimento.Text = String.Empty;
            txt_combustivel.Text = String.Empty;
            acrescimos = 0;
            descricao_acrescimos = "";
            lbl_descricao.Text = String.Empty;
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
            try
            {
                //string[] pedagios = ["oi0", "oi" ];
                string pedagioescolhidonome = await DisplayActionSheet("Escolha um pedágio", "Cancel", null, listaPedagios.Select(x => x.NomePedagio).ToArray());

                if (pedagioescolhidonome != null)
                {
                    string pedadagioescolhidovalor = listaPedagios.Find(p => p.NomePedagio == pedagioescolhidonome).PrecoPedagio;

                    acrescimos += double.Parse(pedadagioescolhidovalor.Replace("R$ ", ""));
                    descricao_acrescimos += $"[ {pedagioescolhidonome} - {pedadagioescolhidovalor} ] ";

                    //Debug.WriteLine("Action: " + action);
                    lbl_descricao.Text = descricao_acrescimos;
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Ops!", $"Ocorreu um erro ao listar os pedágios existentes.", "OK");
            }
        }

        private async void btn_calculoviagem_Clicked(object sender, EventArgs e)
        {
            try {
                if (txt_origem.Text == String.Empty || txt_origem.Text == " " ||
                   txt_destino.Text == String.Empty || txt_origem.Text == " " ||
                   txt_distancia.Text == String.Empty || txt_distancia.Text == " " || double.Parse(txt_distancia.Text) <= 0 ||
                   txt_rendimento.Text == String.Empty || txt_rendimento.Text == " " || double.Parse(txt_rendimento.Text) <= 0 ||
                   txt_combustivel.Text == String.Empty || txt_combustivel.Text == " " || double.Parse(txt_combustivel.Text) <= 0)
                {
                    await DisplayAlert("Ops!", $"Foi verificada a ausência de algum dos campos, verifique e tente novamente.", "OK");
                    
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
                        DescViagem = (descricao_acrescimos == "") ? "Nenhum" : descricao_acrescimos,
                        LitroViagem = litragem + " L",
                        TotalViagem = total_viagem.ToString("C")

                    };
                    await App.Db_viagens.InsertViagem(v);

                    await DisplayAlert($"Viagem de {txt_origem.Text} a {txt_destino.Text}", $"O total de sua viagem resultará em: {total_viagem.ToString("C")}", "Certo, obrigado(a)s!");
                    
               
            }
            }catch (Exception ex){
                await DisplayAlert("Ops!", $"Ocorreu um erro ao registar a viagem, tente novamente.", "Certo, obrigado(a)!");
            }
            Clear();
        }

        private void btn_limparpedagios_Clicked(object sender, EventArgs e)
        {
            acrescimos = 0;
            descricao_acrescimos = "";
            lbl_descricao.Text = String.Empty;
        }
    }

}
