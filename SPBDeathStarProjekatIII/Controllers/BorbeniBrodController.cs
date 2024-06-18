using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class BorbeniBrodController : ControllerBase
{
    [HttpGet("returnAllBorbeniBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnBorbeniBrodovi()
    {
        var (isError, odeljenja, error) = await BorbeniBrodDP.ReturnAllBBrod();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(odeljenja);
    }

    [HttpGet("returnBorbeniBrodById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnBorbeniBrod(int id)
    {
        var (isError, bBrodovi, error) = await BorbeniBrodDP.ReturnBBrodAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(bBrodovi);
    }


    [HttpPost("addBorbeniBrod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddBorbeniBrod([FromBody] BorbeniBrodView borbeniBrod)
    {
        var (isError, bBrodID, error) = await BorbeniBrodDP.AddBBrodAsync(borbeniBrod);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (bBrodID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje borbenog broda",
                BrodID = bBrodID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest(new { message = "Neuspešan upis borbenog broda." });
        }
    }

    [HttpPatch("updateBorbeniBrod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateBorbeniBrod([FromBody] BorbeniBrodView borbeniBrod)
    {
        var data = await BorbeniBrodDP.UpdateBBrodAsync(borbeniBrod);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno azuriranje borbenog broda",
            BrodID = borbeniBrod
        };
        return Ok(response);
    }

    [HttpDelete("deleteBorbeniBrod/{borbeniBrodID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteBorbeniBrod(int borbeniBrodID)
    {
        var data = await BorbeniBrodDP.DeleteBBrodAsync(borbeniBrodID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }
        var response = new
        {
            message = "Uspesno brisanje borbenog broda",
            BrodID = borbeniBrodID
        };
        return Ok(response);
    }
}
