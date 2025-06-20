using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    public class Grafo : IGrafo
    {
        public Dictionary<Vertice, List<Aresta>> DicGrafo { get; set; }
        public int NumeroDeVertices { get; private set; }

        public Grafo()
        {
            DicGrafo = new Dictionary<Vertice, List<Aresta>>();
            NumeroDeVertices = 0;
        }

        public virtual bool AdicionarVertice(Vertice v)
        {
            if (!DicGrafo.ContainsKey(v))
            {
                DicGrafo[v] = new List<Aresta>();
                return true;
            }
            return false;
        }

        public virtual bool AdicionarAresta(Aresta a)
        {
            if (!DicGrafo.ContainsKey(a.Origem) || !DicGrafo.ContainsKey(a.Destino))
                return false;

            AdicionarVertice(a.Origem);
            AdicionarVertice(a.Destino);
            DicGrafo[a.Origem].Add(a);
            return true;
        }

        public virtual void TrocaDoisVertices(Vertice v1, Vertice v2)
        {
            List<Aresta>? temp = DicGrafo[v1];
            DicGrafo[v1] = DicGrafo[v2];
            DicGrafo[v2] = temp;

            foreach (Aresta aresta in DicGrafo[v1])
            {
                if (aresta.Origem.Equals(v2))
                    aresta.Origem = v1;
            }

            foreach (Aresta aresta in DicGrafo[v2])
            {
                if (aresta.Origem.Equals(v1))
                    aresta.Origem = v2;
            }

            foreach (List<Aresta> lista in DicGrafo.Values)
            {
                foreach (Aresta aresta in lista)
                {
                    if (aresta.Destino.Equals(v1))
                        aresta.Destino = v2;
                    else if (aresta.Destino.Equals(v2))
                        aresta.Destino = v1;
                }
            }

        }

        public virtual bool SaoAdjacentes(Vertice v1, Vertice v2)
        {
            DicGrafo.TryGetValue(v1, out List<Aresta>? arestas);

            return arestas?.Any(aresta => aresta.Destino.Equals(v2)) ?? false;
        }

        public void Imprimir()
        {
            foreach (KeyValuePair<Vertice, List<Aresta>> par in DicGrafo)
            {
                Console.Write($"Vértice {par.Key.id + 1}: ");
                if (par.Value.Count > 0)
                {
                    foreach (var aresta in par.Value)
                    {
                        Console.Write($"-> {aresta.Destino.id + 1} (peso {aresta.Peso}) ");
                    }
                }
                else
                {
                    Console.Write("sem conexões");
                }
                Console.WriteLine();
            }
        }
    }

}
