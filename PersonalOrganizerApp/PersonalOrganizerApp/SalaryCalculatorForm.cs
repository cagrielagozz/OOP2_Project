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
        // Initialize the form
        public SalaryCalculatorForm()
        {
            InitializeComponent();
        }

        private void SalaryCalculatorForm_Load(object sender, EventArgs e)
        {
            SalaryCsvManager.InitializeCsv();

            // Fill the first ComboBox data(experience)
            cmbExperience.Items.AddRange(new string[] {
                "2-4 yıl", "5-9 yıl", "10-14 yıl", "15-20 yıl", "20 yıl üstü"
        });

            // Fill the second ComboBox data(city)
            string[] cities = {
                "İstanbul", "Ankara", "İzmir", "Kocaeli", "Sakarya", "Düzce",
                "Bolu", "Yalova", "Edirne", "Kırklareli", "Tekirdağ", "Trabzon",
                "Ordu", "Giresun", "Rize", "Artvin", "Gümüşhane", "Bursa", "Eskişehir",
                "Bilecik", "Aydın", "Denizli", "Muğla", "Adana", "Mersin", "Balıkesir",
                "Çanakkale", "Antalya", "Isparta", "Burdur"
        };
            cmbCity.Items.AddRange(cities);

            // Fill the third ComboBox data(education)
            cmbEducation.Items.AddRange(new string[] {
                "Meslekle ilgili Yüksek Lisans",
                "Meslekle ilgili Doktora",
                "Meslekle ilgili Doçentlik",
                "İlgisiz Yüksek Lisans",
                "İlgisiz Doktora/Doçentlik"
        });

            // Fill the fourth ComboBox data(management role)
            cmbManagementRole.Items.AddRange(new string[] {
                "Takım Lideri/Grup Yöneticisi/Teknik Yönetici/Yazılım Mimarı",
                "Proje Yöneticisi",
                "Direktör/Projeler Yöneticisi",
                "CTO/Genel Müdür",
                "Bilgi İşlem Sorumlusu/Müdürü (≤ 5 personel)",
                "Bilgi İşlem Sorumlusu/Müdürü (> 5 personel)"
        });

            // Load the saved data for the logged-in user
            string username = "user1";
            string[] data = SalaryCsvManager.LoadUserData(username);

            if (data != null)
            {
                // Set the ComboBox selections based on the saved data
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

            double baseSalary = 26829; // Base salary after July 2023 according to BMO
            double totalFactor = 0;


            // Experience factor
            switch (cmbExperience.SelectedItem?.ToString())
            {
                case "2-4 yıl": totalFactor += 0.60; break;
                case "5-9 yıl": totalFactor += 1.00; break;
                case "10-14 yıl": totalFactor += 1.20; break;
                case "15-20 yıl": totalFactor += 1.35; break;
                case "20 yıl üstü": totalFactor += 1.50; break;
            }


            // City factor
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


            // Management role factor
            switch (cmbManagementRole.SelectedItem?.ToString())
            {
                case "Takım Lideri/Grup Yöneticisi/Teknik Yönetici/Yazılım Mimarı": totalFactor += 0.50; break;
                case "Proje Yöneticisi": totalFactor += 0.75; break;
                case "Direktör/Projeler Yöneticisi": totalFactor += 0.85; break;
                case "CTO/Genel Müdür": totalFactor += 1.00; break;
                case "Bilgi İşlem Sorumlusu/Müdürü (≤ 5 personel)": totalFactor += 0.40; break;
                case "Bilgi İşlem Sorumlusu/Müdürü (> 5 personel)": totalFactor += 0.60; break;
            }


            // Education factor
            switch (cmbEducation.SelectedItem?.ToString())
            {
                case "Meslekle ilgili Yüksek Lisans": totalFactor += 0.10; break;
                case "Meslekle ilgili Doktora": totalFactor += 0.30; break;
                case "Meslekle ilgili Doçentlik": totalFactor += 0.35; break;
                case "İlgisiz Yüksek Lisans": totalFactor += 0.05; break;
                case "İlgisiz Doktora/Doçentlik": totalFactor += 0.15; break;
            }


            // English certificate factor
            // English education factor
            // Other language factors
            if (chkEnglishCert.Checked) totalFactor += 0.20;
            if (chkEnglishEdu.Checked) totalFactor += 0.20;
            if (chkOtherLang.Checked) totalFactor += 0.05;


            // Spouse works factor
            if (!chkSpouseWorks.Checked) totalFactor += 0.20;


            // Children factors
            if (chkChild0_6.Checked) totalFactor += 0.20;
            if (chkChild7_18.Checked) totalFactor += 0.30;
            if (chkChildOver18.Checked) totalFactor += 0.40; // Etki etmiyor


            // Calculate the final salary
            double finalSalary = baseSalary * (1 + totalFactor);
            lblResult.Text = $"Calculated Salary: {finalSalary.ToString("C2")}";


            // Save the user data to the CSV file
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
