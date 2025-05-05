using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonalOrganizerApp.ReminderClasses;


namespace PersonalOrganizerApp
{

    public partial class ReminderForm : Form
    {
        // List to store reminders
        private List<IReminder> reminders = new List<IReminder>();

        // Notifier instance for handling reminder notifications
        private ReminderNotifier notifier = new ReminderNotifier();


        public ReminderForm()
        {
            InitializeComponent();

            // Bind event handlers for UI buttons
            this.Load += ReminderForm_Load;
            btnShowAll.Click += btnShowAll_Click;
            btnFilterMeeting.Click += btnFilterMeeting_Click;
            btnFilterTask.Click += btnFilterTask_Click;
            btnDeleteReminder.Click += btnDeleteReminder_Click;
            btnAddReminder.Click += btnAddReminder_Click;
            btnUpdateReminder.Click += btnUpdateReminder_Click;

            // Attach observer for notification behavior
            notifier.Attach(new ShakeObserver());

            // Load user-specific reminders from CSV
            reminders = ReminderCsvManager.LoadReminders("user1");

        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            // Initialize DataGridView columns
            dgvReminders.Columns.Add("Title", "Title");
            dgvReminders.Columns.Add("Description", "Description");
            dgvReminders.Columns.Add("Summary", "Summary");
            dgvReminders.Columns.Add("Date", "Date");
            dgvReminders.Columns.Add("Time", "Time");
            dgvReminders.Columns.Add("Type", "Category");

            dgvReminders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReminders.MultiSelect = false;

            // Reload reminders on form load
            reminders = ReminderCsvManager.LoadReminders("user1");
            RefreshGrid(reminders);
        }


        // Refresh the DataGridView with the provided list of reminders
        private void RefreshGrid(List<IReminder> list)
        {
            dgvReminders.Rows.Clear();
            foreach (var r in list)
            {
                dgvReminders.Rows.Add(
                    r.Title,
                    r.Description,
                    r.Summary,
                    r.Date.ToShortDateString(),
                    r.Time.ToString(@"hh\:mm"),
                    r.Type.ToString()
                );
            }
        }


        // Show all reminders in the DataGridView
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid(reminders);
        }


        // Filter reminders by type and refresh the DataGridView
        private void btnFilterMeeting_Click(object sender, EventArgs e)
        {
            var filtered = reminders.Where(r => r.Type == ReminderType.Meeting).ToList();
            RefreshGrid(filtered);
        }


        // Filter reminders by type and refresh the DataGridView
        private void btnFilterTask_Click(object sender, EventArgs e)
        {
            var filtered = reminders.Where(r => r.Type == ReminderType.Task).ToList();
            RefreshGrid(filtered);
        }


        // Delete the selected reminder from the list and update the CSV file
        private void btnDeleteReminder_Click(object sender, EventArgs e)
        {
            if (dgvReminders.SelectedRows.Count > 0)
            {
                string title = dgvReminders.SelectedRows[0].Cells[0].Value.ToString();
                var itemToRemove = reminders.FirstOrDefault(r => r.Title == title);
                if (itemToRemove != null)
                {
                    reminders.Remove(itemToRemove);

                    // Update the CSV file after deletion
                    ReminderCsvManager.SaveAllReminders(reminders, "user1");

                    RefreshGrid(reminders);
                }
            }
            else
            {
                MessageBox.Show("Please select a reminder to delete.");
            }
        }


        // Add a new reminder using the AddReminderForm
        private void btnAddReminder_Click(object sender, EventArgs e)
        {
            AddReminderForm addForm = new AddReminderForm(reminders);
            addForm.FormClosed += (s, args) =>
            {
                RefreshGrid(reminders);


                var lastReminder = reminders.LastOrDefault();

                if (lastReminder != null)
                {
                    notifier.NotifyObservers(this, lastReminder);
                }
            };
            addForm.Show();
        }


        // Update the selected reminder using the UpdateReminderForm
        private void btnUpdateReminder_Click(object sender, EventArgs e)
        {
            if (dgvReminders.SelectedRows.Count > 0)
            {
                string selectedTitle = dgvReminders.SelectedRows[0].Cells[0].Value.ToString();
                var selectedReminder = reminders.FirstOrDefault(r => r.Title == selectedTitle);
                if (selectedReminder != null)
                {
                    UpdateReminderForm updateForm = new UpdateReminderForm(selectedReminder);
                    updateForm.FormClosed += (s, args) =>
                    {
                        // Update the reminder list and refresh the DataGridView
                        ReminderCsvManager.SaveAllReminders(reminders, "user1");
                        RefreshGrid(reminders);
                    };
                    updateForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select a reminder to update.");
            }
        }

    }
}
