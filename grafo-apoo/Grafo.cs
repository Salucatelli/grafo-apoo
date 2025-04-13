using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace grafo_apoo;

internal class Grafo
{
    //Lista com todos os vértices do grafo
    public List<Vertice> Vertices { get; set; } = new List<Vertice>();
    //public List<Aresta> Arestas { get; set; } =  new List<Aresta>();

    //Insere um novo vértice e adiciona um valor
    public Vertice insertVertex(int id)
    {
        Vertice vertice = new Vertice()
        {
            Id = id,
            Valor = "valor"
        };

        Vertices.Add(vertice);
        return vertice;
    }

    //Exibe todos os vértices
    public void VerVertices()
    {
        Console.WriteLine("Vertices: ");
        foreach(Vertice vertice in Vertices)
        {
            Console.WriteLine($" - Valor do Vértice: {vertice.Valor}\n (Nº {vertice.Id})");
            Console.WriteLine($"  - Arestas:");
            foreach (var aresta in vertice.Arestas)
            {
                Console.WriteLine($"   - {aresta.Valor} - Origem: {aresta.VerticeOrigem.Id} / Destino: {aresta.VerticeDestino.Id}");
            }
            Console.WriteLine("\n");
        }
    }

    //Insere uma aresta com um vértice de origem, um de destino e um valor
    public Aresta insertEdge(Vertice origem, Vertice destino, int valor)
    {
        Aresta aresta = new Aresta()
        {
            VerticeDestino = destino,
            VerticeOrigem = origem,
            Valor = valor
        };

        origem.Arestas.Add(aresta);
        destino.Arestas.Add(aresta);

        return aresta;
    }

    //Retorna o valor de um vértice
    public object vertexValue(Vertice vertice)
    {
        return vertice.Valor;
    }

    //Retorna o Valor de uma aresta
    public object edgeValue(Aresta aresta)
    {
        return aresta.Valor;
    }

    //Substitui o valor do vertice v
    public void replaceVertex(Vertice v, object o)
    {
        v.Valor = o;
    }

    //Substitui o valor da aresta a
    public void replaceEdge(Aresta a, int o)
    {
        a.Valor = o;
    }

    //Retorna true se os vertices forem adjacentes, se não retorna false
    public bool areAdjacent(Vertice v1, Vertice v2)
    {
        foreach(Aresta a in v1.Arestas)
        {
            if(a.VerticeDestino == v2)
            {
                return true;
            }
        }
        return false;
    }

    //Retorna o vertice oposto a 'v' na aresta 'a'. Se não encontrar, retorna null
    public Vertice? opposite(Vertice v, Aresta a)
    {
        if(a.VerticeDestino == v)
        {
            return a.VerticeOrigem;
        }
        else if(a.VerticeOrigem == v)
        {
            return a.VerticeDestino;
        }
        return null;
    }

    //Retorna uma lista com os dois vértices finais da aresta a
    public List<Vertice> endVertices(Aresta a)
    {
        return new List<Vertice>() { a.VerticeOrigem, a.VerticeDestino };
    }

    //Remove uma aresta
    public object removeEdge(Aresta a)
    {
        var obj = a.Valor;

        foreach(var vertice in Vertices)
        {
            //Lista de arestas para remover
            List<Aresta> paraRemover = new List<Aresta>();

            foreach (var aresta in vertice.Arestas)
            {
                if(aresta == a)
                {
                    //vertice.Arestas.Remove(a);
                    paraRemover.Add(aresta);
                }
            }

            foreach(var aresta in paraRemover)
                vertice.Arestas.Remove(aresta);
        }

        //Arestas.Remove(a);
        return obj;
    }

    //Remove um vértice e todas as suas arestas
    public object removeVertex(Vertice v)
    {
        var obj = v.Valor;

        foreach(var vertice in Vertices)
        {
            //Lista de arestas que serão removidas
            List<Aresta> paraRemover = new List<Aresta>();

            foreach (var a in vertice.Arestas)
            {
                if(a.VerticeDestino == v || a.VerticeOrigem == v)
                {
                    //vertice.Arestas.Remove(a);
                    //Arestas.Remove(a);
                    paraRemover.Add(a);
                }
            }

            //Remove as arestas
            foreach(var aresta in paraRemover)
            {
                vertice.Arestas.Remove(aresta);
            }
        }

        Vertices.Remove(v);

        return obj;
    }

    //Algoritmo Dijkstra
    public void Dijkstra(Vertice origem, Vertice destino)
    {
        var distancias = new Dictionary<Vertice, double>();
        var anteriores = new Dictionary<Vertice, Vertice?>();
        var naoVisitados = new List<Vertice>(Vertices);

        foreach (var v in Vertices)
        {
            distancias[v] = double.PositiveInfinity;
            anteriores[v] = null;
        }

        distancias[origem] = 0;

        while (naoVisitados.Count > 0)
        {
            // Pega o vértice mais perto
            var atual = naoVisitados.OrderBy(v => distancias[v]).First();
            naoVisitados.Remove(atual);

            // Se chegou ao final ele para
            if (atual == destino)
                break;

            foreach (var aresta in atual.Arestas)
            {
                var vizinho = opposite(atual, aresta);
                if (vizinho != null && naoVisitados.Contains(vizinho))
                {
                    double peso = Convert.ToDouble(aresta.Valor);
                    double novaDistancia = distancias[atual] + peso;

                    if (novaDistancia < distancias[vizinho])
                    {
                        distancias[vizinho] = novaDistancia;
                        anteriores[vizinho] = atual;
                    }
                }
            }
        }

        var caminho = new List<Vertice>();
        var atualNoCaminho = destino;

        while (atualNoCaminho != null)
        {
            caminho.Insert(0, atualNoCaminho);
            atualNoCaminho = anteriores[atualNoCaminho];
        }

        // Se o primeiro vertice do caminho não for a origem, não existe caminho
        if (caminho.First() != origem)
            Console.WriteLine("Não existe caminho entre os vértices");
        else
        {
            Console.WriteLine("Caminho mais curto (Id dos vértices): ");
            foreach (var ver in caminho)
            {
                Console.Write($"{ver.Id}");
                if(ver != caminho.Last())
                {
                    Console.Write(" - ");
                }
            }
            Console.WriteLine("\n");
        }
    }


    //Função para ler o arquivo e cirar o grafo
    public void CarregarGrafoDoArquivo(string caminhoArquivo)
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);
        int numVertices = int.Parse(linhas[0]);

        // Criar vértices
        for (int i = 0; i < numVertices; i++)
        {
            insertVertex(i + 1); // Assumindo que os valores dos vértices são seus IDs
        }

        // Criar arestas
        for (int i = 1; i < linhas.Length; i++)
        {
            string[] dadosAresta = linhas[i].Split(' ');
            int origemId = int.Parse(dadosAresta[0]);
            int destinoId = int.Parse(dadosAresta[1]);
            //int peso = int.Parse(dadosAresta[2]);

            Vertice origem = Vertices.Find(v => v.Id == origemId)!;
            Vertice destino = Vertices.Find(v => v.Id == destinoId)!;
            insertEdge(origem, destino, 1);
        }
    }

}
