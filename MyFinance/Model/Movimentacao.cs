using Dapper.Contrib.Extensions;

namespace MyFinance.Model;

[Table("MOVIMENTACAO_ABERTA")]
public class Movimentacao
{
    public Movimentacao()
    {
        Item = new Item();
    }
    public int Id { get; set; }
    public string Historico { get; set; }
    public decimal Valor { get; set; }
    public char DC { get; set; }
    public DateTime DataLancamento { get; set; }
    public int IdItem { get; set; }
    [Write(false)]
    public Item Item { get; set; }
}
