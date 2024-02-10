using Dapper.Contrib.Extensions;

namespace MyFinance.Model;

[Table("Item")]
public class Item
{
    public int Id { get; set; }
    public string Nome { get; set; }
}
