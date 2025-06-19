using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    internal class Vertice
    {
        public int id;
        public List<Aresta> arestas = new List<Aresta>();
        public List<Vertice> verticesAdjacentes = new List<Vertice>();

        private int descoberto = 0;
        private int termino = 0;
        private bool visitado = false;
        private Vertice pai = null;

        public Vertice Pai
        {
            get { return pai; }
            set { pai = value; }
        }

        public bool Visitado
        {
            get { return visitado; }
            set { visitado = value; }
        }

        public int Descoberto
        {
            get { return descoberto; }
            set { descoberto = value; }
        }

        public int Termino
        {
            get { return termino; }
            set { termino = value; }
        }


        public List<Aresta> Arestas
        {
            get { return arestas; }
            set { arestas = value; }
        }

        public Vertice(int Id)
        {
            id = Id;
        }

        public Vertice()
        {
        }

        public static bool SaoAdjacentes(Vertice v1, Vertice v2)
        {
            return v1.verticesAdjacentes.Contains(v2);
        }

        public override bool Equals(object obj)
        {
            return obj is Vertice v && id == v.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
