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
        public Dictionary<string, List<string>> vehicleSpiltsDictionary = new Dictionary<string, List<string>>();
        public List<int> count4Spilt = new List<int>();
        public string path = "./Pre";
        public class SingleVehicle
        {
            public string Number { get; set; }
            public bool TypeC { get; set; }
            public List<string> Pics { get; set; } = new List<string>();
            public int Amount { get; set; } = 14;

        }
        public List<SingleVehicle> vehiclesList = new List<SingleVehicle>();
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
            checkedListBox1.Items.Clear();
            button4.Enabled = true;
            checkedListBox1.Enabled = true;
            isAfter.Enabled = false;


            //按照片数量分割出照片档位
            //新建文件夹
            //textBox2.Text = "SortedPics" + string.Format("{0:D}", DateTime.Now);
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("啥都没有！！");
                return;
            }
            string vehicleArray = this.textBox1.Text;
            //得出车号的List
            /*
            | 车号（举例）  |
            | ------- |
            | 3315676 |
            | 3467947 |
            | 1660172 |
            | 3422879 |
            | 3422887 |
            | 3467827 |
            | 5498405 |
            | 3428638 |
            | 5497681 |
             */
            List<string> VPlan = vehicleArray.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //建立车模型
            foreach (var planNum in VPlan)
            {
                if (planNum.Substring(0, 1) == "1" || planNum.Substring(0, 1) == "4")
                {
                    vehiclesList.Add(new SingleVehicle { Number = planNum, TypeC = true, Amount = 12 });

                }
                else
                {
                    vehiclesList.Add(new SingleVehicle { Number = planNum, TypeC = false, Amount = 14 });
                }
            }

            //将照片加载到Pics集合
            List<string> Pics = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(path);//目录信息
            foreach (FileInfo file in folder.GetFiles())
            {
                Pics.Add(file.Name);//读取文件名
            }

            //将照片加入到车Model中
            //计算有多少TypeC的车
            int AmountOfTypeC = vehiclesList.Where(x => x.TypeC == true).Count();
           
            if (isAfter.Checked == true)//是否后置
            {
                for (int i = 0; i < vehiclesList.Count; i++)
                {
                    
                    vehiclesList[i].Pics.AddRange(Pics.GetRange(0, vehiclesList[i].Amount));
                    Pics.RemoveRange(0, vehiclesList[i].Amount);
                }
                //剩下的放进那两个车里
                foreach (var item in vehiclesList.Where(x => x.TypeC == true))
                {
                    item.Pics.AddRange(Pics.GetRange(0, 2));
                    Pics.RemoveRange(0, 2);
                }
                if(Pics.Count>0)
                {
                    MessageBox.Show("咋多出_" + Pics.Count+ "_张照片");
                }
            }
            else
            {
                //pics的起始位改为Pics[AmountOfTypeC*2]
                List<string> rest = Pics.GetRange(0, AmountOfTypeC * 2);
                Pics.RemoveRange(0, AmountOfTypeC * 2);
                //挨个放进去
                for (int i = 0; i < vehiclesList.Count; i++)
                {

                    vehiclesList[i].Pics.AddRange(Pics.GetRange(0, vehiclesList[i].Amount));
                    Pics.RemoveRange(0, vehiclesList[i].Amount);
                }
                //剩下的放进那两个车里
                foreach (var item in vehiclesList.Where(x => x.TypeC == true))
                {
                    item.Pics.AddRange(rest.GetRange(0, 2));
                    rest.RemoveRange(0, 2);
                }
                if (Pics.Count > 0)
                {
                    MessageBox.Show("咋多出_" + Pics.Count + "_张照片");
                }

            }

            //加入完毕现在要显示了
            foreach (var item in vehiclesList)
            {
                checkedListBox1.Items.Add(item.Number, item.TypeC);
            }

            //按照用户显示的数分割照片
            //读取本地文件
            /*
            | 车号（举例）  |
            | ------- |
            | IMG_3422887.JPG |
            | IMG_4678271.JPG |
            | IMG_5498405.JPG |
            | IMG_3428638.JPG |
            | IMG_5497681.JPG |
             */
            /*
            

            

            //剩下的照片集合
            var RestPics = Pics.GetRange(count4Spilt.Sum(), Pics.Count - count4Spilt.Sum());

            //自动将余下照片加入到虚拟目录集合
            //如果用户觉得不对，还要调整
            for (int i = 0; i < count4Spilt.Count; i++)
            {
                if(count4Spilt[i]==12)
                {
                    vehicleDictionary[VPlan[i]].Add(RestPics[0]);
                    vehicleDictionary[VPlan[i]].Add(RestPics[1]);
                }
                

            }

            */



        }

        /// <summary>
        /// 加入两张进当前选中文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {






            if (listBox3.Items.Count > 1 && checkedListBox1.SelectedItem != null)
            {
                string file_0 = listBox3.SelectedItem.ToString();
                string file_1 = listBox3.Items[listBox3.SelectedIndex + 1].ToString();
                string currentFolder = checkedListBox1.SelectedItem.ToString();
                vehicleSpiltsDictionary[currentFolder].Add(file_0);
                vehicleSpiltsDictionary[currentFolder].Add(file_1);

                listBox2.Items.Clear();
                listBox2.Items.AddRange(vehicleSpiltsDictionary[currentFolder].ToArray());

                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                listBox3.Items.RemoveAt(listBox3.SelectedIndex + 1);
                if (listBox3.Items.Count > 0)
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
            //此功能正在开发
        }

        /// <summary>
        /// 应用分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            foreach (var item in vehiclesList)
            {
                string folder = "./Pre/" + item.Number;
                if (false == Directory.Exists(folder))
                {
                    //创建pic文件夹
                    Directory.CreateDirectory(folder);
                }
                foreach (var picFile in item.Pics)
                {
                    File.Move("./Pre/" + picFile, folder + "/" + picFile);
                }

            }
            MessageBox.Show("完成");
            //新建文件夹

        }

        //传入车号
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


            //按传进车号Count进行for循环


            //1-14、15-28...以此类推加入Dictionary
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reavl切换
            listBox2.Items.Clear();
            if (checkedListBox1.SelectedItem != null)
            {
                listBox2.Items.AddRange(vehiclesList.Single(x=>x.Number==checkedListBox1.SelectedItem.ToString()).Pics.ToArray());

            }
            if (listBox2.Items.Count != 0)
                listBox2.SelectedIndex = 0;

        }

        /// <summary>
        /// 显示在pictureBox上预览
        /// </summary>
        public void Reveal(string fileName)
        {
            //pictureBox1.Image = Image.FromFile("./Pre/" + fileName);
            pictureBox1.Image = new Bitmap("./Pre/" + fileName);

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



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    button2_Click(this, EventArgs.Empty);
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("使用方法：请查看使用视频");
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            //更新数据集合
            if (checkedListBox1.Items.Count >= 9)
            {
                button2.Enabled = true;
            }



        }

    }
}
