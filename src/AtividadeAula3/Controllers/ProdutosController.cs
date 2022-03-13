using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AtividadeAula3.Controllers
{
    [ApiController, Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        public async Task<ActionResult<Produto>> Get(string id)
        {
            using var connection
                = new SqlConnection("Server=localhost,5433;Database=AtividadeAula3;User Id=sa;Password=4Jgz2HmDKS;");

            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Produtos WHERE Id = '{id}'";

            using var reader = command.ExecuteReader();

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
}