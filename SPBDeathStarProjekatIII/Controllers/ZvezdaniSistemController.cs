﻿using DeathStarLibrary.DataProviders;

namespace SPBDeathStarProjekatIII.Controllers;

[ApiController]
[Route("[controller]")]
public class ZvezdaniSistemController : ControllerBase
{
    [HttpGet("returnAllZvezdaniSistem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezdaniSistem()
    {
        var (isError, zSistemi, error) = await ZvezdaniSistemDP.ReturnAllZvezdaniSistem();

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);

        return Ok(zSistemi);
    }

    [HttpGet("returnZvezdaniSistemById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezdaniSistem(int id)
    {
        var (isError, zSistem, error) = await ZvezdaniSistemDP.ReturnZvezdaniSistemAsync(id);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(zSistem);
    }

    [HttpGet("returnZvezdaniSistemByZvezdaId/{zvezdaID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezdaniSistemByZvezdaId(int zvezdaID)
    {
        var (isError, zSistemi, error) = await ZvezdaniSistemDP.ReturnSystemByZvezdaId(zvezdaID);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(zSistemi);
    }

    [HttpGet("returnZvezdaniSistemByPlanetaId/{planetaID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ReturnZvezdaniSistemByPlanetaId(int planetaID)
    {
        var (isError, zSistemi, error) = await ZvezdaniSistemDP.ReturnSystemByPlanetaId(planetaID);

        if (isError)
            return StatusCode(error?.StatusCode ?? 400, error?.Message);


        return Ok(zSistemi);
    }

    [HttpPost("addZvezdaniSistem")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddZvezdaniSistem([FromBody] ZvezdaniSistemView zvezdaniSistem)
    {
        var (isError, zsID, error) = await ZvezdaniSistemDP.AddZvezdaniSistemAsync(zvezdaniSistem);

        if (isError)
        {
            return StatusCode(error?.StatusCode ?? 400, error?.Message);
        }

        if (zsID)
        {
            return StatusCode(201, "Uspesno dodavanje zvezdanog sistema");
        }
        else
        {
            return BadRequest("Neuspešan upis Zvezdanog Sistema.");
        }
    }


    [HttpDelete("deleteZvezdaFromSystem/{zvezdaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteZvezdaFromSystem(int zvezdaID)
    {
        var data = await ZvezdaniSistemDP.DeleteZvezdaConnections(zvezdaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje zvezde iz zvezdanog sistema",
            ZvezdaID = zvezdaID
        };

        return Ok(response);
    }

    [HttpDelete("deletePlanetaFromSystem/{planetaID}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeletePlanetaFromSystem(int planetaID)
    {
        var data = await ZvezdaniSistemDP.DeletePlanetaConnections(planetaID);

        if (data.IsError)
        {
            return StatusCode(data.Error.StatusCode, data.Error.Message);
        }

        var response = new
        {
            message = "Uspesno brisanje planete iz zvezdanog sistema",
            PlanetaID = planetaID
        };

        return Ok(response);
    }
}
