using System.Text.Json.Serialization;

namespace DesafioTarget.Models;

public class EstoqueData
{
    [JsonPropertyName("estoque")]
    public List<Produto> Estoque { get; set; } = new();
}