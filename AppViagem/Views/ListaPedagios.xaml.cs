using AppViagem.Models;

namespace AppViagem.Views;

public partial class ListaPedagios : ContentPage
{
    public ListaPedagios()
    {
        InitializeComponent();
    }

    private async void btn_adicionarpedagio_Clicked(object sender, EventArgs e)
    {
        string nome = "";
        string preco = " ";
        string rodovia = "";
        string cancel = null;

        while ((nome == String.Empty || nome == " ") && nome != null)
        {
            nome = await DisplayPromptAsync("Novo Pedágio - Nome", "Insira o nome de determinado pedágio:", "Próximo", "Cancelar", maxLength: 15);
        }

        while ((preco == String.Empty || preco == " ") && nome != null && preco != null)
        {
            preco = await DisplayPromptAsync($"{nome} - taxa", $"Insira a taxação sobre passagem do pedágio:", "Próximo", "Cancelar", maxLength: 15, keyboard: Keyboard.Numeric);
            if ((preco != String.Empty && preco != " " && preco != null))
            {
                if(double.Parse(preco) <= 0) { preco = String.Empty; }
                
            }
        }

        while ((rodovia == String.Empty || rodovia == " ") && nome != null && preco != null && rodovia != null)
        {
            rodovia = await DisplayPromptAsync($"{nome} - rodovia", $"Insira a rodovia onde se localiza o pedágio:", "Finalizar", "Cancelar", maxLength: 15);
        }


        Pedagio p = new Pedagio()
        {
            NomePedagio = nome,
            PrecoPedagio = preco,
            EstacaoPedagio = rodovia

        };
        await App.Db_pedagios.InsertPedagio(p);
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }

    private void lst_pedagios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}