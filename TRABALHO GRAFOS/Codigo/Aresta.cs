using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Aresta
    {
        public Vertice origem;
        public Vertice destino;
        public int peso;

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


        public Aresta(Vertice origem, Vertice destino, int peso)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }

        public void SubstituiPeso(int peso)
        {

        }
    }
}
