using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class GradController : ControllerBase
{
    [HttpGet("returnAllGrad")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezde()
    {
        var (isError, gradovi, error) = await GradDP.ReturnAllGrad();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(gradovi);
    }

    [HttpGet("returnGradById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnGrad(int id)
    {
        var (isError, grad, error) = await GradDP.ReturnGradAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

  
        return Ok(grad);
    }


    [HttpPost("addGrad")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddGrad([FromBody] GradView Grad)
    {
        var (isError, gradID, error) = await GradDP.AddGradAsync(Grad);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (gradID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje grada",
                GradID = gradID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest(new { message = "Neuspešan upis grada." });
        }
    }

    [HttpPatch("updateGrad")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateGrad([FromBody] GradView grad)
    {
        var data = await GradDP.UpdateGradAsync(grad);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno dodavanje zvezde",
            GradID = grad.GradID
        };

        return Ok(response);
    }

    [HttpDelete("deleteGrad/{gradID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteGrad(int gradID)
    {
        var data = await GradDP.DeleteGradAsync(gradID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje grada",
            GradID = gradID
        };

        return Ok(response);
    }
}
