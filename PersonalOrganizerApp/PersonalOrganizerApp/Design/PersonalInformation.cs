using PersonalOrganizerApp.Models;
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
        private ProfileCaretaker caretaker = new ProfileCaretaker();
        private string csvPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csvler", "user_data.csv");
        private List<User> usersList = new List<User>(); // Farklı kullanıcıları tutmak için liste ekledim

        public PersonalInformation()
        {
            InitializeComponent();
            this.KeyPreview = true; // KeyDown olayını alabilmek için
            this.KeyDown += new KeyEventHandler(this.PersonalInformation_KeyDown); // KeyDown olayını bağla
        }

        private void RegisterChangeHandlers()
        {
            txtUsername.Leave += AnyField_Leave;
            txtName.Leave += AnyField_Leave;
            txtSurname.Leave += AnyField_Leave;
            txtPhone.Leave += AnyField_Leave;
            txtAddress.Leave += AnyField_Leave;
            txtEmail.Leave += AnyField_Leave;
            txtPassword.Leave += AnyField_Leave;
        }

        private void PersonalInformation_Load(object sender, EventArgs e)
        {
            LoadUsersFromCsv();  // Kullanıcıları CSV'den yükle
            txtPassword.UseSystemPasswordChar = true;
            caretaker.SaveState(currentUser); // İlk durum kaydedilsin
            RegisterChangeHandlers(); // Yeni: değişiklik izleyici metodu
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            currentUser.Username = txtUsername.Text;
            currentUser.Name = txtName.Text;
            currentUser.Surname = txtSurname.Text;
            currentUser.PhoneNumber = txtPhone.Text;
            currentUser.Address = txtAddress.Text;
            currentUser.Email = txtEmail.Text;
            currentUser.Password = txtPassword.Text;
            currentUser.Base64Photo = ImageToBase64(pbPhoto.Image);

            SaveUserToCsv();  // Kullanıcıyı CSV'ye kaydet
            caretaker.SaveState(currentUser);
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
                Escape(currentUser.PhoneNumber),
                Escape(currentUser.Address),
                Escape(currentUser.Email),
                Escape(currentUser.Password),
                Escape(currentUser.Base64Photo)
            };
            File.WriteAllLines(csvPath, new[] {
                "Username,Name,Surname,Phone,Address,Email,Password,PhotoBase64",
                string.Join(",", fields)
            }, Encoding.UTF8);
        }

        private void LoadUsersFromCsv()
        {
            if (!File.Exists(csvPath)) return;
            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length < 2) return;

            usersList.Clear(); // Kullanıcı listesi sıfırlanacak

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = ParseCsvLine(lines[i]);
                if (data.Length < 8) continue;

                User user = new User
                {
                    Username = data[0],
                    Name = data[1],
                    Surname = data[2],
                    PhoneNumber = data[3],
                    Address = data[4],
                    Email = data[5],
                    Password = data[6],
                    Base64Photo = data[7]
                };

                usersList.Add(user); // Kullanıcıyı listeye ekle
            }

            if (usersList.Count > 0)
            {
                currentUser = usersList[0]; // İlk kullanıcıyı varsayılan olarak seç
                SetUserToForm(currentUser);
            }
            caretaker.SaveState(currentUser);
        }

        private void SetUserToForm(User user)
        {
            txtUsername.Text = user.Username;
            txtName.Text = user.Name;
            txtSurname.Text = user.Surname;
            txtPhone.Text = user.PhoneNumber;
            txtAddress.Text = user.Address;
            txtEmail.Text = user.Email;
            txtPassword.Text = user.Password;
            pbPhoto.Image = Base64ToImage(user.Base64Photo);
        }

        private void AnyField_Leave(object sender, EventArgs e)
        {
            UpdateUserFromForm();
            caretaker.SaveState(currentUser);
        }

        private void UpdateUserFromForm()
        {
            currentUser.Username = txtUsername.Text;
            currentUser.Name = txtName.Text;
            currentUser.Surname = txtSurname.Text;
            currentUser.PhoneNumber = txtPhone.Text;
            currentUser.Address = txtAddress.Text;
            currentUser.Email = txtEmail.Text;
            currentUser.Password = txtPassword.Text;
            currentUser.Base64Photo = ImageToBase64(pbPhoto.Image);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (caretaker.Undo(currentUser) != null)
                SetUserToForm(currentUser);
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (caretaker.Redo(currentUser) != null)
                SetUserToForm(currentUser);
        }

        private string Escape(string input)
        {
            if (input == null) return "";
            // Satır sonlarını boşlukla değiştir
            string cleaned = input.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
            // Eğer tırnak, virgül vs. varsa, çift tırnakla sar
            if (cleaned.Contains(",") || cleaned.Contains("\""))
                return "\"" + cleaned.Replace("\"", "\"\"") + "\"";
            return cleaned;
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
                else if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
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

        // KeyDown olayı: CTRL + Z ve CTRL + Y için Undo ve Redo işlemlerini tetikleyin
        private void PersonalInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                // CTRL + Z için Undo
                if (e.KeyCode == Keys.Z)
                {
                    btnUndo.PerformClick(); // Undo butonunu simüle et
                }
                // CTRL + Y için Redo
                if (e.KeyCode == Keys.Y)
                {
                    btnRedo.PerformClick(); // Redo butonunu simüle et
                }
            }
        }
    }
}
