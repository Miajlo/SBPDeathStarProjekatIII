using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class IgracController : ControllerBase
{
    [HttpGet("returnAllIgrac")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnIgrac()
    {
        var (isError, igraci, error) = await IgracDP.ReturnAllIgrac();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(igraci);
    }

    [HttpGet("returnIgracById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnIgrac(int id)
    {
        var (isError, igrac, error) = await IgracDP.ReturnIgracAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(igrac);
    }


    [HttpPost("addIgrac")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddIgrac([FromBody] IgracView igrac)
    {
        var (isError, igradID, error) = await IgracDP.AddIgracAsync(igrac);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (igradID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje igraca",
                IgracID = igradID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis Igraca.");
        }
    }

    [HttpPatch("updateIgrac")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateIgrac([FromBody] IgracView igrac)
    {
        var data = await IgracDP.UpdateIgracAsync(igrac);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno azuriranje igraca",
            IgracID = igrac.IgracID
        };
        return Ok(response);
    }

    [HttpDelete("deleteIgrac/{igracID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteIgrac(int igracID)
    {
        var data = await IgracDP.DeleteIgracAsync(igracID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno brisanje igraca",
            IgracID = igracID
        };
        return Ok(response);
    }
}
