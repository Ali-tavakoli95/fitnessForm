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

    public async Task<UserDto?> CreateAsync(RegisterFormDto userInput, CancellationToken cancellationToken)
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
                UserName: fitUser.UserName,
                Mobile: fitUser.Mobile,
                FirstName: fitUser.FirstName,
                LastName: fitUser.LastName,
                Weight: fitUser.Weight,
                Height: fitUser.Height,
                Bmi: fitUser.Bmi,
                BmiResult: fitUser.BmiResult,
                Gender: fitUser.Gender,
                RequireTrainer: fitUser.RequireTrainer,
                Package: fitUser.Package,
                Important: fitUser.Important,
                HaveGymBefore: fitUser.HaveGymBefore,
                EnquiryDate: fitUser.EnquiryDate
            );

            return userDto;
        }

        return null;
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<FitUser> fitUsers = await _collection.Find<FitUser>(new BsonDocument()).ToListAsync(cancellationToken);

        List<UserDto> userDtos = new List<UserDto>();

        if (fitUsers.Any())
        {
            foreach (FitUser fitUser in fitUsers)
            {
                UserDto userDto = new UserDto(
                    Id: fitUser.Id!,
                    Email: fitUser.Email,
                    UserName: fitUser.UserName,
                    Mobile: fitUser.Mobile,
                    FirstName: fitUser.FirstName,
                    LastName: fitUser.LastName,
                    Weight: fitUser.Weight,
                    Height: fitUser.Height,
                    Bmi: fitUser.Bmi,
                    BmiResult: fitUser.BmiResult,
                    Gender: fitUser.Gender,
                    RequireTrainer: fitUser.RequireTrainer,
                    Package: fitUser.Package,
                    Important: fitUser.Important,
                    HaveGymBefore: fitUser.HaveGymBefore,
                    EnquiryDate: fitUser.EnquiryDate
                );

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos;
    }

    public async Task<UserDto?> GetFitUserAsync(string userId, CancellationToken cancellationToken)
    {
        FitUser fitUser = await _collection.Find(v => v.Id == userId).FirstOrDefaultAsync();

        if (fitUser.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: fitUser.Id,
                Email: fitUser.Email,
                UserName: fitUser.UserName,
                Mobile: fitUser.Mobile,
                FirstName: fitUser.FirstName,
                LastName: fitUser.LastName,
                Weight: fitUser.Weight,
                Height: fitUser.Height,
                Bmi: fitUser.Bmi,
                BmiResult: fitUser.BmiResult,
                Gender: fitUser.Gender,
                RequireTrainer: fitUser.RequireTrainer,
                Package: fitUser.Package,
                Important: fitUser.Important,
                HaveGymBefore: fitUser.HaveGymBefore,
                EnquiryDate: fitUser.EnquiryDate
            );

            return userDto;
        }

        return null;
    }

    public async Task<UpdateResult> UpdateByFitIdAsync(string userId, UpdateFormDto userIn, CancellationToken cancellationToken)
    {
        var updatedFit = Builders<FitUser>.Update
        .Set((FitUser doc) => doc.Email, userIn.Email)
        .Set(doc => doc.UserName, userIn.UserName)
        .Set(doc => doc.Mobile, userIn.Mobile)
        .Set(doc => doc.Password, userIn.Password)
        .Set(doc => doc.ConfirmPassword, userIn.ConfirmPassword)
        .Set(doc => doc.FirstName, userIn.FirstName)
        .Set(doc => doc.LastName, userIn.LastName)
        .Set(doc => doc.Weight, userIn.Weight)
        .Set(doc => doc.Height, userIn.Height)
        .Set(doc => doc.Bmi, userIn.Bmi)
        .Set(doc => doc.BmiResult, userIn.BmiResult)
        .Set(doc => doc.Gender, userIn.Gender)
        .Set(doc => doc.RequireTrainer, userIn.RequireTrainer)
        .Set(doc => doc.Package, userIn.Package)
        .Set(doc => doc.Important, userIn.Important)
        .Set(doc => doc.HaveGymBefore, userIn.HaveGymBefore)
        .Set(doc => doc.EnquiryDate, userIn.EnquiryDate);

        return await _collection.UpdateOneAsync<FitUser>(doc => doc.Id == userId, updatedFit);
    }

    public async Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        return await _collection.DeleteOneAsync<FitUser>(doc => doc.Id == userId);
    }
}