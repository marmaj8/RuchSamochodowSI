using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Emulator
    {
        Mapa mapa;
        Symulacja kontrolna;
        Symulacja symulacja;
        Czas czas;
        DaneORuchu daneORuchu;
        public int Pojazdy { get; set; } = 0;

        int tryb;       // 0 - z kontrolona / 1 - pojedyncza

        public Emulator()
        {
            czas = new Czas();
            daneORuchu = new DaneORuchu(czas);
            mapa = new Mapa(daneORuchu);
            kontrolna = new Symulacja(daneORuchu);
            symulacja = new Symulacja(daneORuchu);
            tryb = 0;
        }

        public void EmulatorTestowy()
        {
            mapa.MapaTestowa();
            kontrolna.SymulacjaTestowa();
            symulacja.SymulacjaTestowa();

            mapa.PrzekazListePolaczenDoBazy(daneORuchu);
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
            int godzina = czas.PodajGodzine();

            while( czas.PodajGodzine() == godzina)
            {
                if (tryb == 0)
                    kontrolna.Symuluj();
                symulacja.Symuluj();
                
                List<Trasa> trasy = mapa.GenerujTrasy(godzina);
                foreach(Trasa trasa in trasy)
                {
                    Pojazdy++;
                    if (tryb == 0)
                        kontrolna.GenerujPojazd(trasa);
                    symulacja.GenerujPojazd(trasa);
                }
                daneORuchu.UsunStare();
                czas.UplywSekund();
            }

            daneORuchu.ZapamietajWynikGodzinny();
        }

        public List<DaneDoWyswietlania> PrzygotujDaneDoWyswieltenia()
        {
            return daneORuchu.PrzgotujDaneDoWyswieltenia();
        }
        public void AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlania> dane)
        {
            daneORuchu.AktualizujDaneDoWyswieltenia(dane);
        }
        
    }
}
