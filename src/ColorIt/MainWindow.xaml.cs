using System.Windows;

namespace ColorIt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            Dispatcher.UnhandledException += (sender, args) =>
            {
                MessageBox.Show("Fehler: " + args?.Exception?.Message);
            };
        }
    }
}