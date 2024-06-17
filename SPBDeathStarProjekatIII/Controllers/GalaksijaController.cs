using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class GalaksijaController : ControllerBase
{
    [HttpGet("returnAllGalaksija")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnGalaksija()
    {
        var (isError, galaksije, error) = await GalaksijaDP.ReturnAllGalaksija();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(galaksije);
    }

    [HttpGet("returnGalaksijaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnGalaksija(int id)
    {
        var (isError, galaksija, error) = await GalaksijaDP.ReturnGalaksijaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(galaksija);
    }


    [HttpPost("addGalaksija")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddGalaksija([FromBody] GalaksijaView Galaksija)
    {
        var (isError, galaksijaID, error) = await GalaksijaDP.AddGalaksijaAsync(Galaksija);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (galaksijaID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje galaksije",
                GalaksijaID = galaksijaID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis galaksije.");
        }
    }

    [HttpPatch("updateGalaksija")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateGalaksija([FromBody] GalaksijaView galaksija)
    {
        var data = await GalaksijaDP.UpdateGalaksijaAsync(galaksija);

        if (data.IsError)
            return StatusCode(data.Error.StatusCode, data.Error.Message);

        var response = new
        {
            message = "Uspesno azuriranje galaksije",
            GalaksijaID = galaksija.GalaksijaID
        };

        return Ok(response);
    }

    [HttpDelete("deleteGalaksija/{galaksijaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteGalaksija(int galaksijaID)
    {
        var data = await GalaksijaDP.DeleteGalaksijaAsync(galaksijaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje galaksije",
            GalaskijaID = galaksijaID
        };

        return Ok(response);
    }
}
