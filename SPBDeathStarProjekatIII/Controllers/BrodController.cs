using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class BrodController : ControllerBase
{
    [HttpGet("returnAllBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezde()
    {
        var (isError, brodovi, error) = await BrodDP.ReturnAllBrod();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(brodovi);
    }

    [HttpGet("returnBrodById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnBrod(int id)
    {
        var (isError, brodovi, error) = await BrodDP.ReturnBrodAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(brodovi);
    }


    [HttpPost("addBrod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddBrod([FromBody] BrodView brod)
    {
        var (isError, brodID, error) = await BrodDP.AddBrodAsync(brod);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (brodID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje broda",
                BrodID = brodID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis brodovi.");
        }
    }

    [HttpPatch("updateBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateBrod([FromBody] BrodView brod)
    {
        var data = await BrodDP.UpdateBrodAsync(brod);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno azuriranje broda",
            BrodID = brod.BrodID
        };
        return Ok(response);
    }

    [HttpDelete("deleteBrod/{brodID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteBrod(int brodID)
    {
        var data = await BrodDP.DeleteBrodAsync(brodID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Izbrisan Brod, sa ID: {brodID}");
    }
}
