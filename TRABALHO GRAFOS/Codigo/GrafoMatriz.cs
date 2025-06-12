using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class GrafoMatriz : IGrafo
    {
        private int[,] matrizGrafo;

        public GrafoMatriz(int vertices)
        {
            matrizGrafo = new int[vertices, vertices];
        }
        public GrafoMatriz(int vertices, List<List<int>> listaArestas)
        {
            int[,] matrizAdjacencia = new int[vertices, vertices];

            foreach (List<int> aresta in listaArestas)
            {
                matrizAdjacencia[aresta[0], aresta[1]] = aresta[2];
            }
            matrizGrafo = matrizAdjacencia;
        }
        public void TrocaDoisVertices(Vertice v1, Vertice v2)
        {

        }
        public bool AdicionarVertice(Vertice v)
        {
            return false;
        }

        public bool AdicionarAresta(Aresta a)
        {
            try
            {
                if (a.origem < 0 || a.origem >= matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(a.origem), "O vértice está fora dos limites da matriz de adjacência.");

                if (a.destino < 0 || a.destino >= matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(a.destino), "O destino está fora dos limites da matriz de adjacência.");

                if (a.peso <= 0)
                    throw new ArgumentException("O peso deve ser maior que zero.", nameof(a.peso));

                if (matrizGrafo[a.origem, a.destino] > 0)
                    throw new ArgumentOutOfRangeException(nameof(a.destino), "A aresta informada já existe");

                matrizGrafo[a.origem, a.destino] = a.peso;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}"); return false;
            }
        }

        public void CriarGrafoMatriz()
        {

        }
    }
}
