using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSort
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, List<string>> vehicleSingle = new Dictionary<string, List<string>>();
        string path = "./Pre";
        public MainWindow()
        {
            InitializeComponent();

        }

        private void numberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem viewItem = this.numberList.SelectedItem as ListViewItem;
            if (viewItem != null)
            {
                foreach (var item in vehicleSingle[viewItem.Content.ToString()])
                {
                    Console.WriteLine(item);
                }
            }




        }

        //显示拍好的14张车号照片
        public void Display()
        {
            //Image image = new Image(new Uri(""));

        }

        private void GenBtn_Click(object sender, RoutedEventArgs e)
        {

            //读取车号
            string vehicleArray = this.textBox1.Text;
            //解析车号
            List<string> va = vehicleArray.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < va.Count; i++)
            {
                this.numberList.Items.Add(new ListViewItem() { Height = 50, Content = va[i] });
                List<List<string>> PicArray = ReadPic();//返回所有照片集合

                vehicleSingle.Add(va[i], PicArray[i]);
            }





            //int fileCount = 126;
            // string fileName = "";
            // for (int i = 0; i <= fileCount; i=i+14)
            // {
            //     //将文件前十四的都打包进来
            //     for (int j = 0; j < 14; j++)
            //     {
            //         vehicleSingle[0].Add(fileName);
            //     }
            // }




            //将车号逐个添加到ListView

            //将ListViewItem车号作为字典，对应
            //Dictionary<int, List<string>> vehicleSingle = new Dictionary<int, List<string>>();
            //使用Dictionary对应文件名


            //ListViewItem写一个事件，点一下就换到这边来。

            //并且显示图片，动态加载Image
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


            return PicsArray;

        }

        private void JoinNDel_Click(object sender, RoutedEventArgs e)
        {
            ReadPic();

        }


        //加入两张的功能
    }



    public class FileModel : INotifyPropertyChanged
    {
        private string file;
        public string File
        {
            get { return file; }
            set
            {
                file = value;
                OnPropertyChanged("File");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
