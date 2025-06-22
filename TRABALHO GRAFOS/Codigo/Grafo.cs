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

        /// <summary>
        /// Adiciona um vértice ao grafo, se ainda não existir.
        /// </summary>
        /// <param name="v">O vértice a ser adicionado.</param>
        /// <returns>True se o vértice foi adicionado com sucesso; False se já existia.</returns>
        public virtual bool AdicionarVertice(Vertice v)
        {
            if (!DicGrafo.ContainsKey(v))
            {
                DicGrafo[v] = new List<Aresta>();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adiciona uma aresta ao grafo, conectando dois vértices existentes.
        /// </summary>
        /// <param name="a">A aresta a ser adicionada.</param>
        /// <returns>True se a aresta foi adicionada com sucesso; False se os vértices não existem.</returns>
        public virtual bool AdicionarAresta(Aresta a)
        {
            if (!DicGrafo.ContainsKey(a.Origem) || !DicGrafo.ContainsKey(a.Destino))
                return false;

            AdicionarVertice(a.Origem);
            AdicionarVertice(a.Destino);
            DicGrafo[a.Origem].Add(a);
            return true;
        }

        /// <summary>
        /// Troca as posições de dois vértices no dicionário do grafo, atualizando as arestas ligadas.
        /// </summary>
        /// <param name="v1">Primeiro vértice.</param>
        /// <param name="v2">Segundo vértice.</param>
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

        /// <summary>
        /// Verifica se dois vértices são adjacentes, ou seja, se há uma aresta entre eles.
        /// </summary>
        /// <param name="v1">Vértice de origem.</param>
        /// <param name="v2">Vértice de destino.</param>
        /// <returns>True se forem adjacentes; caso contrário, False.</returns>
        public virtual bool SaoAdjacentes(Vertice v1, Vertice v2)
        {
            DicGrafo.TryGetValue(v1, out List<Aresta>? arestas);

            return arestas?.Any(aresta => aresta.Destino.Equals(v2)) ?? false;
        }

        /// <summary>
        /// Imprime o grafo no console, mostrando os vértices e suas conexões.
        /// </summary>
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
