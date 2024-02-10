using MyFinance.Model;
using MyFinance.Repositories;
using MyFinance.Screen.MenuPricipal;
using Spectre.Console;

namespace MyFinance.Screen.UsuarioMenu
{
    public static class CreateUsuario
    {
        public static void Create() 
        {
            var nome    = AnsiConsole.Ask<string>("[green]Digite seu nome:[/]");
            var cpf     = AnsiConsole.Ask<string>("[green]Digite seu CPF:[/]");
            var salario = AnsiConsole.Ask<decimal>("[green]Digite seu Salario:[/]");

            var repository = new Repository<Usuario>(DataBase.connection);
            try
            {
                repository.Create(new Usuario
                {
                    Nome = nome,
                    CPF = cpf,
                    Salario = salario
                });
                AnsiConsole.MarkupLine("[green]Usuario criado com sucesso![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Erro ao criar usuario {ex.Message}[/]");
            }
            finally 
            {
                Thread.Sleep(1000);
                Menu.Exibir();
            }
        }

    }
}
