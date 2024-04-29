using AppViagem.Helpers;

namespace AppViagem
{
    public partial class App : Application
    {
        public static SQLiteDb_Pedagios _db_pedagios;
        public static SQLiteDb_Viagens _db_viagens;

        public static SQLiteDb_Pedagios Db_pedagios
        {
            get
            {
                if (_db_pedagios == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_pedagios.db3");

                    _db_pedagios = new SQLiteDb_Pedagios(path);
                }
                return _db_pedagios;
            }
        }

        public static SQLiteDb_Viagens Db_viagens
        {
            get
            {
                if (_db_viagens == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_viagens.db3");

                    _db_viagens = new SQLiteDb_Viagens(path);
                }
                return _db_viagens;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
