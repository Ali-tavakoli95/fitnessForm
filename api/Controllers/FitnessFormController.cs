namespace api.Controllers;


public class FitnessController : BaseApiController
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

        UserDto? userDto = await _fitnessRepository.CreateAsync(userInput, cancellationToken);

        if (userDto is null)
            return BadRequest("Email/UserName is taken.");

        return userDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _fitnessRepository.GetAllAsync(cancellationToken);

        if (!userDtos.Any())
            return NoContent();

        return userDtos;
    }


    [HttpGet("get-fit-user-by-id/{userId}")]
    public async Task<ActionResult<UserDto>> GetFitUser(string userId, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _fitnessRepository.GetFitUserAsync(userId, cancellationToken);

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

        UpdateResult updateResult = await _fitnessRepository.UpdateByFitIdAsync(userId, userIn, cancellationToken);

        return updateResult;
    }

    [HttpDelete("delete/{userId}")]
    public async Task<ActionResult<DeleteResult>> Delete(string userId, CancellationToken cancellationToken)
    {
        DeleteResult deleteResult = await _fitnessRepository.DeleteAsync(userId, cancellationToken);

        return deleteResult;
    }
}
