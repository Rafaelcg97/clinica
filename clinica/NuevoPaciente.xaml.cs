﻿using clinica.clases;
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

namespace clinica
{
    public partial class NuevoPaciente : Page
    {
        public NuevoPaciente()
        {
            InitializeComponent();
            string sql = "SELECT TOP 100 [idPaciente],[fechaRegistroPaciente],[nombrePaciente],[documentoPaciente],[fechaNacimientoPaciente],[telefonoPaciente],[correoPaciente] FROM [dbo].[pacientes]";

            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    SqlCommand cm = new SqlCommand(sql, cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        lstPacientes.Items.Add(new paciente(Convert.ToInt32(dr["idPaciente"]),dr["nombrePaciente"].ToString(), dr["documentoPaciente"].ToString(), Convert.ToDateTime(dr["fechaNacimientoPaciente"]).ToString("yyyy-MM-dd"), dr["telefonoPaciente"].ToString(), dr["correoPaciente"].ToString()));
                    }
                    dr.Close();
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
                string sql = "INSERT INTO pacientes(fechaRegistroPaciente, nombrePaciente, documentoPaciente, fechaNacimientoPaciente, telefonoPaciente, correoPaciente) " +
                             "VALUES('"+DateTime.Now.ToString("yyyy-MM-dd")+"', '"+txtNombre.Text+"','"+mskDocumento.Text+"','"+Convert.ToDateTime(dpFecha.SelectedDate).ToString("yyyy-MM-dd")+"','"+mskTelefono.Text+"','"+txtCorreo.Text+"')";
                if (txtId.Text != "0")
                {
                    sql = "UPDATE pacientes SET nombrePaciente='" + txtNombre.Text + "', documentoPaciente='" + mskDocumento.Text + "', fechaNacimientoPaciente='" + Convert.ToDateTime(dpFecha.SelectedDate).ToString("yyyy-MM-dd") + "', telefonoPaciente='" + mskTelefono.Text + "', correoPaciente='" + txtCorreo.Text + "' WHERE idPaciente='"+txtId.Text+"'";
                }
                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        cm.ExecuteNonQuery();
                        MessageBox.Show("Paciente ingresado");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
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

            }
        }
    }
}
