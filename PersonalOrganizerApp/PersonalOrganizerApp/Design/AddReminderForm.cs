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
        // List to store reminders passed from the main form
        private List<IReminder> reminders;


        // Constructor - receives the reminder list to update
        public AddReminderForm(List<IReminder> reminders)
        {
            InitializeComponent();
            this.reminders = reminders;
        }


        // Called when the form is loaded
        private void AddReminderForm_Load(object sender, EventArgs e)
        {
            // Populate ComboBox with reminder types
            cmbType.Items.Add("Meeting");
            cmbType.Items.Add("Task");

            cmbType.SelectedIndex = 0; // Set default selected type
        }


        // Called when "Add Reminder" button is clicked
        private void btnAddReminder_Click(object sender, EventArgs e)
        {
            // Collect user inputs
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string summary = txtSummary.Text.Trim();
            DateTime date = dtpDate.Value.Date;
            DateTime time = dtpTime.Value;

            // Validation: Title is required
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a title.");
                return;
            }

            // Determine selected reminder type
            ReminderType type = cmbType.SelectedItem.ToString() == "Meeting"
                ? ReminderType.Meeting
                : ReminderType.Task;

            // Create the appropriate reminder using Abstract Factory
            IReminderFactory factory = type == ReminderType.Meeting
                ? new MeetingReminderFactory()
                : (IReminderFactory)new TaskReminderFactory();

            // Instantiate and populate the reminder object
            IReminder newReminder = factory.CreateReminder();
            newReminder.Title = title;
            newReminder.Description = description;
            newReminder.Summary = summary;
            newReminder.Date = date;
            newReminder.Time = time.TimeOfDay;
    
            reminders.Add(newReminder);

            // Save the new reminder to CSV
            ReminderCsvManager.SaveReminder("user1", newReminder);

            MessageBox.Show("Reminder added successfully.");
            this.Close();
        }
    }
}
