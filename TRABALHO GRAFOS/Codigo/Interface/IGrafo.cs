using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo.Interface
{
    /// <summary>
    /// Interface que define as operações básicas de um grafo.
    /// </summary>
    public interface IGrafo
    {
        /// <summary>
        /// Troca a posição de dois vértices no grafo, mantendo suas conexões.
        /// </summary>
        /// <param name="v1">Primeiro vértice a ser trocado</param>
        /// <param name="v2">Segundo vértice a ser trocado</param>
        /// <exception cref="ArgumentNullException">Lançada quando algum dos vértices é nulo</exception>
        /// <exception cref="ArgumentException">Lançada quando algum dos vértices não existe no grafo</exception>
        void TrocaDoisVertices(Vertice v1, Vertice v2);

        /// <summary>
        /// Adiciona um novo vértice ao grafo.
        /// </summary>
        /// <param name="v">Vértice a ser adicionado</param>
        /// <returns>
        /// True se o vértice foi adicionado com sucesso, 
        /// False se o vértice já existe no grafo
        /// </returns>
        /// <exception cref="ArgumentNullException">Lançada quando o vértice é nulo</exception>
        bool AdicionarVertice(Vertice v);

        /// <summary>
        /// Adiciona uma nova aresta ao grafo conectando dois vértices.
        /// </summary>
        /// <param name="a">Aresta a ser adicionada</param>
        /// <returns>
        /// True se a aresta foi adicionada com sucesso,
        /// False se a aresta já existe ou os vértices não existem no grafo
        /// </returns>
        /// <exception cref="ArgumentNullException">Lançada quando a aresta é nula</exception>
        bool AdicionarAresta(Aresta a);
    }
}