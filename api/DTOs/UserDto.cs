namespace api.DTOs;

public record UserDto(
    string Id,
    string Email,
    string UserName,
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
