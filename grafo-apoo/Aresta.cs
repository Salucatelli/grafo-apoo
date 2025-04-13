using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo_apoo;

internal class Aresta
{
    public Vertice VerticeOrigem { get; set; }
    public Vertice VerticeDestino { get; set; }
    public int Valor { get; set; }
}
