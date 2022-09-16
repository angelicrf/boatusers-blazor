namespace BoatUsersXMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Application.Current.MainPage = new AppShell();

        }
    }
}