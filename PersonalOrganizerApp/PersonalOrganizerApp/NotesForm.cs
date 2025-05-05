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
    public partial class NotesForm : Form
    {
        private FileSystemWatcher fileWatcher;

        private string filePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csvler", "notlar.csv");

        public NotesForm()
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

        public void RefreshDataGrid()
        {
            LoadDataFromCsv();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text1 = textBox1.Text;
            string text2 = textBox2.Text;

            try
            {
                using (StreamWriter file = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    file.WriteLine($"{text1},{text2}");
                }
                MessageBox.Show("Notlar başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string selectedData = selectedRow.Cells[0].Value.ToString();

                string[] lines = File.ReadAllLines(filePath).ToArray();

                int selectedIndex = -1;
                var updatedLines = lines.Where((line, index) =>
                {
                    string formattedLine = string.Join(" - ", line.Split(','));
                    if (formattedLine == selectedData)
                    {
                        selectedIndex = index;
                        return false;
                    }
                    return true;
                }).ToList();

                File.WriteAllLines(filePath, updatedLines, Encoding.UTF8);

                string text1 = textBox1.Text;
                string text2 = textBox2.Text;

                if (!string.IsNullOrWhiteSpace(text1) && !string.IsNullOrWhiteSpace(text2) && selectedIndex != -1)
                {
                    updatedLines.Insert(selectedIndex, $"{text1},{text2}");
                    File.WriteAllLines(filePath, updatedLines, Encoding.UTF8);
                }

                RefreshDataGrid();
                MessageBox.Show("Notunuz başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                }).ToList();

                File.WriteAllLines(filePath, updatedLines, Encoding.UTF8);
                RefreshDataGrid();
                MessageBox.Show("Seçilen not başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
