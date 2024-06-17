using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class PojavaController : ControllerBase
{
    [HttpGet("returnAllPojava")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnPojava()
    {
        var (isError, pojave, error) = await PojavaDP.ReturnAllPojava();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(pojave);
    }

    [HttpGet("returnPojavaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnPojava(int id)
    {
        var (isError, pojave, error) = await PojavaDP.ReturnPojavaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(pojave);
    }


    [HttpPost("addPojava")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddPojava([FromBody] PojavaView pojava)
    {
        var (isError, pojavaID, error) = await PojavaDP.AddPojavaAsync(pojava);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (pojavaID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje pojave",
                PojavaID = pojavaID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis Pojavaa.");
        }
    }

    [HttpPatch("updatePojava")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdatePojava([FromBody] PojavaView pojava)
    {
        var data = await PojavaDP.UpdatePojavaAsync(pojava);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Updated Pojava: {pojava.PojavaID}");
    }

    [HttpDelete("deletePojava/{pojavaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeletePojava(int pojavaID)
    {
        var data = await PojavaDP.DeletePojavaAsync(pojavaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje pojave",
            PojavaID = pojavaID
        };

        return Ok(response);
    }
}
