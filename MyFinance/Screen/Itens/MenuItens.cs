using MyFinance.Screen.MenuPricipal;
using Spectre.Console;

namespace MyFinance.Screen.Itens;

public static class MenuItens
{
    public static void Exibir()
    {
        Console.Clear();
        var option = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[green]Menu Itens[/]")
                                    .PageSize(10)
                                    .MoreChoicesText("[grey](Uilize as setas para cima e para baixo para selecionar a opção.)[/]")
                                    .AddChoices(new[] {
                                                       "1-Cadastrar Item",
                                                       "2-Alterar Item",
                                                       "3-Excluir Item",
                                                       "4-Listar Itens",
                                                       "5-Voltar"
                                    }));
        switch (option)
        {
            case "1-Cadastrar Item": CreateItem.Create(); break;
            case "2-Alterar Item": UpdateItem.Update() ; break;
            case "3-Excluir Item": DeleteItem.Delete(); break;
            case "4-Listar Itens": GetItens.Get(); break;
            case "5-Voltar": Menu.Exibir(); break;
            default: Exibir(); break;
        }
    }
}
