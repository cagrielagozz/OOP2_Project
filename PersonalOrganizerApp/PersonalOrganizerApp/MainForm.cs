using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using PersonalOrganizerApp;
using PersonalOrganizerApp.Design;

namespace PersonalOrganizerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            UserManagementForm userManagementForm = new UserManagementForm();
            userManagementForm.ShowDialog();
        }

        private void btnPhoneBook_Click(object sender, EventArgs e)
        {
            PhoneBookForm phoneBookForm = new PhoneBookForm();
            phoneBookForm.ShowDialog();
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            NotesForm notesForm = new NotesForm();
            notesForm.ShowDialog();
        }

        private void btnPersonalInfo_Click(object sender, EventArgs e)
        {
            PersonalInformation profileForm = new PersonalInformation();
            profileForm.ShowDialog();
        }

        private void btnSalaryCalculator_Click(object sender, EventArgs e)
        {
            SalaryCalculatorForm salaryCalculatorForm = new SalaryCalculatorForm();
            salaryCalculatorForm.ShowDialog();
        }

        private void btnReminder_Click(object sender, EventArgs e)
        {
            ReminderForm reminderForm = new ReminderForm();
            reminderForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }
    }
}
