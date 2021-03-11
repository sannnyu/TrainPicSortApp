using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void numberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

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
            List<string> va = vehicleArray.Split(new char[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries).ToList();

            //将车号逐个添加到ListView

            //将ListViewItem车号作为字典，对应
            //Dictionary<int, List<string>> vehicleSingle = new Dictionary<int, List<string>>();
            //使用Dictionary对应文件名


            //ListViewItem写一个事件，点一下就换到这边来。

            //并且显示图片，动态加载Image
        }

        //加入两张的功能
    }
}
