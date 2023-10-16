namespace api.Interfaces;

public interface IFitnessFormRepository
{
    public Task<UserDto?> CreateAsync(RegisterFormDto userInput, CancellationToken cancellationToken);

    public Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<UserDto?> GetFitUserAsync(string userId, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByFitIdAsync(string userId, UpdateFormDto userIn, CancellationToken cancellationToken);

    public Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken);
}
