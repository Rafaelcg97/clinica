using clinica.clases;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace clinica
{
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
            //colocar fondo personalizado
            string imageName = "imagenes/Wallpapers/" + Properties.Settings.Default.wallpaper + ".jpg";
            gridAreaTrabajo.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), imageName)));
            //colocar usuario
            txtNombreUsuario.Text = Properties.Settings.Default.usuario;
            //colocar imagen de usuario
            try
            {
                imageUsuario.ImageSource = new BitmapImage(new Uri(Properties.Settings.Default.foto));
            }
            catch
            {
                imageUsuario.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "imagenes/usuario.png"));
            }
            //Iniciar reloj
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            string sql = "SELECT idConsulta, textAviso FROM avisos WHERE fechaAviso='"+DateTime.Now.ToString("yyyy-MM-dd")+"';";

            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    SqlCommand cm = new SqlCommand(sql, cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        lstAvisos.Items.Add(new aviso(Convert.ToInt32(dr["idConsulta"]), dr["textAviso"].ToString()));
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #region accionesMenuLateral
        private void btnCambiarImagenUsuario_Click(object sender, RoutedEventArgs e)
        {
            string file = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg|*.jpg|png|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                file = openFileDialog.FileName;
                imageUsuario.ImageSource = new BitmapImage(new Uri(file));
                Properties.Settings.Default.foto = file;
                Properties.Settings.Default.Save();
            }
        }
        private void btnMostarMenu_Checked(object sender, RoutedEventArgs e)
        {
            gridMenu.Margin = new Thickness(0, 0, 0, 0);
            gridAreaTrabajo.Margin = new Thickness(200, 0, 0, 0);
            imgMenu.Source = new BitmapImage(new Uri("/imagenes/cerrarMenu.png", UriKind.Relative));
        }
        private void btnMostarMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            gridMenu.Margin = new Thickness(-200, 0, 0, 0);
            gridAreaTrabajo.Margin = new Thickness(0, 0, 0, 0);
            imgMenu.Source = new BitmapImage(new Uri("/imagenes/abrirMenu.png", UriKind.Relative));
        }
        private void btnEditarUsuario_Checked(object sender, RoutedEventArgs e)
        {
            txtNombreUsuario.IsEnabled = true;
            txtNombreUsuario.BorderThickness = new Thickness(1);
        }
        private void btnEditarUsuario_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.usuario = txtNombreUsuario.Text;
            Properties.Settings.Default.Save();
            txtNombreUsuario.IsEnabled = false;
            txtNombreUsuario.BorderThickness = new Thickness(0);
        }
        #endregion
        #region accionesEncabezado
        private void btnCambiarWallpaper_Click(object sender, RoutedEventArgs e)
        {
            popWallpapers.IsOpen = true;
        }
        private void btnCerrarPop_Click(object sender, RoutedEventArgs e)
        {
            popWallpapers.IsOpen = false;
        }
        private void btnSeleccionarWallpaper_Click(object sender, RoutedEventArgs e)
        {
            if (lstListaWallpapers.SelectedIndex > -1)
            {
                string imageName = "imagenes/Wallpapers/w" + lstListaWallpapers.SelectedIndex.ToString() + ".jpg";
                string wallpaper_ = "w" + lstListaWallpapers.SelectedIndex.ToString();
                gridAreaTrabajo.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), imageName)));

                Properties.Settings.Default.wallpaper = wallpaper_;
                Properties.Settings.Default.Save();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblHora.Content = DateTime.Now.ToLongTimeString();
        }
        #endregion

        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new NuevoPaciente());
        }

        private void btnNuevaConsulta_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NuevaConsulta());
        }
    }
}
