using MyFinance;
using MyFinance.Screen.MenuPricipal;
using Spectre.Console;
using System.Data.SqlClient;

try
{
    AnsiConsole.Status()
    .Start("[green]Iniciando Sistema[/]...", ctx =>
    {
        AnsiConsole.MarkupLine("[green]Bem Vindo ao Myfinance...[/]");
    });
    DataBase.connection = new SqlConnection(DataBase._connectionString);
    DataBase.connection.Open();
    Menu.Exibir();
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Erro ao conectar no banco de dados {ex.Message}![/]");
}
finally
{
    DataBase.connection?.Close();
}