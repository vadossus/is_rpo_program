using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO ;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            form.FormClosed += (s, args) => this.Close();
            this.Hide();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text) && comboBox1.SelectedIndex != -1)
            {
                MessageBox.Show("Вы успешно зарегистрировались", "Уведомление");
                Form2 form = new Form2();
                Method_Zapicat_v_File(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.SelectedItem.ToString());
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Вы не заполнили поля!", "Уведомление");
            }
        }

        public void Method_Zapicat_v_File(string text, string text2, string text3, string text4, string text5)
        {
            using (StreamWriter sw = new StreamWriter("dannie.txt", append: true))
            {
                sw.WriteLine(text);
                sw.WriteLine(text2);
                sw.WriteLine(text3);
                sw.WriteLine(text4);
                sw.WriteLine(text5);
            }
        }
    }

    
}
