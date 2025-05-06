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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PersonalOrganizerApp.Design
{
    public partial class LoginForm : Form
    {
        public static List<User> userList = new List<User>();
        public static string path = "user_data.csv";
        public static LoginForm Form1Instance;
        MainForm mainForm = new MainForm();
        RegisterForm register = new RegisterForm();
        public LoginForm()
        {
            InitializeComponent();
            Form1Instance = this;

            checkRememberUser();
        }

        private void CheckRememberUser()
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox5.Hide();
            pictureBox3.Show();
            txtName.Text = "";
            txtPassword.Text = "";
            lblMesaj.Text = "";
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtName.Text;
            string password = txtPassword.Text;
            bool rememberMe = RememberMe.Checked;
            for (int i = 0; i < userList.Count; i++)
            {
                User user = userList[i];
                if (user.IsValid(username, password, rememberMe) == true)
                {
                   user.RememberMe = rememberMe;
                   LoginedUser.getInstance().UserGetSet = user;
                    lblMesaj.BackColor = Color.Green;
                    lblMesaj.Text = "successful login";
                    lblMesaj.Visible = true;
                    
                    if (rememberMe == true)
                    {
                        Util.SaveCsv(userList, path);
                    }
                    return;
                    
                }
            }
            lblMesaj.BackColor = Color.Red;
            lblMesaj.Text = "failed login";
            lblMesaj.Visible = true;
            txtName.Text = "";
            txtPassword.Text = "";
        }
        private void newKayıt_Click(object sender, EventArgs e)
        {
            this.Hide();
            register.ShowDialog();
            if (register.IsDisposed)
            {
                this.Show();
            }
        }
        
        private void checkRememberUser()
        {
            foreach (User user in userList)
            {
                if (user.RememberMe)
                {
                    LoginedUser.getInstance().UserGetSet = user;
                    this.Hide();
                    if (mainForm.ShowDialog() == DialogResult.Cancel && LoginedUser.getInstance().UserGetSet.RememberMe == true)
                    {
                        System.Environment.Exit(1);
                    }
                }
            }
        }
        
        private void Delay_Tick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPassword.Text = "";
            lblMesaj.Text = "";
            //Delay.Enabled = false;
            this.Hide();
            if (mainForm.ShowDialog() == DialogResult.Cancel && LoginedUser.getInstance().UserGetSet.RememberMe == true)
            {
                this.Close();
            }
            else
            {
                LoginForm Frm = new LoginForm();
                if (Frm.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            pictureBox3.Hide();
            pictureBox5.Show();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            pictureBox5.Hide();
            pictureBox3.Show();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Add any initialization logic here if needed
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            register.ShowDialog();
            if (register.IsDisposed)
            {
                this.Show();
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            {
                string username = txtName.Text;
                string password = txtPassword.Text;
                bool rememberMe = RememberMe.Checked;
                for (int i = 0; i < userList.Count; i++)
                {
                    User user = userList[i];
                    if (user.IsValid(username, password, rememberMe) == true)
                    {
                        user.RememberMe = rememberMe;
                        LoginedUser.getInstance().UserGetSet = user;
                        lblMesaj.BackColor = Color.Green;
                        lblMesaj.Text = "successful login";
                        lblMesaj.Visible = true;
                        if (rememberMe == true)
                        {
                            Util.SaveCsv(userList, path);
                        }
                        return;
                    }
                }
                lblMesaj.BackColor = Color.Red;
                lblMesaj.Text = "failed login";
                lblMesaj.Visible = true;
                txtName.Text = "";
                txtPassword.Text = "";
            }
        }
    }
    public class LoginedUser
    {
        private static LoginedUser instance;
        public User UserGetSet { get; set; }

        private LoginedUser() { }

        public static LoginedUser getInstance()
        {
            if (instance == null)
            {
                instance = new LoginedUser();
            }
            return instance;
        }
    }
}
