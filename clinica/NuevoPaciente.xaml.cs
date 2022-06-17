using clinica.clases;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace clinica
{
    public partial class NuevoPaciente : Page
    {
        public NuevoPaciente()
        {
            InitializeComponent();
            //Para estandarizar formatos de fecha
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;

            //para mostrar datos preliminares en listview
            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_consultar_pacientes", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombrePaciente", "");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lstPacientes.Items.Add(
                                new paciente(
                                    Convert.ToInt32(dt.Rows[i][0]),
                                    Convert.ToString(dt.Rows[i][2]),
                                    Convert.ToString(dt.Rows[i][3]),
                                    Convert.ToDateTime(dt.Rows[i][4]),
                                    Convert.ToString(dt.Rows[i][5]),
                                    Convert.ToString(dt.Rows[i][6])
                                    )
                                );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text) || !mskDocumento.IsMaskCompleted || !mskTelefono.IsMaskCompleted || Convert.ToDateTime(dpFecha.SelectedDate).ToString("yyyy-MM-dd")=="0001-01-01")
            {
                MessageBox.Show("Debe ingresar: nombre, DUI, telefono y fecha de nacimiento en un formato valido");
            }
            else
            {
                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        if (txtId.Text == "0")
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_insertar_paciente", cn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@nombrePaciente", Convert.ToString(txtNombre.Text));
                                cmd.Parameters.AddWithValue("@documentoPaciente", mskDocumento.Text);
                                cmd.Parameters.AddWithValue("@fechaNacimientoPaciente", Convert.ToDateTime(dpFecha.SelectedDate));
                                cmd.Parameters.AddWithValue("@telefonoPaciente", mskTelefono.Text);
                                cmd.Parameters.AddWithValue("@correoPaciente", txtCorreo.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_actualizar_paciente", cn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@idPaciente",Convert.ToInt32(txtId.Text));
                                cmd.Parameters.AddWithValue("@nombrePaciente", Convert.ToString(txtNombre.Text));
                                cmd.Parameters.AddWithValue("@documentoPaciente", mskDocumento.Text);
                                cmd.Parameters.AddWithValue("@fechaNacimientoPaciente", Convert.ToDateTime(dpFecha.SelectedDate));
                                cmd.Parameters.AddWithValue("@telefonoPaciente", mskTelefono.Text);
                                cmd.Parameters.AddWithValue("@correoPaciente", txtCorreo.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                NavigationService.Navigate(new Index());
            }
        }
        private void lstPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPacientes.SelectedIndex > -1)
            {
                paciente itemSeleccionado = (paciente)lstPacientes.SelectedItem;
                txtId.Text = itemSeleccionado.Id.ToString();
                txtNombre.Text = itemSeleccionado.Nombre;
                mskDocumento.Text = itemSeleccionado.Dui;
                mskTelefono.Text = itemSeleccionado.Telefono;
                txtCorreo.Text = itemSeleccionado.Correo;
                dpFecha.SelectedDate= Convert.ToDateTime(itemSeleccionado.FechaNacimiento);
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "0";
            txtNombre.Clear();
            mskDocumento.Clear();
            mskTelefono.Clear();
            txtCorreo.Clear();

        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            lstPacientes.Items.Clear();
            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_consultar_pacientes", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombrePaciente", txtBuscar.Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lstPacientes.Items.Add(
                                new paciente(
                                    Convert.ToInt32(dt.Rows[i][0]),
                                    Convert.ToString(dt.Rows[i][2]),
                                    Convert.ToString(dt.Rows[i][3]),
                                    Convert.ToDateTime(dt.Rows[i][4]),
                                    Convert.ToString(dt.Rows[i][5]),
                                    Convert.ToString(dt.Rows[i][6])
                                    )
                                );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void dpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime nacimiento = Convert.ToDateTime(dpFecha.SelectedDate); //Fecha de nacimiento
            int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            txtEdad.Text = edad.ToString();
        }
    }
}
