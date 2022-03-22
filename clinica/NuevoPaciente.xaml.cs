using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clinica
{
    /// <summary>
    /// Lógica de interacción para NuevoPaciente.xaml
    /// </summary>
    public partial class NuevoPaciente : Page
    {
        public NuevoPaciente()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
