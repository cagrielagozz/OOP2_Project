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
    public partial class AddReminderForm : Form
    {
        private List<IReminder> reminders;

        public AddReminderForm(List<IReminder> reminders)
        {
            InitializeComponent();
            this.reminders = reminders;
        }

        private void AddReminderForm_Load(object sender, EventArgs e)
        {
            // ComboBox'a reminder türlerini ekle
            cmbType.Items.Add("Meeting");
            cmbType.Items.Add("Task");

            cmbType.SelectedIndex = 0; // varsayılan olarak "Meeting" seç
        }

        private void btnAddReminder_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string summary = txtSummary.Text.Trim();
            DateTime date = dtpDate.Value.Date;
            DateTime time = dtpTime.Value;

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a title.");
                return;
            }

            // Reminder Type seçimi
            ReminderType type = cmbType.SelectedItem.ToString() == "Meeting"
                ? ReminderType.Meeting
                : ReminderType.Task;

            // Abstract Factory ile oluşturma
            IReminderFactory factory = type == ReminderType.Meeting
                ? new MeetingReminderFactory()
                : (IReminderFactory)new TaskReminderFactory();

            IReminder newReminder = factory.CreateReminder();
            newReminder.Title = title;
            newReminder.Description = description;
            newReminder.Summary = summary;
            newReminder.Date = date;
            newReminder.Time = time.TimeOfDay;
    
            reminders.Add(newReminder);

            ReminderCsvManager.SaveReminder("user1", newReminder);

            MessageBox.Show("Reminder added successfully.");
            this.Close();
        }
    }
}
