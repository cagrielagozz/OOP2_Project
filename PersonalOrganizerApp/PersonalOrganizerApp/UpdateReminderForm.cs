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
        //
        private IReminder selectedReminder;


        //// Constructor that accepts a reminder object to update
        public UpdateReminderForm(IReminder reminder)
        {
            InitializeComponent();
            selectedReminder = reminder;

            // Populate ComboBox with ReminderType enum values
            cmbType.DataSource = Enum.GetValues(typeof(ReminderType));

            // Fill form controls with the current reminder data
            txtTitle.Text = selectedReminder.Title;
            txtDescription.Text = selectedReminder.Description;
            txtSummary.Text = selectedReminder.Summary;
            dtpDate.Value = selectedReminder.Date;
            dtpTime.Value = DateTime.Today.Add(selectedReminder.Time);
            cmbType.SelectedItem = selectedReminder.Type;

            // Bind the update button click even
            btnUpdateReminder.Click += BtnUpdateReminder_Click;
        }


        // Triggered when the "Update" button is clicked
        private void BtnUpdateReminder_Click(object sender, EventArgs e)
        {
            // Update the reminder object with new data from form fields
            selectedReminder.Title = txtTitle.Text;
            selectedReminder.Description = txtDescription.Text;
            selectedReminder.Summary = txtSummary.Text;
            selectedReminder.Date = dtpDate.Value.Date;
            selectedReminder.Time = dtpTime.Value.TimeOfDay;

            // Update the reminder type based on the selected ComboBox item
            if (Enum.TryParse(cmbType.SelectedItem?.ToString(), out ReminderType selectedType))
            {
                selectedReminder.Type = selectedType;
            }

            MessageBox.Show("Reminder updated successfully.");
            this.Close();
        }

    }
}