using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    /// <summary>
    /// Representa uma aresta em um grafo, conectando dois vértices com um peso associado.
    /// </summary>
    public class Aresta
    {
        private Vertice origem;
        private Vertice destino;
        private int peso;

        /// <summary>
        /// Vértice de origem da aresta.
        /// </summary>
        public Vertice Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        /// <summary>
        /// Vértice de destino da aresta.
        /// </summary>
        public Vertice Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        /// <summary>
        /// Peso/custo associado à aresta.
        /// </summary>
        public int Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        /// <summary>
        /// Cria uma nova aresta com vértices de origem, destino e peso especificados.
        /// </summary>
        /// <param name="origem">Vértice de origem</param>
        /// <param name="destino">Vértice de destino</param>
        /// <param name="peso">Peso da aresta</param>
        public Aresta(Vertice origem, Vertice destino, int peso)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }

        /// <summary>
        /// Cria uma nova aresta com vértices de origem e destino especificados e peso zero.
        /// </summary>
        /// <param name="origem">Vértice de origem</param>
        /// <param name="destino">Vértice de destino</param>
        public Aresta(Vertice origem, Vertice destino)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = 0;
        }

        /// <summary>
        /// Substitui o peso atual da aresta por um novo valor.
        /// </summary>
        /// <param name="peso">Novo peso da aresta</param>
        public void SubstituiPeso(int peso)
        {
            this.peso = peso;
        }

        /// <summary>
        /// Determina se duas arestas são iguais comparando origem, destino e peso.
        /// </summary>
        /// <param name="obj">Aresta a ser comparada</param>
        /// <returns>True se as arestas são iguais, False caso contrário</returns>
        public override bool Equals(object? obj)
        {
            return obj is Aresta aresta &&
                   this.origem.Equals(aresta.origem) &&
                   this.destino.Equals(aresta.destino) &&
                   this.peso == aresta.peso;
        }
    }
}