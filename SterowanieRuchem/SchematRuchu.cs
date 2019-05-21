using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie schematu wedłóg którego
     * generowane są pojazdy (ilść, źródła, cele)
     */
    class SchematRuchu
    {
        public List<int> ruch { get; set; }
        
        public SchematRuchu(List<int> ruch)
        {
            if (ruch.Count() != 24)
                throw new Exception("SchematRuchu musi zawierac 24 godziny");

            this.ruch = ruch;
        }
        public SchematRuchu(int ruch)
        {
            this.ruch = new List<int>();

            for (int i = 0; i < 24; i++)
            {
                this.ruch.Add(ruch);
            }
        }
        public SchematRuchu()
        {

        }

        public int PodajRuch(int godzina)
        {
            return ruch[godzina];
        }
        public void ZmianaRuchu(int godzina, int ruch)
        {
            this.ruch[godzina % 24] = ruch;
        }
    }
}
