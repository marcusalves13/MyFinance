using MyFinance.Screen.MenuPricipal;
using Spectre.Console;

namespace MyFinance.Screen.MovimentacaoAberta;

public class MenuMovimentacaoAberta
{
    public static void Exibir()
    {
        Console.Clear();
        var option = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[green]Menu Movimentações[/]")
                                    .PageSize(10)
                                    .MoreChoicesText("[grey](Uilize as setas para cima e para baixo para selecionar a opção.)[/]")
                                    .AddChoices(new[] {
                                                       "1-Cadastrar Movimentação",
                                                       "2-Alterar Movimentação",
                                                       "3-Excluir Movimentação",
                                                       "4-Voltar"
                                    }));
        switch (option)
        {
            case "1-Cadastrar Movimentação": CreateMovimentaoAberta.Create(); break;
            case "2-Alterar Movimentação"  : UpdateMovimentacaoAberta.Update();  break;
            case "3-Excluir Movimentação"  : DeleteMovimentaccaoAberta.Delete(); break;
            case "4-Voltar": Menu.Exibir(); break;
            default: Exibir(); break;
        }
    }
}
