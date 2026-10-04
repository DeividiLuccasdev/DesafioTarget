using DesafioTarget.Models;

namespace DesafioTarget.Services;

public class EstoqueService
{
    public Movimentacao Movimentar(
        Produto produto,
        string tipo,
        int quantidade)
    {
        var movimentacao = new Movimentacao
        {
            Quantidade = quantidade
        };

        if (tipo.Equals("entrada", StringComparison.OrdinalIgnoreCase))
        {
            produto.Estoque += quantidade;
            movimentacao.Descricao = "Entrada de mercadoria";
        }
        else if (tipo.Equals("saida", StringComparison.OrdinalIgnoreCase))
        {
            if (quantidade > produto.Estoque)
            {
                throw new ArgumentException(
                    $"Estoque insuficiente. Disponível: {produto.Estoque}."
                );
            }

            produto.Estoque -= quantidade;
            movimentacao.Descricao = "Saída de mercadoria";
        }
        else
        {
            throw new ArgumentException(
                "Tipo de movimentação inválido. Utilize entrada ou saida."
            );
        }

        return movimentacao;
    }
}