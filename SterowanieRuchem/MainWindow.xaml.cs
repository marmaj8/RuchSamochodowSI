using Microsoft.Win32;
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

        private void Button_Start(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Krok(object sender, RoutedEventArgs e)
        {
            emul.EmulacjaGodziny();
            //List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            dane = emul.AktualizujDaneDoWyswieltenia(dane);

            wyniki.ItemsSource = dane;
            wyniki.Items.Refresh();

            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;

        }

        private void Button_Wczytaj_Mape(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                emul.ZaladujMapeZPliku(openFileDialog.FileName);
                dane = emul.PrzygotujDaneDoWyswieltenia();
                wyniki.ItemsSource = dane;
                wyniki.Items.Refresh();
            }
        }

        private void Button_Wczytaj_Si(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                emul.ZaladujSi(openFileDialog.FileName);
            }
        }

        private void Button_Zapisz_Si(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                emul.ZapiszSi(openFileDialog.FileName);
            }
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

        private void Tryb_Kontrolny_Check(object sender, RoutedEventArgs e)
        {
            emul.UstawTryb(0);
        }

        private void Tryb_Kontrolny_Uncheck(object sender, RoutedEventArgs e)
        {
            emul.UstawTryb(1);
        }
    }
}
