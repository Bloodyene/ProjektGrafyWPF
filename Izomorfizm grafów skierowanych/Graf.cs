using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izomorfizm_grafów_skierowanych
{
    class Graf
    {
        public int[,] macierz_incydencji;
        List<Wierzchołek> lista_wierzcholkow;
        List<Tuple<int, int>> lista_krawedzi = new List<Tuple<int, int>>();
        public int ilosc_wierzcholkow;
        public int ilosc_krawedzi = 0;
        public Graf(int ilosc_wierzcholkow)
        {
            this.ilosc_wierzcholkow = ilosc_wierzcholkow;
            this.lista_wierzcholkow = new List<Wierzchołek>();
            for (int i = 0; i < ilosc_wierzcholkow; i++)
            {
                this.lista_wierzcholkow.Add(new Wierzchołek(i + 1));
            }
        }
        public void stworz_macierz()
        {
            int[,] macierz = new int[ilosc_wierzcholkow, ilosc_krawedzi];
            for (int i = 0; i < ilosc_wierzcholkow; i++)
            {
                for (int j = 0; j < ilosc_krawedzi; j++)
                {
                    macierz[i, j] = 0;
                }
            }
            for (int i = 0; i < lista_krawedzi.Count; i++)
            {
                macierz[lista_krawedzi[i].Item1-1, i] = 1;
                macierz[lista_krawedzi[i].Item2-1, i] = -1;
            }
            macierz_incydencji = macierz;
        }

        public void Dodaj_krawedz(int start, int stop)
        {
            lista_wierzcholkow[start - 1].lista_krawedzi_wychodzacych.Add(stop);
            lista_wierzcholkow[stop - 1].lista_krawedzi_wchodzacych.Add(start);
            ilosc_krawedzi++;
            lista_krawedzi.Add(new Tuple<int, int>(start, stop));
        }
    }
}
