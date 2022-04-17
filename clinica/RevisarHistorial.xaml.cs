using clinica.clases;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            string sql = "SELECT TOP 100 idConsulta, fechaConsulta, c.nombrePaciente, c.documentoPaciente, motivoConsulta, diagnosticoConsulta, " +
    "planTerapeuticoConsulta, examenesFisicosConsulta, b.personalesAntecedentes, b.familiaresAntecedentes, " +
    "b.gAntecedentes, b.pvAntecedentes, b.pcAntecendentes, b.ppAntecendentes, b.abAntecendentes, b.vAntecendentes, " +
    "b.pAntecendentes, b.furAntecedentes, b.fppAntecedentes, b.tallaAntecedentes, c.fechaNacimientoPaciente, b.hallazgosAntecedentes, b.egAntecedentes, b.auAntecedentes," +
    "b.presentAntecedentes,b.fcfAntecedentes, b.mfAntecedentes, b.rhAntecedentes,  b.hbAntecedentes,  b.vdlrAntecedentes," +
    "b.vihAntecedentes, b.egoAntecedentes, b.uroAntecedentes, b.glcAntecedentes, b.osAntecedentes, b.usgAntecedentes, " +
    "b.pesoAntecedentes	 FROM consultas a " +
    "LEFT JOIN antecedentes b ON a.idConsulta=b.idConsultas " +
    "LEFT JOIN pacientes c ON a.idPaciente=c.idPaciente ORDER BY idConsulta DESC";
            using (SqlConnection cn = conexioSQL.Clinica())
            {
                try
                {
                    SqlCommand cm = new SqlCommand(sql, cn);
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        lstPacientes.Items.Add(
                            new resumen(
                                Convert.ToInt32(dr["IdConsulta"]),
                                Convert.ToDateTime(dr["fechaConsulta"]).ToString("yyyy-MM-dd"),
                                dr["nombrePaciente"].ToString(),
                                dr["documentoPaciente"].ToString(),
                                dr["motivoConsulta"].ToString(),
                                dr["diagnosticoConsulta"].ToString(),
                                dr["planTerapeuticoConsulta"].ToString(),
                                dr["examenesFisicosConsulta"].ToString(),
                                dr["personalesAntecedentes"].ToString(),
                                dr["familiaresAntecedentes"].ToString(),
                                dr["gAntecedentes"].ToString(),
                                dr["pvAntecedentes"].ToString(),
                                dr["pcAntecendentes"].ToString(),
                                dr["ppAntecendentes"].ToString(),
                                dr["abAntecendentes"].ToString(),
                                dr["vAntecendentes"].ToString(),
                                dr["pAntecendentes"].ToString(),
                                dr["furAntecedentes"].ToString(),
                                dr["fppAntecedentes"].ToString(),
                                Convert.ToDouble(dr["tallaAntecedentes"] is DBNull ? 0 : dr["tallaAntecedentes"]),
                                Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"]),
                                Convert.ToDateTime(dr["fechaNacimientoPaciente"]),
                                    dr["hallazgosAntecedentes"].ToString(),
                                    dr["egAntecedentes"].ToString(),
                                    dr["auAntecedentes"].ToString(),
                                    dr["presentAntecedentes"].ToString(),
                                    dr["fcfAntecedentes"].ToString(),
                                    dr["mfAntecedentes"].ToString(),
                                    dr["rhAntecedentes"].ToString(),
                                    dr["hbAntecedentes"].ToString(),
                                    dr["vihAntecedentes"].ToString(),
                                    dr["vdlrAntecedentes"].ToString(),
                                    dr["egoAntecedentes"].ToString(),
                                    dr["uroAntecedentes"].ToString(),
                                    dr["glcAntecedentes"].ToString(),
                                    dr["osAntecedentes"].ToString(),
                                    dr["usgAntecedentes"].ToString(),
                                    false
                                ));
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

        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            lstPacientes.Items.Clear();
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                string sql = "SELECT TOP 100 idConsulta, fechaConsulta, c.nombrePaciente, c.documentoPaciente, motivoConsulta, diagnosticoConsulta, " +
                    "planTerapeuticoConsulta, examenesFisicosConsulta, b.personalesAntecedentes, b.familiaresAntecedentes, " +
                    "b.gAntecedentes, b.pvAntecedentes, b.pcAntecendentes, b.ppAntecendentes, b.abAntecendentes, b.vAntecendentes, " +
                    "b.pAntecendentes, b.furAntecedentes, b.fppAntecedentes, b.tallaAntecedentes, c.fechaNacimientoPaciente, b.hallazgosAntecedentes, b.egAntecedentes, b.auAntecedentes," +
                    "b.presentAntecedentes,b.fcfAntecedentes, b.mfAntecedentes, b.rhAntecedentes,  b.hbAntecedentes,  b.vdlrAntecedentes," +
                    "b.vihAntecedentes, b.egoAntecedentes, b.uroAntecedentes, b.glcAntecedentes, b.osAntecedentes, b.usgAntecedentes, " +
                    "b.pesoAntecedentes	 FROM consultas a " +
                    "LEFT JOIN antecedentes b ON a.idConsulta=b.idConsultas " +
                    "LEFT JOIN pacientes c ON a.idPaciente=c.idPaciente ORDER BY idConsulta DESC";

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            lstPacientes.Items.Add(
                                new resumen(
                                    Convert.ToInt32(dr["IdConsulta"]),
                                    Convert.ToDateTime(dr["fechaConsulta"]).ToString("yyyy-MM-dd"),
                                    dr["nombrePaciente"].ToString(),
                                    dr["documentoPaciente"].ToString(),
                                    dr["motivoConsulta"].ToString(),
                                    dr["diagnosticoConsulta"].ToString(),
                                    dr["planTerapeuticoConsulta"].ToString(),
                                    dr["examenesFisicosConsulta"].ToString(),
                                    dr["personalesAntecedentes"].ToString(),
                                    dr["familiaresAntecedentes"].ToString(),
                                    dr["gAntecedentes"].ToString(),
                                    dr["pvAntecedentes"].ToString(),
                                    dr["pcAntecendentes"].ToString(),
                                    dr["ppAntecendentes"].ToString(),
                                    dr["abAntecendentes"].ToString(),
                                    dr["vAntecendentes"].ToString(),
                                    dr["pAntecendentes"].ToString(),
                                    dr["furAntecedentes"].ToString(),
                                    dr["fppAntecedentes"].ToString(),
                                    Convert.ToDouble(dr["tallaAntecedentes"] is DBNull ? 0 : dr["tallaAntecedentes"]),
                                    Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"]),
                                    Convert.ToDateTime(dr["fechaNacimientoPaciente"]),
                                    dr["hallazgosAntecedentes"].ToString(),
                                    dr["egAntecedentes"].ToString(),
                                    dr["auAntecedentes"].ToString(),
                                    dr["presentAntecedentes"].ToString(),
                                    dr["fcfAntecedentes"].ToString(),
                                    dr["mfAntecedentes"].ToString(),
                                    dr["rhAntecedentes"].ToString(),
                                    dr["hbAntecedentes"].ToString(),
                                    dr["vihAntecedentes"].ToString(),
                                    dr["vdlrAntecedentes"].ToString(),
                                    dr["egoAntecedentes"].ToString(),
                                    dr["uroAntecedentes"].ToString(),
                                    dr["glcAntecedentes"].ToString(),
                                    dr["osAntecedentes"].ToString(),
                                    dr["usgAntecedentes"].ToString(),
                                    false
                                    ));
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
                string sql = "SELECT TOP 100 idConsulta, fechaConsulta, c.nombrePaciente, c.documentoPaciente, motivoConsulta, diagnosticoConsulta, " +
                    "planTerapeuticoConsulta, examenesFisicosConsulta, b.personalesAntecedentes, b.familiaresAntecedentes, " +
                    "b.gAntecedentes, b.pvAntecedentes, b.pcAntecendentes, b.ppAntecendentes, b.abAntecendentes, b.vAntecendentes, " +
                    "b.pAntecendentes, b.furAntecedentes, b.fppAntecedentes, b.tallaAntecedentes, " +
                    "b.pesoAntecedentes, c.fechaNacimientoPaciente, b.hallazgosAntecedentes, b.egAntecedentes, b.auAntecedentes," +
                    "b.presentAntecedentes,b.fcfAntecedentes, b.mfAntecedentes, b.rhAntecedentes,  b.hbAntecedentes,  b.vdlrAntecedentes," +
                    "b.vihAntecedentes, b.egoAntecedentes, b.uroAntecedentes, b.glcAntecedentes, b.osAntecedentes, b.usgAntecedentes FROM consultas a " +
                    "LEFT JOIN antecedentes b ON a.idConsulta=b.idConsultas " +
                    "LEFT JOIN pacientes c ON a.idPaciente=c.idPaciente " +
                    "WHERE c.nombrePaciente LIKE '%" + txtBuscar.Text + "%' ORDER BY idConsulta DESC";

                using (SqlConnection cn = conexioSQL.Clinica())
                {
                    try
                    {
                        SqlCommand cm = new SqlCommand(sql, cn);
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            lstPacientes.Items.Add(
                                new resumen(
                                    Convert.ToInt32(dr["IdConsulta"]),
                                    Convert.ToDateTime(dr["fechaConsulta"]).ToString("yyyy-MM-dd"),
                                    dr["nombrePaciente"].ToString(),
                                    dr["documentoPaciente"].ToString(),
                                    dr["motivoConsulta"].ToString(),
                                    dr["diagnosticoConsulta"].ToString(),
                                    dr["planTerapeuticoConsulta"].ToString(),
                                    dr["examenesFisicosConsulta"].ToString(),
                                    dr["personalesAntecedentes"].ToString(),
                                    dr["familiaresAntecedentes"].ToString(),
                                    dr["gAntecedentes"].ToString(),
                                    dr["pvAntecedentes"].ToString(),
                                    dr["pcAntecendentes"].ToString(),
                                    dr["ppAntecendentes"].ToString(),
                                    dr["abAntecendentes"].ToString(),
                                    dr["vAntecendentes"].ToString(),
                                    dr["pAntecendentes"].ToString(),
                                    dr["furAntecedentes"].ToString(),
                                    dr["fppAntecedentes"].ToString(),
                                    Convert.ToDouble(dr["tallaAntecedentes"] is DBNull ? 0 : dr["tallaAntecedentes"]),
                                    Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"]),
                                    Convert.ToDateTime(dr["fechaNacimientoPaciente"]),
                                    dr["hallazgosAntecedentes"].ToString(),
                                    dr["egAntecedentes"].ToString(),
                                    dr["auAntecedentes"].ToString(),
                                    dr["presentAntecedentes"].ToString(),
                                    dr["fcfAntecedentes"].ToString(),
                                    dr["mfAntecedentes"].ToString(),
                                    dr["rhAntecedentes"].ToString(),
                                    dr["hbAntecedentes"].ToString(),
                                    dr["vihAntecedentes"].ToString(),
                                    dr["vdlrAntecedentes"].ToString(),
                                    dr["egoAntecedentes"].ToString(),
                                    dr["uroAntecedentes"].ToString(),
                                    dr["glcAntecedentes"].ToString(),
                                    dr["osAntecedentes"].ToString(),
                                    dr["usgAntecedentes"].ToString(),
                                    false
                                    ));
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

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            lstPacientes.SelectedItem = button.DataContext;
        }

        private void btnAntedentes_Click(object sender, RoutedEventArgs e)
        {
            popUpAntedentes.IsOpen = true;
            resumen consulta = (resumen)lstPacientes.SelectedItem;
            lblFamiliares.Text = consulta.Familiares;
            lblPersonales.Text = consulta.Personales;
            lblg.Text = consulta.G;
            lblpv.Text = consulta.Pv;
            lblpc.Text = consulta.Pc;
            lblpp.Text = consulta.Pp;
            lblab.Text = consulta.Ab;
            lblv.Text = consulta.V;
        }

        private void btnFisicos_Click(object sender, RoutedEventArgs e)
        {
            popUpFisicos.IsOpen = true;
            resumen consulta = (resumen)lstPacientes.SelectedItem;
            lblFamiliares.Text = consulta.Familiares;
            lblPersonales.Text = consulta.Personales;
            lblp.Text = consulta.P;
            lblHallazgos.Text = consulta.Hallazgos;
            lblfur.Text = consulta.Fur;
            lblimc.Text = consulta.Imc;
            lbltalla.Text = consulta.Talla.ToString();
            lblpeso.Text = consulta.Peso.ToString();
        }

        private void btnObstetricos_Click(object sender, RoutedEventArgs e)
        {
            popObstetricos.IsOpen = true;
            resumen consulta = (resumen)lstPacientes.SelectedItem;
            lblFPP.Text = consulta.Fpp;
            lblEG.Text = consulta.Eg;
            lblAU.Text = consulta.Au;
            lblPresent.Text = consulta.Presentacion;
            lblFCF.Text = consulta.Fcf;
            lblMF.Text = consulta.Mf;
            lblRH.Text = consulta.Rh;
            lblHB.Text = consulta.Hb;
            lblVIH.Text = consulta.Vih;
            lblVDLR.Text = consulta.Vdlr;
            lblEGO.Text = consulta.Ego;
            lblURO.Text = consulta.Uro;
            lblGLC.Text = consulta.Glc;
            lblOS.Text = consulta.Os;
            lblUSG.Text = consulta.Usg;
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
            foreach (resumen item in lstPacientes.Items)
            {
                buffer.Append(item.Fecha);
                buffer.Append(",");
                buffer.Append(item.Nombre);
                buffer.Append(",");
                buffer.Append(item.Dui);
                buffer.Append(",");
                buffer.Append(item.Edad);
                buffer.Append(",");
                buffer.Append(item.Motivo.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Diagnostico.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Plan.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Examenes.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Personales.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Familiares.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Fur);
                buffer.Append(",");
                buffer.Append(item.G);
                buffer.Append(",");
                buffer.Append(item.Pv);
                buffer.Append(",");
                buffer.Append(item.Pc);
                buffer.Append(",");
                buffer.Append(item.Pp);
                buffer.Append(",");
                buffer.Append(item.Ab);
                buffer.Append(",");
                buffer.Append(item.V);
                buffer.Append(",");
                buffer.Append(item.P);
                buffer.Append(",");
                buffer.Append(item.Talla);
                buffer.Append(",");
                buffer.Append(item.Peso);
                buffer.Append(",");
                buffer.Append(item.Imc);
                buffer.Append(",");
                buffer.Append(item.Hallazgos.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                buffer.Append(",");
                buffer.Append(item.Fpp);
                buffer.Append(",");
                buffer.Append(item.Eg);
                buffer.Append(",");
                buffer.Append(item.Au);
                buffer.Append(",");
                buffer.Append(item.Presentacion);
                buffer.Append(",");
                buffer.Append(item.Fcf);
                buffer.Append(",");
                buffer.Append(item.Mf);
                buffer.Append(",");
                buffer.Append(item.Rh);
                buffer.Append(",");
                buffer.Append(item.Hb);
                buffer.Append(",");
                buffer.Append(item.Vih);
                buffer.Append(",");
                buffer.Append(item.Vdlr);
                buffer.Append(",");
                buffer.Append(item.Ego);
                buffer.Append(",");
                buffer.Append(item.Uro);
                buffer.Append(",");
                buffer.Append(item.Glc);
                buffer.Append(",");
                buffer.Append(item.Os);
                buffer.Append(",");
                buffer.Append(item.Usg);
                buffer.Append("\n");
            }
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
            List<resumen> datosSeleccionados= new List<resumen>();
            List<resumen> datosPerfil = new List<resumen>();
            foreach (resumen item in lstPacientes.Items)
            {
                if (item.Seleccionado == true)
                {
                    conteo++;
                    datosSeleccionados.Add(item);
                    if (item.Rh.Length > 0)
                    {
                        datosPerfil.Add(item);
                        conteoPerfil++;
                    }
                }
            }
            if (conteo > 0)
            {
                popPDF.IsOpen = true;
                resumen ultimoRegistro = datosSeleccionados.Last(); //estan ordenados de manera descendente por fecha
                lblNombre.Content = ultimoRegistro.Nombre;
                lblEdad.Content = ultimoRegistro.Edad;
                lblImpFPP.Content = ultimoRegistro.Fpp;
                lblFUR.Content = ultimoRegistro.Fur;
                lblImpG.Content = ultimoRegistro.G;
                lblImpPV.Content = ultimoRegistro.Pv;
                lblImpPC.Content = ultimoRegistro.Pc;
                lblImpPP.Content = ultimoRegistro.Pp;
                lblImpAB.Content = ultimoRegistro.Ab;
                lblImpNV.Content = ultimoRegistro.V;
                lblImpFamiliares.Text = ultimoRegistro.Familiares;
                lblImpPersonales.Text = ultimoRegistro.Personales;
                lblImpPeso.Content = ultimoRegistro.Peso;
                lblImpTalla.Content = ultimoRegistro.Talla;
                lblImpImc.Content = ultimoRegistro.Imc;

                datosSeleccionados.Reverse();
                dgExamenFisico.ItemsSource = datosSeleccionados;


                dgPrimerPerfil.Items.Clear();
                dgSegundoPerfil.Items.Clear();

                resumen primerPerfil = datosPerfil.Last();
                dgPrimerPerfil.Items.Add(primerPerfil);

                if (conteoPerfil > 1)
                {
                    resumen segundoPerfil = datosPerfil.First();
                    dgSegundoPerfil.Items.Add(segundoPerfil);
                }



                
            }
            else
            {
                MessageBox.Show("No hay items seleccionados","Atencion",MessageBoxButton.OK,MessageBoxImage.Information);
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
    }
}
