using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Imprime
    {

        public void ImprimirListaAdj(Grafo grafo)
        {

        }
        public void ImprimirMatrizAdj(Grafo grafo)
        {

        }
        public void ImprimirArestasAdj(int idAresta)
        {

        }
        public void ImprimirVerticesAdj(Grafo grafo, int idVertice)
        {
            try
            {
                Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == idVertice);

                if (v == null) { throw new Exception("Vértice não encontrado com o ID informado."); }

                List<Aresta> arestas = grafo.DicGrafo[v];

                if (arestas.Count == 0) { throw new Exception("Não existem vértices adjacentes ao vértice informado."); }

                Console.WriteLine($"Vértices adjacentes ao vértice {idVertice}: ");

                foreach (Aresta a in arestas)
                {
                    Console.WriteLine($" {a.Destino.id} - Peso: {a.Peso}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ImprimirArestasInc(int idVertice)
        {

        }

        public void ImprimirVerticeInc(Grafo grafo, Aresta a)
        {
            try
            {
                if (grafo.DicGrafo.Values.Any(arestas => arestas.Contains(a)))
                {
                    Console.WriteLine("Vértices incidentes à aresta:");
                    Console.WriteLine($" Origem: {a.Origem.id} - Destino: {a.Destino.id}");
                }
                else
                {
                    throw new Exception("Aresta não encontrada no grafo.");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void ImprimirGrau(int idVertice)
        {

        }
        public void ImprimirBuscaLargura()
        {

        }
        public void ImprimirBuscaProfundidade(Grafo grafo, int idVerticeRaiz)
        {
            try
            {
                Vertice? verticeRaiz = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == idVerticeRaiz);

                if (verticeRaiz == null) { throw new Exception("Vértice raiz não encontrado."); }

                foreach (Vertice vertice in grafo.DicGrafo.Keys)
                {
                    vertice.Visitado = false;
                    vertice.Termino = 0;
                    vertice.Descoberto = 0;
                    vertice.Pai = null;
                }

                int td = 0;
                VisitaVerticeBuscaProfundidade(grafo, verticeRaiz, ref td);

                Console.WriteLine("Resultado da Busca em Profundidade: ");
                foreach (Vertice v in grafo.DicGrafo.Keys.OrderBy(v => v.id))
                {
                    Console.WriteLine($" Vértice {v.id}: \n Tempo de Descoberta: {v.Descoberto} - Tempo de Término: {v.Termino} - Pai: {v.Pai?.id.ToString() ?? "null"}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void VisitaVerticeBuscaProfundidade(Grafo grafo, Vertice v, ref int tempo)
        {
            tempo++;
            v.Descoberto = tempo;
            v.Visitado = true;

            IOrderedEnumerable<Vertice> vizinhos = grafo.DicGrafo[v].Select(v => v.Destino).OrderBy(destinoo => destinoo.id);

            foreach (Vertice verticeVizinho in vizinhos)
            {
                if (!verticeVizinho.Visitado)
                {
                    verticeVizinho.Pai = v;
                    VisitaVerticeBuscaProfundidade(grafo, verticeVizinho, ref tempo);
                }
            }
            tempo++;
            v.Termino = tempo;
        }

        public void ImprimirDijkstra()
        {

        }
        public void ImprimirFloydWarshal(Grafo grafo, int idVerticeOrigem)
        {
            try
            {
                List<Vertice>? vertices = grafo.DicGrafo.Keys.OrderBy(v => v.id).ToList();
                int n = vertices.Count;

                Dictionary<int, int> indexMatriz = vertices.Select((v, index) => new { v.id, index })
                                                         .ToDictionary(x => x.id, x => x.index);

                int[,] dist = new int[n, n];
                const int INF = int.MaxValue / 2;

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        dist[i, j] = (i == j) ? 0 : INF;

                foreach (var par in grafo.DicGrafo)
                {
                    int i = indexMatriz[par.Key.id];
                    foreach (var aresta in par.Value)
                    {
                        int j = indexMatriz[aresta.Destino.id];
                        dist[i, j] = aresta.Peso;
                    }
                }

                for (int k = 0; k < n; k++)
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            if (dist[i, k] + dist[k, j] < dist[i, j])
                                dist[i, j] = dist[i, k] + dist[k, j];

                if (!indexMatriz.ContainsKey(idVerticeOrigem))
                    throw new Exception("Vértice origem não encontrado.");


                int origemIndex = indexMatriz[idVerticeOrigem];
                Console.WriteLine($"Menores distâncias a partir do vértice {idVerticeOrigem}:");
                for (int j = 0; j < n; j++)
                {
                    int destinoId = vertices[j].id;
                    string resultado = dist[origemIndex, j] >= INF
                        ? "infinito"
                        : dist[origemIndex, j].ToString();
                    Console.WriteLine($"→ {destinoId}: {resultado}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
