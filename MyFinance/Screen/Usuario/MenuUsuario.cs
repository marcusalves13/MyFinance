using MyFinance.Screen.MenuPricipal;
using Spectre.Console;

namespace MyFinance.Screen.UsuarioMenu;

public static class MenuUsuario
{
    public static void Exibir() 
    {
        Console.Clear();
        var option = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[green]Menu Usuario[/]")
                                    .PageSize(10)
                                    .MoreChoicesText("[grey](Uilize as setas para cima e para baixo para selecionar a opção.)[/]")
                                    .AddChoices(new[] {
                                                       "1-Atualizar Dados",
                                                       "2-Voltar"
                                    }));
        switch (option) 
        {
            case "1-Atualizar Dados": UpdateUsuario.Update(); break;
            case "2-Voltar": Menu.Exibir(); break;
            default: Exibir(); break;
        }
    }
}
