using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TRABALHO_GRAFOS.Codigo.Interface;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Program
    {
        #region Main

        private static void Main(string[] args)
        {
            Console.Clear();
            Iniciar();

        }
        #endregion
        #region Menu Inicial
        public static void Iniciar()
        {
            int opcao;
            do
            {
                try
                {
                    Console.WriteLine(MenuInicial());
                    if (!int.TryParse(Console.ReadLine(), out opcao))
                    {
                        Console.Clear();
                        Console.WriteLine("Por favor, insira uma das opções acima.");
                        continue;
                    }
                    Console.Clear();
                    switch (opcao)
                    {
                        case 1:
                            ConstroiGrafo();
                            break;
                        case 2:
                            ValidaGrafoDIMAC();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Por favor, insira uma opção válida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                    opcao = -1;
                }
            }
            while (true);
        }

        private static Imprime imprimir = new Imprime();
        private static Busca buscar = new Busca();
        private static Caminho calcular = new Caminho();

        private static void Cabecalho(string titulo = "Trabalho de Grafos")
        {
            StringBuilder cabecalho = new StringBuilder();
            Console.Clear();
            cabecalho.AppendLine("================================================");
            cabecalho.AppendLine($"         {titulo}");
            cabecalho.AppendLine("================================================\n");
            Console.WriteLine(cabecalho.ToString());
        }

        private static void Pausa()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private static string MenuInicial()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            Cabecalho("Bem-Vindo! Escolha uma opção.");
            menu.AppendLine("================================================");
            menu.AppendLine("1 - Construir o Grafo a mão");
            menu.AppendLine("2 – Importar arquivo DIMAC");
            menu.AppendLine("0 – Sair");
            menu.AppendLine("================================================");
            return menu.ToString();
        }

        #endregion
        #region Menu Operações
        private static int MenuOperacoes(IGrafo grafo)
        {
            Console.Clear();
            Cabecalho("Menu Principal");
            Console.WriteLine("1  – Adicionar aresta");
            Console.WriteLine("2  – Mostrar Grafo como Lista");
            Console.WriteLine("3  – Mostrar Grafo como Matriz");
            Console.WriteLine("4  – Imprimir arestas adjacentes a uma aresta");
            Console.WriteLine("5  – Imprimir vértices adjacentes a um vértice");
            Console.WriteLine("6  – Imprimir arestas incidentes a um vértice");
            Console.WriteLine("7  – Imprimir vértices incidentes a uma aresta");
            Console.WriteLine("8  – Imprimir grau de um vértice");
            Console.WriteLine("9  – Verificar se dois vértices são adjacentes");
            Console.WriteLine("10 – Substituir peso de uma aresta");
            Console.WriteLine("11 – Trocar dois vértices");
            Console.WriteLine("12 – Busca em Largura (BFS)");
            Console.WriteLine("13 – Busca em Profundidade (DFS)");
            Console.WriteLine("14 – Caminho mínimo (Dijkstra)");
            Console.WriteLine("15 – Caminho mínimo (Floyd‑Warshall)");
            Console.WriteLine("0  – Voltar");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out int opcao))
                return opcao;

            return -1;
        }

        private static void ControlarOperacoes(IGrafo grafo)
        {
            if (grafo == null)
            {
                Console.WriteLine("Nenhum grafo disponível.");
                Pausa();
                return;
            }

            int opcao;
            do
            {
                opcao = MenuOperacoes(grafo);

                switch (opcao)
                {
                    case 1: AdicionaAresta(grafo); break;
                    case 2: ImprimirListaAdj(grafo); break;
                    case 3: ImprimirMatrizAdj(grafo); break;
                    case 4: ImprimirArestasAdjacentes(grafo); break;
                    case 5: ImprimirVerticesAdjacentes(grafo); break;
                    case 6: ImprimirArestasIncidentes(grafo); break;
                    case 7: ImprimirVerticesIncidentesaAresta(grafo); break;
                    case 8: ImprimirGrauVertice(grafo); break;
                    case 9: VerificarAdjacenciaVertices(grafo); break;
                    case 10: SubstituirPesoAresta(grafo); break;
                    case 11: TrocarVertices(grafo); break;
                    case 12: ExecutarBfs(grafo); break;
                    case 13: ExecutarDfs(grafo); break;
                    case 14: ExecutarDijkstra(grafo); break;
                    case 15: ExecutarFloydWarshall(grafo); break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Saindo do menu...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (opcao != 0)
                    Pausa();

            } while (opcao != 0);
        }

        public static int LerInteiro(string mensagem)
        {
            int opcao;
            Console.Write($"{mensagem}: ");
            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                opcao = -1;
                Console.WriteLine("Favor digitar somente números.");
            }
            catch (OverflowException)
            {
                opcao = -1;
                Console.WriteLine("Favor digitar somente valores do menu.");
            }
            return opcao;
        }
        #endregion
        #region Métodos do Menu Principal
        public static bool AdicionaAresta(IGrafo grafo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Insira o número do vértice de origem:");
                if (!int.TryParse(Console.ReadLine(), out int verticeOrigem))
                {
                    Console.WriteLine("Vértice de origem inválido.");
                    Pausa();
                    return false;
                }

                Console.WriteLine("Insira o número do vértice de destino:");
                if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                {
                    Console.WriteLine("Vértice de destino inválido.");
                    Pausa();
                    return false;
                }

                Console.WriteLine("Insira o peso da aresta:");
                if (!int.TryParse(Console.ReadLine(), out int peso))
                {
                    Console.WriteLine("Peso inválido.");
                    Pausa();
                    return false;
                }
                Aresta a = new Aresta(verticeOrigem, verticeDestino, peso);
                if (grafo.AdicionarAresta(a))
                {
                    Console.Clear();
                    Console.WriteLine("Aresta adicionada com sucesso.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erro ao adicionar a aresta. Verifique os vértices.");
                    Pausa();
                    return false;
                }
                Pausa();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar aresta: {ex.Message}");
                Pausa();
                return false;
            }
        }
        private static void ConstroiGrafo()
        {
            
            try
            {
                Console.Clear();
                Cabecalho("Construção do Grafo");
                Console.WriteLine("Informe a quantidade de vértices:");
                if (!int.TryParse(Console.ReadLine(), out int numVertices) || numVertices <= 0)
                {
                    Console.WriteLine("Número de vértices inválido.");
                    return;
                }

                Console.WriteLine("Informe a quantidade de arestas:");
                if (!int.TryParse(Console.ReadLine(), out int numArestas) || numArestas < 0)
                {
                    Console.WriteLine("Número de arestas inválido.");
                    return;
                }

                Console.Clear();
                List<List<int>> dimac = ConstruirDimac(numVertices, numArestas);

                IGrafo grafo;

                if (CalculaDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafoMatriz(numVertices, dimac);
                }
                else
                {
                    grafo = new GrafoLista(numVertices, dimac);
                }

                Console.Clear();
                Console.WriteLine("Grafo criado com sucesso!");
                Pausa();
                ControlarOperacoes(grafo);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
            }

            //criar uma lista de vertices adjacentes
            //Definir se o grafo é direcionado ou não
            //Informar se as arestas têm direção(grafo direcionado) ou não(grafo não direcionado).

            //Definir se o grafo é ponderado ou não
            //Informar se as arestas possuem pesos(grafo ponderado) ou não.

            //Adicionar os vértices
            //Inserir os vértices no grafo com seus identificadores ou dados.

            //Adicionar as arestas
            //Inserir as conexões entre os vértices, informando peso e direção se necessário.

            //Definir o tipo de representação
            //Escolher se o grafo será representado por lista de adjacência, matriz de adjacência ou matriz de incidência.

            //Inicializar o grafo
            //Criar a instância do grafo com base na representação escolhida(por exemplo, inicializar listas ou matrizes).
        }

        public static List<List<int>> ConstruirDimac(int numVertices, int numArestas)
        {
            List<List<int>> dimac = new List<List<int>>();

            for (int i = 1; i <= numArestas; i++)
            {
                try
                {
                    Cabecalho("Construção do Grafo DIMAC");
                    Console.WriteLine($"Informe o vértice de origem da aresta {i} (0 a {numVertices - 1}):");
                    if (!int.TryParse(Console.ReadLine(), out int verticeOrigem) || verticeOrigem < 0 || verticeOrigem >= numVertices)
                    {
                        Console.WriteLine("Vértice de origem inválido.");
                        i--;
                        continue;
                    }

                    Console.WriteLine($"Informe o vértice de destino da aresta {i} (0 a {numVertices - 1}):");
                    if (!int.TryParse(Console.ReadLine(), out int verticeDestino) || verticeDestino < 0 || verticeDestino >= numVertices)
                    {
                        Console.WriteLine("Vértice de destino inválido.");
                        i--;
                        continue;
                    }

                    Console.WriteLine($"Informe o peso da aresta {i}:");
                    if (!int.TryParse(Console.ReadLine(), out int peso) || peso < 0)
                    {
                        Console.WriteLine("Peso inválido.");
                        i--;
                        continue;
                    }

                    dimac.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar a aresta {i}: {ex.Message}");
                    i--;
                }
            }

            return dimac;
        }

        public static void ValidaGrafoDIMAC()
        {
            try
            {
                Console.Write("Caminho do arquivo DIMACS: ");
                string caminhoArquivo = Console.ReadLine();
                if (!File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("Arquivo não encontrado.");
                    return;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo)
                                      .Where(l => !string.IsNullOrWhiteSpace(l))
                                      .ToArray();

                if (linhas.Length < 1)
                {
                    Console.WriteLine("Arquivo DIMACS inválido.");
                    return;
                }

                string[] primeiraLinha = linhas[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (primeiraLinha.Length != 2 ||
                    !int.TryParse(primeiraLinha[0], out int numVertices) ||
                    !int.TryParse(primeiraLinha[1], out int numArestas))
                {
                    Console.WriteLine($"Formato inválido na primeira linha: {linhas[0]}");
                    return;
                }

                List<List<int>> dimac = new List<List<int>>();

                for (int i = 1; i < linhas.Length; i++)
                {
                    string linhaAtual = linhas[i].Trim();
                    Console.WriteLine($"Processando linha {i + 1}: '{linhaAtual}'");

                    if (string.IsNullOrWhiteSpace(linhaAtual))
                    {
                        Console.WriteLine($"Linha {i + 1} está vazia ou contém apenas espaços. Ignorando.");
                        continue;
                    }

                    string[] dadosAresta = linhaAtual.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (dadosAresta.Length != 3)
                    {
                        Console.WriteLine($"Formato inválido na linha {i + 1}: '{linhaAtual}'");
                        continue;
                    }

                    if (!int.TryParse(dadosAresta[0], out int verticeOrigem) ||
                        !int.TryParse(dadosAresta[1], out int verticeDestino) ||
                        !int.TryParse(dadosAresta[2], out int peso))
                    {
                        Console.WriteLine($"Valores inválidos na linha {i + 1}: '{linhaAtual}'");
                        continue;
                    }

                    verticeDestino--;
                    verticeOrigem--;
                    dimac.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }

                IGrafo grafo;
                if (CalculaDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafoMatriz(numVertices, dimac);
                }
                else
                {
                    grafo = new GrafoLista(numVertices, dimac);
                }

                Console.Clear();
                Console.WriteLine("Grafo criado com sucesso!");
                
                Console.ReadLine();
                Console.Clear();
                ControlarOperacoes(grafo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
                Pausa();
                Console.ReadLine();
            }
        }

        public static double CalculaDensidade(int numVertices, int numArestas)
        {
            double densidade = (2 * numArestas) / (numVertices * (numVertices - 1));
            return densidade;
        }
        
        //private static Vertice ObterVerticePorId(IGrafo grafo)
        //{
        //    //int id = lerInteiro("ID do vértice");
        //    //return grafo.ObterVertice(id);
        //}


        //private static Aresta ObterArestaPorId(IGrafo grafo)
        //{
        //    //int id = lerInteiro("ID da aresta");
        //    //return grafo.ObterAresta(id);
        //}

        private static void ImprimirListaAdj(IGrafo grafo)
        {
            //imprimir.ImprimirListaAdj(grafo);
        }

        private static void ImprimirMatrizAdj(IGrafo grafo)
        {
            //imprimir.ImprimirMatrizAdj(grafo);
        }


        private static void ImprimirArestasAdjacentes(IGrafo grafo)
        {
            //Aresta a = ObterArestaPorId(grafo);
            //imprimir.ImprimirArestaAdj(a);
            //Pausa();
        }


        private static void ImprimirVerticesAdjacentes(IGrafo grafo)
        {
            //Vertice v = ObterVerticePorId(grafo);
            //imprimir.ImprimirVerticeAdj(v);
            //Pausa();
        }


        private static void ImprimirArestasIncidentes(IGrafo grafo)
        {
            //Vertice v = ObterVerticePorId(grafo);
            //imprimir.ImprimirArestasInc(v);
            //Pausa();
        }


        private static void ImprimirVerticesIncidentesaAresta(IGrafo grafo)
        {
            //Aresta a = ObterArestaPorId(grafo);
            //imprimir.ImprimirVerticeInc(a);
            //Pausa();
        }


        private static void ImprimirGrauVertice(IGrafo grafo)
        {
            //Vertice v = ObterVerticePorId(grafo);
            //int grau = imprimir.ImprimirGrau(v);
            //Console.WriteLine($"\nGrau: {grau}");
            //Pausa();
        }


        private static void VerificarAdjacenciaVertices(IGrafo grafo)
        {
            //Console.WriteLine("Primeiro vértice:");
            //Vertice v1 = ObterVerticePorId(grafo);
            //Console.WriteLine("Segundo vértice:");
            //Vertice v2 = ObterVerticePorId(grafo);
            //bool adj = Vertice.SaoAdjacentes(v1, v2);
            //Console.WriteLine(adj ? "Adjacentes" : "Não adjacentes");
            //Pausa();
        }


        private static void SubstituirPesoAresta(IGrafo grafo)
        {

        }


        private static void TrocarVertices(IGrafo grafo)
        {
        //    Console.WriteLine("Vértice v1:");
        //    Vertice v1 = ObterVerticePorId(grafo);
        //    Console.WriteLine("Vértice v2:");
        //    Vertice v2 = ObterVerticePorId(grafo);
        //    grafo.TrocaDoisVertices(v1, v2);
        //    Console.WriteLine("Vértices trocados!");
        //    Pausa();
        }


        private static void ExecutarBfs(IGrafo grafo)
        {
            //Vertice raiz = ObterVerticePorId(grafo);
            //buscar.BuscaEmLargura(raiz);
            //Pausa();
        }


        private static void ExecutarDfs(IGrafo grafo)
        {
            //Vertice raiz = ObterVerticePorId(grafo);
            //buscar.BuscaProfundidade(raiz);
            //Pausa();
        }


        private static void ExecutarDijkstra(IGrafo grafo)
        {
            //Console.WriteLine("Origem:");
            //Vertice origem = ObterVerticePorId(grafo);
            //Console.WriteLine("Destino:");
            //Vertice destino = ObterVerticePorId(grafo);
            //List<Vertice> caminho = calcular.Dijkstra(origem, destino);
            //imprimir.ImprimirDijkstra(caminho);
            //Pausa();
        }


        private static void ExecutarFloydWarshall(IGrafo grafo)
        {
            //Console.WriteLine("Origem:");
            //Vertice origem = ObterVerticePorId(grafo);
            //calcular.FloydWarshal(origem, grafo);
            //Pausa();
        }
        #endregion

    }
}

