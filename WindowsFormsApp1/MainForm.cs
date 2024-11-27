using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private FileSystemWatcher fileWatcher;
        public MainForm()
        {
            InitializeComponent();
            LoadData();
            SetupFileWatcher();
            InitializeDataGridViews();
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }

        private void LoadData()
        {
            flowLayoutPanel1.Controls.Clear();

            string directoryPath = @"C:\Users\Vadim\source\repos\WindowsFormsApp1\bin\Debug";
            string textFilePath = Path.Combine(directoryPath, "list_data_products.txt");

            if (File.Exists(textFilePath))
            {
                string[] lines = File.ReadAllLines(textFilePath);

                for (int i = 0; i < lines.Length; i += 4)
                {
                    if (i + 3 < lines.Length)
                    {
                        string label1Text = lines[i];
                        string label2Text = lines[i + 1];
                        string label3Text = lines[i + 2];
                        string imagePath = lines[i + 3];

                        CreateContainer(label1Text, label2Text, label3Text, imagePath);
                    }
                }
            }
        }

        private void CreateContainer(string label1Text, string label2Text, string label3Text, string imagePath)
        {
            Panel container = new Panel
            {
                Width = 200,
                Height = 250,
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(imagePath),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 180,
                Height = 120,
                Location = new Point(10, 10)
            };

            Label label1 = new Label { Text = "Товар: " + label1Text, AutoSize = true, Location = new Point(10, 140) };
            Label label2 = new Label { Text = "Цена: " + label2Text, AutoSize = true, Location = new Point(10, 160) };
            Label label3 = new Label { Text = "ИП: " + label3Text, AutoSize = true, Location = new Point(10, 180) };

            Button button = new Button
            {
                Text = "Добавить",
                Width = 100,
                Height = 30,
                Location = new Point(50, 210)
            };

            container.Controls.Add(pictureBox);
            container.Controls.Add(label1);
            container.Controls.Add(label2);
            container.Controls.Add(label3);
            container.Controls.Add(button);

            flowLayoutPanel1.Controls.Add(container);
        }

        private void SetupFileWatcher()
        {
            string directoryPath = @"C:\Users\Vadim\source\repos\WindowsFormsApp1\bin\Debug";
            string textFilePath = Path.Combine(directoryPath, "list_data_products.txt");

            fileWatcher = new FileSystemWatcher(directoryPath, "list_data_products.txt");
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += OnFileChanged;
            fileWatcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            fileWatcher.EnableRaisingEvents = false;

            this.Invoke((MethodInvoker) delegate
            {
                LoadData();
            });

            fileWatcher.EnableRaisingEvents = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                button1.Visible = false;
            }
            else
            {
                button1.Visible = true;
            }

            if (tabControl1.SelectedIndex != 2)
            {
                button2.Visible = false;
            }
            else
            {
                button2.Visible = true;
            }
        }

        private void InitializeDataGridViews()
        {
            // Заказы
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Название", HeaderText = "Название" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Цена", HeaderText = "Цена" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Пункт выдачи", HeaderText = "Пункт выдачи" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Количество", HeaderText = "Количество" });
            tabPage2.Controls.Add(dataGridView1);

            // Склад
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Название", HeaderText = "Название" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Количество", HeaderText = "Количество" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Пункт выдачи", HeaderText = "Пункт выдачи" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Склад", HeaderText = "Склад" });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn { Name = "Транспорт", HeaderText = "Транспорт" });
            tabPage3.Controls.Add(dataGridView2);

            // Финансы 
            dataGridView3.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID" });
            dataGridView3.Columns.Add(new DataGridViewTextBoxColumn { Name = "Название", HeaderText = "Название" });
            dataGridView3.Columns.Add(new DataGridViewTextBoxColumn { Name = "Количество", HeaderText = "Количество" });
            dataGridView3.Columns.Add(new DataGridViewTextBoxColumn { Name = "Сумма", HeaderText = "Сумма" });
            tabPage4.Controls.Add(dataGridView3);
        }

        
    }
}
