using MahApps.Metro.Controls;
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

namespace noodlenoteClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ApiManager _apiManager;
        private Book _currentBook;
        private Note _currentNote;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this._apiManager = new ApiManager();
        }


        private void Button_Ping_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this._apiManager.Ping().ToString());
        }


        private void Button_Init_Click(object sender, RoutedEventArgs e)
        {
            this._apiManager.InitBook();
        }
    }
}
