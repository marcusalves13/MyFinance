using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;

namespace MyFinance.Screen.Itens;

public static class UpdateItem
{
    public static void Update() 
    {
        try
        {
            var repository = new Repository<Item>(DataBase.connection);
            var itens = repository.Get();
            List<String> itensCadastrados = new List<string>();

            if (itens.Count() == 0)
                throw new Exception("Não existem itens cadastrados!");

            foreach (var item in itens)
            {
                itensCadastrados.Add(item.Nome);
            }
            string[] opcoes = itensCadastrados.ToArray();

            var opcaoSelecionada = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[green]Selecione o item que deseja editar:[/]")
            .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
            .AddChoiceGroup("Itens", opcoes)
            .AddChoices(new[] { "[red]Sair[/]" })
            );
            if (opcaoSelecionada != "[red]Sair[/]") 
            {
                var idItemSelecionado = itens.FirstOrDefault(item => item.Nome == opcaoSelecionada).Id;
                var nome = AnsiConsole.Ask<string>("[green]Digite o novo Nome[/]");

                repository.Update(new Item
                {
                    Id = idItemSelecionado,
                    Nome = nome
                });
                AnsiConsole.MarkupLine("[green]Item atualizado com sucesso![/]");
            } 
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao atualizar item {ex.Message}[/]");
        }
        finally
        {
            Thread.Sleep(1000);
            MenuItens.Exibir();
        }
    }
}
