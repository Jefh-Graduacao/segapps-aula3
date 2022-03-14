using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AtividadeAula3.Controllers;

[ApiController, Route("[controller]")]
public class ProdutosV2Controller : ControllerBase
{
    private readonly SqlConnection _conexao;

    public ProdutosV2Controller()
    {
        var stringConexao = Environment.GetEnvironmentVariable("STRING_CONEXAO");
        _conexao = new SqlConnection(stringConexao);
    }

    [HttpGet]
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