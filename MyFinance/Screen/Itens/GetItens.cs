using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;

namespace MyFinance.Screen.Itens
{
    public static class GetItens
    {
        public static void Get()
        {
            try
            {
                var repository = new Repository<Item>(DataBase.connection);
                var itens = repository.Get();
                if (itens.Count() == 0)
                    throw new Exception("Não existem itens cadastrados!");
                var table = new Table().Centered();
                table.AddColumns(new string[] { "ID", "Nome" });
                for (int i = 0; i < itens.Count(); i++)
                {
                    if (i > 0)
                        table.AddEmptyRow();
                    table.AddRow(new string[] { itens.ToArray()[i].Id.ToString(), itens.ToArray()[i].Nome }); 
                }
                    
                AnsiConsole.Write(table);
                AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a opção)[/]")
                .AddChoices(new[] { "[red]Sair[/]" })
                );
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Erro ao listar Item {ex.Message}[/]");
            }
            finally
            {
                Thread.Sleep(1000);
                MenuItens.Exibir();
            }
        }
    }
}
