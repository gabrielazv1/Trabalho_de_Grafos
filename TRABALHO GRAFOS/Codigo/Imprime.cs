using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    public static class Imprime
    {

        public static void ImprimirListaAdj(Grafo grafo)
        {

        }
        public static void ImprimirMatrizAdj(Grafo grafo)
        {

        }

        public static void ImprimirArestasAdj(int verticeOrigem, int verticeDestino, Grafo grafo)
        {
            Vertice? vOrigem = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == verticeOrigem);
            Vertice? vDestino = grafo.DicGrafo.Keys.FirstOrDefault(v => v.id == verticeDestino);

            if (vOrigem == null || vDestino == null)
                throw new Exception("Um ou ambos os vértices não existem no grafo.");

            Console.WriteLine($"\nArestas adjacentes ao vértice {vOrigem.id}:");
            foreach (Aresta aresta in grafo.DicGrafo[vOrigem])
            {
                Console.WriteLine($"({aresta.Origem.id}, {aresta.Destino.id}) peso {aresta.Peso}");
            }

            Console.WriteLine($"\nArestas adjacentes ao vértice {vDestino.id}:");
            foreach (Aresta aresta in grafo.DicGrafo[vDestino])
            {
                Console.WriteLine($"({aresta.Origem.id}, {aresta.Destino.id}) peso {aresta.Peso}");
            }
        }


        public static void ImprimirVerticesAdj(Grafo grafo, int idVertice)
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

        public static void ImprimirArestasInc(int idVertice)
        {

        }

        public static void ImprimirVerticeInc(Grafo grafo, int idVertOrigem, int idVertDestino)
        {
            Aresta? a = grafo.DicGrafo.Values
                .SelectMany(lista => lista)
                .FirstOrDefault(aresta =>
                    aresta.Origem.id == idVertOrigem && aresta.Destino.id == idVertDestino);

            if (a == null)
                throw new Exception("Aresta não encontrada no grafo.");

            Console.WriteLine("Vértices incidentes à aresta:");
            Console.WriteLine($" Origem: {a.Origem.id} - Destino: {a.Destino.id}");
        }

        public static void ImprimirGrau(int idVertice)
        {

        }

        public static void ImprimirBuscaLargura()
        {

        }

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

            Console.WriteLine("Resultado da Busca em Profundidade: ");
            foreach (Vertice v in grafo.DicGrafo.Keys.OrderBy(v => v.id))
            {
                Console.WriteLine($" Vértice {v.id} --> Tempo de Descoberta: {v.Descoberto} - Tempo de Término: {v.Termino} - Pai: {v.Pai?.id.ToString() ?? "null"}");
            }
        }

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

        public static void ImprimirDijkstra()
        {

        }

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
                Console.WriteLine($"{idVerticeOrigem} --> {destinoId}: {resultado}");
            }
        }

        public static void ImprimirAdjacentes(Vertice v1, Vertice v2, Grafo grafo)
        {
            Vertice? v = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.Equals(v1));

            if (v == null) { throw new Exception($"Vértice V1 ({v1.ToString()}) informado não faz parte do grafo.  "); }

            bool saoAdjacentes = grafo.SaoAdjacentes(v1, v2);

            Console.WriteLine(saoAdjacentes ? "SÃO adjacentes." : "NÃO são adjacentes.");

        }

        public static void ImprimirTrocaVertices(Grafo grafo, int id_Vertice1, int id_Vertice2)
        {
            Vertice? v1 = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == id_Vertice1);
            Vertice? v2 = grafo.DicGrafo.Keys.FirstOrDefault(vertice => vertice.id == id_Vertice2);

            if (v1 == null)
                throw new Exception($"Vértice {v1.id} não encontrado para troca. ");

            if (v2 == null)
                throw new Exception($"Vértice {v2.id} não encontrado para troca. ");

            grafo.TrocaDoisVertices(v1, v2);

            Console.WriteLine("Troca realizada. ");
            grafo.Imprimir();
        }
    }
}
