using clinica.clases;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Navigation;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace clinica
{
    public partial class RevisarHistorial : Page
    {
        public RevisarHistorial()
        {
            InitializeComponent();
            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_consultar_historial", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombrePaciente", txtBuscar.Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        lstPacientes.ItemsSource = dt.DefaultView;
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
        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_consultar_historial", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombrePaciente", txtBuscar.Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        lstPacientes.ItemsSource = dt.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ha ocurrido un error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            lstPacientes.SelectedItem = button.DataContext;
        }
        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder buffer = new StringBuilder();
            #region encabezados
            buffer.Append("FECHA");
            buffer.Append(",");
            buffer.Append("NOMBRE PACIENTE");
            buffer.Append(",");
            buffer.Append("DUI");
            buffer.Append(",");
            buffer.Append("EDAD");
            buffer.Append(",");
            buffer.Append("MOTIVO CONSULTA");
            buffer.Append(",");
            buffer.Append("DIAGNOSTICO");
            buffer.Append(",");
            buffer.Append("PLAN TERAPEUTICO");
            buffer.Append(",");
            buffer.Append("HISTORIA CLINICA");
            buffer.Append(",");
            buffer.Append("ANTECEDENTES PERSONALES");
            buffer.Append(",");
            buffer.Append("ANTECEDENTES FAMILIARES");
            buffer.Append(",");
            buffer.Append("FUR");
            buffer.Append(",");
            buffer.Append("G");
            buffer.Append(",");
            buffer.Append("PV");
            buffer.Append(",");
            buffer.Append("PC");
            buffer.Append(",");
            buffer.Append("PP");
            buffer.Append(",");
            buffer.Append("AB");
            buffer.Append(",");
            buffer.Append("NV");
            buffer.Append(",");
            buffer.Append("P/A");
            buffer.Append(",");
            buffer.Append("TALLA");
            buffer.Append(",");
            buffer.Append("PESO");
            buffer.Append(",");
            buffer.Append("IMC");
            buffer.Append(",");
            buffer.Append("HALLAZGOS EXAMEN FISICOS");
            buffer.Append(",");
            buffer.Append("FPP");
            buffer.Append(",");
            buffer.Append("EG");
            buffer.Append(",");
            buffer.Append("AU");
            buffer.Append(",");
            buffer.Append("PRESENTACION");
            buffer.Append(",");
            buffer.Append("FCF");
            buffer.Append(",");
            buffer.Append("MF");
            buffer.Append(",");
            buffer.Append("RH");
            buffer.Append(",");
            buffer.Append("HB");
            buffer.Append(",");
            buffer.Append("VIH");
            buffer.Append(",");
            buffer.Append("VDLR");
            buffer.Append(",");
            buffer.Append("EGO");
            buffer.Append(",");
            buffer.Append("URO");
            buffer.Append(",");
            buffer.Append("GLC");
            buffer.Append(",");
            buffer.Append("O'S");
            buffer.Append(",");
            buffer.Append("USG");
            buffer.Append("\n");
            #endregion
            String result = buffer.ToString();
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                string fileName = "";
                if (saveFileDialog.ShowDialog() == true)
                {
                    fileName = saveFileDialog.FileName;
                    StreamWriter sw = new StreamWriter(fileName);
                    sw.WriteLine(result);
                    sw.Close();

                }

                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            int conteo = 0;
            int conteoPerfil = 0;
            List<consulta> datosSeleccionados = new List<consulta>();
            List<consulta> datosPerfil = new List<consulta>();
            foreach (DataRowView dataRowView in lstPacientes.SelectedItems)
            {
                conteo++;
                consulta consulta = new consulta();
                consulta.Id = Convert.ToInt32(dataRowView.Row.ItemArray[0]);
                consulta.Nombre = Convert.ToString(dataRowView.Row.ItemArray[1]);
                consulta.FechaNacimiento = Convert.ToDateTime(dataRowView.Row.ItemArray[2]);
                consulta.IdConsulta = Convert.ToInt32(dataRowView.Row.ItemArray[4]);
                consulta.TipoConsulta = Convert.ToBoolean(dataRowView.Row.ItemArray[5]);
                consulta.FechaConsulta = Convert.ToDateTime(dataRowView.Row.ItemArray[6]);
                consulta.Personales = Convert.ToString(dataRowView.Row.ItemArray[7]);
                consulta.Familiares = Convert.ToString(dataRowView.Row.ItemArray[8]);
                consulta.Fur = Convert.ToDateTime(dataRowView.Row.ItemArray[9]);
                consulta.G = Convert.ToString(dataRowView.Row.ItemArray[10]);
                consulta.Pv = Convert.ToString(dataRowView.Row.ItemArray[11]);
                consulta.Pc = Convert.ToString(dataRowView.Row.ItemArray[12]);
                consulta.Pp = Convert.ToString(dataRowView.Row.ItemArray[13]);
                consulta.Ab = Convert.ToString(dataRowView.Row.ItemArray[14]);
                consulta.Nv = Convert.ToString(dataRowView.Row.ItemArray[15]);
                consulta.Pa = Convert.ToString(dataRowView.Row.ItemArray[16]);
                consulta.Talla = Convert.ToSingle(dataRowView.Row.ItemArray[17]);
                consulta.Peso = Convert.ToSingle(dataRowView.Row.ItemArray[18]);
                consulta.HallazgosFisicos = Convert.ToString(dataRowView.Row.ItemArray[19]);
                consulta.Fpp = Convert.ToDateTime(dataRowView.Row.ItemArray[20]);
                consulta.Eg = Convert.ToString(dataRowView.Row.ItemArray[21]);
                consulta.Au = Convert.ToString(dataRowView.Row.ItemArray[22]);
                consulta.Present = Convert.ToString(dataRowView.Row.ItemArray[23]);
                consulta.Fcf = Convert.ToString(dataRowView.Row.ItemArray[24]);
                consulta.Mf = Convert.ToString(dataRowView.Row.ItemArray[25]);
                consulta.Rh = Convert.ToString(string.IsNullOrEmpty(dataRowView.Row.ItemArray[26].ToString()) ? "-" : dataRowView.Row.ItemArray[26]);
                consulta.Hb = Convert.ToString(dataRowView.Row.ItemArray[27]);
                consulta.Vih = Convert.ToString(dataRowView.Row.ItemArray[28]);
                consulta.Vdlr = Convert.ToString(dataRowView.Row.ItemArray[29]);
                consulta.Ego = Convert.ToString(dataRowView.Row.ItemArray[30]);
                consulta.Uro = Convert.ToString(dataRowView.Row.ItemArray[31]);
                consulta.Glc= Convert.ToString(dataRowView.Row.ItemArray[32]);
                consulta.Os= Convert.ToString(dataRowView.Row.ItemArray[33]);
                consulta.Usg= Convert.ToString(dataRowView.Row.ItemArray[34]);
                consulta.MotivoConsulta= Convert.ToString(dataRowView.Row.ItemArray[35]);
                consulta.HistoriaClinica= Convert.ToString(dataRowView.Row.ItemArray[36]);
                consulta.Diagnostico= Convert.ToString(dataRowView.Row.ItemArray[37]);
                consulta.PlanTerapeutico= Convert.ToString(dataRowView.Row.ItemArray[38]);
                datosSeleccionados.Add(consulta);

                //agregar conteo de perfil
                if(Convert.ToString(dataRowView.Row.ItemArray[26]) != "-")
                {
                    datosPerfil.Add(consulta);
                    conteoPerfil++;
                }
            }
            if (conteo > 0)
            {
                popPDF.IsOpen = true;
                consulta ultimoRegistro = datosSeleccionados.Last(); //estan ordenados de manera descendente por fecha
                lblNombre.Content = ultimoRegistro.Nombre;
                lblEdad.Content = ultimoRegistro.Edad;
                lblImpFPP.Content = ultimoRegistro.Fpp;
                lblFUR.Content = ultimoRegistro.Fur;
                lblImpG.Content = ultimoRegistro.G;
                lblImpPV.Content = ultimoRegistro.Pv;
                lblImpPC.Content = ultimoRegistro.Pc;
                lblImpPP.Content = ultimoRegistro.Pp;
                lblImpAB.Content = ultimoRegistro.Ab;
                lblImpNV.Content = ultimoRegistro.Nv;
                lblImpFamiliares.Text = ultimoRegistro.Familiares;
                lblImpPersonales.Text = ultimoRegistro.Personales;
                lblImpPeso.Content = ultimoRegistro.Peso;
                lblImpTalla.Content = ultimoRegistro.Talla;
                lblImpImc.Content = ultimoRegistro.Imc;

                datosSeleccionados.Reverse();
                dgExamenFisico.ItemsSource = datosSeleccionados;


                dgPrimerPerfil.Items.Clear();
                dgSegundoPerfil.Items.Clear();


                if (conteoPerfil > 0)
                {
                    consulta primerPerfil = datosPerfil.Last();
                    dgPrimerPerfil.Items.Add(primerPerfil);

                    if (conteoPerfil > 1)
                    {
                        consulta segundoPerfil = datosPerfil.First();
                        dgSegundoPerfil.Items.Add(segundoPerfil);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay items seleccionados", "Atencion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void Print_WPF_Preview(FrameworkElement wpf_Element, string nomre)

        {

            //------------< WPF_Print_current_Window >------------

            //--< create xps document >--

            XpsDocument doc = new XpsDocument(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\" + nomre, FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator preview_Document = writer.CreateVisualsCollator();

            preview_Document.BeginBatchWrite();

            preview_Document.Write(wpf_Element);  //*this or wpf xaml control

            preview_Document.EndBatchWrite();

            //--</ create xps document >--



            //var doc2 = new XpsDocument("Druckausgabe.xps", FileAccess.Read);



            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();



            var window = new Window();

            window.Content = new DocumentViewer { Document = preview };

            window.ShowDialog();



            doc.Close();

            //------------</ WPF_Print_current_Window >------------





        }
        private void buttonImprimir_Click(object sender, RoutedEventArgs e)
        {

            string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss")+"_";

            string nombre = nombreArchivo + Guid.NewGuid().ToString("n").Substring(0, 8) + ".xps";
            Print_WPF_Preview(areaImpresion, nombre);
        }
        private void Button_GotFocus_1(object sender, RoutedEventArgs e)
        {
            scroll.ScrollToHome();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)lstPacientes.SelectedItem;
            consulta consulta = new consulta();
            consulta.Id = Convert.ToInt32(dataRowView.Row.ItemArray[0]);
            consulta.Nombre = Convert.ToString(dataRowView.Row.ItemArray[1]);
            consulta.FechaNacimiento = Convert.ToDateTime(dataRowView.Row.ItemArray[2]);
            consulta.IdConsulta = Convert.ToInt32(dataRowView.Row.ItemArray[4]);
            consulta.TipoConsulta = Convert.ToBoolean(dataRowView.Row.ItemArray[5]);
            consulta.FechaConsulta = Convert.ToDateTime(dataRowView.Row.ItemArray[6]);
            consulta.Personales = Convert.ToString(dataRowView.Row.ItemArray[7]);
            consulta.Familiares = Convert.ToString(dataRowView.Row.ItemArray[8]);
            consulta.Fur = Convert.ToDateTime(dataRowView.Row.ItemArray[9]);
            consulta.G = Convert.ToString(dataRowView.Row.ItemArray[10]);
            consulta.Pv = Convert.ToString(dataRowView.Row.ItemArray[11]);
            consulta.Pc = Convert.ToString(dataRowView.Row.ItemArray[12]);
            consulta.Pp = Convert.ToString(dataRowView.Row.ItemArray[13]);
            consulta.Ab = Convert.ToString(dataRowView.Row.ItemArray[14]);
            consulta.Nv = Convert.ToString(dataRowView.Row.ItemArray[15]);
            consulta.Pa = Convert.ToString(dataRowView.Row.ItemArray[16]);
            consulta.Talla = Convert.ToSingle(dataRowView.Row.ItemArray[17]);
            consulta.Peso = Convert.ToSingle(dataRowView.Row.ItemArray[18]);
            consulta.HallazgosFisicos = Convert.ToString(dataRowView.Row.ItemArray[19]);
            consulta.Fpp = Convert.ToDateTime(dataRowView.Row.ItemArray[20]);
            consulta.Eg = Convert.ToString(dataRowView.Row.ItemArray[21]);
            consulta.Au = Convert.ToString(dataRowView.Row.ItemArray[22]);
            consulta.Present = Convert.ToString(dataRowView.Row.ItemArray[23]);
            consulta.Fcf = Convert.ToString(dataRowView.Row.ItemArray[24]);
            consulta.Mf = Convert.ToString(dataRowView.Row.ItemArray[25]);
            consulta.Rh = Convert.ToString(string.IsNullOrEmpty(dataRowView.Row.ItemArray[26].ToString()) ? "-" : dataRowView.Row.ItemArray[26]);
            consulta.Hb = Convert.ToString(dataRowView.Row.ItemArray[27]);
            consulta.Vih = Convert.ToString(dataRowView.Row.ItemArray[28]);
            consulta.Vdlr = Convert.ToString(dataRowView.Row.ItemArray[29]);
            consulta.Ego = Convert.ToString(dataRowView.Row.ItemArray[30]);
            consulta.Uro = Convert.ToString(dataRowView.Row.ItemArray[31]);
            consulta.Glc= Convert.ToString(dataRowView.Row.ItemArray[32]);
            consulta.Os= Convert.ToString(dataRowView.Row.ItemArray[33]);
            consulta.Usg= Convert.ToString(dataRowView.Row.ItemArray[34]);
            consulta.MotivoConsulta= Convert.ToString(dataRowView.Row.ItemArray[35]);
            consulta.HistoriaClinica= Convert.ToString(dataRowView.Row.ItemArray[36]);
            consulta.Diagnostico= Convert.ToString(dataRowView.Row.ItemArray[37]);
            consulta.PlanTerapeutico= Convert.ToString(dataRowView.Row.ItemArray[38]);
            
            NavigationService.Navigate(new NuevaConsulta(consulta));
        }
        private void btnPDF2_Click(object sender, RoutedEventArgs e)
        {
            int conteo = 0;
            consulta consulta = new consulta();
            foreach (DataRowView dataRowView in lstPacientes.SelectedItems)
            {
                conteo++;
                consulta.Id = Convert.ToInt32(dataRowView.Row.ItemArray[0]);
                consulta.Nombre = Convert.ToString(dataRowView.Row.ItemArray[1]);
                consulta.FechaNacimiento = Convert.ToDateTime(dataRowView.Row.ItemArray[2]);
                consulta.IdConsulta = Convert.ToInt32(dataRowView.Row.ItemArray[4]);
                consulta.TipoConsulta = Convert.ToBoolean(dataRowView.Row.ItemArray[5]);
                consulta.FechaConsulta = Convert.ToDateTime(dataRowView.Row.ItemArray[6]);
                consulta.Personales = Convert.ToString(dataRowView.Row.ItemArray[7]);
                consulta.Familiares = Convert.ToString(dataRowView.Row.ItemArray[8]);
                consulta.Fur = Convert.ToDateTime(dataRowView.Row.ItemArray[9]);
                consulta.G = Convert.ToString(dataRowView.Row.ItemArray[10]);
                consulta.Pv = Convert.ToString(dataRowView.Row.ItemArray[11]);
                consulta.Pc = Convert.ToString(dataRowView.Row.ItemArray[12]);
                consulta.Pp = Convert.ToString(dataRowView.Row.ItemArray[13]);
                consulta.Ab = Convert.ToString(dataRowView.Row.ItemArray[14]);
                consulta.Nv = Convert.ToString(dataRowView.Row.ItemArray[15]);
                consulta.Pa = Convert.ToString(dataRowView.Row.ItemArray[16]);
                consulta.Talla = Convert.ToSingle(dataRowView.Row.ItemArray[17]);
                consulta.Peso = Convert.ToSingle(dataRowView.Row.ItemArray[18]);
                consulta.HallazgosFisicos = Convert.ToString(dataRowView.Row.ItemArray[19]);
                consulta.Fpp = Convert.ToDateTime(dataRowView.Row.ItemArray[20]);
                consulta.Eg = Convert.ToString(dataRowView.Row.ItemArray[21]);
                consulta.Au = Convert.ToString(dataRowView.Row.ItemArray[22]);
                consulta.Present = Convert.ToString(dataRowView.Row.ItemArray[23]);
                consulta.Fcf = Convert.ToString(dataRowView.Row.ItemArray[24]);
                consulta.Mf = Convert.ToString(dataRowView.Row.ItemArray[25]);
                consulta.Rh = Convert.ToString(string.IsNullOrEmpty(dataRowView.Row.ItemArray[26].ToString()) ? "-" : dataRowView.Row.ItemArray[26]);
                consulta.Hb = Convert.ToString(dataRowView.Row.ItemArray[27]);
                consulta.Vih = Convert.ToString(dataRowView.Row.ItemArray[28]);
                consulta.Vdlr = Convert.ToString(dataRowView.Row.ItemArray[29]);
                consulta.Ego = Convert.ToString(dataRowView.Row.ItemArray[30]);
                consulta.Uro = Convert.ToString(dataRowView.Row.ItemArray[31]);
                consulta.Glc = Convert.ToString(dataRowView.Row.ItemArray[32]);
                consulta.Os = Convert.ToString(dataRowView.Row.ItemArray[33]);
                consulta.Usg = Convert.ToString(dataRowView.Row.ItemArray[34]);
                consulta.MotivoConsulta = Convert.ToString(dataRowView.Row.ItemArray[35]);
                consulta.HistoriaClinica = Convert.ToString(dataRowView.Row.ItemArray[36]);
                consulta.Diagnostico = Convert.ToString(dataRowView.Row.ItemArray[37]);
                consulta.PlanTerapeutico = Convert.ToString(dataRowView.Row.ItemArray[38]);

                if (conteo == 1)
                {
                    break;
                }
            }

            if (conteo == 0)
            {
                MessageBox.Show("No ha seleccionado ningun registro");
            }
            else
            {
                popPDF2.IsOpen = true;
                lblNombre2.Content = consulta.Nombre;
                lblEdad2.Content = consulta.Edad;
                lblFUR2.Content = consulta.Fur;
                lblImpG2.Content = consulta.G;
                lblImpPV2.Content = consulta.Pv;
                lblImpPC2.Content = consulta.Pc;
                lblImpPP2.Content = consulta.Pp;
                lblImpAB2.Content = consulta.Ab;
                lblImpNV2.Content = consulta.Nv;
                lblImpPeso2.Content = consulta.Peso;
                lblImpTalla2.Content = consulta.Talla;
                lblImpPA2.Content = consulta.Pa;
                txtFamiliares2.Text = consulta.Familiares;
                txtPersonales2.Text = consulta.Personales;
                txtHallazgos2.Text = consulta.HallazgosFisicos;
                txtDiagnostico2.Text = consulta.Diagnostico;
                txtPlan2.Text = consulta.PlanTerapeutico;
                lblImpImc2.Content = consulta.Imc;

            }
        }
        private void btnImprimir2_Click(object sender, RoutedEventArgs e)
        {

            string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + "_";

            string nombre = nombreArchivo + Guid.NewGuid().ToString("n").Substring(0, 8) + ".xps";
            Print_WPF_Preview(areaImpresion2, nombre);
        }
        private void btnImprimir2_GotFocus(object sender, RoutedEventArgs e)
        {
            scroll2.ScrollToHome();
        }
    }
}
