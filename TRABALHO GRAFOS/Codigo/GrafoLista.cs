using System;
using System.Collections.Generic;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class GrafoLista : IGrafo
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

        public GrafoLista(int vertices, List<List<int>> dimac)
        {
            listaGrafo = new List<Aresta>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                listaGrafo[i] = new List<Aresta>();
            }

            foreach (List<int> linha in dimac)
            {
                if (linha.Count == 3)
                {
                    int origem = linha[0];
                    int destino = linha[1];
                    int peso = linha[2];
                    Aresta ar = new Aresta(origem, destino, peso);
                    AdicionarAresta(ar);
                }
            }
        }

        public bool AdicionarAresta(Aresta a)
        {
            if (a.origem >= 0 && a.origem < listaGrafo.Length &&
                a.destino >= 0 && a.destino < listaGrafo.Length)
            {
                listaGrafo[a.origem].Add(a);
                return true;
            }
            return false;
        }

        public void TrocaDoisVertices(Vertice v1, Vertice v2)
        {
        }

        public bool AdicionarVertice(Vertice v)
        {
            return false;
        }

        public void CriarGrafoLista()
        {

        }
    }
}
