using Dapper;
using MyFinance.Model;
using System.Data.SqlClient;

namespace MyFinance.Repositories;

public class MovimentacaoRepository : Repository<Movimentacao>
{
    private readonly SqlConnection _connection;

    public MovimentacaoRepository(SqlConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public IEnumerable<Movimentacao> RetornaMovimentacaoEItem() 
    {
        var sql = @"SELECT A.ID,A.HISTORICO,A.VALOR,A.DC,A.DATALANCAMENTO,A.IDITEM,B.ID ITEM, B.NOME
                    FROM MOVIMENTACAO_ABERTA A, ITEM B
                    WHERE A.IDITEM = B.ID";

        return _connection.Query<Movimentacao, Item, Movimentacao>
                (sql, (m, i) =>
                {
                    if (i != null)
                    {
                        m.Item.Id = i.Id;
                        m.Item.Nome = i.Nome;
                    }
                    return m;
                },splitOn:"ITEM");
    }
}
