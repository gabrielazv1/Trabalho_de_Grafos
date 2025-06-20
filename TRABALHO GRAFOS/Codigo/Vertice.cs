using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRABALHO_GRAFOS.Codigo
{
    public class Vertice
    {
        public int id;
        private List<Aresta> arestas = new List<Aresta>();
        private List<Vertice> verticesAdjacentes = new List<Vertice>();

        private int descoberto = 0;
        private int termino = 0;
        private bool visitado = false;
        private Vertice pai = null;

        public Vertice Pai
        {
            get { return pai; }
            set { pai = value; }
        }


        public List<Vertice> VerticesAdjacentes
        {
            get { return verticesAdjacentes; }
            private set { verticesAdjacentes = value; }
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

        public override bool Equals(object obj)
        {
            return obj is Vertice v && id == v.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public void AdicionarAresta(Aresta a)
        {
            arestas.Add(a);
        }

        public override string ToString()
        {
            return $"ID Vértice: {id}";
        }
    }
}
