using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo.Interface
{
    public interface IGrafo
    {
        void TrocaDoisVertices(Vertice v1, Vertice v2);
        bool AdicionarVertice(Vertice v);
        bool AdicionarAresta(Aresta a);
    }
}
