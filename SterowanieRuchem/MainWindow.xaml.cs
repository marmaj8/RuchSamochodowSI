using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SterowanieRuchem
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Emulator emul;
        List<DaneDoWyswietlenia> dane;

        public MainWindow()
        {
            emul = new Emulator();
            emul.UstawTryb(1);
            emul.EmulatorTestowy();

            //List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            dane = emul.PrzygotujDaneDoWyswieltenia();

            InitializeComponent();

            wyniki.ItemsSource = dane;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            emul.EmulacjaGodziny();
            //List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            dane = emul.AktualizujDaneDoWyswieltenia(dane);

            wyniki.ItemsSource = dane;
            wyniki.Items.Refresh();

            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                emul.EmulacjaGodziny();
            }
            List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            wyniki.ItemsSource = dane;
            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;
        }

        private void SI_Check(object sender, RoutedEventArgs e)
        {
            int koniec = Int32.Parse(((CheckBox)sender).Tag.ToString());

            foreach(DaneDoWyswietlenia d in dane)
            {
                if (d.Koniec == koniec)
                {
                    d.SI = true;
                }
            }
            emul.WlaczSi(koniec);
            wyniki.Items.Refresh();
        }

        private void SI_UnCheck(object sender, RoutedEventArgs e)
        {
            int koniec = Int32.Parse(((CheckBox)sender).Tag.ToString());

            foreach (DaneDoWyswietlenia d in dane)
            {
                if (d.Koniec == koniec)
                {
                    d.SI = false;
                }
            }
            emul.WylaczSi(koniec);
            wyniki.Items.Refresh();
        }
    }
}
