using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FitnessController : ControllerBase
{
    #region Token Settings
    private readonly IFitnessFormRepository _fitnessRepository;

    public FitnessController(IFitnessFormRepository fitnessFormRepository)
    {
        _fitnessRepository = fitnessFormRepository;
    }
    #endregion


    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Create(RegisterFormDto userInput, CancellationToken cancellationToken)
    {
        if (userInput.Password != userInput.ConfirmPassword)
            return BadRequest("Password don't match!");

        UserDto? userDto = await _fitnessRepository.Create(userInput, cancellationToken);

        if (userDto is null)
            return BadRequest("Email/UserName is taken.");

        return userDto;
    }

}
