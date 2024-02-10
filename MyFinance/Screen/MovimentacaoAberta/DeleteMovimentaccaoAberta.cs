using MyFinance.Repositories;
using Spectre.Console;

namespace MyFinance.Screen.MovimentacaoAberta;

public static class DeleteMovimentaccaoAberta
{
    public static void Delete()
    {
        try
        {
            var table = new Table();
            table.AddColumns(new string[] { "ID", "DATA", "HISTORICO", "VALOR", "TIPO DE MOVIMENTACAO", "ITEM" });

            var repositoryMovimentacao = new MovimentacaoRepository(DataBase.connection);
            var movimentacoes = repositoryMovimentacao.RetornaMovimentacaoEItem().OrderBy(X => X.DataLancamento);
            var listaIDMovimentacoes = new List<string>();

            if (movimentacoes.Count() == 0)
                throw new Exception("Não existem Movimentações cadastradas");

            foreach (var movimentacao in movimentacoes)
            { 
                var cor = "blue";

                if (movimentacao.DC == 'D')
                    cor = "red";

                table.AddRow(new string[]{
                                         $"[{cor}]{movimentacao.Id}[/]",
                                         $"[{cor}]{movimentacao.DataLancamento.ToString("dd/MM/yyyy")}[/]",
                                         $"[{cor}]{movimentacao.Historico}[/]",
                                         $"[{cor}]{movimentacao.Valor}[/]",
                                         $"[{cor}]{movimentacao.DC}[/]",
                                         $"[{cor}]{movimentacao.Item.Nome}[/]",
                });
                listaIDMovimentacoes.Add(movimentacao.Id.ToString());

            }
            AnsiConsole.Write(table);
            var movimentacaoSelecionada = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[green]Selecione o ID da movimentação a ser excluida:[/]")
            .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser excluida)[/]")
            .AddChoiceGroup("ID Movimentações", listaIDMovimentacoes.ToArray())
            .AddChoices(new[] { "[red]Sair[/]" })
            );
            if (movimentacaoSelecionada != "[red]Sair[/]")
            {
                repositoryMovimentacao.Delete(int.Parse(movimentacaoSelecionada));
            }
            AnsiConsole.MarkupLine("[green]Movimentação excluida com sucesso![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao excluir Movimentação {ex.Message}[/]");
        }
        finally
        {
            Thread.Sleep(1000);
            MenuMovimentacaoAberta.Exibir();
        }

    }
}
