using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Detecting
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier cascadeClassfier = new CascadeClassifier("face.xml");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog() {Multiselect = false,Filter = "JPEG|*.jpg"})
            {
                if (fd.ShowDialog()==DialogResult.OK)
                {
                    pic.Image = Image.FromFile(fd.FileName);
                    Bitmap bm = new Bitmap(pic.Image);
                    Image<Bgr,byte> grayImage = new Image<Bgr, byte>(bm);
                    Rectangle[] rect = cascadeClassfier.DetectMultiScale(grayImage, 1.4, 0);
                    foreach (Rectangle rectangle in rect)
                    {
                        using (Graphics gr = Graphics.FromImage(bm))
                        {
                            using(Pen pen= new Pen(Color.Red,1))
                            {
                               gr.DrawRectangle(pen, rectangle);
                            }
                        }
                    }
                    pic.Image = bm;
                }
            }
        }
    }
}
