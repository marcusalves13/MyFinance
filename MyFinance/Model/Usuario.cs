using Dapper.Contrib.Extensions;

namespace MyFinance.Model;

[Table("Usuario")]
public class Usuario
{
    public int Id { get; set; } 
    public string Nome { get; set; }
    public string CPF  { get; set; }
    public decimal Salario { get; set; }
}
