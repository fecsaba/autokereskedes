using _2018_11_05_AutoKereskedes;
using _2018_11_05_AutoKereskedes.POCO;
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

namespace _2018_11_26_Autokereskedes.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dgLista.ItemsSource = new AdatEleres().ListAutok();
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            AutoReszletek autoReszletek = new AutoReszletek();
            if (autoReszletek.ShowDialog() == true)
            {
                dgLista.ItemsSource = new AdatEleres().ListAutok();
            }
        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null)
            {
                Auto modositando = (Auto)dgLista.SelectedItem;
                AutoReszletek autoReszletek = new AutoReszletek(modositando);
                if (autoReszletek.ShowDialog() == true)
                {
                    dgLista.ItemsSource = new AdatEleres().ListAutok();
                }
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null)
            {
                Auto torlendo = (Auto)dgLista.SelectedItem;
                if (MessageBox.Show("Biztosan törölni szeretnéd?","Kérdés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    new AdatEleres().DeleteAuto(torlendo);
                    dgLista.ItemsSource = new AdatEleres().ListAutok();
                }
            }
        }
    }
}
