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
using System.Windows.Shapes;

namespace _2018_11_26_Autokereskedes.WPF
{
    /// <summary>
    /// Interaction logic for AutoReszletek.xaml
    /// </summary>
    public partial class AutoReszletek : Window
    {
        private int? id = null;

        public AutoReszletek()
        {
            InitializeComponent();
            cboTipus.ItemsSource = new AdatEleres().ListAutoTipusok();
            cboTipus.DisplayMemberPath = "Megnevezes";
            cboTipus.SelectedValuePath = "Id";

            cboUzemanyag.ItemsSource = (UzemanyagEnum[])Enum.GetValues(typeof(UzemanyagEnum));
        }

        public AutoReszletek(Auto auto): this()
        {
            id = auto.Id;
            txtRendszam.Text = auto.Rendszam;
            cboTipus.SelectedValue = auto.Tipus.Id;
            txtMotorSzam.Text = auto.Motorszam;
            txtAlvazSzam.Text = auto.Alvazszam;
            dpElsoForgHelyezes.SelectedDate = auto.ElsoForgalombaHelyezes;
            chbAutomata.IsChecked = auto.AutomataValto;
            txtKmOraAllas.Text = auto.KmOraAllas == null ? "" : auto.KmOraAllas.ToString();
            cboUzemanyag.SelectedValue = auto.Uzemanyag;

        }

        private Auto Begyujtes()
        {
            Auto auto = new Auto();
            if (this.id != null)
                auto.Id = (int)this.id;
            auto.Rendszam = txtRendszam.Text;
            auto.Tipus = (AutoTipus)cboTipus.SelectedItem;
            auto.Motorszam = txtMotorSzam.Text;
            auto.Alvazszam = txtAlvazSzam.Text;
            auto.ElsoForgalombaHelyezes = dpElsoForgHelyezes.SelectedDate;
            auto.AutomataValto = chbAutomata.IsChecked == true;
            try
            {
                auto.KmOraAllas = int.Parse(txtKmOraAllas.Text);
            }
            catch
            {
                auto.KmOraAllas = null;
            }
            auto.Uzemanyag = (UzemanyagEnum)cboUzemanyag.SelectedItem;
            
            return auto;
        }

        private bool kotelezoMezoEllenorzes()
        {
            if (string.IsNullOrEmpty(txtRendszam.Text))
            {
                MessageBox.Show("Kérem töltse ki a rendszámot");
                txtRendszam.Focus();
                return false;
            }
            if (cboTipus.SelectedValue == null)
            {
                MessageBox.Show("Kérem adja meg a típust");
                cboTipus.IsDropDownOpen = true;
                return false;
            }
            if (cboUzemanyag.SelectedValue == null)
            {
                MessageBox.Show("Kérem adja meg az üzemanyag fajtáját");
                cboUzemanyag.IsDropDownOpen = true;
                return false;
            }
            return true;
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (kotelezoMezoEllenorzes())
            {
                Auto auto = Begyujtes();
                if (this.id == null)
                    new AdatEleres().InsertAuto(auto);
                else
                    new AdatEleres().UpdateAuto(auto);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
