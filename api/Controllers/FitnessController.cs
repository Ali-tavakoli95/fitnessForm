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

    [HttpGet("get-fit-user-by-id/{userId}")]
    public async Task<ActionResult<UserDto>> GetFitUser(string userId, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _fitnessRepository.GetFitUser(userId, cancellationToken);

        if (userDto is null)
        {
            return NoContent();
        }

        return userDto;
    }

    [HttpPut("update/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateByFitId(string userId, UpdateFormDto userIn, CancellationToken cancellationToken)
    {
        if (userIn.Password != userIn.ConfirmPassword)
            return BadRequest("Password don't match!");

        UpdateResult updateResult = await _fitnessRepository.UpdateByFitId(userId, userIn, cancellationToken);

        return updateResult;
    }

    [HttpDelete("delete/{userId}")]
    public async Task<ActionResult<DeleteResult>> Delete(string userId, CancellationToken cancellationToken)
    {
        DeleteResult deleteResult = await _fitnessRepository.Delete(userId, cancellationToken);

        return deleteResult;
    }
}
