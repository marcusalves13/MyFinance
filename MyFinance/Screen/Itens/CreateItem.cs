using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;

namespace MyFinance.Screen.Itens;

public class CreateItem
{
    public static void Create()
    {
        var nome = AnsiConsole.Ask<string>("[green]Digite Nome do item:[/]");
        try
        {
            var repository = new Repository<Item>(DataBase.connection);
            repository.Create(new Item
            {
                Nome = nome
            }); 
            AnsiConsole.MarkupLine("[green]Item criado com sucesso![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao criar Item {ex.Message}[/]");
        }
        finally
        {
            Thread.Sleep(1000);
            MenuItens.Exibir();
        }
    }
}
