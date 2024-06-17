using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class SirenjePosedaController : ControllerBase
{
    [HttpGet("returnAllSirenjePoseda")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSirenjePoseda()
    {
        var (isError, sirenjaPoseda, error) = await SirenjePosedaDP.ReturnAllSirenjePoseda();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(sirenjaPoseda);
    }

    [HttpGet("returnSirenjePosedaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSirenjePoseda(int id)
    {
        var (isError, sirenjePoseda, error) = await SirenjePosedaDP.ReturnSirenjePosedaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(sirenjePoseda);
    }


    [HttpPost("addSirenjePoseda")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSirenjePoseda([FromBody] SirenjePosedaView sirenjePoseda)
    {
        var (isError, sPID, error) = await SirenjePosedaDP.AddSirenjePosedaAsync(sirenjePoseda);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (sPID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje sirenja poseda",
                SPID = sPID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis SirenjePosedaa.");
        }
    }

    [HttpPatch("updateSirenjePoseda")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateSirenjePoseda([FromBody] SirenjePosedaView sirenjePoseda)
    {
        var data = await SirenjePosedaDP.UpdateSirenjePosedaAsync(sirenjePoseda);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Updated SirenjePoseda: {sirenjePoseda.SPID}");
    }

    [HttpDelete("deleteSirenjePoseda/{SirenjePosedaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSirenjePoseda(int sirenjePosedaID)
    {
        var data = await SirenjePosedaDP.DeleteSirenjePosedaAsync(sirenjePosedaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje sirenja poseda",
            SPID = sirenjePosedaID
        };

        return Ok(response);
    }
}
