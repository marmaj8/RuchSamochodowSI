using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        BackgroundWorker wTle;
        Boolean pracuj;
        Boolean zmieniono;
        int kolumna;

        public MainWindow()
        {
            emul = new Emulator();
            emul.UstawTrybKontrolny(true);
            emul.EmulatorTestowy();

            //List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            dane = emul.PrzygotujDaneDoWyswieltenia();

            InitializeComponent();

            wyniki.ItemsSource = dane;


            kolumna = 0;
            pracuj = false;
            zmieniono = false;

            wTle = new BackgroundWorker();
            wTle.DoWork += new DoWorkEventHandler(wTle_DoWork);
            wTle.ProgressChanged += new ProgressChangedEventHandler(wTle_ProgressChanged);
            wTle.WorkerReportsProgress = true;
            
            wTle.RunWorkerAsync();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            btnKrok.IsEnabled = false;
            btnWczytajMape.IsEnabled = false;
            btnWczytajSi.IsEnabled = false;
            btnZapiszSi.IsEnabled = false;
            chboxTryb.IsEnabled = false;
            
            foreach(DaneDoWyswietlenia d in dane)
            {
                d.MozliwePrzestawianie = false;
            }
            wyniki.ItemsSource = dane;
            wyniki.Items.Refresh();
            
            pracuj = true;
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            pracuj = false;
        }
        private void Button_Krok(object sender, RoutedEventArgs e)
        {
            emul.EmulacjaGodziny();
            //List<DaneDoWyswietlenia> dane = emul.PrzygotujDaneDoWyswieltenia();
            dane = emul.AktualizujDaneDoWyswieltenia(dane);

            wyniki.ItemsSource = dane;
            wyniki.Items.Refresh();

            ZmienKolumne();

            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;
        }

        private void Button_Wczytaj_Mape(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Mapy Skrzyżowań (*.jsonMap)|*.jsonMap|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                emul.ZaladujMapeZPliku(openFileDialog.FileName);
                dane = emul.PrzygotujDaneDoWyswieltenia();
                wyniki.ItemsSource = dane;
                wyniki.Items.Refresh();
            }
            kolumna = 0;
        }

        private void Button_Wczytaj_Si(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Sieci Neuronowe (*.nnsi)|*.nnsi|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                emul.ZaladujSi(openFileDialog.FileName);
            }
        }

        private void Button_Zapisz_Si(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Sieci Neuronowe (*.nnsi)|*.nnsi";
            if (saveFileDialog.ShowDialog() == true)
            {
                emul.ZapiszSi(saveFileDialog.FileName);
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
            emul.UstawTrybKontrolny(true);
        }

        private void Tryb_Kontrolny_Uncheck(object sender, RoutedEventArgs e)
        {
            emul.UstawTrybKontrolny(false);
        }


        private void wTle_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (pracuj)
                {
                    emul.EmulacjaGodziny();
                    zmieniono = true;
                }

                wTle.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void wTle_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dane = emul.AktualizujDaneDoWyswieltenia(dane);

            lbPojazdow.Content = "Pojazdow: " + Pojazd.nastepnaRejestracja;
            if (!pracuj)
            {
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
                btnKrok.IsEnabled = true;
                btnWczytajMape.IsEnabled = true;
                btnWczytajSi.IsEnabled = true;
                btnZapiszSi.IsEnabled = true;
                chboxTryb.IsEnabled = true;

                foreach (DaneDoWyswietlenia d in dane)
                {
                    d.MozliwePrzestawianie = true;
                }
                zmieniono = true;
            }
            if (zmieniono)
            {
                wyniki.ItemsSource = dane;
                wyniki.Items.Refresh();

                ZmienKolumne();
                zmieniono = false;
            }
        }

        // Podswietlenie kolumny
        private void ZmienKolumne()
        {
            kolumna++;
            kolumna = kolumna % 24;
        }
    }
}
