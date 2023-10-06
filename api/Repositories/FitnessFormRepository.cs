namespace api.Repositories;

public class FitnessFormRepository : IFitnessFormRepository
{
    private const string _collectionName = "fit-users";
    private readonly IMongoCollection<FitUser>? _collection;
    public FitnessFormRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<FitUser>(_collectionName);
    }

    public async Task<UserDto?> Create(RegisterFormDto userInput, CancellationToken cancellationToken)
    {
        bool doesExistEmail = await _collection.Find<FitUser>(user => user.Email == userInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesExistEmail)
            return null;

        bool doesExistUserName = await _collection.Find<FitUser>(user => user.UserName == userInput.UserName.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesExistUserName)
            return null;

        FitUser fitUser = new FitUser(
            Id: null,
            Email: userInput.Email.ToLower().Trim(),
            UserName: userInput.UserName.ToLower().Trim(),
            Mobile: userInput.Mobile.Trim(),
            Password: userInput.Password,
            ConfirmPassword: userInput.ConfirmPassword,
            FirstName: userInput.FirstName,
            LastName: userInput.LastName,
            Weight: userInput.Weight,
            Height: userInput.Height,
            Bmi: userInput.Bmi,
            BmiResult: userInput.BmiResult,
            Gender: userInput.Gender,
            RequireTrainer: userInput.RequireTrainer,
            Package: userInput.Package,
            Important: userInput.Important,
            HaveGymBefore: userInput.HaveGymBefore,
            EnquiryDate: userInput.EnquiryDate
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(fitUser, null, cancellationToken);

        if (fitUser.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: fitUser.Id,
                Email: fitUser.Email,
                UserName: fitUser.UserName
            );

            return userDto;
        }

        return null;
    }
}
