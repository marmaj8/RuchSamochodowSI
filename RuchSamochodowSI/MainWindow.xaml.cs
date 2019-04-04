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

namespace RuchSamochodowSI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Emulator emul;

        public MainWindow()
        {
            emul = new Emulator();
            emul.UstawTryb(1);
            emul.EmulatorTestowy();

            List<DaneDoWyswietlania> dane;
            dane = emul.PrzygotujDaneDoWyswieltenia();
            
            InitializeComponent();

            wyniki.ItemsSource = dane;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            emul.EmulacjaGodziny();
            List<DaneDoWyswietlania> dane = emul.PrzygotujDaneDoWyswieltenia();

            wyniki.ItemsSource = dane;
            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 24; i++)
            {
                emul.EmulacjaGodziny();
            }
            List<DaneDoWyswietlania> dane = emul.PrzygotujDaneDoWyswieltenia();
            wyniki.ItemsSource = dane;
            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;
        }
    }
}
