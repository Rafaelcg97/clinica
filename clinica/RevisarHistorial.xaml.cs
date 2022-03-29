using clinica.clases;
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
    public partial class RevisarHistorial : Page
    {
        public RevisarHistorial()
        {
            InitializeComponent();
            string sql = "SELECT TOP 100 idConsulta, fechaConsulta, c.nombrePaciente, c.documentoPaciente, motivoConsulta, diagnosticoConsulta, " +
      "planTerapeuticoConsulta, examenesFisicosConsulta, b.personalesAntecedentes, b.familiaresAntecedentes, " +
      "b.gAntecedentes, b.pvAntecedentes, b.pcAntecendentes, b.ppAntecendentes, b.abAntecendentes, b.vAntecendentes, " +
      "b.pAntecendentes, b.furAntecedentes, b.fppAntecedentes, b.tallaAntecedentes, " +
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
                                Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"])
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
                    "b.pAntecendentes, b.furAntecedentes, b.fppAntecedentes, b.tallaAntecedentes, " +
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
                                    Convert.ToDouble(dr["tallaAntecedentes"] is DBNull?0:dr["tallaAntecedentes"]),
                                    Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"])
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
                    "b.pesoAntecedentes	 FROM consultas a "+
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
                                    Convert.ToDouble(dr["pesoAntecedentes"] is DBNull ? 0 : dr["pesoAntecedentes"])
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
            lblp.Text = consulta.P;
            lblfpp.Text = consulta.Fpp;
            lblfur.Text = consulta.Fur;
            lblimc.Text = consulta.Imc;
            lbltalla.Text = consulta.Talla.ToString();
            lblpeso.Text = consulta.Peso.ToString();

        }
    }
}
