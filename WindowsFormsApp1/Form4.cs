using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 :Form
    {
        private string file_path;
        public Form4 ()
        {
            InitializeComponent( );
        }

        private void button1_Click (object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog( );
            ofd.InitialDirectory = "C:\\";
            ofd.Filter = "png,jpg,jpeg,gif файлы (*.png,*.jpg,*.jpeg,*.gif)|*.png,*.jpg,*.jpeg,*.gif|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file_path = ofd.FileName;
                pictureBox1.Image = Image.FromFile(file_path);
            }
        }

        private void button2_Click (object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                string directoryPath = @"C:\Users\Vadim\source\repos\WindowsFormsApp1\bin\Debug";

                string textFilePath = Path.Combine(directoryPath, "list_data_products.txt");
                using (StreamWriter sw = new StreamWriter(textFilePath, append: true))
                {
                    sw.WriteLine(textBox1.Text);
                    sw.WriteLine(textBox2.Text);
                    sw.WriteLine(textBox3.Text);

                    ImageFormat format = ProverkaFormat(pictureBox1);
                    if (format != null)
                    {
                        string imageFileName = "saved_image_" + Guid.NewGuid().ToString() + "." + GetImageExtension(format);
                        string imageFilePath = Path.Combine(directoryPath, imageFileName);
                        pictureBox1.Image.Save(imageFilePath, format);

                        sw.WriteLine(imageFilePath);
                    }
                }

                MessageBox.Show("Данные были сохранены!");
            }
            else
            {
                MessageBox.Show("Изображение отсутствует, или были не заполнены поля","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ImageFormat ProverkaFormat(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                ImageFormat format = pictureBox.Image.RawFormat;
                if (format.Equals(ImageFormat.Png) || format.Equals(ImageFormat.Jpeg) || format.Equals(ImageFormat.Gif))
                {
                    return format;
                }
            }
            return null;
        }

        private string GetImageExtension(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Png))
            {
                return "png";
            }
            else if (format.Equals(ImageFormat.Jpeg))
            {
                return "jpg";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                return "gif";
            }
            return string.Empty;
        }
    }
}
