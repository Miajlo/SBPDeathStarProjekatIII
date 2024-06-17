using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class ZvezdaController : ControllerBase
{
    [HttpGet("returnAllZvezda")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezde()
    {
        var (isError, zvezde, error) = await ZvezdaDP.ReturnAllZvezda();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(zvezde);
    }

    [HttpGet("returnZvezdaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezda(int id)
    {
        var (isError, zvezde, error) = await ZvezdaDP.ReturnZvezdaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        

        return Ok(zvezde);
    }


    [HttpPost("addZvezda")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddZvezda([FromBody] ZvezdaView zvezda)
    {
        var (isError, zvezdaID, error) = await ZvezdaDP.AddZvezdaAsync(zvezda);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (zvezdaID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje zvezde",
                ZvezdaID = zvezdaID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis zvezde.");
        }
    }

    [HttpPatch("updateZvezda")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateZvezda([FromBody] ZvezdaView zvezda)
    {
        var data = await ZvezdaDP.UpdateZvezdaAsync(zvezda);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno azuriranje zvezde",
            ZvezdaID = zvezda.ZvezdaID
        };
        return Ok(response);
    }

    [HttpDelete("deleteZvezda/{zvezdaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteZvezda(int zvezdaID)
    {
        var data = await ZvezdaDP.DeleteZvezdaAsync(zvezdaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno brisanje zvezde",
            ZvezdaID = zvezdaID
        };
        return Ok(response);
    }
}
