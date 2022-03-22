using System;
using System.Windows;
using System.Windows.Threading;

namespace clinica
{
    public partial class SplashWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();

        public SplashWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 5);
            dt.Start();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            MainWindow inicio = new MainWindow();
            inicio.Show();

            dt.Stop();
            this.Close();
        }
    }
}
