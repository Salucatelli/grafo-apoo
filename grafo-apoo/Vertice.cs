using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo_apoo;

internal class Vertice
{
    public int Id {  get; set; }
    public object Valor { get; set; }
    public List<Aresta> Arestas { get; set; } = new();
}
