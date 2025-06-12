using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Aresta
    {
        public int origem;
        public int destino;
        public int peso;

        public Aresta(int origem, int destino, int peso)
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
