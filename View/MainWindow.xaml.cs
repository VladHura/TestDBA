using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestDBA.Models;
using System.Data.Common;

namespace TestDBA.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        
            InitializeComponent();
            DataContext = new ViewModel.MainViewModel();
        }
    }
}