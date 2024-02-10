using MyFinance.Model;
using MyFinance.Repositories;
using MyFinance.Screen.Itens;
using MyFinance.Screen.MovimentacaoAberta;
using MyFinance.Screen.UsuarioMenu;
using Spectre.Console;

namespace MyFinance.Screen.MenuPricipal;
public static class Menu
{
    public static void Exibir()
    {
        var repository = new Repository<Usuario>(DataBase.connection);
        var usuario = repository.Get();
        if (usuario.Count() != 0)
        {
            Console.Clear();
            CarregaResumoMovimentacoes(usuario.FirstOrDefault()!.Salario);   
            var option = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                                .Title("[green]Selecione a opção desejada[/]")
                                                .PageSize(10)
                                                .MoreChoicesText("[blue](Uilize as setas para cima e para baixo para selecionar a opção.)[/]")
                                                .AddChoices(new[] {
                                                       "1-Meus Dados",
                                                       "2-Itens",
                                                       "3-Movimentações",
                                                       "4-Sair"
                                                }));
            switch (option) 
            {
                case "1-Meus Dados": MenuUsuario.Exibir(); break;
                case "2-Itens": MenuItens.Exibir(); break;
                case "3-Movimentações": MenuMovimentacaoAberta.Exibir(); break;
                case "4-Sair": Environment.Exit(0); break;
                default: Exibir(); break;
            }
            
        }
        else
        {
            AnsiConsole.Markup("[Red]Identificamos que é o seu primeiro acesso! Por gentileza preencha os dados abaixo:[/]");
            Console.WriteLine();
            CreateUsuario.Create();
        }
        
    }
    private static void CarregaResumoMovimentacoes(decimal salario) 
    {
        var table = new Table();

        var repositoryMovimentacao = new MovimentacaoRepository(DataBase.connection);
        var movimentacoes = repositoryMovimentacao.Get();
        var valorTotalCredito = movimentacoes.Where(x => x.DC == 'C').Sum( x=> x.Valor);
        valorTotalCredito += salario;
        var valorTotalDebito  =  movimentacoes.Where(x => x.DC == 'D').Sum(x => x.Valor);
        var valorLiquido = valorTotalCredito - valorTotalDebito;
        var cor = "green";
        if (valorLiquido < 0)
            cor = "orangered1";
        table.AddColumns(new string[] { "[blue]TOTAL DE CREDITOS[/]", "[red]TOTAL DE DEBITOS[/]", $"[{cor}]SALDO LIQUIDO[/]" });
        table.AddRow(new string[] { $"[blue]{valorTotalCredito}[/]", $"[red]{valorTotalDebito}[/]", $"[{cor}]{valorLiquido}[/]"});
        AnsiConsole.Write(table);
    }
}
