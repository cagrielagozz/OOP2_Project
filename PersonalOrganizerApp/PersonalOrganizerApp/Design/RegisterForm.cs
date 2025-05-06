using PersonalOrganizerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalOrganizerApp.Design
{
    public partial class RegisterForm : Form
    {

        // Fix for CS0103: The name 'LblPath' does not exist in the current context  
        // Ensure that the LblPath control is defined in the form's designer file.  
        // Add the following declaration if it is missing:  
        private Label LblPath;
        public RegisterForm()
        {
            InitializeComponent();
        }
        
        private void KayıtFormu_Load(object sender, EventArgs e)
        {
            txtNewName.Text = "";
            txtNewPass.Text = "";
            TxtName.Text = "";
            TxtSurname.Text = "";
            MTxtPhoneNumber.Text = "";
            TxtAddress.Text = "";
            TxtEmail.Text = "";
            LblPath.Text = "";
            PicPhoto.ImageLocation = "";
        }
        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginForm form = LoginForm.Form1Instance;
            form.Show();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            // Add any desired functionality here, or leave it empty if no action is needed.
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Add any initialization logic here if needed
        }
        // Add this method to the RegisterForm class
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // Add any logic you want to handle the TextChanged event for TxtEmail here.
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            int counter = 0;
            if (LoginForm.userList.Any())
            {
                for (int i = 0; i < LoginForm.userList.Count(); i++)
                {
                    if (txtNewName.Text == LoginForm.userList[i].Username)
                    {
                        MessageBox.Show("User already exists!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        counter++;
                        break;
                    }
                    else if (txtNewName.Text == "" || txtNewPass.Text == "" || TxtName.Text == "" || TxtSurname.Text == "" || MTxtPhoneNumber.Text == "" || TxtAddress.Text == "" || TxtEmail.Text == "" || LblPath.Text == "")
                    {
                        MessageBox.Show("Please fill in the blanks!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        counter++;
                        break;
                    }
                }
                if (counter != 1)
                {
                    if (Util.isEmailValid(TxtEmail.Text))
                    {
                        if (MTxtPhoneNumber.Text.Length == 14)
                        {
                            var newUser = new User
                            {
                                Username = txtNewName.Text,
                                Password = Util.ComputeStringToSha256Hash(txtNewPass.Text),
                                RememberMe = false,
                                UserTypes = LoginForm.userList.Any() ? "user" : "*Admin",
                                Name = TxtName.Text,
                                Surname = TxtSurname.Text,
                                PhoneNumber = MTxtPhoneNumber.Text,
                                Address = TxtAddress.Text,
                                Email = TxtEmail.Text,
                                Base64Photo = LblPath.Text
                            };
                            LoginForm.userList.Add(newUser);
                            Util.SaveCsv(LoginForm.userList, LoginForm.path);
                            MessageBox.Show("Registration Successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Phone number order incorrect!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email order incorrect!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                if (txtNewName.Text == "" || txtNewPass.Text == "" || TxtName.Text == "" || TxtSurname.Text == "" || MTxtPhoneNumber.Text == "" || TxtAddress.Text == "" || TxtEmail.Text == "" || LblPath.Text == "")
                {
                    MessageBox.Show("Please fill in the blanks!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (Util.isEmailValid(TxtEmail.Text))
                    {
                        if (MTxtPhoneNumber.Text.Length == 14)
                        {
                            var newUser = new User
                            {
                                Username = txtNewName.Text,
                                Password = Util.ComputeStringToSha256Hash(txtNewPass.Text),
                                RememberMe = false,
                                UserTypes = "*Admin",
                                Name = TxtName.Text,
                                Surname = TxtSurname.Text,
                                PhoneNumber = MTxtPhoneNumber.Text,
                                Address = TxtAddress.Text,
                                Email = TxtEmail.Text,
                                Base64Photo = LblPath.Text
                            };
                            LoginForm.userList.Add(newUser);
                            Util.SaveCsv(LoginForm.userList, LoginForm.path);
                            MessageBox.Show("Registration Successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Phone number order incorrect!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email order incorrect!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        /*
private void BtnImagePath_Click(object sender, EventArgs e)
{
   openFileDialog1.ShowDialog();
   PicPhoto.ImageLocation = openFileDialog1.FileName;
   LblPath.Text = Util.ImageToBase64(openFileDialog1.FileName);
}
*/
    }
}
