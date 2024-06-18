using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class SvemirskaStanicaController : ControllerBase
{
    [HttpGet("returnAllSvemirskaStanica")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSvemirskaStanica()
    {
        var (isError, svemirskeStanice, error)=
            await SvemirskaStanicaDP.ReturnAllSvemirskaStanica();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(svemirskeStanice);
    }

    [HttpGet("returnSvemirskaStanicaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnSvemirskaStanica(int id)
    {
        var (isError, svemirskaStanica, error) =
            await SvemirskaStanicaDP.ReturnSvemirskaStanicaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(svemirskaStanica);
    }


    [HttpPost("addSvemirskaStanica")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSvemirskaStanica([FromBody] SvemirskaStanicaView svemirskaStanica)
    {
        var (isError, sSID, error) = await SvemirskaStanicaDP.AddSStanicaAsync(svemirskaStanica);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (sSID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje svemirske stanice",
                SSID = sSID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis SvemirskaStanica.");
        }
    }

    [HttpPatch("updateSvemirskaStanica")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateSvemirskaStanica([FromBody] SvemirskaStanicaView svemirskaStanica)
    {
        var data = await SvemirskaStanicaDP.UpdateSvemirskaStanicaAsync(svemirskaStanica);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno azuriranje svemirske stanice",
            SSID = svemirskaStanica.SSID
        };

        return Ok(response);
    }

    [HttpDelete("deleteSvemirskaStanica/{svemirskaStanicaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSvemirskaStanica(int svemirskaStanicaID)
    {
        var data = await SvemirskaStanicaDP.DeleteSvemirskaStanicaAsync(svemirskaStanicaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje svemirske stanice",
            SSID = svemirskaStanicaID
        };

        return Ok($"Izbrisana SvemirskaStanica, sa ID: {svemirskaStanicaID}");
    }
}
