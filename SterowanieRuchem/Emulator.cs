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

        public int Pojazdy { get; private set; } = 0;   // laczna ilosc pojazdow wprowadzonych do ruchu

        Boolean trybKontrolny;              // true - 2 symulacje, false - 1 symulaja

        public Emulator()
        {
            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            daneORuchuKontrolne = new DaneORuchu(czas);
            mapa = new Mapa();
            si = new SterowanieSi();

            trybKontrolny = true;
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

        /*
        public void ZapiszMapeDoPliku(string plik)
        {
            using (StreamWriter file = File.CreateText(plik))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, mapa);
            }
        }
        */

        public void ZaladujSi(string plik)
        {
            si.ZaladujSiecZPliku(plik);
        }

        public void ZapiszSi(string plik)
        {
            si.ZapiszSiecDoPliku(plik);
        }

        // wybor trybu pracy emulatora
        public void UstawTrybKontrolny(Boolean tryb)
        {
            trybKontrolny = tryb;
        }

        // emulacja ruchu trwajaca 1 godzine
        public void EmulacjaGodziny()
        {
            int godzina = czas.godzin;
            List<Trasa> trasy = mapa.GenerujTrasy(godzina);
            double pps = trasy.Count() / 3600;
            int odstep;
            int ile;
            int minelo = 0;

            if (pps >= 1)
            {
                odstep = 1;
                ile = (int)pps;
            }
            else
            {
                odstep = (int) (1 / pps);
                ile = 1;
            }

            while (czas.godzin == godzina)
            {
                if (trybKontrolny)
                    kontrolna.Symuluj();
                symulacja.Symuluj();

                if(minelo >= odstep)
                {
                    for(int i = 0; i < ile; i++)
                    {
                        if (trasy.Count() > 0)
                        {
                            Pojazdy++;

                            if (trybKontrolny)
                                kontrolna.GenerujPojazd(trasy.First());
                            symulacja.GenerujPojazd(trasy.First());

                            trasy.RemoveAt(0);
                        }
                        else
                        {
                            break;
                        }
                    }
                    minelo = 0;
                }
                else
                {
                    minelo++;
                }
                daneORuchu.UsunStare();
                daneORuchuKontrolne.UsunStare();

                czas.UplywCzasu();
            }

            daneORuchu.ArchiwizujAktualne();
            daneORuchuKontrolne.ArchiwizujAktualne();

            if (trybKontrolny)
                si.UczZKontrolnymi(daneORuchuKontrolne, czas);
            else
                si.UczZPoprzedniaDoba(daneORuchu);
        }

        // emulacja ruchu trwajaca 1 godzine
        public void xxxEmulacjaGodziny()
        {
            int godzina = czas.godzin;
            
            while (czas.godzin == godzina)
            {
                if (trybKontrolny)
                    kontrolna.Symuluj();
                symulacja.Symuluj();

                List<Trasa> trasy = mapa.GenerujTrasy(godzina);
                foreach (Trasa trasa in trasy)
                {
                    Pojazdy++;
                    if (trybKontrolny)
                        kontrolna.GenerujPojazd(trasa);
                    symulacja.GenerujPojazd(trasa);
                }
                daneORuchu.UsunStare();
                daneORuchuKontrolne.UsunStare();

                czas.UplywCzasu();
            }

            daneORuchu.ArchiwizujAktualne();
            daneORuchuKontrolne.ArchiwizujAktualne();

            if (trybKontrolny)
                si.UczZKontrolnymi(daneORuchuKontrolne, czas);
            else
                si.UczZPoprzedniaDoba(daneORuchu);
        }

        // przygotowanie modelu do wyswietlenia danych o zajerejstrowanym ruchu
        public List<DaneDoWyswietlenia> PrzygotujDaneDoWyswieltenia()
        {
            return daneORuchu.PrzgotujDaneDoWyswieltenia();
        }

        // przygotowanie danych w modelu do wyswietlenia danych o zajerejstrowanym ruchu
        public List<DaneDoWyswietlenia> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlenia> dane)
        {

            if (trybKontrolny)
            {
                dane = daneORuchu.AktualizujDaneDoWyswieltenia(dane, false);
                dane = daneORuchuKontrolne.AktualizujDaneDoWyswieltenia(dane, true);
            }
            else
            {
                dane = daneORuchu.AktualizujDaneDoWyswieltenia(dane);
            }

            return dane;
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
