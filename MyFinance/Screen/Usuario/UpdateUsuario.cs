using MyFinance.Model;
using MyFinance.Repositories;
using Spectre.Console;

namespace MyFinance.Screen.UsuarioMenu;

public static class UpdateUsuario
{
    public static void Update() 
    {
        var repository = new Repository<Usuario>(DataBase.connection);

        int id = 0;
        string nome = "";
        string cpf = "";
        decimal salario = 0m;

        foreach (var item in repository.Get())
        {
            id = item.Id;
            nome = item.Nome;
            cpf = item.CPF;
            salario = item.Salario;
        }

        var dadosSelecionado = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("[green]Selecione a informação que deseja editar:[/]")
        .PageSize(10)
        .MoreChoicesText("[grey](Utilize as seta para cima e para baixo para selecionar a informação a ser editada)[/]")
        .AddChoiceGroup("Nome", nome)
        .AddChoiceGroup("CPF", cpf)
        .AddChoiceGroup("Salario", salario.ToString())
        .AddChoices(new[] {"[red]Sair[/]" })
        );
        if (dadosSelecionado.ToString() == nome)
        {
            nome =  AnsiConsole.Ask<string>("[green]Digite o novo Nome[/]");
        } else if (dadosSelecionado.ToString() == cpf) 
        {
            cpf = AnsiConsole.Ask<string>("[green]Digite o novo CPF[/]");
        }
        else if (dadosSelecionado.ToString() == salario.ToString())
        {
            salario = AnsiConsole.Ask<decimal>("[green]Digite o novo Salario[/]");
        }

        try
        {
            if (dadosSelecionado != "[red]Sair[/]")
            {
                repository.Update(new Usuario
                {
                    Id = id,
                    Nome = nome,
                    CPF = cpf,
                    Salario = salario,
                });
                AnsiConsole.MarkupLine("[green]Usuario atualizado com sucesso![/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Erro ao atualizar usuario {ex.Message}[/]");
        }
        finally 
        {
            Thread.Sleep(1000);
            MenuUsuario.Exibir();
        }

    }
}
