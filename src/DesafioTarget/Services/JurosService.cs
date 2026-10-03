namespace DesafioTarget.Services;

public class JurosService
{
    public decimal CalcularJuros(decimal valor, DateTime dataVencimento)
    {
        var hoje = DateTime.Today;

        var diasAtraso = (hoje - dataVencimento.Date).Days;

        if (diasAtraso <= 0)
        {
            return 0m;
        }

        return valor * 0.025m * diasAtraso;
    }
}