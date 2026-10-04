using System.Text.Json;
using DesafioTarget.Models;
using DesafioTarget.Services;
using System.Globalization;

// ============================
// EXERCÍCIO 1 - COMISSÕES
// ============================

var caminhoVendas = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "vendas.json"
);

if (File.Exists(caminhoVendas))
{
    var jsonVendas = File.ReadAllText(caminhoVendas);

    var dadosVendas =
        JsonSerializer.Deserialize<VendasData>(jsonVendas);

    if (dadosVendas is not null)
    {
        var comissaoService = new ComissaoService();

        var comissoes =
            comissaoService.CalcularComissoesPorVendedor(
                dadosVendas.Vendas
            );

        Console.WriteLine("========================================");
        Console.WriteLine("       COMISSÃO DOS VENDEDORES");
        Console.WriteLine("========================================");
        Console.WriteLine();

        foreach (var resultado in comissoes)
        {
            Console.WriteLine(
                $"{resultado.Key,-20} R$ {resultado.Value:N2}"
            );
        }

        Console.WriteLine();
    }
}

// ============================
// EXERCÍCIO 2 - ESTOQUE
// ============================

var caminhoEstoque = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "estoque.json"
);

if (!File.Exists(caminhoEstoque))
{
    Console.WriteLine("Arquivo de estoque não encontrado.");
    return;
}

var jsonEstoque = File.ReadAllText(caminhoEstoque);

var dadosEstoque =
    JsonSerializer.Deserialize<EstoqueData>(jsonEstoque);

if (dadosEstoque is null)
{
    Console.WriteLine("Não foi possível carregar o estoque.");
    return;
}

Console.WriteLine("========================================");
Console.WriteLine("        MOVIMENTAÇÃO DE ESTOQUE");
Console.WriteLine("========================================");
Console.WriteLine();

foreach (var produto in dadosEstoque.Estoque)
{
    Console.WriteLine(
        $"{produto.CodigoProduto} - {produto.DescricaoProduto} - Estoque: {produto.Estoque}"
    );
}

Console.WriteLine();

Console.Write("Informe o código do produto: ");
var codigoTexto = Console.ReadLine();

if (!int.TryParse(codigoTexto, out var codigoProduto))
{
    Console.WriteLine("Código inválido.");
    return;
}

var produtoSelecionado =
    dadosEstoque.Estoque.FirstOrDefault(
        produto => produto.CodigoProduto == codigoProduto
    );

if (produtoSelecionado is null)
{
    Console.WriteLine("Produto não encontrado.");
    return;
}

Console.Write("Informe o tipo da movimentação (entrada/saida): ");
var tipo = Console.ReadLine() ?? string.Empty;

Console.Write("Informe a quantidade: ");
var quantidadeTexto = Console.ReadLine();

if (!int.TryParse(quantidadeTexto, out var quantidade))
{
    Console.WriteLine("Quantidade inválida.");
    return;
}

var estoqueService = new EstoqueService();

try
{
    var movimentacao = estoqueService.Movimentar(
        produtoSelecionado,
        tipo,
        quantidade
    );

    Console.WriteLine();
    Console.WriteLine("Movimentação realizada com sucesso.");
    Console.WriteLine($"Identificador: {movimentacao.Id}");
    Console.WriteLine($"Descrição: {movimentacao.Descricao}");
    Console.WriteLine($"Produto: {produtoSelecionado.DescricaoProduto}");
    Console.WriteLine($"Quantidade movimentada: {movimentacao.Quantidade}");
    Console.WriteLine($"Estoque final: {produtoSelecionado.Estoque}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
// ============================
// EXERCÍCIO 3 - JUROS
// ============================

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("          CÁLCULO DE JUROS");
Console.WriteLine("========================================");
Console.WriteLine();

Console.Write("Informe o valor: R$ ");
var valorTexto = Console.ReadLine();

var culturaBrasileira = new CultureInfo("pt-BR");

if (!decimal.TryParse(
        valorTexto,
        NumberStyles.Number,
        culturaBrasileira,
        out var valor))
{
    Console.WriteLine("Valor inválido.");
    return;
}

Console.Write("Informe a data de vencimento (dd/mm/aaaa): ");
var dataTexto = Console.ReadLine();

if (!DateTime.TryParseExact(
        dataTexto,
        "dd/MM/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out var dataVencimento))
{
    Console.WriteLine("Data inválida.");
    return;
}

var jurosService = new JurosService();

var juros = jurosService.CalcularJuros(
    valor,
    dataVencimento
);

Console.WriteLine();
Console.WriteLine($"Valor dos juros: R$ {juros:N2}");