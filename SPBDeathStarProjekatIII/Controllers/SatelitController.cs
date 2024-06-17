using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class SatelitController : ControllerBase
{
    [HttpGet("returnAllSatelit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezde()
    {
        var (isError, sateliti, error) = await SatelitDP.ReturnAllSatelit();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(sateliti);
    }

    [HttpGet("returnSatelitById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSatelit(int id)
    {
        var (isError, satelit, error) = await SatelitDP.ReturnSatelitAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(satelit);
    }


    [HttpPost("addSatelit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSatelit([FromBody] SatelitView satelit)
    {
        var (isError, satelitID, error) = await SatelitDP.AddSatelitAsync(satelit);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (satelitID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje satelita",
                SatelitID = satelitID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis satelita.");
        }
    }

    [HttpPatch("updateSatelit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateSatelit([FromBody] SatelitView satelit)
    {
        var data = await SatelitDP.UpdateSatelitAsync(satelit);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno azuriranje satelita",
            SatelitID = satelit.SatelitID
        };

        return Ok(response);
    }

    [HttpDelete("deleteSatelit/{satelitID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSatelit(int satelitID)
    {
        var data = await SatelitDP.DeleteSatelitAsync(satelitID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje satelita",
            SatelitID = satelitID
        };

        return Ok(response);
    }
}
