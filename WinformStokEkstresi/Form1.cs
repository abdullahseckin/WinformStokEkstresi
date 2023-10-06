using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformStokEkstresi
{
    public partial class Form1 : Form
    {
        // global tanımlama yapıldı
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;

        public Form1()
        {
            InitializeComponent();

            // SQL bağlantısı oluşturuldu (bağlantı dizesini ve gerekli bağlantı seçeneklerini ayarlandı)
            string connectionString = "Data Source=apolex73;Initial Catalog=Test;Integrated Security=True;User ID=sa;Password=1234;";
            sqlConnection = new SqlConnection(connectionString);

            // DataTable ve SqlDataAdapter oluşturuldu
            dataTable = new DataTable();
            sqlDataAdapter = new SqlDataAdapter();

            // DataGridView için veri kaynağını belirt
            dataGridView1.DataSource = dataTable;
        }


        private void btnAra_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı açtık
                sqlConnection.Open();

                // SQL sorgusu için bir komut oluşturulabılır  (Prosedürü kullanabiliriz)
                // Prosedürü sorgu denemesi
                 string StokEkstresi = "EXEC StokEkstresi\r\n @MalKodu = '10086 SİEMENS',\r\n @BaslangicTarihi = '2012-01-01',\r\n @BitisTarihi = '2023-12-31'";
                // normal sorgu denemesi
                // string StokEkstresi = "select * from STI";

                SqlCommand sqlCommand = new SqlCommand(StokEkstresi, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Parametreleri ekledik (Malkodu, Başlangıç Tarihi ve Bitiş Tarihi)
                sqlCommand.Parameters.AddWithValue("@MalKodu", txtMalKoduAdi.Text);
                sqlCommand.Parameters.AddWithValue("@BaslangicTarihi", Convert.ToDateTime(dtpBaslangicTarihi.Value));
                sqlCommand.Parameters.AddWithValue("@BitisTarihi", Convert.ToDateTime(dtpBitisTarihi.Value));

                // SqlDataAdapter ile sorguyu çalıştırdık ve sonuçları DataTable'a doldurduk
                sqlDataAdapter.SelectCommand = sqlCommand;
                dataTable.Clear(); // Önceki verileri temizle
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: "+ ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                sqlConnection.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
/*
 Projede DevExpress kullanmadıgım için
•	Devexpress compenantları kullanmak
•	Oluşturulan grid için export seçeneği olabilir
•	Oluşturulan grid için yazdırma seçeneği olabilir
  bunlara değinmedim
***
  procedure  acıklamaalrını yaptım
** genel olarak mantıgına uygun kodları yazmaya calıstım
 
 */