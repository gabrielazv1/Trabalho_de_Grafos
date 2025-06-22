using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    /// <summary>
    /// Representa um vértice em um grafo, contendo todas as propriedades e métodos necessários
    /// para operações em algoritmos de grafos.
    /// </summary>
    public class Vertice
    {
        /// <summary>
        /// Identificador único do vértice.
        /// </summary>
        public int id;

        private List<Aresta> arestas = new List<Aresta>();
        private List<Vertice> verticesAdjacentes = new List<Vertice>();

        // Campos para algoritmos de grafos
        private int descoberto = 0;
        private int termino = 0;
        private bool visitado = false;
        private Vertice pai = null;
        private int nivel = 0;
        private int distancia = 0;

        /// <summary>
        /// Vértice pai na árvore de busca (usado em BFS/DFS).
        /// </summary>
        public Vertice? Pai
        {
            get { return pai; }
            set { pai = value; }
        }

        /// <summary>
        /// Lista de vértices adjacentes a este vértice.
        /// </summary>
        public List<Vertice> VerticesAdjacentes
        {
            get { return verticesAdjacentes; }
            private set { verticesAdjacentes = value; }
        }

        /// <summary>
        /// Indica se o vértice foi visitado em algoritmos de busca.
        /// </summary>
        public bool Visitado
        {
            get { return visitado; }
            set { visitado = value; }
        }

        /// <summary>
        /// Tempo de descoberta em algoritmos de busca (DFS).
        /// </summary>
        public int Descoberto
        {
            get { return descoberto; }
            set { descoberto = value; }
        }

        /// <summary>
        /// Tempo de término em algoritmos de busca (DFS).
        /// </summary>
        public int Termino
        {
            get { return termino; }
            set { termino = value; }
        }

        /// <summary>
        /// Nível do vértice em busca em largura (BFS).
        /// </summary>
        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        /// <summary>
        /// Distância do vértice em algoritmos de caminho mínimo.
        /// </summary>
        public int Distancia
        {
            get { return distancia; }
            set { distancia = value; }
        }

        /// <summary>
        /// Lista de arestas conectadas a este vértice.
        /// </summary>
        public List<Aresta> Arestas
        {
            get { return arestas; }
            set { arestas = value; }
        }

        /// <summary>
        /// Cria uma nova instância de vértice com o ID especificado.
        /// </summary>
        /// <param name="Id">Identificador único do vértice.</param>
        public Vertice(int Id)
        {
            id = Id;
        }

        /// <summary>
        /// Cria uma nova instância de vértice sem ID especificado.
        /// </summary>
        public Vertice()
        {
        }

        /// <summary>
        /// Determina se dois vértices são iguais comparando seus IDs.
        /// </summary>
        /// <param name="obj">Vértice a ser comparado.</param>
        /// <returns>True se os vértices têm o mesmo ID, False caso contrário.</returns>
        public override bool Equals(object obj)
        {
            return obj is Vertice v && id == v.id;
        }

        /// <summary>
        /// Retorna o código hash baseado no ID do vértice.
        /// </summary>
        /// <returns>Código hash do vértice.</returns>
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        /// <summary>
        /// Adiciona uma aresta à lista de arestas do vértice.
        /// </summary>
        /// <param name="a">Aresta a ser adicionada.</param>
        public void AdicionarAresta(Aresta a)
        {
            arestas.Add(a);
        }

        /// <summary>
        /// Retorna uma representação em string do vértice.
        /// </summary>
        /// <returns>String no formato "ID Vértice: X".</returns>
        public override string ToString()
        {
            return $"ID Vértice: {id}";
        }
    }
}