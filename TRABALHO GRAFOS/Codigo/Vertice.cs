using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Vertice
    {
        public int id;
        public List<Aresta> arestas = new List<Aresta>();
        public List<Vertice> verticesAdjacentes = new List<Vertice>();

        public Vertice(int Id)
        {
            id = Id;
        }

        public static bool SaoAdjacentes(Vertice v1, Vertice v2)
        {
            
            return default;
        }
    }
}
