using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class TransportniBrodController : ControllerBase
{
    [HttpGet("returnAllTransportniBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezde()
    {
        var (isError, tBrodobi, error) = await TransportniBrodDP.ReturnAllTBrod();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(tBrodobi);
    }

    [HttpGet("returnTransportniBrodById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnTransportniBrod(int id)
    {
        var (isError, tBrod, error) = await TransportniBrodDP.ReturnTBrodAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(tBrod);
    }


    [HttpPost("addTransportniBrod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddTransportniBrod([FromBody] TransportniBrodView transportniBrod)
    {
        var (isError, tBrodID, error) = await TransportniBrodDP.AddTBrodAsync(transportniBrod);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (tBrodID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje transportnog broda",
                BrodID = tBrodID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis transportnog broda.");
        }
    }

    [HttpPatch("updateTransportniBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateTransportniBrod([FromBody] TransportniBrodView transportniBrod)
    {
        var data = await TransportniBrodDP.UpdateTBrodAsync(transportniBrod);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno azuriranje transportnog broda",
            BrodID = transportniBrod.BrodID
        };

        return Ok(response);
    }

    [HttpDelete("deleteTransportniBrod/{transportniBrodID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteTransportniBrod(int transportniBrodID)
    {
        var data = await TransportniBrodDP.DeleteTBrodAsync(transportniBrodID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje transportnog broda",
            BrodID = transportniBrodID
        };

        return Ok($"Izbrisan TransportniBrod, sa ID: {transportniBrodID}");
    }
}
