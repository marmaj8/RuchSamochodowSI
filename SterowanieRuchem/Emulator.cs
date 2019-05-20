using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class Emulator
    {
        Mapa mapa;
        Symulacja kontrolna;
        Symulacja symulacja;
        Czas czas;
        DaneORuchu daneORuchu;
        DaneORuchu daneORuchuKontrolne;
        SterowanieSi si;

        public int Pojazdy { get; set; } = 0;

        int tryb;       // 0 - z kontrolona / 1 - pojedyncza

        public Emulator()
        {
            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            daneORuchuKontrolne = new DaneORuchu(czas);
            mapa = new Mapa(daneORuchu);
            si = new SterowanieSi();
            kontrolna = new Symulacja(daneORuchuKontrolne, daneORuchuKontrolne, true);
            symulacja = new Symulacja(daneORuchu, daneORuchuKontrolne, si);
            tryb = 0;
        }

        public void EmulatorTestowy()
        {
            mapa.MapaTestowa();
            kontrolna.SymulacjaTestowa();
            symulacja.SymulacjaTestowa();

            mapa.PrzekazListePolaczenDoBazy(daneORuchu);
            mapa.PrzekazListePolaczenDoBazy(daneORuchuKontrolne);
            
        }

        public void UstawTryb(int tryb)
        {
            if (tryb > 0)
                tryb = 1;
            else
                tryb = 0;
        }

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

        public List<DaneDoWyswietlenia> PrzygotujDaneDoWyswieltenia()
        {
            return daneORuchu.PrzgotujDaneDoWyswieltenia();
        }
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

        public void WlaczSi(int id)
        {
            symulacja.WlaczSi(id);
        }
        public void WylaczSi(int id)
        {
            symulacja.WylaczSi(id);
        }
    }
}
