using System.Data.SqlClient;

namespace MyFinance;

public static class DataBase
{
    public static readonly string _connectionString = @"Server=localhost,1433;Database=MYFINANCE;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;";
    public static SqlConnection connection;
}
