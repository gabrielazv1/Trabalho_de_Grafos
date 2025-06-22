using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    /// <summary>
    /// Implementação de grafo utilizando matriz de adjacência.
    /// </summary>
    public class GrafoMatriz : Grafo
    {
        /// <summary>
        /// Matriz de adjacência que representa o grafo.
        /// </summary>
        public static Aresta[,] matrizGrafo;

        /// <summary>
        /// Inicializa um grafo a partir de uma lista de arestas.
        /// </summary>
        /// <param name="vertices">Número de vértices do grafo</param>
        /// <param name="listaArestas">Lista de arestas no formato [origem, destino, peso]</param>
        public GrafoMatriz(int vertices, List<List<int>> listaArestas)
        {
            matrizGrafo = new Aresta[vertices, vertices];
            foreach (List<int> aresta in listaArestas)
            {
                int origem = aresta[0];
                int destino = aresta[1];
                int peso = aresta[2];

                AdicionarAresta(new Aresta(new Vertice(origem), new Vertice(destino), peso));
            }

            DicGrafo = PopularDicionario();
        }

        /// <summary>
        /// Converte a matriz de adjacência em um dicionário de adjacência.
        /// </summary>
        /// <returns>Dicionário representando o grafo</returns>
        private Dictionary<Vertice, List<Aresta>> PopularDicionario()
        {
            Dictionary<Vertice, List<Aresta>> grafo = new Dictionary<Vertice, List<Aresta>>();

            for (int i = 0; i < matrizGrafo.GetLength(0); i++)
            {
                grafo[new Vertice(i)] = new List<Aresta>();
            }

            for (int i = 0; i < matrizGrafo.GetLength(0); i++)
            {
                for (int j = 0; j < matrizGrafo.GetLength(1); j++)
                {
                    Aresta aresta = matrizGrafo[i, j];

                    if (aresta != null)
                    {
                        grafo[new Vertice(i)].Add(new Aresta(new Vertice(i), new Vertice(j), aresta.Peso));
                    }
                }
            }

            return grafo;
        }

        /// <summary>
        /// Troca a posição de dois vértices na matriz de adjacência.
        /// </summary>
        /// <param name="v1">Primeiro vértice</param>
        /// <param name="v2">Segundo vértice</param>
        public void TrocaDoisVertices(Vertice v1, Vertice v2)
        {
            // Implementação existente
        }

        /// <summary>
        /// Adiciona um novo vértice ao grafo.
        /// </summary>
        /// <param name="v">Vértice a ser adicionado</param>
        /// <returns>False, pois a matriz tem tamanho fixo</returns>
        public bool AdicionarVertice(Vertice v)
        {
            return false;
        }

        /// <summary>
        /// Adiciona uma nova aresta à matriz de adjacência.
        /// </summary>
        /// <param name="a">Aresta a ser adicionada</param>
        /// <returns>
        /// True se a aresta foi adicionada com sucesso,
        /// False se ocorrer algum erro
        /// </returns>
        public bool AdicionarAresta(Aresta a)
        {
            try
            {
                if (a.Origem.id < 0 || a.Origem.id >= matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(a.Origem), "O vértice está fora dos limites da matriz de adjacência.");

                if (a.Destino.id < 0 || a.Destino.id >= matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(a.Destino), "O destino está fora dos limites da matriz de adjacência.");

                if (a.Peso <= 0)
                    throw new ArgumentException("O peso deve ser maior que zero.", nameof(a.Peso));

                if (matrizGrafo[a.Origem.id, a.Destino.id] != null)
                    throw new InvalidOperationException("A aresta informada já existe.");

                matrizGrafo[a.Origem.id, a.Destino.id] = a;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }
    }
}