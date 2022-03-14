using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AtividadeAula3.Controllers;

[ApiController, Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly SqlConnection _conexao;

    public ProdutosController()
    {
        _conexao = new SqlConnection("Server=localhost,5433;Database=AtividadeAula3;User Id=sa;Password=4Jgz2HmDKS;");
    }

    /* 
     * Código com vulnerabilidade para injeção SQL
     * Para explorar a vulnerabilidade é possível passar um comando SQL para 
     * a querystring que deveria receber o Id do produto
     * Por exemplo: app.url/produtos?id=10868EAE-1F2B-48C4-B0E3-54998C8B1833'; UPDATE Produtos SET Preco = 100; select 'a
    */
    [HttpGet]
    public async Task<ActionResult<Produto>> Get(string id)
    {
        using (_conexao)
        {
            await _conexao.OpenAsync();

            using var command = _conexao.CreateCommand();
            command.CommandText = $"SELECT * FROM Produtos WHERE Id = '{id}'";

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Ok(new Produto(
                    reader.GetGuid(0).ToString(),
                    reader.GetString(1),
                    reader.GetDecimal(2)
                ));
            }

            return NotFound();
        }
    }

    /*
     * Código com a vulnerabilidade corrigida. Aqui usamos o mecanismo de parâmetros que faz com que 
     * o parâmetro enviado ao banco seja sanitizado. 
     * Outra medida que poderia ser tomada seria fazer um uso melhor dos tipos do C# e trocar 'string' 
     * por 'Guid'. Dessa forma seria impossível enviar qualquer informação que estivesse no formato correto.
    */
    [HttpGet("corrigido")]
    public async Task<ActionResult<Produto>> ObterPorId(string id)
    {
        using (_conexao)
        {
            await _conexao.OpenAsync();
            using var comando = _conexao.CreateCommand();
            comando.CommandText = "SELECT * FROM Produtos WHERE Id = @id";
            comando.Parameters.AddWithValue("@id", id);

            using var reader = await comando.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Ok(new Produto(
                    reader.GetGuid(0).ToString(),
                    reader.GetString(1),
                    reader.GetDecimal(2)
                ));
            }
        }

        return NotFound();
    }
}
