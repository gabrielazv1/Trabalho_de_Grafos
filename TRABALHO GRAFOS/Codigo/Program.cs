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
    public class Program
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
                        case 3:
                            Console.Clear();
                            ImportarBeeCrowd();
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
            menu.AppendLine("3 – Importar arquivo BeeCrowd");
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
                            Imprime.ImprimirListaAdj();
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
                            Imprime.ImprimirMatrizAdj();
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
                            Separador();
                            Console.Write("Informe o vértice de DESTINO da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Imprime.ImprimirArestasAdj(verticeOrigem - 1, verticeDestino - 1, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }
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

                            Imprime.ImprimirVerticesAdj(grafo, id_verticeV - 1);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }

                        break;
                    case 5:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int vertice))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Clear();
                            Separador();
                            Imprime.ImprimirArestasInc(vertice - 1, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;
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

                            Imprime.ImprimirVerticeInc(grafo, id_verticeOrigem - 1, id_verticeDestino - 1);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }


                        break;
                    case 7:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int vertice))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Clear();
                            Separador();
                            Imprime.ImprimirGrau(vertice - 1, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;
                    case 8:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice ORIGEM: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v1))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();
                            Console.Write("Informe o vértice DESTINO: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v2))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Vertice v1 = new Vertice(id_v1 - 1);
                            Vertice v2 = new Vertice(id_v2 - 1);

                            Imprime.ImprimirAdjacentes(v1, v2, grafo);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }

                        break;
                    case 9:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vertice de ORIGEM da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeOrigem))
                            {
                                throw new Exception("Entrada inválida, a entrada ");
                            }

                            Separador();

                            Console.Write("Informe o vertice de DESTINO da aresta: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser inteira");
                            }

                            Separador();

                            Console.Write($"Informe o novo peso da aresta ({verticeOrigem}, {verticeDestino}): ");

                            if (!int.TryParse(Console.ReadLine(), out int peso))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser inteira");
                            }

                            Separador();
                            Console.Clear();

                            Separador();
                            Imprime.TrocarPesoArestas(verticeOrigem - 1, verticeDestino - 1, peso, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "Aperte ENTER para continuar..."); }

                        break;
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
                            Separador();
                            Console.Write("Segundo vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int id_v2))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Separador();

                            Vertice v1 = new Vertice(id_v1);
                            Vertice v2 = new Vertice(id_v2);

                            Imprime.ImprimirTrocaVertices(grafo, id_v1 - 1, id_v2 - 1);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }

                        break;
                    case 11:
                        try
                        {
                            Console.Clear();
                            Separador();

                            Console.Write("Informe o vértice: ");

                            if (!int.TryParse(Console.ReadLine(), out int vertice))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Console.Clear();
                            Separador();
                            Imprime.ImprimirBuscaLargura(grafo, vertice - 1);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;
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

                            Imprime.ImprimirBuscaProfundidade(grafo, idVraiz - 1);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }

                        break;
                    case 13:

                        try
                        {
                            Console.Clear();
                            Separador();
                            Console.Write("Informe o vértice de ORIGEM: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeOrigem))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }

                            Separador();

                            Console.Write("Informe o vértice de DESTINO: ");

                            if (!int.TryParse(Console.ReadLine(), out int verticeDestino))
                            {
                                throw new Exception("Entrada inválida, a entrada deve ser um número inteiro.");
                            }
                            Console.Clear();
                            Separador();
                            Imprime.ImprimirDijkstra(verticeOrigem - 1, verticeDestino - 1, grafo);
                            Separador();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + " Aperte ENTER para continuar"); }

                        break;
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

                            Imprime.ImprimirFloydWarshal(grafo, id_vertOrigem - 1);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message + "\nPressione ENTER para continuar..."); }

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
                    Console.Write("\nNúmero de vértices inválido.\n");
                    Pausa();
                    return;
                }
                Separador();
                Console.Write("Informe a quantidade de arestas: ");
                if (!int.TryParse(Console.ReadLine(), out int numArestas) || numArestas < 0)
                {
                    Console.Write("\nNúmero de arestas inválido.\n");
                    Pausa();
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
            List<List<int>> arestas = new List<List<int>>();


            for (int i = 1; i <= numArestas; i++)
            {
                try
                {
                    Cabecalho("Construção do Grafo");
                    Console.Write($"Informe o vértice de origem da aresta {i} (0 a {numVertices - 1}): ");
                    if (!int.TryParse(Console.ReadLine(), out int verticeOrigem) || verticeOrigem < 0 || verticeOrigem >= numVertices)
                    {
                        Console.WriteLine("\nVértice de origem inválido.\n");
                        Pausa();
                        i--;
                        continue;
                    }
                    Separador();
                    Console.Write($"Informe o vértice de destino da aresta {i} (0 a {numVertices - 1}): ");
                    if (!int.TryParse(Console.ReadLine(), out int verticeDestino) || verticeDestino < 0 || verticeDestino >= numVertices)
                    {
                        Console.WriteLine("\nVértice de destino inválido.\n");
                        Pausa();
                        i--;
                        continue;
                    }
                    Separador();
                    Console.Write($"Informe o peso da aresta {i}: ");
                    if (!int.TryParse(Console.ReadLine(), out int peso) || peso < 0)
                    {
                        Console.WriteLine("\nPeso inválido.\n");
                        Pausa();
                        i--;
                        continue;
                    }
                    Console.WriteLine();
                    arestas.Add(new List<int> { verticeOrigem, verticeDestino, peso });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar a aresta {i}: {ex.Message}");
                    i--;
                }
            }

            return arestas;
        }

        public static void ImportarGrafoDimacs()
        {
            try
            {
                Separador();
                Console.WriteLine("Caminho do arquivo DIMACs (ex: C:\\pasta\\arquivo.txt):");
                string caminhoArquivo = Console.ReadLine();
                Separador();
                if (string.IsNullOrWhiteSpace(caminhoArquivo))
                {
                    Console.WriteLine("\nO caminho informado está vazio ou inválido.\n");
                    Pausa();
                    return;
                }

                if (!File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("\nArquivo não encontrado. Verifique se o caminho está correto.\n");
                    Pausa();
                    return;
                }

                Separador();
                string[] linhas = File.ReadAllLines(caminhoArquivo)
                                      .Where(l => !string.IsNullOrWhiteSpace(l))
                                      .ToArray();

                if (linhas.Length < 1)
                {
                    Console.WriteLine("\nArquivo DIMACs inválido.\n");
                    Pausa();
                    return;
                }

                string[] primeiraLinha = linhas[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (primeiraLinha.Length != 2 ||
                    !int.TryParse(primeiraLinha[0], out int numVertices) ||
                    !int.TryParse(primeiraLinha[1], out int numArestas))
                {
                    Console.WriteLine($"\nFormato inválido na primeira linha: {linhas[0]}\n");
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
                Console.Clear();
                if (CalculaDensidade(numVertices, numArestas) >= 0.5)
                {
                    grafo = new GrafoMatriz(numVertices, dimacs);
                    Separador();
                    Console.WriteLine("Criando grafo em formato de MATRIZ\n"
                                    + "de acordo com o cálculo da densidade...");
                    Separador();
                    Pausa();
                }
                else
                {
                    grafo = new GrafoLista(numVertices, dimacs);
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
            if (numVertices <= 1) return 0;
            double densidade = (double)numArestas / (numVertices * (numVertices - 1));
            return densidade;
        }
        #endregion

        #region Método BeeCrowd

        static List<(int destino, int peso)>[] grafo;
        static bool[] visitado;
        static List<(int u, int v, int peso)> caminhoAtual;

        public static void ImportarBeeCrowd()
        {
            try
            {
                Separador();
                Console.WriteLine("Caminho do arquivo BeeCrowd (ex: C:\\pasta\\arquivo.txt):");
                string caminhoArquivo = Console.ReadLine();
                Separador();

                if (string.IsNullOrWhiteSpace(caminhoArquivo))
                {
                    Console.WriteLine("\nO caminho informado está vazio ou inválido.\n");
                    Pausa();
                    return;
                }

                if (!File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("\nArquivo não encontrado. Verifique se o caminho está correto.\n");
                    Pausa();
                    return;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo)
                                      .Where(l => !string.IsNullOrWhiteSpace(l))
                                      .ToArray();

                if (linhas.Length < 2)
                {
                    Console.WriteLine("\nArquivo BeeCrowd inválido: poucas linhas.\n");
                    Pausa();
                    return;
                }

                string[] primeiraLinha = linhas[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (primeiraLinha.Length != 2 ||
                    !int.TryParse(primeiraLinha[0], out int N) ||
                    !int.TryParse(primeiraLinha[1], out int K))
                {
                    Console.WriteLine($"\nFormato inválido na primeira linha: {linhas[0]}\n");
                    Pausa();
                    return;
                }

                var arestas = new List<(int, int, int)>();
                for (int i = 1; i <= N - 1; i++)
                {
                    if (i >= linhas.Length)
                    {
                        Console.WriteLine($"\nFaltam arestas no arquivo.\n");
                        Pausa();
                        return;
                    }

                    string[] partes = linhas[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length != 3 ||
                        !int.TryParse(partes[0], out int u) ||
                        !int.TryParse(partes[1], out int v) ||
                        !int.TryParse(partes[2], out int peso))
                    {
                        Console.WriteLine($"Erro na linha {i + 1}: '{linhas[i]}'");
                        Pausa();
                        return;
                    }

                    arestas.Add((u, v, peso));
                }

                if (N >= linhas.Length)
                {
                    Console.WriteLine("\nArquivo incompleto: faltam rotas.\n");
                    Pausa();
                    return;
                }

                if (!int.TryParse(linhas[N], out int Q))
                {
                    Console.WriteLine($"\nValor inválido para número de rotas: {linhas[N]}\n");
                    Pausa();
                    return;
                }

                var rotas = new List<(int, int)>();
                for (int i = N + 1; i < linhas.Length; i++)
                {
                    string[] partes = linhas[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length != 2 ||
                        !int.TryParse(partes[0], out int origem) ||
                        !int.TryParse(partes[1], out int destino))
                    {
                        Console.WriteLine($"Erro na linha {i + 1}: '{linhas[i]}'");
                        Pausa();
                        return;
                    }

                    rotas.Add((origem, destino));
                }

                Console.Clear();
                Separador();
                Console.WriteLine("Arquivo BeeCrowd lido com sucesso!");
                Separador();

                var desafio = new Desafio();
                int resultado = desafio.DesafioPokemon(N, K, arestas, rotas);

                Console.WriteLine($"Maior força possível: {resultado}");
                Separador();
                Pausa();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar arquivo BeeCrowd: {ex.Message}");
                Pausa();
            }
        }

        static bool BuscaProfundidade(int atual, int destino)
        {
            if (atual == destino)
                return true;

            visitado[atual] = true;

            foreach (var (vizinho, peso) in grafo[atual])
            {
                if (!visitado[vizinho])
                {
                    caminhoAtual.Add((atual, vizinho, peso));
                    if (BuscaProfundidade(vizinho, destino))
                        return true;
                    caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
                }
            }

            return false;
        }

        static int DesafioPokemon(int N, int K, List<(int, int, int)> arestas, List<(int, int)> rotas)
        {
            grafo = new List<(int destino, int peso)>[N + 1];
            for (int i = 0; i <= N; i++)
                grafo[i] = new List<(int destino, int peso)>();

            foreach ((int u, int v, int w) in arestas)
            {
                grafo[u].Add((v, w));
                grafo[v].Add((u, w));
            }

            List<(int custo, int valor)> itens = new List<(int custo, int valor)>();

            foreach ((int origem, int destino) in rotas)
            {
                visitado = new bool[N + 1];
                caminhoAtual = new List<(int u, int v, int peso)>();

                BuscaProfundidade(origem, destino);

                HashSet<int> verticesVisitados = new HashSet<int>();
                int maiorForca = 0;

                foreach ((int u, int v, int peso) in caminhoAtual)
                {
                    verticesVisitados.Add(u);
                    verticesVisitados.Add(v);
                    if (peso > maiorForca)
                        maiorForca = peso;
                }

                int custo = verticesVisitados.Count;
                if (custo <= K)
                    itens.Add((custo, maiorForca));
            }

            int[] dp = new int[K + 1];
            for (int i = 0; i <= K; i++)
                dp[i] = 0;

            foreach ((int custo, int valor) in itens)
            {
                for (int j = K; j >= custo; j--)
                {
                    if (dp[j - custo] + valor > dp[j])
                        dp[j] = dp[j - custo] + valor;
                }
            }

            int max = 0;
            foreach (int valor in dp)
            {
                if (valor > max)
                    max = valor;
            }

            return max == 0 ? -1 : max;
        }
        #endregion
    }
}

