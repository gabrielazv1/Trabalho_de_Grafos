using System;
using System.Collections.Generic;

namespace TRABALHO_GRAFOS.Codigo
{
    public class Desafio
    {
        private int nVertices;
        private int limiteK;
        private int logN;

        private List<(int, int)>[] adjacentes;
        private int[] profundidade;
        private int[][] pai;
        private int[][] pesoMaximo;

        public int DesafioPokemon(int nVertices, int limiteK, List<(int u, int v, int peso)> arestas, List<(int origem, int destino)> rotas)
        {
            this.nVertices = nVertices;
            this.limiteK = limiteK;

            adjacentes = new List<(int, int)>[nVertices + 1];
            for (int i = 1; i <= nVertices; i++)
                adjacentes[i] = new List<(int, int)>();

            foreach ((int u, int v, int peso) in arestas)
            {
                adjacentes[u].Add((v, peso));
                adjacentes[v].Add((u, peso));
            }

            logN = 1;
            while ((1 << logN) <= nVertices) logN++;

            profundidade = new int[nVertices + 1];
            for (int i = 0; i <= nVertices; i++)
                profundidade[i] = -1;

            pai = new int[nVertices + 1][];
            pesoMaximo = new int[nVertices + 1][];
            for (int i = 0; i <= nVertices; i++)
            {
                pai[i] = new int[logN];
                pesoMaximo[i] = new int[logN];
            }

            InicializarProfundidadeEAncestrais(1);

            for (int k = 1; k < logN; k++)
            {
                for (int v = 1; v <= nVertices; v++)
                {
                    int meio = pai[v][k - 1];
                    pai[v][k] = pai[meio][k - 1];
                    pesoMaximo[v][k] = Math.Max(pesoMaximo[v][k - 1], pesoMaximo[meio][k - 1]);
                }
            }

            List<(int custo, int valor)> itens = new List<(int custo, int valor)>();

            foreach ((int origem, int destino) in rotas)
            {
                int ancestralComum = ObterLCA(origem, destino);
                int custoCaminho = profundidade[origem] + profundidade[destino] - 2 * profundidade[ancestralComum] + 1;

                if (custoCaminho <= limiteK)
                {
                    int maiorPeso = Math.Max(ConsultaPesoMaximo(origem, ancestralComum), ConsultaPesoMaximo(destino, ancestralComum));
                    itens.Add((custoCaminho, maiorPeso));
                }
            }

            int[] dp = new int[limiteK + 1];
            for (int i = 1; i <= limiteK; i++)
                dp[i] = -1;
            dp[0] = 0;

            foreach ((int custo, int valor) in itens)
            {
                for (int j = limiteK; j >= custo; j--)
                {
                    if (dp[j - custo] != -1)
                        dp[j] = Math.Max(dp[j], dp[j - custo] + valor);
                }
            }

            int resposta = -1;
            for (int i = 0; i <= limiteK; i++)
                if (dp[i] > resposta)
                    resposta = dp[i];

            if (resposta <= 0)
                resposta = -1;

            return resposta;
        }

        private void InicializarProfundidadeEAncestrais(int raiz)
        {
            Stack<int> pilha = new Stack<int>();
            profundidade[raiz] = 0;
            pai[raiz][0] = 0;
            pilha.Push(raiz);

            while (pilha.Count > 0)
            {
                int atual = pilha.Pop();

                foreach ((int vizinho, int peso) in adjacentes[atual])
                {
                    if (profundidade[vizinho] == -1)
                    {
                        profundidade[vizinho] = profundidade[atual] + 1;
                        pai[vizinho][0] = atual;
                        pesoMaximo[vizinho][0] = peso;
                        pilha.Push(vizinho);
                    }
                }
            }
        }

        private int ObterLCA(int u, int v)
        {
            if (profundidade[u] < profundidade[v])
            {
                int temp = u; u = v; v = temp;
            }

            int diferenca = profundidade[u] - profundidade[v];
            for (int i = 0; i < logN; i++)
                if (((diferenca >> i) & 1) == 1)
                    u = pai[u][i];

            if (u == v) return u;

            for (int i = logN - 1; i >= 0; i--)
            {
                if (pai[u][i] != pai[v][i])
                {
                    u = pai[u][i];
                    v = pai[v][i];
                }
            }

            return pai[u][0];
        }

        private int ConsultaPesoMaximo(int u, int ancestral)
        {
            int resultado = 0;
            int diferenca = profundidade[u] - profundidade[ancestral];
            for (int i = 0; i < logN; i++)
            {
                if (((diferenca >> i) & 1) == 1)
                {
                    resultado = Math.Max(resultado, pesoMaximo[u][i]);
                    u = pai[u][i];
                }
            }
            return resultado;
        }
    }

}
