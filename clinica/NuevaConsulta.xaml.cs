using clinica.clases;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace clinica
{
    public partial class NuevaConsulta : Page
    {
        int idConsulta = 0;

        public NuevaConsulta(consulta consulta)
        {
            InitializeComponent();
            //para estandarizar formatos de fecha
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            //
            idConsulta = consulta.IdConsulta;
            cmbMF.Items.Add("Si");
            cmbMF.Items.Add("No");

            //cargar datos de consulta en caso de que sea una actualización 
            if (idConsulta != 0)
            {
                lblId.Content = consulta.Id;
                lblNombre.Content = consulta.Nombre;
                txtPersonales.AppendText(consulta.Personales);
                txtFamiliares.AppendText(consulta.Familiares);
                dtpFUR.SelectedDate = consulta.Fur;
                txtG.Text = consulta.G;
                txtPV.Text = consulta.Pv;
                txtPC.Text = consulta.Pc;
                txtPP.Text = consulta.Pp;
                txtAB.Text = consulta.Ab;
                txtNV.Text = consulta.Nv;
                txtPA.Text = consulta.Pa;
                txtTalla.Text = consulta.Talla.ToString();
                txtPeso.Text = consulta.Peso.ToString();
                txtResultadosFisicos.AppendText(consulta.HallazgosFisicos);
                txtFPP.Text = consulta.Fpp.ToString();
                txtEG.Text = consulta.Eg.ToString();
                txtAU.Text = consulta.Au;
                txtPresent.Text = consulta.Present;
                txtFCF.Text = consulta.Fcf;
                cmbMF.SelectedItem = consulta.Mf;
                txtRH.Text = consulta.Rh;
                txtHB.Text = consulta.Hb;
                txtVIH.Text = consulta.Vih;
                txtVDLR.Text = consulta.Vdlr;
                txtEGO.Text = consulta.Ego;
                txtURO.Text = consulta.Uro;
                txtGluc.Text = consulta.Glc;
                txtOS.Text = consulta.Os;
                txtUSG.Text = consulta.Usg;
                txtMotivo.AppendText(consulta.MotivoConsulta);
                txtHistoriaClinica.AppendText(consulta.HistoriaClinica);
                txtDiagnostico.AppendText(consulta.Diagnostico);
                txtPlanTerapeutico.AppendText(consulta.PlanTerapeutico);
                chkTipoConsulta.IsChecked = consulta.TipoConsulta;

            }

        }
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            popUpPaciente.IsOpen = true;
            lstPacientes.Items.Clear();
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

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ultima_consulta", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idPaciente", pacienteSeleccionado.Id);
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                txtPersonales.AppendText(dt.Rows[0][0].ToString());
                                txtFamiliares.AppendText(dt.Rows[0][1].ToString());
                                txtblMotivo.Text = dt.Rows[0][2].ToString();
                                txtblDiagnostico.Text = dt.Rows[0][3].ToString();
                                txtblPlan.Text = dt.Rows[0][4].ToString();
                            }
                        }
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
            //formateo de datos a ingresar
            string personales = new TextRange(txtPersonales.Document.ContentStart, txtPersonales.Document.ContentEnd).Text;
            string familiares = new TextRange(txtFamiliares.Document.ContentStart, txtFamiliares.Document.ContentEnd).Text;
            string fur = Convert.ToDateTime(dtpFUR.SelectedDate).ToString();
            string g = Convert.ToString(txtG.Text);
            string pv = Convert.ToString(txtPV.Text);
            string pc = Convert.ToString(txtPC.Text);
            string pp = Convert.ToString(txtPP.Text);
            string ab = Convert.ToString(txtAB.Text);
            string nv = Convert.ToString(txtNV.Text);
            string pa = Convert.ToString(txtPA.Text);
            float talla = Convert.ToSingle(txtTalla.Text == "" ? "0" : txtTalla.Text);
            float peso = Convert.ToSingle(txtPeso.Text == "" ? "0" : txtPeso.Text);
            string hallazgosFisicos = new TextRange(txtResultadosFisicos.Document.ContentStart, txtResultadosFisicos.Document.ContentEnd).Text;
            string fpp = Convert.ToString(txtFPP.Text);
            string eg = Convert.ToString(txtEG.Text);
            string au = Convert.ToString(txtAU.Text);
            string present = Convert.ToString(txtPresent.Text);
            string fcf = Convert.ToString(txtFCF.Text);
            string mf = cmbMF.SelectedIndex != -1 ? cmbMF.SelectedValue.ToString() : "";
            string rh = Convert.ToString(txtRH.Text == "" ? "-" : txtRH.Text);
            string hb = Convert.ToString(txtHB.Text);
            string vih = Convert.ToString(txtVIH.Text);
            string vdlr = Convert.ToString(txtVDLR.Text);
            string ego = Convert.ToString(txtEGO.Text);
            string uro = Convert.ToString(txtURO.Text);
            string glc = Convert.ToString(txtGluc.Text);
            string os = Convert.ToString(txtOS.Text);
            string usg = Convert.ToString(txtUSG.Text);
            string motivo = new TextRange(txtMotivo.Document.ContentStart, txtMotivo.Document.ContentEnd).Text;
            string historiaClinica = new TextRange(txtHistoriaClinica.Document.ContentStart, txtHistoriaClinica.Document.ContentEnd).Text;
            string diagnostico = new TextRange(txtDiagnostico.Document.ContentStart, txtDiagnostico.Document.ContentEnd).Text;
            string plan = new TextRange(txtPlanTerapeutico.Document.ContentStart, txtPlanTerapeutico.Document.ContentEnd).Text;
            bool tipo = (bool)chkTipoConsulta.IsChecked ? true : false;

            //si el id de la consulta es cero es una nueva consulta si no es una actualizacion (este parametro se recibe en el costructor)
            if (idConsulta == 0)
            {
                //Cuando el registro es nuevo se exige haber seleccionado un paciente 
                if (lblId.Content.ToString() != "----")
                {
                    //numero de registro ingresado
                    int registroId = 0;

                    //ingreso de datos
                    using (SqlConnection cn = conexioSQL.Clinica())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_insertar_consulta", cn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@tipoConsulta", tipo);
                                cmd.Parameters.AddWithValue("@idPaciente", lblId.Content);
                                cmd.Parameters.AddWithValue("@personales", personales);
                                cmd.Parameters.AddWithValue("@familiares", familiares);
                                cmd.Parameters.AddWithValue("@fur", fur);
                                cmd.Parameters.AddWithValue("@g", g);
                                cmd.Parameters.AddWithValue("@pv", pv);
                                cmd.Parameters.AddWithValue("@pc", pc);
                                cmd.Parameters.AddWithValue("@pp", pp);
                                cmd.Parameters.AddWithValue("@ab", ab);
                                cmd.Parameters.AddWithValue("@nv", nv);
                                cmd.Parameters.AddWithValue("@pa", pa);
                                cmd.Parameters.AddWithValue("@talla", talla);
                                cmd.Parameters.AddWithValue("@peso", peso);
                                cmd.Parameters.AddWithValue("@hallazgosFisicos", hallazgosFisicos);
                                cmd.Parameters.AddWithValue("@fpp", fpp);
                                cmd.Parameters.AddWithValue("@eg", eg);
                                cmd.Parameters.AddWithValue("@au", au);
                                cmd.Parameters.AddWithValue("@present", present);
                                cmd.Parameters.AddWithValue("@fcf", fcf);
                                cmd.Parameters.AddWithValue("@mf", mf);
                                cmd.Parameters.AddWithValue("@rh", rh);
                                cmd.Parameters.AddWithValue("@hb", hb);
                                cmd.Parameters.AddWithValue("@vih", vih);
                                cmd.Parameters.AddWithValue("@vdlr", vdlr);
                                cmd.Parameters.AddWithValue("@ego", ego);
                                cmd.Parameters.AddWithValue("@uro", uro);
                                cmd.Parameters.AddWithValue("@glc", glc);
                                cmd.Parameters.AddWithValue("@os", os);
                                cmd.Parameters.AddWithValue("@usg", usg);
                                cmd.Parameters.AddWithValue("@motivoConsulta", motivo);
                                cmd.Parameters.AddWithValue("@historiaClinica", historiaClinica);
                                cmd.Parameters.AddWithValue("@diagnostico", diagnostico);
                                cmd.Parameters.AddWithValue("@planTerapeutico", plan);

                                DataTable dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.Fill(dt);
                                registroId = Convert.ToInt32(dt.Rows[0][0]);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        if (Convert.ToDateTime(dtpFecha.SelectedDate).ToString() != "0001-01-01")
                        {
                            try
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_crear_aviso", cn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@idConsulta", registroId);
                                    cmd.Parameters.AddWithValue("@fechaAviso", Convert.ToDateTime(dtpFecha.SelectedDate).ToString("yyyy-MM-dd"));
                                    cmd.Parameters.AddWithValue("@textAviso", txtAviso.Text);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    MessageBox.Show("Consulta Guardada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new Index());
                }
                else
                {
                    MessageBox.Show("Seleccione un paciente");
                }
            }
            else
            {
                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_actualizar_consulta", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
                            cmd.Parameters.AddWithValue("@tipoConsulta", tipo);
                            cmd.Parameters.AddWithValue("@idPaciente", lblId.Content);
                            cmd.Parameters.AddWithValue("@personales", personales);
                            cmd.Parameters.AddWithValue("@familiares", familiares);
                            cmd.Parameters.AddWithValue("@fur", fur);
                            cmd.Parameters.AddWithValue("@g", g);
                            cmd.Parameters.AddWithValue("@pv", pv);
                            cmd.Parameters.AddWithValue("@pc", pc);
                            cmd.Parameters.AddWithValue("@pp", pp);
                            cmd.Parameters.AddWithValue("@ab", ab);
                            cmd.Parameters.AddWithValue("@nv", nv);
                            cmd.Parameters.AddWithValue("@pa", pa);
                            cmd.Parameters.AddWithValue("@talla", talla);
                            cmd.Parameters.AddWithValue("@peso", peso);
                            cmd.Parameters.AddWithValue("@hallazgosFisicos", hallazgosFisicos);
                            cmd.Parameters.AddWithValue("@fpp", fpp);
                            cmd.Parameters.AddWithValue("@eg", eg);
                            cmd.Parameters.AddWithValue("@au", au);
                            cmd.Parameters.AddWithValue("@present", present);
                            cmd.Parameters.AddWithValue("@fcf", fcf);
                            cmd.Parameters.AddWithValue("@mf", mf);
                            cmd.Parameters.AddWithValue("@rh", rh);
                            cmd.Parameters.AddWithValue("@hb", hb);
                            cmd.Parameters.AddWithValue("@vih", vih);
                            cmd.Parameters.AddWithValue("@vdlr", vdlr);
                            cmd.Parameters.AddWithValue("@ego", ego);
                            cmd.Parameters.AddWithValue("@uro", uro);
                            cmd.Parameters.AddWithValue("@glc", glc);
                            cmd.Parameters.AddWithValue("@os", os);
                            cmd.Parameters.AddWithValue("@usg", usg);
                            cmd.Parameters.AddWithValue("@motivoConsulta", motivo);
                            cmd.Parameters.AddWithValue("@historiaClinica", historiaClinica);
                            cmd.Parameters.AddWithValue("@diagnostico", diagnostico);
                            cmd.Parameters.AddWithValue("@planTerapeutico", plan);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                MessageBox.Show("Consulta Actualizada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new Index());
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
        private void dtpFUR_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int dia = (Convert.ToDateTime(dtpFUR.SelectedDate).AddDays(7)).Day;
            int mes = (Convert.ToDateTime(dtpFUR.SelectedDate).AddMonths(9)).Month;
            int anio = (Convert.ToDateTime(dtpFUR.SelectedDate).AddMonths(9)).Year;

            txtFPP.Text = anio.ToString("0000")+ "-" + mes.ToString("00") + "-" + dia.ToString("00");

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            grbObste.IsEnabled = true;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            grbObste.IsEnabled = false;
        }
    }
}
