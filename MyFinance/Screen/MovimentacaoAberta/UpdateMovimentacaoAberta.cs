using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;
using System.Globalization;

namespace MyFinance.Screen.MovimentacaoAberta;

public class UpdateMovimentacaoAberta
{
    public static void Update()
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
            .Title("[green]Selecione o ID da movimentação a ser editada:[/]")
            .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
            .AddChoiceGroup("ID Movimentações", listaIDMovimentacoes.ToArray())
            .AddChoices(new[] { "[red]Sair[/]" })
            );
            if (movimentacaoSelecionada != "[red]Sair[/]") 
            {
                var informacoesMovimentacaoSelecionada = movimentacoes.FirstOrDefault(x => x.Id == int.Parse(movimentacaoSelecionada));
                var dadosSelecionado = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Selecione a informação que deseja editar:[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
                .AddChoiceGroup("Historico", informacoesMovimentacaoSelecionada!.Historico)
                .AddChoiceGroup("Data de Lançamento", informacoesMovimentacaoSelecionada.DataLancamento.ToString("dd/MM/yyyy"))
                .AddChoiceGroup("Valor Atual", informacoesMovimentacaoSelecionada.Valor.ToString())
                .AddChoiceGroup("Debito Ou Credito", informacoesMovimentacaoSelecionada.DC.ToString())
                .AddChoiceGroup("Item", informacoesMovimentacaoSelecionada.Item.Nome)
                .AddChoices(new[] { "[red]Sair[/]" })
                );

                if (dadosSelecionado != "[red]Sair[/]")
                {
                    if (dadosSelecionado == informacoesMovimentacaoSelecionada.Historico)
                        informacoesMovimentacaoSelecionada.Historico = AnsiConsole.Ask<string>("[green]Digite o novo historico[/]");

                    else if (dadosSelecionado == informacoesMovimentacaoSelecionada.DataLancamento.ToString("dd/MM/yyyy"))
                        informacoesMovimentacaoSelecionada.DataLancamento = DateTime.ParseExact(AnsiConsole.Ask<string>("[green]Digite a nova data do lançamento[/]"),
                                                                                                "dd/MM/yyyy",
                                                                                                CultureInfo.CurrentCulture);

                    else if (dadosSelecionado == informacoesMovimentacaoSelecionada.Valor.ToString())
                        informacoesMovimentacaoSelecionada.Valor = AnsiConsole.Ask<decimal>("[green]Digite o novo valor[/]");

                    else if (dadosSelecionado == informacoesMovimentacaoSelecionada.DC.ToString())
                        informacoesMovimentacaoSelecionada.DC = AnsiConsole.Prompt(
                                                                                    new SelectionPrompt<string>()
                                                                                    .Title("[green]Selecione o novo tipo de movimentação:[/]")
                                                                                    .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
                                                                                    .AddChoices(new[] { "[blue]Credito[/]", "[red]Debito[/]" })) == "[blue]Credito[/]" ? 'C' : 'D';

                    else if (dadosSelecionado == informacoesMovimentacaoSelecionada.Item.Nome)
                    {
                        var itensDisponiveis = new List<string>();
                        var repositoryItem = new Repository<Item>(DataBase.connection);
                        var itens = repositoryItem.Get();
                        foreach (var item in itens)
                        {
                            itensDisponiveis.Add(item.Nome);
                        }
                        informacoesMovimentacaoSelecionada.Item.Nome = AnsiConsole.Prompt(
                                                                                            new SelectionPrompt<string>()
                                                                                            .Title("[green]Selecione o ITEM da movimentação:[/]")
                                                                                            .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
                                                                                            .AddChoices(itensDisponiveis.ToArray())
                                                                                            );
                        informacoesMovimentacaoSelecionada.IdItem = repositoryItem.Get().FirstOrDefault(x => x.Nome == informacoesMovimentacaoSelecionada.Item.Nome)!.Id;
                    }

                    repositoryMovimentacao.Update(new Movimentacao
                    {
                        Id = informacoesMovimentacaoSelecionada.Id,
                        Historico = informacoesMovimentacaoSelecionada.Historico,
                        Valor = informacoesMovimentacaoSelecionada.Valor,
                        DataLancamento = informacoesMovimentacaoSelecionada.DataLancamento,
                        DC = informacoesMovimentacaoSelecionada.DC,
                        IdItem = informacoesMovimentacaoSelecionada.IdItem
                    });

                    AnsiConsole.MarkupLine("[green]Movimentação atualizada com sucesso![/]");
                }
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao atualizar movimentação {ex.Message}[/]");
        }
        finally
        {
            Thread.Sleep(1000);
            MenuMovimentacaoAberta.Exibir();
        }
    }
}
