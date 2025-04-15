using grafo_apoo;
using System.Diagnostics.Metrics;

Grafo grafo = new Grafo();

bool arquivoCarregado = false;

while (true)
{
    Console.WriteLine(MenuFunction());

    string resp = "";

    resp = Convert.ToString(Console.ReadLine())!;

    if(int.TryParse(resp, out int option))
    {
        switch (option)
        {
            case 0:
                Console.WriteLine("Volte Sempre");
                return;
            case 1:
                InserirVertice();
                break;
            case 2:
                VerVertices();
                break;
            case 3:
                AdicionarAresta();
                break;
            case 4:
                SubstituirVertice();
                break;
            case 5:
                SubstituirAresta();
                break;
            case 6:
                RemoverVertice();
                break;
            case 7:
                RemoverAresta();
                break;
            case 8:
                EncontrarMenorCaminho();
                break;
            case 9:
                CarregarArquivo();
                break;
            default:
                Console.Clear();
                continue;
        }
    }
    else
    {
        Console.Clear();
    }
}

static string MenuFunction()
{
    string menu = @"====Bem vindo ao programa do Grafo, o que deseja fazer?====

    1 - Inserir Vértice
    2 - Ver Vértices
    3 - Adicionar Aresta
    4 - Substituir Vértice
    5 - Substituir Aresta
    6 - Remover Vértice
    7 - Remover Aresta
    8 - Encontrar menor caminha entre dois vértices
    9 - Carregar Grafo do arquivo .txt
    0 - Sair do Programa
";
    return menu;
}

void InserirVertice()
{
    Console.Clear();
    int valor = 0;

    Console.WriteLine("Digite o id do Vértice que deseja criar: ");
    valor = Convert.ToInt32(Console.ReadLine())!;

    var vertice = grafo.insertVertex(valor);

    if(vertice == null)
    {
        Console.WriteLine("Erro ao criar vértice, id já existe");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine($"\nVértice {vertice.Valor} criado com sucesso!");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
    MenuFunction();
    return;
}

void VerVertices()
{
    Console.Clear();
    grafo.VerVertices();
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void AdicionarAresta()
{
    Console.Clear();

    int origem = 0;
    int destino = 0;
    int valor = 0;

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº {vertex.Id} - {vertex.Valor}");
    }

    Console.WriteLine("Digite o numero do vértice de origem: ");
    origem = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite o numero do vértice de destino: ");
    destino = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Digite o valor da aresta (Peso da aresta): ");
    valor = Convert.ToInt32(Console.ReadLine())!;

    //Valida para não serem iguais
    if (origem == destino)
    {
        Console.WriteLine("Uma aresta não pode ter o mesmo vértice de origem e destino");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Vertice verOrigem = grafo.Vertices.FirstOrDefault(v => v.Id == origem)!;
    Vertice verDestino = grafo.Vertices.FirstOrDefault(v => v.Id == destino)!;

    var aresta = grafo.insertEdge(verOrigem, verDestino, valor);

    if(aresta == null)
    {
        Console.WriteLine("Já existe uma aresta entre estes dois vértices");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine($"Aresta {aresta.Valor} Adicionada com sucesso!\n");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
    MenuFunction();
    return;
}

void SubstituirVertice()
{
    Console.Clear();

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº {vertex.Id} - {vertex.Valor}");
    }

    int v = 0;
    string valor = "";

    Console.WriteLine("Escolha o vértice que deseja substituir: ");
    v = Convert.ToInt32(Console.ReadLine());

    Vertice? vertice = grafo.Vertices.FirstOrDefault(vert => vert.Id == v) ?? null;

    if(vertice is null)
    {
        Console.WriteLine("Vértice não encontrado!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine("Escolha o valor: ");
    valor = Console.ReadLine()!.ToString();

    vertice.Valor = valor;
    Console.WriteLine("Valor alterado com sucesso");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void SubstituirAresta()
{
    Console.Clear();

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº [{vertex.Id}] ");
    }

    int v = 0;
    int a = 0;
    int counter = 1;
    int valor = 0;

    Console.WriteLine("Escolha o vértice: ");
    v = Convert.ToInt32(Console.ReadLine());

    Vertice? vertice = grafo.Vertices.FirstOrDefault(vert => vert.Id == v) ?? null;

    //Verifica se o vértice existe
    if (vertice is null)
    {
        Console.WriteLine("Vértice não encontrado!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine("\nArestas disponíveis: ");
    foreach (var ar in vertice!.Arestas)
    {
        Console.WriteLine($"Nº: {counter} - Origem: {ar.VerticeOrigem.Valor} - Destino: {ar.VerticeDestino.Valor}");
        counter++;
    }

    Console.WriteLine("Escolha a aresta: ");
    a = Convert.ToInt32(Console.ReadLine());

    Aresta? aresta = vertice.Arestas[a-1] ?? null;

    //Verifica se a aresta existe
    if (aresta is null)
    {
        Console.WriteLine("Aresta não encontrada!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine("Escolha o valor: ");
    valor = Convert.ToInt32(Console.ReadLine());

    aresta.Valor = valor;
    Console.WriteLine("Valor alterado com sucesso");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void RemoverVertice()
{
    int v = 0;

    Console.Clear();

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº {vertex.Id} - {vertex.Valor}");
    }

    Console.WriteLine("\nEscolha o vértice: ");
    v = Convert.ToInt32(Console.ReadLine());

    Vertice? vertice = grafo.Vertices.FirstOrDefault(ver => ver.Id == v) ?? null;

    //Valida se o vértice existe
    if(vertice is null)
    {
        Console.WriteLine("Vértice não encontardo!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    grafo.removeVertex(vertice);

    Console.WriteLine("Vertice removido com sucesso");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void RemoverAresta()
{
    int v = 0;
    int a = 0;
    int counter = 1;

    Console.Clear();

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº {vertex.Id} - {vertex.Valor}");
    }

    Console.WriteLine("\nEscolha o vértice: ");
    v = Convert.ToInt32(Console.ReadLine());

    Vertice? vertice = grafo.Vertices.FirstOrDefault(ver => ver.Id == v) ?? null;

    //Valida se o vértice existe
    if (vertice is null)
    {
        Console.WriteLine("Vértice não encontardo!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    Console.WriteLine("\nArestas disponíveis: ");
    foreach (var ar in vertice!.Arestas)
    {
        Console.WriteLine($"Nº: {counter} - Origem: {ar.VerticeOrigem.Valor} - Destino: {ar.VerticeDestino.Valor}");
        counter++;
    }

    Console.WriteLine("Escolha a aresta: ");
    a = Convert.ToInt32(Console.ReadLine());

    Aresta? aresta = vertice.Arestas[a - 1] ?? null;

    //Verifica se a aresta existe
    if (aresta is null)
    {
        Console.WriteLine("Aresta não encontrada!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    grafo.removeEdge(aresta);

    Console.WriteLine("Aresta removida com sucesso");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void EncontrarMenorCaminho()
{
    int vo = 0;
    int vd = 0;

    Console.Clear();

    Console.WriteLine("Vertices disponíveis: ");
    foreach (var vertex in grafo.Vertices)
    {
        Console.WriteLine($"Nº {vertex.Id} - {vertex.Valor}");
    }

    //Vértice de origem
    Console.WriteLine("\nEscolha o vértice de origem: ");
    vo = Convert.ToInt32(Console.ReadLine());

    //Vértice de destino
    Console.WriteLine("\nEscolha o vértice de destino: ");
    vd = Convert.ToInt32(Console.ReadLine());

    Vertice? verticeDestino = grafo.Vertices.FirstOrDefault(ver => ver.Id == vo) ?? null;
    Vertice? verticeOrigem = grafo.Vertices.FirstOrDefault(ver => ver.Id == vd) ?? null;

    //Valida se o vértice existe
    if (verticeDestino is null || verticeOrigem is null)
    {
        Console.WriteLine("Vértice não encontardo!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        MenuFunction();
        return;
    }

    grafo.Dijkstra(verticeOrigem, verticeDestino);

    Console.WriteLine("\nDigite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}

void CarregarArquivo()
{
    Console.Clear();

    if(!arquivoCarregado)
        arquivoCarregado = true;
    else
    {
        Console.WriteLine("O arquivo já foi carregado!");
        Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        return;
    }

    string arquivoPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\grafo.txt"));

    grafo.CarregarGrafoDoArquivo(arquivoPath);

    Console.WriteLine("Arquivo carregado com sucesso!");
    Console.WriteLine("Digite Qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}