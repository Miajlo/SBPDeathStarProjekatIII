using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class RasaController : ControllerBase
{
    [HttpGet("returnAllRasa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnRasa()
    {
        var (isError, rase, error) = await RasaDP.ReturnAllRasa();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(rase);
    }

    [HttpGet("returnRasaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnRasa(int id)
    {
        var (isError, rase, error) = await RasaDP.ReturnRasaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(rase);
    }


    [HttpPost("addRasa")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddRasa([FromBody] RasaView rasa)
    {
        var (isError, rasaID, error) = await RasaDP.AddRasaAsync(rasa);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (rasaID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje rase",
                RasaID = rasaID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest(new { message = "Neuspešan upis rase." });
        }
    }

    [HttpPatch("updateRasa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateRasa([FromBody] RasaView rasa)
    {
        var data = await RasaDP.UpdateRasaAsync(rasa);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno dodavanje rase",
            RasaID = rasa.RasaID
        };

        return Ok(response);
    }

    [HttpDelete("deleteRasa/{rasaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteRasa(int rasaID)
    {
        var data = await RasaDP.DeleteRasaAsync(rasaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno dodavanje zvezde",
            RasaID = rasaID
        };

        return Ok(response);
    }
}
