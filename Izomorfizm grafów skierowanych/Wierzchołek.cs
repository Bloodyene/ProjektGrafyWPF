using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izomorfizm_grafów_skierowanych 
{
    class Wierzchołek
    {
        int etykieta;
        public List<int> lista_krawedzi_wchodzacych;
        public List<int> lista_krawedzi_wychodzacych;
        public Wierzchołek(int etykieta)
        {
            this.etykieta = etykieta;
            lista_krawedzi_wchodzacych = new List<int>();
            lista_krawedzi_wychodzacych = new List<int>();
        }
    }
}
