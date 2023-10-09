namespace api.Interfaces;

public interface IFitnessFormRepository
{
    public Task<UserDto?> Create(RegisterFormDto userInput, CancellationToken cancellationToken);

    public Task<UserDto?> GetFitUser(string userId, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByFitId(string userId, UpdateFormDto userIn, CancellationToken cancellationToken);

    public Task<DeleteResult> Delete(string userId, CancellationToken cancellationToken);
}
