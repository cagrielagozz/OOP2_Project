using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PersonalOrganizerApp
{
    public partial class PhoneBookForm : Form
    {
        private FileSystemWatcher fileWatcher;
        private string filePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csvler", "veriler.csv");
        public PhoneBookForm()
        {
            InitializeComponent();
            InitializeFileWatcher();
            LoadDataFromCsv();
        }

        private void InitializeFileWatcher()
        {
            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = Path.GetDirectoryName(filePath);
            fileWatcher.Filter = Path.GetFileName(filePath);
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += OnCsvFileChanged;
            fileWatcher.EnableRaisingEvents = true;
        }

        private void OnCsvFileChanged(object sender, FileSystemEventArgs e)
        {
            
            if (InvokeRequired)
            {
                Invoke(new Action(LoadDataFromCsv));
            }
            else
            {
                LoadDataFromCsv();
            }
        }
        private void LoadDataFromCsv()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                DataTable table = new DataTable();
                table.Columns.Add("Bilgiler");

                foreach (string line in lines)
                {
                    string formattedLine = string.Join(" - ", line.Split(','));
                    table.Rows.Add(formattedLine);
                }

                dataGridView1.DataSource = table;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.ScrollBars = ScrollBars.Both;
            }
            else
            {
                MessageBox.Show("notlar.csv dosyası bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        /*    Form1 form1 = new Form1();
            form1.Show();*/

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void RefreshDataGrid()
        {
            LoadDataFromCsv();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string selectedData = selectedRow.Cells[0].Value.ToString();

                string[] lines = File.ReadAllLines(filePath).ToArray();

                var updatedLines = lines.Where(line =>
                {
                    string formattedLine = string.Join(" - ", line.Split(','));
                    return formattedLine != selectedData;
                });

                File.WriteAllLines(filePath, updatedLines);

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string selectedData = selectedRow.Cells[0].Value.ToString();

                string[] dataParts = selectedData.Split(new[] { " - " }, StringSplitOptions.None);

                string[] lines = File.ReadAllLines(filePath).ToArray();

                var updatedLines = lines.Where(line =>
                {
                    string formattedLine = string.Join(" - ", line.Split(','));
                    return formattedLine != selectedData;
                });

                File.WriteAllLines(filePath, updatedLines);

                RefreshDataGrid();

                /*Form1 form1 = new Form1();
                if (dataParts.Length >= 5)
                {
                    form1.SetTextBoxValues(dataParts[0], dataParts[1], dataParts[2], dataParts[3], dataParts[4], dataParts[5]);
                }
                form1.Show();*/
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
