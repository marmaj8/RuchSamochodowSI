using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Klasa sterująca wykonywanymi symulacjami ruchu
     */
    class Emulator
    {
        Mapa mapa;                      // mapa skrzyzowan z ich polaczeniami
        Symulacja symulacja;            // symulacja z mozliwym SI
        Symulacja kontrolna;            // symulacja bez SI
        Czas czas;                      // 24 godzinny zegar symulacji
        DaneORuchu daneORuchu;          // dane o zajerejstrowanym ruchu podczas symulacji z mozliwym SI
        DaneORuchu daneORuchuKontrolne; // dane o zajerejstrowanym ruchu podczas symuylacji kontrolnej
        SterowanieSi si;                // moduł sztucznej inteligencji do sterowania ruchem

        public int Pojazdy { get; set; } = 0;   // laczna ilosc pojazdow wprowadzonych do ruchu

        int tryb;       // 0 - z kontrolona / 1 - pojedyncza

        public Emulator()
        {
            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            daneORuchuKontrolne = new DaneORuchu(czas);
            mapa = new Mapa();
            si = new SterowanieSi();

            tryb = 0;
        }
        
        // zaladowanie probnych ustawien emulatora
        public void EmulatorTestowy()
        {
            mapa.MapaTestowa();

            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            daneORuchuKontrolne = new DaneORuchu(czas);

            symulacja = new Symulacja(czas, daneORuchu, daneORuchu, mapa, si);
            kontrolna = new Symulacja(czas, daneORuchuKontrolne, daneORuchuKontrolne, mapa);

            mapa.PrzekazListePolaczenDoBazy(daneORuchu);
            mapa.PrzekazListePolaczenDoBazy(daneORuchuKontrolne);
            si = new SterowanieSi();
        }

        public void ZaladujMapeZPliku(string plik)
        {
            using (StreamReader r = new StreamReader(plik))
            {
                string json = r.ReadToEnd();
                mapa = JsonConvert.DeserializeObject<Mapa>(json);
            }

            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            daneORuchuKontrolne = new DaneORuchu(czas);

            symulacja = new Symulacja(czas, daneORuchu, daneORuchu, mapa, si);
            kontrolna = new Symulacja(czas, daneORuchuKontrolne, daneORuchuKontrolne, mapa);

            mapa.PrzekazListePolaczenDoBazy(daneORuchu);
            mapa.PrzekazListePolaczenDoBazy(daneORuchuKontrolne);
            si = new SterowanieSi();
        }
        public void ZapiszMapeDoPliku(string plik)
        {
            using (StreamWriter file = File.CreateText(plik))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, mapa);
            }
        }

        public void ZaladujSi(string plik)
        {
            si.ZaladujSiecZPliku(plik);
        }

        public void ZapiszSi(string plik)
        {
            si.ZapiszSiecDoPliku(plik);
        }

        // wybor trybu pracy emulatora
        public void UstawTryb(int tryb)
        {
            if (tryb > 0)
                tryb = 1;
            else
                tryb = 0;
        }
        
        // emulacja ruchu trwajaca 1 godzine
        public void EmulacjaGodziny()
        {
            int godzina = czas.godzin;
            
            while (czas.godzin == godzina)
            {
                if (tryb == 0)
                    kontrolna.Symuluj();
                symulacja.Symuluj();

                List<Trasa> trasy = mapa.GenerujTrasy(godzina);
                foreach (Trasa trasa in trasy)
                {
                    Pojazdy++;
                    if (tryb == 0)
                        kontrolna.GenerujPojazd(trasa);
                    symulacja.GenerujPojazd(trasa);
                }
                daneORuchu.UsunStare();
                czas.UplywCzasu();
            }

            daneORuchu.ArchiwizujAktualne();
            daneORuchuKontrolne.ArchiwizujAktualne();
        }

        // przygotowanie modelu do wyswietlenia danych o zajerejstrowanym ruchu
        public List<DaneDoWyswietlenia> PrzygotujDaneDoWyswieltenia()
        {
            return daneORuchu.PrzgotujDaneDoWyswieltenia();
        }

        // przygotowanie danych w modelu do wyswietlenia danych o zajerejstrowanym ruchu
        public List<DaneDoWyswietlenia> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlenia> dane)
        {
            List<DaneDoWyswietlenia> wyswietl = daneORuchu.AktualizujDaneDoWyswieltenia(dane, false);

            if (tryb == 0)
                wyswietl = daneORuchuKontrolne.AktualizujDaneDoWyswieltenia(wyswietl, true);
            else
                wyswietl = daneORuchu.AktualizujDaneDoWyswieltenia(wyswietl, false);

            return wyswietl;
            //return daneORuchuKontrolne.AktualizujDaneDoWyswieltenia(dane, true);
        }

        // wlaczenie sterowania SI na wybranym skrzyzowaniu
        public void WlaczSi(int id)
        {
            symulacja.WlaczSi(id);
        }

        // wylaczenie sterowania SI na wybranym skrzyzowaniu
        public void WylaczSi(int id)
        {
            symulacja.WylaczSi(id);
        }
    }
}
