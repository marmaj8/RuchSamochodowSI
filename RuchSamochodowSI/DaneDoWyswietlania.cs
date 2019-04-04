using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class DaneDoWyswietlania
    {
        public int Poczatek { get; set; }
        public int Koniec { get; set; }
        public string Kierunek { get;} = "->";
        public Boolean SI { get; set; } = false;

        public double g1 { get; set; }
        public double g2 { get; set; }
        public double g3 { get; set; }
        public double g4 { get; set; }
        public double g5 { get; set; }
        public double g6 { get; set; }
        public double g7 { get; set; }
        public double g8 { get; set; }
        public double g9 { get; set; }
        public double g10 { get; set; }
        public double g11 { get; set; }
        public double g12 { get; set; }
        public double g13 { get; set; }
        public double g14 { get; set; }
        public double g15 { get; set; }
        public double g16 { get; set; }
        public double g17 { get; set; }
        public double g18 { get; set; }
        public double g19 { get; set; }
        public double g20 { get; set; }
        public double g21 { get; set; }
        public double g22 { get; set; }
        public double g23 { get; set; }
        public double g24 { get; set; }


        public void UstawSrednia(double srednia, int godzina)
        {
            switch(godzina)
            {
                case 1:
                    g1 = srednia;
                    break;
                case 2:
                    g2 = srednia;
                    break;
                case 3:
                    g3 = srednia;
                    break;
                case 4:
                    g4 = srednia;
                    break;
                case 5:
                    g5 = srednia;
                    break;
                case 6:
                    g6 = srednia;
                    break;
                case 7:
                    g7 = srednia;
                    break;
                case 8:
                    g8 = srednia;
                    break;
                case 9:
                    g9 = srednia;
                    break;
                case 10:
                    g10 = srednia;
                    break;
                case 11:
                    g11 = srednia;
                    break;
                case 12:
                    g12 = srednia;
                    break;
                case 13:
                    g13 = srednia;
                    break;
                case 14:
                    g14 = srednia;
                    break;
                case 15:
                    g15 = srednia;
                    break;
                case 16:
                    g16 = srednia;
                    break;
                case 17:
                    g17 = srednia;
                    break;
                case 18:
                    g18 = srednia;
                    break;
                case 19:
                    g19 = srednia;
                    break;
                case 20:
                    g20 = srednia;
                    break;
                case 21:
                    g21 = srednia;
                    break;
                case 22:
                    g22 = srednia;
                    break;
                case 23:
                    g23 = srednia;
                    break;
                case 24:
                    g24 = srednia;
                    break;
            }
        }

        public double max()
        {
            double max =  g1;
            if (g2 > max)
                max = g2;
            if (g3 > max)
                max = g3;
            if (g4 > max)
                max = g4;
            if (g5 > max)
                max = g5;
            if (g6 > max)
                max = g6;
            if (g7 > max)
                max = g7;
            if (g8 > max)
                max = g8;
            if (g9 > max)
                max = g9;
            if (g10 > max)
                max = g10;
            if (g11 > max)
                max = g11;
            if (g12 > max)
                max = g12;

            return max;
        }
    }
}
