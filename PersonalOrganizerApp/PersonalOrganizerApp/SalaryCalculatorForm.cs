using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp
{
    public partial class SalaryCalculatorForm : Form
    {
        public SalaryCalculatorForm()
        {
            InitializeComponent();
        }

        private void SalaryCalculatorForm_Load(object sender, EventArgs e)
        {
            SalaryCsvManager.InitializeCsv();

            // 1. ComboBox verilerini doldur
            cmbExperience.Items.AddRange(new string[] {
        "2-4 yıl", "5-9 yıl", "10-14 yıl", "15-20 yıl", "20 yıl üstü"
    });

            string[] cities = {
        "İstanbul", "Ankara", "İzmir", "Kocaeli", "Sakarya", "Düzce",
        "Bolu", "Yalova", "Edirne", "Kırklareli", "Tekirdağ", "Trabzon",
        "Ordu", "Giresun", "Rize", "Artvin", "Gümüşhane", "Bursa", "Eskişehir",
        "Bilecik", "Aydın", "Denizli", "Muğla", "Adana", "Mersin", "Balıkesir",
        "Çanakkale", "Antalya", "Isparta", "Burdur"
    };
            cmbCity.Items.AddRange(cities);

            cmbEducation.Items.AddRange(new string[] {
        "Meslekle ilgili Yüksek Lisans",
        "Meslekle ilgili Doktora",
        "Meslekle ilgili Doçentlik",
        "İlgisiz Yüksek Lisans",
        "İlgisiz Doktora/Doçentlik"
    });

            cmbManagementRole.Items.AddRange(new string[] {
        "Takım Lideri/Grup Yöneticisi/Teknik Yönetici/Yazılım Mimarı",
        "Proje Yöneticisi",
        "Direktör/Projeler Yöneticisi",
        "CTO/Genel Müdür",
        "Bilgi İşlem Sorumlusu/Müdürü (≤ 5 personel)",
        "Bilgi İşlem Sorumlusu/Müdürü (> 5 personel)"
    });

            // 2. CSV'den veri yükle
            string username = "user1"; // Giriş yapılmış kullanıcı
            string[] data = SalaryCsvManager.LoadUserData(username);

            if (data != null)
            {
                cmbExperience.SelectedItem = data[1];
                cmbCity.SelectedItem = data[2];
                cmbEducation.SelectedItem = data[3];
                cmbManagementRole.SelectedItem = data[4];

                chkEnglishCert.Checked = bool.Parse(data[5]);
                chkEnglishEdu.Checked = bool.Parse(data[6]);
                chkOtherLang.Checked = bool.Parse(data[7]);
                chkSpouseWorks.Checked = bool.Parse(data[8]);
                chkChild0_6.Checked = bool.Parse(data[9]);
                chkChild7_18.Checked = bool.Parse(data[10]);
                chkChildOver18.Checked = bool.Parse(data[11]);

                lblResult.Text = $"Son Hesaplanan Ücret: {Convert.ToDouble(data[12]):C2}";
                lblResult.ForeColor = Color.Red;
            }
            else
            {
                cmbExperience.SelectedIndex = 0;
                cmbCity.SelectedIndex = -1;
                cmbEducation.SelectedIndex = 0;
                cmbManagementRole.SelectedIndex = 0;
            }
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {

            double baseSalary = 26829; // BMO'ya göre 2023 Temmuz sonrası baz ücret
            double totalFactor = 0;

            // Deneyim süresi katsayısı
            switch (cmbExperience.SelectedItem?.ToString())
            {
                case "2-4 yıl": totalFactor += 0.60; break;
                case "5-9 yıl": totalFactor += 1.00; break;
                case "10-14 yıl": totalFactor += 1.20; break;
                case "15-20 yıl": totalFactor += 1.35; break;
                case "20 yıl üstü": totalFactor += 1.50; break;
            }

            // Şehir katsayısı
            string city = cmbCity.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(city))
            {
                var cityCoefficients = new Dictionary<string, double>
            {
                {"İstanbul", 0.30}, {"Ankara", 0.20}, {"İzmir", 0.20},
                {"Kocaeli", 0.10}, {"Sakarya", 0.10}, {"Düzce", 0.10}, {"Bolu", 0.10}, {"Yalova", 0.10},
                {"Edirne", 0.10}, {"Kırklareli", 0.10}, {"Tekirdağ", 0.10},
                {"Trabzon", 0.05}, {"Ordu", 0.05}, {"Giresun", 0.05}, {"Rize", 0.05},
                {"Artvin", 0.05}, {"Gümüşhane", 0.05}, {"Bursa", 0.05}, {"Eskişehir", 0.05},
                {"Bilecik", 0.05}, {"Aydın", 0.05}, {"Denizli", 0.05}, {"Muğla", 0.05},
                {"Adana", 0.05}, {"Mersin", 0.05}, {"Balıkesir", 0.05}, {"Çanakkale", 0.05},
                {"Antalya", 0.05}, {"Isparta", 0.05}, {"Burdur", 0.05}
            };

                if (cityCoefficients.ContainsKey(city))
                    totalFactor += cityCoefficients[city];
            }

            // Yönetici Görevleri katsayısı
            switch (cmbManagementRole.SelectedItem?.ToString())
            {
                case "Takım Lideri/Grup Yöneticisi/Teknik Yönetici/Yazılım Mimarı": totalFactor += 0.50; break;
                case "Proje Yöneticisi": totalFactor += 0.75; break;
                case "Direktör/Projeler Yöneticisi": totalFactor += 0.85; break;
                case "CTO/Genel Müdür": totalFactor += 1.00; break;
                case "Bilgi İşlem Sorumlusu/Müdürü (≤ 5 personel)": totalFactor += 0.40; break;
                case "Bilgi İşlem Sorumlusu/Müdürü (> 5 personel)": totalFactor += 0.60; break;
            }

            // Eğitim düzeyi katsayısı
            switch (cmbEducation.SelectedItem?.ToString())
            {
                case "Meslekle ilgili Yüksek Lisans": totalFactor += 0.10; break;
                case "Meslekle ilgili Doktora": totalFactor += 0.30; break;
                case "Meslekle ilgili Doçentlik": totalFactor += 0.35; break;
                case "İlgisiz Yüksek Lisans": totalFactor += 0.05; break;
                case "İlgisiz Doktora/Doçentlik": totalFactor += 0.15; break;
            }

            // Yabancı Dil Bilgisi
            if (chkEnglishCert.Checked) totalFactor += 0.20;
            if (chkEnglishEdu.Checked) totalFactor += 0.20;
            if (chkOtherLang.Checked) totalFactor += 0.05;

            // Eşi çalışmıyorsa
            if (!chkSpouseWorks.Checked) totalFactor += 0.20;

            // Çocuk katsayıları
            if (chkChild0_6.Checked) totalFactor += 0.20;
            if (chkChild7_18.Checked) totalFactor += 0.30;
            if (chkChildOver18.Checked) totalFactor += 0.40; // Etki etmiyor

            // Hesapla
            double finalSalary = baseSalary * (1 + totalFactor);
            lblResult.Text = $"Calculated Salary: {finalSalary.ToString("C2")}";

            // Geçici kullanıcı adı, ileride login sistemiyle değiştirilebilir
            string username = "user1";

            SalaryCsvManager.SaveUserSalaryData(
                username,
                cmbExperience.SelectedItem?.ToString(),
                cmbCity.SelectedItem?.ToString(),
                cmbEducation.SelectedItem?.ToString(),
                cmbManagementRole.SelectedItem?.ToString(),
                chkEnglishCert.Checked,
                chkEnglishEdu.Checked,
                chkOtherLang.Checked,
                chkSpouseWorks.Checked,
                chkChild0_6.Checked,
                chkChild7_18.Checked,
                chkChildOver18.Checked,
                finalSalary
            );


        }
    }
}
