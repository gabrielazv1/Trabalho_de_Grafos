using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    /// <summary>
    /// Classe responsável por imprimir informações relacionadas ao grafo,
    /// como listas e matrizes de adjacência, vértices, arestas e resultados de algoritmos.
    /// </summary>
    public static class Imprime
    {
        /// <summary>
        /// Imprime a lista de adjacência do grafo usando a estrutura GrafoLista.
        /// </summary>
        public static void ImprimirListaAdj()
        {
            for (int i = 0; i < GrafoLista.listaGrafo.Length; i++)
            {
                Console.Write($"Vértice {i + 1} ");
                if (GrafoLista.listaGrafo[i].Count >= 0)
                {
                    foreach (Aresta a in GrafoLista.listaGrafo[i])
                    {
                        Console.Write($"-> {a.Destino.id + 1} (peso {a.Peso}) ");
                    }
                }
                else
                {
                    Console.Write("sem conexões");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Imprime a matriz de adjacência do grafo usando a estrutura GrafoMatriz.
        /// </summary>

        public static void ImprimirMatrizAdj()
        {
            Console.WriteLine("Matriz de Adjacência:");
            for (int i = 0; i < GrafoMatriz.matrizGrafo.GetLength(0); i++)
            {
                Console.Write($"Vértice {i + 1} ");
                for (int j = 0; j < GrafoMatriz.matrizGrafo.GetLength(1); j++)
                {
                    if (GrafoMatriz.matrizGrafo[i, j] != null)
                        Console.Write($"[{GrafoMatriz.matrizGrafo[i, j].Peso}] ");
                    else
                        Console.Write("[0] ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Imprime as arestas adjacentes de dois vértices específicos.
        /// </summary>
        /// <param name="verticeOrigem">ID do vértice de origem.</param>
        /// <param name="verticeDestino">ID do vértice de destino.</param>
        /// <param name="grafo">Grafo contendo os dados.</param>
        public static void ImprimirArestasAdj(int verticeOrigem, int verticeDestino, Grafo grafo)
        {
            Vertice? vOrigem = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == verticeOrigem);
            Vertice? vDestino = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == verticeDestino);

            if (vOrigem == null || vDestino == null)
                throw new Exception("Um ou ambos os vértices não existem no grafo.");

            Console.WriteLine($"Arestas adjacentes ao vértice {vOrigem.id + 1}:");

            foreach (Aresta aresta in grafo.DicGrafo[vOrigem])
            {
                Console.WriteLine($"({aresta.Origem.id + 1}, {aresta.Destino.id + 1}) - peso {aresta.Peso}");
            }

            Console.WriteLine($"\nArestas adjacentes ao vértice {vDestino.id + 1}:");
            foreach (Aresta aresta in grafo.DicGrafo[vDestino])
            {
                Console.WriteLine($"({aresta.Origem.id + 1}, {aresta.Destino.id + 1}) - peso {aresta.Peso}");
            }
        }

        /// <summary>
        /// Imprime os vértices adjacentes a um vértice informado.
        /// </summary>
        /// <param name="grafo">Grafo a ser consultado.</param>
        /// <param name="idVertice">ID do vértice.</param>
        public static void ImprimirVerticesAdj(Grafo grafo, int idVertice)
        {
            Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == idVertice);

            if (v == null) { throw new Exception("Vértice não encontrado com o ID informado."); }

            List<Aresta> arestas = grafo.DicGrafo[v];

            if (arestas.Count == 0) { throw new Exception("Não existem vértices adjacentes ao vértice informado."); }

            Console.WriteLine($"Vértices adjacentes ao vértice {idVertice + 1}: ");

            foreach (Aresta a in arestas)
            {
                Console.WriteLine($"Vértice {a.Destino.id + 1} - Peso: {a.Peso}");
            }
        }

        /// <summary>
        /// Imprime todas as arestas que incidem sobre o vértice informado.
        /// </summary>
        /// <param name="idVertice">ID do vértice de destino.</param>
        /// <param name="grafo">Grafo que contém os dados.</param>
        public static void ImprimirArestasInc(int idVertice, Grafo grafo)
        {
            try
            {
                Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == idVertice);

                if (v == null)
                {
                    throw new Exception("Vértice não encontrado com o ID informado.");
                }

                Console.WriteLine($"Arestas incidentes ao vértice {idVertice + 1}");

                foreach (List<Aresta> listaArestas in grafo.DicGrafo.Values)
                {
                    foreach (Aresta aresta in listaArestas)
                    {
                        if (aresta.Destino.id == idVertice)
                        {
                            Console.WriteLine($"({aresta.Origem.id + 1}, {aresta.Destino.id + 1}) - Peso: {aresta.Peso}");
                        }
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "Aperte ENTER para continuar"); }
        }

        /// <summary>
        /// Imprime os vértices origem e destino de uma aresta específica.
        /// </summary>
        /// <param name="grafo">Grafo a ser consultado.</param>
        /// <param name="idVertOrigem">ID do vértice de origem.</param>
        /// <param name="idVertDestino">ID do vértice de destino.</param>
        public static void ImprimirVerticeInc(Grafo grafo, int idVertOrigem, int idVertDestino)
        {
            Aresta? a = grafo.DicGrafo.Values
                .SelectMany(lista => lista)
                .FirstOrDefault(aresta =>
                    aresta.Origem.id == idVertOrigem && aresta.Destino.id == idVertDestino);

            if (a == null)
                throw new Exception("Aresta não encontrada no grafo.");

            Console.WriteLine("Vértices incidentes à aresta:");
            Console.WriteLine($" Origem: {a.Origem.id + 1} - Destino: {a.Destino.id + 1}");
        }

        /// <summary>
        /// Imprime o grau do vértice informado (quantidade de arestas conectadas).
        /// </summary>
        /// <param name="idVertice">ID do vértice.</param>
        /// <param name="grafo">Grafo onde o vértice está localizado.</param>
        public static void ImprimirGrau(int idVertice, Grafo grafo)
        {
            try
            {
                Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == idVertice);

                if (v == null)
                {
                    throw new Exception("Vértice não encontrado com o ID informado.");
                }

                List<Aresta> a = grafo.DicGrafo[v];

                Console.WriteLine($"Grau do vértice {v.id + 1} é {a.Count}");

            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "Aperte ENTER para continuar"); }
        }

        /// <summary>
        /// Altera o peso de uma aresta entre dois vértices.
        /// </summary>
        /// <param name="verticeOrigem">ID do vértice de origem.</param>
        /// <param name="verticeDestino">ID do vértice de destino.</param>
        /// <param name="peso">Novo peso a ser definido.</param>
        /// <param name="grafo">Grafo que contém a aresta.</param>
        public static void TrocarPesoArestas(int verticeOrigem, int verticeDestino, int peso, Grafo grafo)
        {
            Vertice? vOrigem = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == verticeOrigem);

            foreach (Aresta aresta in grafo.DicGrafo[vOrigem])
            {
                if (aresta.Destino.id == verticeDestino)
                {
                    Console.WriteLine($"A aresta ({verticeOrigem + 1}, {verticeDestino + 1}) teve o seu peso alterado de {aresta.Peso} para {peso}!");
                    aresta.Peso = peso;
                }
            }
        }

        /// <summary>
        /// Executa e imprime o resultado da busca em largura (BFS) a partir de um vértice inicial.
        /// </summary>
        /// <param name="grafo">Grafo onde será feita a busca.</param>
        /// <param name="idVerticeInicial">ID do vértice inicial.</param>
        public static void ImprimirBuscaLargura(Grafo grafo, int idVerticeInicial)
        {
            Vertice? vInicial = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == idVerticeInicial);

            if (vInicial == null)
                throw new Exception("Vértice inicial não encontrado.");

            foreach (Vertice vertice in grafo.DicGrafo.Keys)
            {
                vertice.Visitado = false;
                vertice.Pai = null;
                vertice.Nivel = -1;
                vertice.Descoberto = 0;
            }

            Queue<Vertice> fila = new Queue<Vertice>();
            int tempo = 1;

            vInicial.Visitado = true;
            vInicial.Nivel = 0;
            vInicial.Descoberto = tempo++;
            fila.Enqueue(vInicial);

            while (fila.Count > 0)
            {
                Vertice v = fila.Dequeue();

                IOrderedEnumerable<Vertice> vizinhos = grafo.DicGrafo[v]
                    .Select(a => grafo.DicGrafo.Keys.First(dest => dest.id == a.Destino.id))
                    .OrderBy(vert => vert.id);

                foreach (Vertice w in vizinhos)
                {
                    if (w.Descoberto == 0)
                    {
                        Console.WriteLine($"Aresta árvore: ({v.id + 1}, {w.id + 1})");
                        w.Pai = v;
                        w.Nivel = v.Nivel + 1;
                        w.Descoberto = tempo++;
                        fila.Enqueue(w);
                    }
                    else if (w.Nivel == v.Nivel + 1)
                    {
                        Console.WriteLine($"Aresta tio: ({v.id + 1}, {w.id + 1})");
                    }
                    else if (w.Nivel == v.Nivel && v.Pai == w.Pai && w.Descoberto > v.Descoberto)
                    {
                        Console.WriteLine($"Aresta irmão: ({v.id + 1}, {w.id + 1})");
                    }
                    else if (w.Nivel == v.Nivel && v.Pai != w.Pai && w.Descoberto > v.Descoberto)
                    {
                        Console.WriteLine($"Aresta primo: ({v.id + 1}, {w.id + 1})");
                    }
                }
            }

            Console.WriteLine("================================================\nRESULTADO FINAL:\n================================================");

            foreach (Vertice v in grafo.DicGrafo.Keys.OrderBy(v => v.id))
            {
                Console.Write($"Vértice: {v.id + 1} | Nível: {v.Nivel} | Pai: ");

                if (v.Pai != null)
                    Console.Write($"{v.Pai.id + 1}\n");
                else
                    Console.Write("-\n");
            }
        }

        /// <summary>
        /// Executa e imprime a busca em profundidade (DFS) a partir do vértice raiz.
        /// </summary>
        /// <param name="grafo">Grafo onde será feita a busca.</param>
        /// <param name="idVerticeRaiz">ID do vértice raiz da busca.</param>
        public static void ImprimirBuscaProfundidade(Grafo grafo, int idVerticeRaiz)
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

            Console.WriteLine("RESULTADO FINAL:\n================================================");
            foreach (Vertice v in grafo.DicGrafo.Keys.OrderBy(v => v.id))
            {
                Console.Write($"Vértice: {v.id + 1} | Tempo de Descoberta: {v.Descoberto} | Tempo de Término: {v.Termino} | Pai: ");
                if (v.Pai != null)
                    Console.Write($"{v.Pai.id + 1}\n");
                else
                    Console.Write("-\n");
            }
        }

        /// <summary>
        /// Método auxiliar para realizar a visita de um vértice durante a DFS, atualizando tempos.
        /// </summary>
        /// <param name="grafo">Grafo a ser percorrido.</param>
        /// <param name="v">Vértice atual.</param>
        /// <param name="tempo">Tempo de descoberta e término sendo atualizado.</param>
        private static void VisitaVerticeBuscaProfundidade(Grafo grafo, Vertice v, ref int tempo)
        {
            tempo++;
            v.Descoberto = tempo;
            v.Visitado = true;

            IOrderedEnumerable<Vertice> vizinhos = grafo.DicGrafo[v]
                .Select(aresta => grafo.DicGrafo.Keys
                .First(dest => dest.id == aresta.Destino.id))
                .OrderBy(destinoo => destinoo.id);

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

        /// <summary>
        /// Executa o algoritmo de Dijkstra e imprime o menor caminho entre dois vértices.
        /// </summary>
        /// <param name="idOrigem">ID do vértice de origem.</param>
        /// <param name="idDestino">ID do vértice de destino.</param>
        /// <param name="grafo">Grafo a ser analisado.</param>
        public static void ImprimirDijkstra(int idOrigem, int idDestino, Grafo grafo)
        {
            Vertice? origem = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == idOrigem);
            Vertice? destino = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == idDestino);

            if (origem == null || destino == null)
            {
                Console.WriteLine("Vértice não encontrado");
                return;
            }

            foreach (Vertice v in grafo.DicGrafo.Keys)
            {
                v.Distancia = int.MaxValue;
                v.Pai = null;
                v.Visitado = false;
            }

            origem.Distancia = 0;

            List<Vertice> naoExplorados = grafo.DicGrafo.Keys.ToList();

            while (naoExplorados.Count > 0)
            {

                Vertice? u = naoExplorados.Where(v => !v.Visitado)
                                          .OrderBy(v => v.Distancia)
                                          .FirstOrDefault();

                if (u == null || u.Distancia == int.MaxValue)
                    break;

                u.Visitado = true;

                foreach (Aresta aresta in grafo.DicGrafo[u])
                {
                    Vertice vizinho = grafo.DicGrafo.Keys.First(v => v.id == aresta.Destino.id);

                    if (vizinho.Visitado) continue;

                    int novaDistancia = u.Distancia + aresta.Peso;

                    if (novaDistancia < vizinho.Distancia)
                    {
                        vizinho.Distancia = novaDistancia;
                        vizinho.Pai = u;
                    }
                }
            }

            List<Vertice> caminho = new();
            Vertice? atual = destino;
            while (atual != null)
            {
                caminho.Insert(0, atual);
                atual = atual.Pai;
            }

            if (caminho.First() != origem)
            {
                Console.WriteLine("Não há caminho entre os vértices");
                return;
            }

            Console.WriteLine($"Caminho mínimo de {idOrigem + 1} até {idDestino + 1}:");

            int pesoTotal = 0;
            for (int i = 0; i < caminho.Count - 1; i++)
            {
                Vertice de = caminho[i];
                Vertice para = caminho[i + 1];

                Aresta? a = grafo.DicGrafo[de].FirstOrDefault(ar => ar.Destino.id == para.id);

                if (a != null)
                {
                    Console.WriteLine($"({de.id + 1} -> {para.id + 1}) - Peso: {a.Peso}");
                    pesoTotal += a.Peso;
                }
            }
            Console.WriteLine("================================================");
            Console.WriteLine($"Custo do menor caminho: {pesoTotal}");
        }

        /// <summary>
        /// Executa o algoritmo de Floyd-Warshall e imprime as menores distâncias a partir de um vértice.
        /// </summary>
        /// <param name="grafo">Grafo onde será executado o algoritmo.</param>
        /// <param name="idVerticeOrigem">ID do vértice de origem.</param>
        public static void ImprimirFloydWarshal(Grafo grafo, int idVerticeOrigem)
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

            foreach (KeyValuePair<Vertice, List<Aresta>> par in grafo.DicGrafo)
            {
                int i = indexMatriz[par.Key.id];
                foreach (Aresta aresta in par.Value)
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
            Console.WriteLine($"Menores distâncias a partir do vértice {idVerticeOrigem + 1}:");
            for (int j = 0; j < n; j++)
            {
                int destinoId = vertices[j].id;
                string resultado = dist[origemIndex, j] >= INF
                    ? "infinito"
                    : dist[origemIndex, j].ToString();
                Console.WriteLine($"{idVerticeOrigem + 1} --> {destinoId + 1}: {resultado}");
            }
        }

        /// <summary>
        /// Verifica e imprime se dois vértices são adjacentes no grafo.
        /// </summary>
        /// <param name="v1">Primeiro vértice.</param>
        /// <param name="v2">Segundo vértice.</param>
        /// <param name="grafo">Grafo a ser consultado.</param>
        public static void ImprimirAdjacentes(Vertice v1, Vertice v2, Grafo grafo)
        {

            Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.Equals(v1));

            if (v == null) { throw new Exception($"Vértice V1 ({v1.ToString()}) informado não faz parte do grafo.  "); }

            bool saoAdjacentes = grafo.SaoAdjacentes(v1, v2);

            Console.WriteLine(saoAdjacentes ? "SÃO adjacentes." : "NÃO são adjacentes.");
        }

        /// <summary>
        /// Troca dois vértices no grafo e imprime o estado do grafo após a troca.
        /// </summary>
        /// <param name="grafo">Grafo onde a troca será realizada.</param>
        /// <param name="id_Vertice1">ID do primeiro vértice.</param>
        /// <param name="id_Vertice2">ID do segundo vértice.</param>
        public static void ImprimirTrocaVertices(Grafo grafo, int id_Vertice1, int id_Vertice2)
        {
            Vertice? v1 = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == id_Vertice1);
            Vertice? v2 = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == id_Vertice2);

            if (v1 == null)
                throw new Exception($"Vértice {id_Vertice1 + 1} não encontrado para troca. ");

            if (v2 == null)
                throw new Exception($"Vértice {id_Vertice2 + 1} não encontrado para troca. ");

            grafo.TrocaDoisVertices(v1, v2);

            Console.WriteLine("Troca realizada. ");
            grafo.Imprimir();
        }
    }
}
