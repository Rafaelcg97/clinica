using clinica.clases;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace clinica
{
    public partial class NuevaConsulta : Page
    {
        public NuevaConsulta()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            popUpPaciente.IsOpen = true;
            string sql = "SELECT TOP 100 [idPaciente],[fechaRegistroPaciente],[nombrePaciente],[documentoPaciente],[fechaNacimientoPaciente],[telefonoPaciente],[correoPaciente] FROM [dbo].[pacientes]";

            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    SqlCommand cm = new SqlCommand(sql, cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        lstPacientes.Items.Add(new paciente(Convert.ToInt32(dr["idPaciente"]), dr["nombrePaciente"].ToString(), dr["documentoPaciente"].ToString(), Convert.ToDateTime(dr["fechaNacimientoPaciente"]).ToString("yyyy-MM-dd"), dr["telefonoPaciente"].ToString(), dr["correoPaciente"].ToString()));
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            lstPacientes.Items.Clear();
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                string sql = "SELECT TOP 100 [idPaciente],[fechaRegistroPaciente],[nombrePaciente],[documentoPaciente],[fechaNacimientoPaciente],[telefonoPaciente],[correoPaciente] FROM [dbo].[pacientes]";

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            lstPacientes.Items.Add(new paciente(Convert.ToInt32(dr["idPaciente"]), dr["nombrePaciente"].ToString(), dr["documentoPaciente"].ToString(), Convert.ToDateTime(dr["fechaNacimientoPaciente"]).ToString("yyyy-MM-dd"), dr["telefonoPaciente"].ToString(), dr["correoPaciente"].ToString()));
                        }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                string sql = "SELECT [idPaciente],[fechaRegistroPaciente],[nombrePaciente],[documentoPaciente],[fechaNacimientoPaciente],[telefonoPaciente],[correoPaciente] FROM [dbo].[pacientes] WHERE nombrePaciente LIKE'%" + txtBuscar.Text + "%'";

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            lstPacientes.Items.Add(new paciente(Convert.ToInt32(dr["idPaciente"]), dr["nombrePaciente"].ToString(), dr["documentoPaciente"].ToString(), Convert.ToDateTime(dr["fechaNacimientoPaciente"]).ToString("yyyy-MM-dd"), dr["telefonoPaciente"].ToString(), dr["correoPaciente"].ToString()));
                        }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnSeleccionarPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (lstPacientes.SelectedIndex > -1)
            {
                paciente pacienteSeleccionado = (paciente)lstPacientes.SelectedItem;
                lblNombre.Content = pacienteSeleccionado.Nombre;
                lblId.Content = pacienteSeleccionado.Id;

                txtblDiagnostico.Text = "----";
                txtblMotivo.Text = "----";
                txtblPlan.Text = "----";

                string sql = "SELECT TOP 1 motivoConsulta, diagnosticoConsulta, planTerapeuticoConsulta FROM consultas WHERE idPaciente='"+pacienteSeleccionado.Id+"' ORDER BY idConsulta DESC;";

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            txtblDiagnostico.Text= dr["diagnosticoConsulta"].ToString();
                            txtblMotivo.Text= dr["motivoConsulta"].ToString();
                            txtblPlan.Text= dr["planTerapeuticoConsulta"].ToString();
                        }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                popUpPaciente.IsOpen = false;
            }
        }

        private void soloNumerosDecimales(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Contains("."))
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Tab))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Decimal) || (e.Key == Key.OemPeriod) || (e.Key == Key.Tab))
                    e.Handled = false;
                else
                    e.Handled = true;
            }

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (lblId.Content.ToString() != "----")
            {
                string sql = "INSERT INTO consultas(fechaConsulta, idPaciente, motivoConsulta, diagnosticoConsulta, planTerapeuticoConsulta, examenesFisicosConsulta) " +
                    "VALUES('"+DateTime.Now.ToString("yyyy-MM-dd")+"','"+lblId.Content+"','"+txtMotivo.Text+"','"+txtDiagnostico.Text+"','"+txtPlanTerapeutico.Text+"','"+txtExamenes.Text+ "')SELECT SCOPE_IDENTITY()";
                int idSolicitud = 0;
                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        dr.Read();
                        idSolicitud = Convert.ToInt32(dr[0]);
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    try
                    {
                        string sql2 = "INSERT INTO antecedentes(idPaciente, idConsultas, personalesAntecedentes, familiaresAntecedentes, gAntecedentes, pvAntecedentes, pcAntecendentes, ppAntecendentes, abAntecendentes, vAntecendentes, pAntecendentes, furAntecedentes, fppAntecedentes, TallaAntecedentes, PesoAntecedentes)" +
                                        " VALUES('" + lblId.Content + "','" + idSolicitud + "','" + txtPersonales.Text + "','" + txtFamiliares.Text + "','" + txtG.Text + "','" + txtPV.Text + "','" + txtPC.Text + "','" + txtPP.Text + "','" + txtAB.Text + "','" + txtV.Text + "','" + txtP.Text + "','" + txtFUR.Text + "','" + txtFPP.Text + "', '" + txtTalla.Text + "', '" + txtPeso.Text + "');";
                        SqlCommand cm = new SqlCommand(sql2, cn);
                        cm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (Convert.ToDateTime(dtpFecha.SelectedDate).ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        try
                        {
                            string sql3 = "INSERT INTO avisos(idConsulta, fechaAviso, textAviso) VALUES('"+idSolicitud+"','"+Convert.ToDateTime(dtpFecha.SelectedDate).ToString("yyyy-MM-dd")+"','"+txtAviso.Text+"')";
                            SqlCommand cm = new SqlCommand(sql3, cn);
                            cm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                }
                MessageBox.Show("Consulta Guardada", "Aviso",MessageBoxButton.OK,MessageBoxImage.Information);
                NavigationService.Navigate(new Index());
            }
            else
            {
                MessageBox.Show("Seleccione un paciente");
            }
        }

        private string generarImc()
        {
            string imc = "---";
            double n;
            bool result = double.TryParse(txtTalla.Text, out n) && double.TryParse(txtPeso.Text, out n);
            if (result)
            {
                imc = ((Convert.ToDouble(txtPeso.Text) / 2.2) / Math.Pow(Convert.ToDouble(txtTalla.Text), 2)).ToString();
            }

            return imc;
        }

        private void txtTalla_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtblImc.Text = generarImc();
        }
    }
}
