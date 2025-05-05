using PersonalOrganizerApp.ReminderClasses;
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
    public partial class UpdateReminderForm : Form
    {
        private IReminder selectedReminder;

        public UpdateReminderForm(IReminder reminder)
        {
            InitializeComponent();
            selectedReminder = reminder;

            // ComboBox'a enum değerlerini ata
            cmbType.DataSource = Enum.GetValues(typeof(ReminderType));

            // Mevcut verileri arayüze aktar
            txtTitle.Text = selectedReminder.Title;
            txtDescription.Text = selectedReminder.Description;
            txtSummary.Text = selectedReminder.Summary;
            dtpDate.Value = selectedReminder.Date;
            dtpTime.Value = DateTime.Today.Add(selectedReminder.Time);
            cmbType.SelectedItem = selectedReminder.Type;

            // Buton olayını bağla
            btnUpdateReminder.Click += BtnUpdateReminder_Click;
        }

        private void BtnUpdateReminder_Click(object sender, EventArgs e)
        {
            // Alanlardan gelen yeni verilerle güncelleme yap
            selectedReminder.Title = txtTitle.Text;
            selectedReminder.Description = txtDescription.Text;
            selectedReminder.Summary = txtSummary.Text;
            selectedReminder.Date = dtpDate.Value.Date;
            selectedReminder.Time = dtpTime.Value.TimeOfDay;

            // Tipi güncelle (Enum'a çevirerek)
            if (Enum.TryParse(cmbType.SelectedItem?.ToString(), out ReminderType selectedType))
            {
                selectedReminder.Type = selectedType;
            }

            MessageBox.Show("Reminder updated successfully.");
            this.Close();
        }
    }
}