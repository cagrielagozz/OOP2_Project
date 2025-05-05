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
        private List<IReminder> reminders = new List<IReminder>();
        private ReminderNotifier notifier = new ReminderNotifier();


        public ReminderForm()
        {
            InitializeComponent();
            this.Load += ReminderForm_Load;
            btnShowAll.Click += btnShowAll_Click;
            btnFilterMeeting.Click += btnFilterMeeting_Click;
            btnFilterTask.Click += btnFilterTask_Click;
            btnDeleteReminder.Click += btnDeleteReminder_Click;
            btnAddReminder.Click += btnAddReminder_Click;
            btnUpdateReminder.Click += btnUpdateReminder_Click;
            notifier.Attach(new ShakeObserver());
            reminders = ReminderCsvManager.LoadReminders("user1");

        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            dgvReminders.Columns.Add("Title", "Title");
            dgvReminders.Columns.Add("Description", "Description");
            dgvReminders.Columns.Add("Summary", "Summary");
            dgvReminders.Columns.Add("Date", "Date");
            dgvReminders.Columns.Add("Time", "Time");
            dgvReminders.Columns.Add("Type", "Category");

            dgvReminders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReminders.MultiSelect = false;

            reminders = ReminderCsvManager.LoadReminders("user1");
            RefreshGrid(reminders); // veya RefreshGrid(reminders); varsa
        }

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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid(reminders);
        }

        private void btnFilterMeeting_Click(object sender, EventArgs e)
        {
            var filtered = reminders.Where(r => r.Type == ReminderType.Meeting).ToList();
            RefreshGrid(filtered);
        }

        private void btnFilterTask_Click(object sender, EventArgs e)
        {
            var filtered = reminders.Where(r => r.Type == ReminderType.Task).ToList();
            RefreshGrid(filtered);
        }

        private void btnDeleteReminder_Click(object sender, EventArgs e)
        {
            if (dgvReminders.SelectedRows.Count > 0)
            {
                string title = dgvReminders.SelectedRows[0].Cells[0].Value.ToString();
                var itemToRemove = reminders.FirstOrDefault(r => r.Title == title);
                if (itemToRemove != null)
                {
                    reminders.Remove(itemToRemove);

                    // Güncellenmiş listeyi CSV'ye yaz
                    ReminderCsvManager.SaveAllReminders(reminders, "user1");

                    RefreshGrid(reminders);
                }
            }
            else
            {
                MessageBox.Show("Please select a reminder to delete.");
            }
        }

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
                        // CSV'yi güncelle
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
