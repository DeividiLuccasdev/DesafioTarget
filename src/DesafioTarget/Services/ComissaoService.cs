using DesafioTarget.Models;

namespace DesafioTarget.Services;

public class ComissaoService
{
    public decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 100m)
        {
            return 0m;
        }

        if (valorVenda < 500m)
        {
            return valorVenda * 0.01m;
        }

        return valorVenda * 0.05m;
    }

    public Dictionary<string, decimal> CalcularComissoesPorVendedor(
        IEnumerable<Venda> vendas)
    {
        return vendas
            .GroupBy(venda => venda.Vendedor)
            .ToDictionary(
                grupo => grupo.Key,
                grupo => grupo.Sum(
                    venda => CalcularComissao(venda.Valor)
                )
            );
    }
}