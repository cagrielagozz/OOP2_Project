using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PersonalOrganizerApp
{
    public partial class PersonalInformation : Form
    {
        private User currentUser = new User();
        private Dictionary<TextBox, Stack<string>> undoStacks = new Dictionary<TextBox, Stack<string>>();
        private Dictionary<TextBox, Stack<string>> redoStacks = new Dictionary<TextBox, Stack<string>>();
        private string csvPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csvler", "user_data.csv");

        public PersonalInformation()
        {
            InitializeComponent();
            this.KeyPreview = true;
            InitializeUndoRedo();
        }

        private void PersonalInformation_Load(object sender, EventArgs e)
        {
            LoadUserFromCsv();
            SetTextBoxEvents();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void InitializeUndoRedo()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    undoStacks[txt] = new Stack<string>();
                    redoStacks[txt] = new Stack<string>();
                }
            }
        }

        private void SetTextBoxEvents()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.TextChanged += (s, e) =>
                    {
                        if (undoStacks[txt].Count == 0 || undoStacks[txt].Peek() != txt.Text)
                        {
                            undoStacks[txt].Push(txt.Text);
                        }
                    };
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            currentUser.Username = txtUsername.Text;
            currentUser.Name = txtName.Text;
            currentUser.Surname = txtSurname.Text;
            currentUser.Phone = txtPhone.Text;
            currentUser.Address = txtAddress.Text;
            currentUser.Email = txtEmail.Text;
            currentUser.Password = txtPassword.Text;
            currentUser.PhotoBase64 = ImageToBase64(pbPhoto.Image);

            SaveUserToCsv();
            MessageBox.Show("Bilgiler kaydedildi.");
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPhoto.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private string ImageToBase64(Image image)
        {
            if (image == null) return "";
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private Image Base64ToImage(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return null;
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void SaveUserToCsv()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(csvPath));
            string[] fields = {
                Escape(currentUser.Username),
                Escape(currentUser.Name),
                Escape(currentUser.Surname),
                Escape(currentUser.Phone),
                Escape(currentUser.Address),
                Escape(currentUser.Email),
                Escape(currentUser.Password),
                Escape(currentUser.PhotoBase64)
            };
            File.WriteAllLines(csvPath, new[]
            {
                "Username,Name,Surname,Phone,Address,Email,Password,PhotoBase64",
                string.Join(",", fields)
            }, Encoding.UTF8);
        }

        private void LoadUserFromCsv()
        {
            if (!File.Exists(csvPath)) return;
            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length < 2) return;

            string[] data = ParseCsvLine(lines[1]);
            if (data.Length < 8) return;

            currentUser.Username = data[0];
            currentUser.Name = data[1];
            currentUser.Surname = data[2];
            currentUser.Phone = data[3];
            currentUser.Address = data[4];
            currentUser.Email = data[5];
            currentUser.Password = data[6];
            currentUser.PhotoBase64 = data[7];

            txtUsername.Text = currentUser.Username;
            txtName.Text = currentUser.Name;
            txtSurname.Text = currentUser.Surname;
            txtPhone.Text = currentUser.Phone;
            txtAddress.Text = currentUser.Address;
            txtEmail.Text = currentUser.Email;
            txtPassword.Text = currentUser.Password;
            pbPhoto.Image = Base64ToImage(currentUser.PhotoBase64);
        }

        private string Escape(string input)
        {
            // Yeni satır ve geri satır karakterlerini kaldırıyoruz
            if (input.Contains(",") || input.Contains("\"") || input.Contains(Environment.NewLine))
                return "\"" + input.Replace("\"", "\"\"").Replace(Environment.NewLine, " ") + "\"";
            return input;
        }

        private string[] ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            bool inQuotes = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == ',' && !inQuotes)
                {
                    values.Add(sb.ToString());
                    sb.Clear();
                }
                else if (c == '\"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        sb.Append('\"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }
            values.Add(sb.ToString());
            return values.ToArray();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string cleaned = new string(txtName.Text.Where(c => char.IsLetter(c) || c == ' ').ToArray());
            if (txtName.Text != cleaned)
            {
                int cursor = txtName.SelectionStart - (txtName.Text.Length - cleaned.Length);
                txtName.Text = cleaned;
                txtName.SelectionStart = Math.Max(0, cursor);
            }
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            string cleaned = new string(txtSurname.Text.Where(c => char.IsLetter(c) || c == ' ').ToArray());
            if (txtSurname.Text != cleaned)
            {
                int cursor = txtSurname.SelectionStart - (txtSurname.Text.Length - cleaned.Length);
                txtSurname.Text = cleaned;
                txtSurname.SelectionStart = Math.Max(0, cursor);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length > 11)
            {
                int cursor = txtPhone.SelectionStart - (txtPhone.Text.Length - 11);
                txtPhone.Text = txtPhone.Text.Substring(0, 11);
                txtPhone.SelectionStart = Math.Max(0, cursor);
            }
        }

        private bool ValidateInputs()
        {
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Geçerli bir e-posta adresi girin.");
                return false;
            }
            if (!Regex.IsMatch(txtName.Text, @"^[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$"))
            {
                MessageBox.Show("İsim yalnızca harf içermelidir.");
                return false;
            }
            if (!Regex.IsMatch(txtSurname.Text, @"^[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$"))
            {
                MessageBox.Show("Soyisim yalnızca harf içermelidir.");
                return false;
            }
            if (!Regex.IsMatch(txtAddress.Text, @"^[a-zA-Z0-9çÇğĞıİöÖşŞüÜ\s,.-]+$"))
            {
                MessageBox.Show("Adres geçersiz karakter içeriyor.");
                return false;
            }
            if (!Regex.IsMatch(txtPhone.Text, @"^\d{11}$"))
            {
                MessageBox.Show("Telefon numarası 11 haneli olmalıdır.");
                return false;
            }
            return true;
        }

        private void PersonalInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void PersonalInformation_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    if (e.Control && e.KeyCode == Keys.Z && undoStacks[txt].Count > 1)
                    {
                        redoStacks[txt].Push(undoStacks[txt].Pop());
                        txt.Text = undoStacks[txt].Peek();
                    }
                    else if (e.Control && e.KeyCode == Keys.Y && redoStacks[txt].Count > 0)
                    {
                        string redo = redoStacks[txt].Pop();
                        undoStacks[txt].Push(redo);
                        txt.Text = redo;
                    }
                }
            }
        }
    }

    public class User
    {
        public string Username, Name, Surname, Phone, Address, Email, Password;
        public string PhotoBase64;
    }
}
