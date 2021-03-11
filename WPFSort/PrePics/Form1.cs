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

namespace PrePics
{
    public partial class Form1 : Form
    {
        public Dictionary<string, List<string>> vehicleDictionary = new Dictionary<string, List<string>>();
        public string path = "./Pre";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 分析车号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text= "SortedPics" + string.Format("{0:D}", DateTime.Now);
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                return;
            }
            string vehicleArray = this.textBox1.Text;
            List<string> va = vehicleArray.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            listBox1.Items.AddRange(va.ToArray());

            //读取本地文件
            //生成虚拟目录
            //加1是为多余照片准备
            for (int i = 0; i < va.Count; i++)
            {
                List<List<string>> PicArray = ReadPic();//返回所有照片集合
                vehicleDictionary.Add(va[i], PicArray[i]);
                if (i == va.Count - 1)
                {
                    if (PicArray.Count - i >= 1)
                    {
                        listBox3.Items.Clear();
                        listBox3.Items.AddRange(PicArray[i + 1].ToArray());
                    }

                }
            }

            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;


        }

        /// <summary>
        /// 加入两张进当前选中文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox3.Items.Count > 1 && listBox1.SelectedItem != null)
            {
                string file_0 = listBox3.SelectedItem.ToString();
                string file_1 = listBox3.Items[listBox3.SelectedIndex + 1].ToString();
                string currentFolder = listBox1.SelectedItem.ToString();
                vehicleDictionary[currentFolder].Add(file_0);
                vehicleDictionary[currentFolder].Add(file_1);

                listBox2.Items.Clear();
                listBox2.Items.AddRange(vehicleDictionary[currentFolder].ToArray());

                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                listBox3.Items.RemoveAt(listBox3.SelectedIndex + 1);
                listBox3.SelectedIndex = 0;


            }
            else
            {
                MessageBox.Show("此功能仅能在车内照片数量>1时使用");
            }
        }

        /// <summary>
        /// 后悔重来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 应用分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            foreach (var item in vehicleDictionary)
            {
                string folder = "./Pre/" + item.Key;
                if (false == Directory.Exists(folder))
                {
                    //创建pic文件夹
                    Directory.CreateDirectory(folder);
                }
                foreach (var picFile in vehicleDictionary[item.Key])
                {
                    File.Copy("./Pre/" + picFile, folder + "/" + picFile);
                }

            }
            MessageBox.Show("完成");
            //新建文件夹

        }


        public List<List<string>> ReadPic()
        {
            List<List<string>> PicsArray = new List<List<string>>();
            List<string> Pics = new List<string>();
            //读取文件名
            DirectoryInfo folder = new DirectoryInfo(path);//目录信息
            foreach (FileInfo file in folder.GetFiles())
            {
                Pics.Add(file.Name); //获取每个文件的所有信息
            }

            //只弄9个车
            //1-14、15-28...一次类推加入Dictionary
            for (int j = 0; j < 9; j++)
            {
                PicsArray.Add(new List<string>());
                for (int i = 0; i < 14; i++)
                {
                    PicsArray[j].Add(Pics[i + j * 14]);
                }
            }
            //把最后多的都加进来
            PicsArray.Add(new List<string>());
            for (int i = 126; i < Pics.Count; i++)
            {
                PicsArray[9].Add(Pics[i]);
            }


            return PicsArray;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (listBox1.SelectedItem != null)
            {
                listBox2.Items.AddRange(vehicleDictionary[listBox1.SelectedItem.ToString()].ToArray());
            }


        }

        /// <summary>
        /// 显示在pictureBox上预览
        /// </summary>
        public void Reveal(string fileName)
        {
            pictureBox1.Image = Image.FromFile("./Pre/" + fileName);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Reveal(listBox2.SelectedItem.ToString());
            }

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                Reveal(listBox3.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// 打开资源文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Environment.CurrentDirectory);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                System.Diagnostics.Process.Start("explorer.exe", Environment.CurrentDirectory + "\\Pre\\" + listBox1.SelectedItem.ToString());

            }

        }
    }
}
