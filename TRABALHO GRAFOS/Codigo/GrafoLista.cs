using System;
using System.Collections.Generic;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    /// <summary>
    /// Implementação de grafo utilizando lista de adjacência.
    /// </summary>
    public class GrafoLista : Grafo
    {
        /// <summary>
        /// Estrutura de dados principal que armazena o grafo como lista de adjacência.
        /// Cada índice do array representa um vértice e contém uma lista de suas arestas adjacentes.
        /// </summary>
        public static List<Aresta>[] listaGrafo;

        /// <summary>
        /// Inicializa um grafo vazio com um número específico de vértices.
        /// </summary>
        /// <param name="vertices">Número de vértices do grafo</param>
        /// <exception cref="ArgumentOutOfRangeException">Lançada quando o número de vértices é menor que 0</exception>
        public GrafoLista(int vertices)
        {
            if (vertices < 0)
                throw new ArgumentOutOfRangeException(nameof(vertices), "O número de vértices não pode ser negativo");

            listaGrafo = new List<Aresta>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                listaGrafo[i] = new List<Aresta>();
                AdicionarVertice(new Vertice(i));
            }
        }

        /// <summary>
        /// Inicializa um grafo a partir de dados no formato DIMACS.
        /// </summary>
        /// <param name="vertices">Número de vértices do grafo</param>
        /// <param name="dimac">Lista de arestas no formato [origem, destino, peso]</param>
        /// <exception cref="ArgumentNullException">Lançada quando a lista dimac é nula</exception>
        /// <exception cref="ArgumentException">Lançada quando o formato dos dados é inválido</exception>
        public GrafoLista(int vertices, List<List<int>> dimac) : this(vertices)
        {
            foreach (List<int> linha in dimac)
            {
                if (linha.Count == 3)
                {
                    int origemId = linha[0];
                    int destinoId = linha[1];
                    int peso = linha[2];

                    Vertice origem = new Vertice(origemId);
                    Vertice destino = new Vertice(destinoId);
                    Aresta ar = new Aresta(origem, destino, peso);

                    AdicionarAresta(ar);
                }
            }
        }

        /// <summary>
        /// Adiciona uma aresta ao grafo.
        /// </summary>
        /// <param name="a">Aresta a ser adicionada</param>
        /// <returns>
        /// True se a aresta foi adicionada com sucesso,
        /// False se os vértices da aresta estão fora do intervalo válido
        /// </returns>
        /// <exception cref="ArgumentNullException">Lançada quando a aresta é nula</exception>
        public override bool AdicionarAresta(Aresta a)
        {
            if (a.Origem.id >= 0 && a.Origem.id < listaGrafo.Length &&
                a.Destino.id >= 0 && a.Destino.id < listaGrafo.Length)
            {
                listaGrafo[a.Origem.id].Add(a);

                if (!DicGrafo.ContainsKey(a.Origem))
                    AdicionarVertice(a.Origem);

                if (!DicGrafo.ContainsKey(a.Destino))
                    AdicionarVertice(a.Destino);

                DicGrafo[a.Origem].Add(a);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Adiciona um vértice ao grafo.
        /// </summary>
        /// <param name="v">Vértice a ser adicionado</param>
        /// <returns>
        /// True se o vértice foi adicionado com sucesso,
        /// False se o vértice já existe no grafo
        /// </returns>
        /// <exception cref="ArgumentNullException">Lançada quando o vértice é nulo</exception>
        public bool AdicionarVertice(Vertice v)
        {
            return base.AdicionarVertice(v);
        }
    }
}