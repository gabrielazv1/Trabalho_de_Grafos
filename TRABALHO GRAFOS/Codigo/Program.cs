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
                    opcao = MenuInicial();
                    switch (opcao)
                    {
                        case 1:
                            Console.Clear();
                            SelecionaDadosDoGrafo();
                            break;
                        case 2:
                            Console.Clear();
                            ImportarGrafoDimacs();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Por favor, insira uma opção válida.");
                            Pausa();
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
        private static int MenuInicial()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            Cabecalho("Bem-Vindo ao Trabalho de Grafos!");
            menu.AppendLine("1 - Construir o Grafo a mão");
            menu.AppendLine("2 – Importar arquivo DIMACs");
            menu.AppendLine("0 – Sair");

            Console.Write(menu.ToString());
            Separador();
            return LerInteiro("Escolha uma opção: ");
        }

        public static void Cabecalho(string titulo = "Trabalho de Grafos")
        {
            StringBuilder cabecalho = new StringBuilder();
            Console.Clear();
            cabecalho.Append("\n");
            Separador();
            cabecalho.AppendLine($"        {titulo}");
            Console.WriteLine(cabecalho.ToString());
            Separador();
            cabecalho.Append("\n");
        }

        private static void Separador()
        {
            StringBuilder separador = new StringBuilder();
            separador.AppendLine("================================================");
            Console.Write(separador.ToString());
        }

        private static void Pausa()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\nPressione ENTER para continuar...");
            Console.Write(sb.ToString());
            Console.ReadLine();
        }

        public static int LerInteiro(string mensagem)
        {
            Console.Write($"{mensagem}");
            string entrada = Console.ReadLine();

            try
            {
                return int.Parse(entrada);
            }
            catch (FormatException)
            {
                Console.WriteLine("Favor digitar somente números.");
                return -1;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Favor digitar apenas valores do menu.");
                return -1;
            }
        }

        #endregion
        #region Menu Operações
        private static int MenuOperacoes(Grafo grafo)
        {
            Console.Clear();
            Cabecalho("Menu Principal");

            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1  – Mostrar Grafo como Lista");
            menu.AppendLine("2  – Mostrar Grafo como Matriz");
            menu.AppendLine("3  – Imprimir arestas adjacentes a uma aresta");
            menu.AppendLine("4  – Imprimir vértices adjacentes a um vértice");
            menu.AppendLine("5  – Imprimir arestas incidentes a um vértice");
            menu.AppendLine("6  – Imprimir vértices incidentes a uma aresta");
            menu.AppendLine("7  – Imprimir grau de um vértice");
            menu.AppendLine("8  – Verificar se dois vértices são adjacentes");
            menu.AppendLine("9  – Substituir peso de uma aresta");
            menu.AppendLine("10 – Trocar dois vértices");
            menu.AppendLine("11 – Busca em Largura (BFS)");
            menu.AppendLine("12 – Busca em Profundidade (DFS)");
            menu.AppendLine("13 – Caminho mínimo (Dijkstra)");
            menu.AppendLine("14 – Caminho mínimo (Floyd-Warshall)");
            menu.AppendLine("0  – Voltar");

            Console.Write(menu.ToString());
            Separador();

            return LerInteiro("Escolha uma opção: ");
        }

        private static void ControlarOperacoes(Grafo grafo)
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
                    case 1:
                        if (grafo is GrafoLista glista)
                        {
                            Console.Clear();
                            Cabecalho("Lista de Adjacência");
                            glista.ImprimirListaAdjacencia();
                            Separador();
                        }

                        else
                        {
                            Console.Clear();
                            Separador();
                            Console.WriteLine("Este grafo não usa lista de adjacência.");
                            Separador();
                        }

                        break;
                    case 2:
                        if (grafo is GrafoMatriz gmatriz)
                        {
                            Console.Clear();
                            Cabecalho("Matriz de Adjacência");
                            gmatriz.ImprimirMatrizAdjacencia();
                            Separador();
                        }

                        else
                        {
                            Console.Clear();
                            Separador();
                            Console.WriteLine("Este grafo não usa matriz de adjacência.");
                            Separador();
                        }

                        break;
                    case 3:
                        try
                        {
                            Console.Clear();
                            Separador();
                            Console.Write("Informe o vértice de ORIGEM da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeOrigem))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Write("Informe o vértice de DESTINO da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Imprime.ImprimirArestasAdj(verticeOrigem, verticeDestino, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }
                        break;

                    case 4:
                        try
                        {
                            Console.Clear();
                            Separador();
                            Console.Write("Informe o vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_verticeV))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Separador();

                            Imprime.ImprimirVerticesAdj(grafo, id_verticeV);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;

                    case 5: ImprimirArestasIncidentes(grafo); break;

                    case 6:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice de ORIGEM da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_verticeOrigem))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Write("Informe o vértice de DESTINO da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_verticeDestino))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Imprime.ImprimirVerticeInc(grafo, id_verticeOrigem, id_verticeDestino);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }


                        break;

                    case 7: ImprimirGrauVertice(grafo); break;

                    case 8:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice V1: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v1))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Write("Informe o vértice V2: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v2))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Vertice v1 = new Vertice(id_v1);
                            Vertice v2 = new Vertice(id_v2);

                            Imprime.ImprimirAdjacentes(v1, v2, grafo);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;

                    case 9: SubstituirPesoAresta(grafo); break;

                    case 10:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.WriteLine("Informe quais vértices gostaria de trocar...");
                            Console.Write("Primeiro vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v1))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Write("Segundo vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v2))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Vertice v1 = new Vertice(id_v1);
                            Vertice v2 = new Vertice(id_v2);

                            Imprime.ImprimirTrocaVertices(grafo, id_v1, id_v2);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;

                    case 11: ExecutarBfs(grafo); break;

                    case 12:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe qual será o vértice raiz: ");

                            if (!int.TryParse(Console.ReadLine(), out int idVraiz))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Imprime.ImprimirBuscaProfundidade(grafo, idVraiz);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;

                    case 13: ExecutarDijkstra(grafo); break;


                    case 14:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe qual será o vértice de origem: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_vertOrigem))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Imprime.ImprimirFloydWarshal(grafo, id_vertOrigem);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;


                    case 0:
                        Console.Clear();
                        break;
                }

                if (opcao != 0)
                    Pausa();

            } while (opcao != 0);
        }
        #endregion
        #region Métodos de Criação do Grafo
        private static void SelecionaDadosDoGrafo()
        {

            try
            {
                Console.Clear();
                Cabecalho("Capturando Dados para Construir Grafo");
                Console.Write("Informe a quantidade de vértices: ");
                if (!int.TryParse(Console.ReadLine(), out int numVertices) || numVertices <= 0)
                {
                    Console.Write("Número de vértices inválido.");
                    return;
                }
                Separador();
                Console.Write("Informe a quantidade de arestas: ");
                if (!int.TryParse(Console.ReadLine(), out int numArestas) || numArestas < 0)
                {
                    Console.Write("Número de arestas inválido.");
                    return;
                }
                Separador();
                Console.Clear();
                List<List<int>> arestasLidas = ConstruirGrafo(numVertices, numArestas);

                Grafo grafo;
                Separador();
                if (CalculaDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafoMatriz(numVertices, arestasLidas);
                    Console.Clear();
                    Separador();
                    Console.WriteLine("Criando grafo em formato de MATRIZ\n"
                                    + "de acordo com o cálculo da densidade...");
                    Separador();
                    Pausa();
                }
                else
                {
                    grafo = new GrafoLista(numVertices, arestasLidas);
                    Console.Clear();
                    Separador();
                    Console.WriteLine("Criando grafo em formato de LISTA\n"
                                    + "de acordo com o cálculo da densidade...");
                    Separador();
                    Pausa();
                }

                Console.Clear();
                Separador();
                Console.WriteLine("Grafo criado com sucesso!");
                Separador();
                Pausa();
                ControlarOperacoes(grafo);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
            }
        }

        public static List<List<int>> ConstruirGrafo(int numVertices, int numArestas)
        {
            List<List<int>> dimac = new List<List<int>>();


            for (int i = 1; i <= numArestas; i++)
            {
                try
                {
                    Cabecalho("Construção do Grafo");
                    Console.Write($"Informe o vértice de origem da aresta {i} (0 a {numVertices - 1}): ");
                    if (!int.TryParse(Console.ReadLine(), out int verticeOrigem) || verticeOrigem < 0 || verticeOrigem >= numVertices)
                    {
                        Console.WriteLine("Vértice de origem inválido.");
                        i--;
                        continue;
                    }
                    Separador();
                    Console.Write($"Informe o vértice de destino da aresta {i} (0 a {numVertices - 1}): ");
                    if (!int.TryParse(Console.ReadLine(), out int verticeDestino) || verticeDestino < 0 || verticeDestino >= numVertices)
                    {
                        Console.WriteLine("Vértice de destino inválido.");
                        i--;
                        continue;
                    }
                    Separador();
                    Console.Write($"Informe o peso da aresta {i}: ");
                    if (!int.TryParse(Console.ReadLine(), out int peso) || peso < 0)
                    {
                        Console.WriteLine("Peso inválido.");
                        i--;
                        continue;
                    }
                    Console.WriteLine();
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

        public static void ImportarGrafoDimacs()
        {
            try
            {
                Separador();
                Console.Write("Caminho do arquivo DIMACs: ");
                string caminhoArquivo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(caminhoArquivo))
                {
                    Console.WriteLine("O caminho informado está vazio ou inválido.");
                    Pausa();
                    return;
                }

                if (!File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("Arquivo não encontrado. Verifique se o caminho está correto.");
                    Pausa();
                    return;
                }

                Separador();
                string[] linhas = File.ReadAllLines(caminhoArquivo)
                                      .Where(l => !string.IsNullOrWhiteSpace(l))
                                      .ToArray();

                if (linhas.Length < 1)
                {
                    Console.WriteLine("Arquivo DIMACs inválido.");
                    Pausa();
                    return;
                }

                string[] primeiraLinha = linhas[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (primeiraLinha.Length != 2 ||
                    !int.TryParse(primeiraLinha[0], out int numVertices) ||
                    !int.TryParse(primeiraLinha[1], out int numArestas))
                {
                    Console.WriteLine($"Formato inválido na primeira linha: {linhas[0]}");
                    Pausa();
                    return;
                }

                List<List<int>> dimacs = new List<List<int>>();

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
                    dimacs.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }

                Grafo grafo;
                if (CalculaDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafoMatriz(numVertices, dimacs);
                    Console.Clear();
                    Separador();
                    Console.WriteLine("Criando grafo em formato de MATRIZ\n"
                                    + "de acordo com o cálculo da densidade...");
                    Separador();
                    Pausa();
                }
                else
                {
                    grafo = new GrafoLista(numVertices, dimacs);
                    Console.Clear();
                    Separador();
                    Console.WriteLine("Criando grafo em formato de LISTA\n"
                                    + "de acordo com o cálculo da densidade...");
                    Separador();
                    Pausa();
                }

                Console.Clear();
                Separador();
                Console.WriteLine("Grafo criado com sucesso!");
                Separador();
                Pausa();
                Console.Clear();
                ControlarOperacoes(grafo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar grafo: {ex.Message}");
                Pausa();
            }
        }

        public static double CalculaDensidade(int numVertices, int numArestas)
        {
            double densidade = (2 * numArestas) / (numVertices * (numVertices - 1));
            return densidade;
        }
        #endregion
        #region Métodos do Menu Principal


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

