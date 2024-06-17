using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class SavezController : ControllerBase
{
    [HttpGet("returnAllSavez")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSavez()
    {
        var (isError, savezi, error) = await SavezDP.ReturnAllSavez();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(savezi);
    }

    [HttpGet("returnSavezById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSavez(int id)
    {
        var (isError, savez, error) = await SavezDP.ReturnSavezAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(savez);
    }


    [HttpPost("addSavez")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSavez([FromBody] SavezView savez)
    {
        var (isError, savezID, error) = await SavezDP.AddSavezAsync(savez);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (savezID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje saveza",  
                SavezID =  savezID    
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest(new { message = "Neuspešan upis saveza." });
        }
    }

    [HttpPatch("updateSavez")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateSavez([FromBody] SavezView savez)
    {
        var data = await SavezDP.UpdateSavezAsync(savez);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno azuriranje saveza",
            SavezID = savez.SavezID
        };

        return Ok(response);
    }

    [HttpDelete("deleteSavez/{savezID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSavez(int savezID)
    {
        var data = await SavezDP.DeleteSavezAsync(savezID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje saveza",
            SavezID = savezID
        };

        return Ok(response);
    }
}
