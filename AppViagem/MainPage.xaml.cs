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
    }

}
