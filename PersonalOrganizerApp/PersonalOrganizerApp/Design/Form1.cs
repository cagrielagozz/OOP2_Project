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

namespace PersonalOrganizerApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += TextBox_OnlyLetters_KeyPress;
            textBox2.KeyPress += TextBox_OnlyLetters_KeyPress;

            textBox3.KeyPress += TextBox_OnlyNumbers_KeyPress;
            textBox3.TextChanged += TextBox3_FormatPhoneNumber;
        }

        private void TextBox_OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBox_OnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) && textBox.Text.Count(char.IsDigit) >= 10)
            {
                e.Handled = true;
            }
        }
        private void TextBox3_FormatPhoneNumber(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            string digits = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (digits.Length > 10)
            {
                digits = digits.Substring(0, 10);
            }

            if (digits.Length > 0)
            {
                string formatted;
                if (digits.Length <= 3)
                {
                    formatted = $"({digits}";
                }
                else if (digits.Length <= 6)
                {
                    formatted = $"({digits.Substring(0, 3)}) {digits.Substring(3)}";
                }
                else if (digits.Length <= 8)
                {
                    formatted = $"({digits.Substring(0, 3)}) {digits.Substring(3, 3)} {digits.Substring(6)}";
                }
                else
                {
                    formatted = $"({digits.Substring(0, 3)}) {digits.Substring(3, 3)} {digits.Substring(6, 2)} {digits.Substring(8)}";
                }

                textBox.Text = formatted;
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
        public void SetTextBoxValues(string value1, string value2, string value3, string value4, string value5, string value6)
        {
            textBox1.Text = value1;
            textBox2.Text = value2;
            textBox3.Text = value3;
            textBox4.Text = value4;
            textBox5.Text = value5;
            textBox6.Text = value6;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string data1 = textBox1.Text;
            string data2 = textBox2.Text;
            string data3 = textBox3.Text;
            string data4 = textBox4.Text;
            string data5 = textBox5.Text;
            string data6 = textBox6.Text;

            string digits = new string(data3.Where(char.IsDigit).ToArray());
            if (digits.Length != 10)
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası girin (10 rakam).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!data6.EndsWith("@gmail.com") && !data6.EndsWith("@hotmail.com"))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csvler", "veriler.csv");


        string csvLine = $"{data1},{data2},{data3},{data4},{data5},{data6}";

            File.AppendAllText(filePath, csvLine + Environment.NewLine);

            MessageBox.Show("Veriler başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
