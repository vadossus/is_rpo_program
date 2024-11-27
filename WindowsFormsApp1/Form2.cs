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
using System.Security.Authentication.ExtendedProtection.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public string name_emp;
        public string logins;
        public string seller_or_pokypatel;
        public string pass;

        public Form2()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
            form.FormClosed += (s, args) => this.Close();
            this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string passwordErr = Proverka(textBox2.Text);
                if (string.IsNullOrEmpty(passwordErr))
                {
                    Proverka_dannix(textBox1.Text, textBox2.Text);
                }
                else
                {
                    MessageBox.Show(passwordErr, "Уведомление");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели логин или пароль!", "Уведомление");
            }
        }

        public void Proverka_dannix(string text, string text2)
        {
            string path = @"C:\Users\Vadim\source\repos\WindowsFormsApp1\bin\Debug\dannie.txt";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);

                bool proverka = false;


                for (int i = 0; i < lines.Length; i += 5)
                {
                    if (i + 4 < lines.Length)
                    {
                        string login = lines[i].Trim();
                        string password = lines[i + 1].Trim();
                        string name = lines[i + 4].Trim();

                        name_emp = name;
                        logins = login;
                        seller_or_pokypatel = name;
                        pass = password;

                        if (login == textBox1.Text && password == textBox2.Text)
                        {
                            proverka = true;
                            break;
                        }
                        
                    }
                }

                if (proverka)
                {
                    if (logins.Contains(pass))
                    {
                        MessageBox.Show("Логин и пароль не должны быть одинаковы","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (seller_or_pokypatel == "Продавец")
                        {
                            MainForm form = new MainForm();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MainForm2 form = new MainForm2();
                            form.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Уведомление");
                }

            }
        }

        

        public string Proverka(string pass)
        {
            if (pass.Length < 8)
            {
                return "Пароль не должен содержать не менее 8 символов";
            }

            if (pass.Contains(" "))
            {
                return "Пароль не должен содержать пробелы.";
            }

            if (!Regex.IsMatch(pass, @"[a-z]") || !Regex.IsMatch(pass, @"[A-Z]"))
            {
                return "Пароль должен содержать буквы разного регистра";
            }

            if (!Regex.IsMatch(pass, @"[!\№;@%:?]"))
            {
                return "Пароль должен содержать спецсимволы (!(слеш символ)№;@%:?).";
            }

            if (!Regex.IsMatch(pass, "[0-9]"))
            {
                return "Пароль должен содержать цифры.";
            }

            return string.Empty;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
