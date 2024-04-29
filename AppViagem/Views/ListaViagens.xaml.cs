using AppViagem.Models;
using System.Collections.ObjectModel;

namespace AppViagem.Views;

public partial class ListaViagens : ContentPage
{
    ObservableCollection<Viagem> listViagens = new ObservableCollection<Viagem>();
    public ListaViagens()
	{
		InitializeComponent();
        lst_viagensviewer.ItemsSource = listViagens;
        lst_viagensviewer.Focus();
    }
    protected async override void OnAppearing()
    {
        if (listViagens.Count == 0)
        {

            List<Viagem> tmp = await App.Db_viagens.SelectAllViagens();
            foreach (Viagem v in tmp)
            {
                listViagens.Add(v);
            }

        }
    }


    private void lst_viagens_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selected = (MenuItem)sender;
            Viagem v = selected.BindingContext as Viagem;
            bool confirm = await DisplayAlert("Tem certeza?", $"Remover viagem de {v.OrigemViagem} a {v.DestinoViagem}?", "Confirmar", "Cancelar");

            if (confirm)
            {
                await App.Db_viagens.DeleteViagem(v.Id);
                await DisplayAlert("Sucesso", $"A viagem {v.OrigemViagem} a {v.DestinoViagem} foi removida da lista!", "OK");
                listViagens.Remove(v);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}