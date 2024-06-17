using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class KvadrantController : ControllerBase
{
    [HttpGet("returnAllKvadrant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnKvadrant()
    {
        var (isError, kvadranti, error) = await KvadrantDP.ReturnAllKvadrant();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(kvadranti);
    }

    [HttpGet("returnKvadrantById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnKvadrant(int id)
    {
        var (isError, kvadranti, error) = await KvadrantDP.ReturnKvadrantAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(kvadranti);
    }


    [HttpPost("addKvadrant")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddKvadrant([FromBody] KvadrantView Kvadrant)
    {
        var (isError, kvadrantID, error) = await KvadrantDP.AddKvadrantAsync(Kvadrant);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (kvadrantID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje kvadranta",
                KvadrantID = kvadrantID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis Kvadranta.");
        }
    }

    [HttpPatch("updateKvadrant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateKvadrant([FromBody] KvadrantView kvadrant)
    {
        var data = await KvadrantDP.UpdateKvadrantAsync(kvadrant);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno azuriranje kvadranta",
            KvadrantID = kvadrant.KvadrantID
        };

        return Ok(response);
    }

    [HttpDelete("deleteKvadrant/{kvadrantID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteKvadrant(int kvadrantID)
    {
        var data = await KvadrantDP.DeleteKvadrantAsync(kvadrantID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje kvadranta",
            KvadrantID = kvadrantID
        };

        return Ok(response);
    }
}
