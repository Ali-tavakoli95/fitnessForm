namespace api.Interfaces;

public interface IFitnessFormRepository
{
    public Task<UserDto?> Create(RegisterFormDto registerFormDto, CancellationToken cancellationToken);
}
