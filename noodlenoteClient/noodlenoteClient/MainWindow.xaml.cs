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
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            updateBookList(new List<string> { "wtf1", "wtf2", "wtf3", "wtf4" });
        }



        private void updateBookList(List<string> books)
        {
            this.BookList.ItemsSource = books;

        }

        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("selected change");
        }
    }
}
