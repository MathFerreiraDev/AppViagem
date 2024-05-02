using AppViagem.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AppViagem.Views;

public partial class ListaPedagios : ContentPage
{
    ObservableCollection<Pedagio> listPedagios = new ObservableCollection<Pedagio>();
    public ListaPedagios()
    {
        InitializeComponent();
        lst_pedagios.ItemsSource = listPedagios;
    }

    protected async override void OnAppearing()
    {
        //if (listViagens.Count == 0)
        //{
        listPedagios.Clear();

        List<Pedagio> tmp = await App.Db_pedagios.SelectAllPedagios();
        foreach (Pedagio p in tmp)
        {
            listPedagios.Add(p);
        }

        Debug.WriteLine("TOTAL  ============================ " + listPedagios.Count);
        //}
    }

    private async void btn_adicionarpedagio_Clicked(object sender, EventArgs e)
    {
        string nome = "";
        string preco = " ";
        string rodovia = "";

        try
        {
            while ((nome == String.Empty || nome == " ") && nome != null)
            {
                nome = await DisplayPromptAsync("Novo Pedágio - Nome", "Insira o nome de determinado pedágio:", "Próximo", "Cancelar", maxLength: 15);
            }

            while ((preco == String.Empty || preco == " ") && nome != null && preco != null)
            {
                preco = await DisplayPromptAsync($"{nome} - taxa", $"Insira a taxação sobre passagem do pedágio:", "Próximo", "Cancelar", maxLength: 15, keyboard: Keyboard.Numeric);
                if ((preco != String.Empty && preco != " " && preco != null))
                {
                    if (double.Parse(preco) <= 0) { preco = String.Empty; }

                }
            }

            while ((rodovia == String.Empty || rodovia == " ") && nome != null && preco != null && rodovia != null)
            {
                rodovia = await DisplayPromptAsync($"{nome} - rodovia", $"Insira a rodovia onde se localiza o pedágio:", "Finalizar", "Cancelar", maxLength: 15);
            }

            if (nome != null && preco != null && rodovia != null)
            {

                Pedagio p = new Pedagio()
                {
                    NomePedagio = nome,
                    PrecoPedagio = double.Parse(preco).ToString("C"),
                    EstacaoPedagio = rodovia

                };
                await App.Db_pedagios.InsertPedagio(p);

                listPedagios.Clear();

                List<Pedagio> tmp = await App.Db_pedagios.SelectAllPedagios();
                foreach (Pedagio pd in tmp)
                {
                    listPedagios.Add(pd);
                }

                lst_pedagios.ItemsSource = listPedagios;
            }
        }catch (Exception ex)
        {
            await DisplayAlert("Ops!", $"Ocorreu um erro ao registrar o novo pedágio.", "Certo, obrigado(a)!");
        }
    }

    

    private async void lst_pedagios_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {


        
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Pedagio p = (Pedagio)sender;

        Debug.WriteLine(p);
        Debug.WriteLine("-------------------------------------------------------");

        bool opcao = await DisplayAlert("Excluir", $"Deseja excluir o pedágio ?", "Sim", "Não");

        if (opcao)
        {

        }
    }
}