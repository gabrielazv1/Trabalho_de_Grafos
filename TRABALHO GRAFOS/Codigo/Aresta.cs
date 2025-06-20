using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    public class Aresta
    {
        private Vertice origem;
        private Vertice destino;
        private int peso;

        public Vertice Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        public Vertice Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        public int Peso
        {
            get { return peso; }
            set { peso = value; }
        }


        public Aresta(Vertice origem, Vertice destino, int peso)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }

        public Aresta(Vertice origem, Vertice destino)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = 0;
        }

        public void SubstituiPeso(int peso)
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Aresta aresta && this.origem.Equals(aresta.origem) && this.destino.Equals(aresta.destino) && this.peso == aresta.peso;
        }
    }
}
