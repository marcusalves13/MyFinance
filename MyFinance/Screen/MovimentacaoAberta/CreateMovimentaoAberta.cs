using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;
using System.ComponentModel;
using System.Globalization;

namespace MyFinance.Screen.MovimentacaoAberta;

public class CreateMovimentaoAberta
{
    public static void Create()
    {
        var itensDisponiveis = new List<string>();
        var repositoryItem = new Repository<Item>(DataBase.connection);
        try
        {
            var itens = repositoryItem.Get();

            if (itens.Count() == 0)
                throw new Exception("Não existem itens cadastrados!");

            foreach (var item in itens)
            {
                itensDisponiveis.Add(item.Nome);
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao listar itens disponiveis {ex.Message}[/]");
            Thread.Sleep(1000);
            MenuMovimentacaoAberta.Exibir();
        }

        var historico = AnsiConsole.Ask<string>("[green]Digite o historico da movimentação:[/]");
        var valor = AnsiConsole.Ask<decimal>("[green]Digite o valor da movimentação:[/]");
        var debitoOuCredito = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("[green]Selecione o tipo de movimentação:[/]")
                                .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
                                .AddChoices(new[] { "[blue]Credito[/]", "[red]Debito[/]" })
                                ) == "[blue]Credito[/]" ? 'C' : 'D';
        var dataLancamento = DateTime.ParseExact(AnsiConsole.Ask<string>("[green]Digite a data do lançamento:[/]"), 
                                                                         "dd/MM/yyyy",
                                                                          CultureInfo.CurrentCulture);
        var itemSelecionado = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[green]Selecione o ITEM da movimentação:[/]")
            .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
            .AddChoices(itensDisponiveis.ToArray())
            );
        try
        {
            var movimentacaoRepository = new MovimentacaoRepository(DataBase.connection);
            movimentacaoRepository.Create(new Movimentacao
            {
                Historico = historico,
                Valor = valor,
                DC = debitoOuCredito,
                DataLancamento = dataLancamento,
                IdItem = repositoryItem.Get().FirstOrDefault(x => x.Nome == itemSelecionado)!.Id
            });
            AnsiConsole.MarkupLine("[green]Movimentação inserida com sucesso![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao inserir movimentação itens disponiveis {ex.Message}[/]");
        }
        finally 
        {
            Thread.Sleep(1000);
            MenuMovimentacaoAberta.Exibir();
        }

    }
}
