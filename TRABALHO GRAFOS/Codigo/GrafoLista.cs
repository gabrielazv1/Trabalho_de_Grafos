using System;
using System.Collections.Generic;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class GrafoLista : Grafo
    {
        private List<Aresta>[] listaGrafo;

        public GrafoLista(int vertices)
        {
            listaGrafo = new List<Aresta>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                listaGrafo[i] = new List<Aresta>();
            }
        }

        public GrafoLista(int vertices, List<List<int>> dimac) : this(vertices)
        {
            foreach (List<int> linha in dimac)
            {
                if (linha.Count == 3)
                {
                    Vertice origem = new Vertice(linha[0]);
                    Vertice destino = new Vertice(linha[1]);
                    int peso = linha[2];
                    Aresta ar = new Aresta(origem, destino, peso);
                    AdicionarAresta(ar);
                }
            }
        }

        public bool AdicionarAresta(Aresta a)
        {
            if (a.Origem.id >= 0 && a.Origem.id < listaGrafo.Length &&
                a.Destino.id >= 0 && a.Destino.id < listaGrafo.Length)
            {
                listaGrafo[a.Origem.id].Add(a);
                return true;
            }
            return false;
        }

        public bool AdicionarVertice(Vertice v)
        {
            return false;
        }

        public void CriarGrafoLista()
        {

        }

        public void ImprimirListaAdjacencia()
        {
            for (int i = 0; i < listaGrafo.Length; i++)
            {
                Console.Write($"Vértice {i}: ");
                if (listaGrafo[i].Count > 0)
                {
                    foreach (Aresta a in listaGrafo[i])
                    {
                        Console.Write($"-> {a.Destino.id} (peso {a.Peso}) ");
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
