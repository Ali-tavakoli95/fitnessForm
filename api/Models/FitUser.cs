
namespace api.Models;

public record FitUser(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword,
    string Mobile,
    string FirstName,
    string LastName,
    int Weight,
    double Height,
    double Bmi,
    string BmiResult,
    string Gender,
    string RequireTrainer,
    string Package,
    string[] Important,
    string HaveGymBefore,
    string EnquiryDate
);
