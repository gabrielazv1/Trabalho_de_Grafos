using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Grafo : IGrafo
    {
        public Dictionary<Vertice, List<Aresta>> DicGrafo { get; private set; }

        public Grafo()
        {
            DicGrafo = new Dictionary<Vertice, List<Aresta>>();
        }

        public bool AdicionarVertice(Vertice v)
        {
            if (!DicGrafo.ContainsKey(v))
            {
                DicGrafo[v] = new List<Aresta>();
                return true;
            }
            return false;
        }

        public bool AdicionarAresta(Aresta a)
        {
            if (!DicGrafo.ContainsKey(a.Origem) || !DicGrafo.ContainsKey(a.Destino))
                return false;

            DicGrafo[a.Origem].Add(a);
            return true;
        }

        public void TrocaDoisVertices(Vertice v1, Vertice v2)
        {
            if (!DicGrafo.ContainsKey(v1) || !DicGrafo.ContainsKey(v2))
                return;

            var temp = DicGrafo[v1];
            DicGrafo[v1] = DicGrafo[v2];
            DicGrafo[v2] = temp;
        }
    }

}
