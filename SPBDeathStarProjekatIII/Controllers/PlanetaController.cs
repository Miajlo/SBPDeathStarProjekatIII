using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetaController : ControllerBase
{
    [HttpGet("returnAllPlaneta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnPlaneta()
    {
        var (isError, planete, error) = await PlanetaDP.ReturnAllPlaneta();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(planete);
    }

    [HttpGet("returnPlanetaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnPlaneta(int id)
    {
        var (isError, planeta, error) = await PlanetaDP.ReturnPlanetaAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(planeta);
    }


    [HttpPost("addPlaneta")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddPlaneta([FromBody] PlanetaView planeta)
    {
        var (isError, planetaID, error) = await PlanetaDP.AddPlanetaAsync(planeta);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (planetaID != 0)
        {
            var response = new
            {
                message = "Uspesno dodavanje planete",
                PlanetaID = planetaID
            };
            return StatusCode(201, response);
        }
        else
        {
            return BadRequest("Neuspešan upis Planetaa.");
        }
    }

    [HttpPatch("updatePlaneta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdatePlaneta([FromBody] PlanetaView planeta)
    {
        var data = await PlanetaDP.UpdatePlanetaAsync(planeta);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        return Ok($"Updated Planeta: {planeta.PlanetaID}");
    }

    [HttpDelete("deletePlaneta/{planetaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeletePlaneta(int planetaID)
    {
        var data = await PlanetaDP.DeletePlanetaAsync(planetaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje planete",
            PlanetaID = planetaID
        };

        return Ok(response);
    }
}
