using System.Windows;

namespace clinica
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frameMain.Content = new Index();
        }
    }
}
