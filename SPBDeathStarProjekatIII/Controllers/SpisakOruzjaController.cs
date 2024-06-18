using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class SpisakOruzjaController : ControllerBase
{
    [HttpGet("returnAllSpisakOruzja")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSpisakOruzja()
    {
        var (isError, spisakOruzja, error) = await SpisakOruzjaDP.ReturnAllSpisakOruzja();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(spisakOruzja);
    }

    [HttpGet("returnSpisakOruzjaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSpisakOruzja(int id)
    {
        var (isError, spisakOruzja, error) = await SpisakOruzjaDP.ReturnSpisakOruzjaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(spisakOruzja);
    }


    [HttpPost("addSpisakOruzja")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSpisakOruzja([FromBody] SpisakOruzjaView spisakOruzja)
    {
        var (isError, sOID, error) = await SpisakOruzjaDP.AddSpisakOruzjaAsync(spisakOruzja);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (sOID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje spiska oruzja",
                SOID = sOID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest(new { message = "Neuspešan upis SpisakOruzjaa." });
        }
    }

    [HttpPatch("updateSpisakOruzja")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateSpisakOruzja([FromBody] SpisakOruzjaView spisakOruzja)
    {
        var data = await SpisakOruzjaDP.UpdateSpisakOruzjaAsync(spisakOruzja);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Updated SpisakOruzja: {spisakOruzja.SOID}");
    }

    [HttpDelete("deleteSpisakOruzja/{spisakOruzjaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSpisakOruzja(int spisakOruzjaID)
    {
        var data = await SpisakOruzjaDP.DeleteSpisakOruzjaAsync(spisakOruzjaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje spiska oruzja",
            SOID = spisakOruzjaID
        };

        return Ok(response);
    }
}
