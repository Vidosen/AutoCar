using System.Text.RegularExpressions;

namespace AutoCar.Services;

public class ValidationService
{
    public readonly static short MIN_CAR_RELEASE_YEAR = 1900;
    public readonly static short MAX_CAR_RELEASE_YEAR = Convert.ToInt16(DateTime.Today.Year);
    public readonly static string SAMPLE_CAR_NUMBER = "А888НА174";
    public bool PassedAllValidations { get; private set; }
    private readonly List<string> _validationMessages;
    public static DateOnly GetMaxBirthDate()
    {
        var maxDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
        return maxDate;
    }
    public static DateOnly GetMinBirthDate() => new(1900,01,01);

    public ValidationService()
    {
        PassedAllValidations = true;
        _validationMessages = new List<string>();
    }
    public void ClearHistory()
    {
        PassedAllValidations = true;
        _validationMessages.Clear();
    }
    public IEnumerable<string> GetValidationMessages() => _validationMessages;
    public bool ValidateInitials(string input, string fieldName)
    {
        var isValid = IsCyrillicInitials(input);
        if (!isValid) _validationMessages.Add($"Поле \"{fieldName}\" должно состоять только " +
                                              "из букв кириллицы с заглавной буквы!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidatePhoneNumber(string input)
    {
        var isValid = IsPhoneNumber(input);
        if (!isValid) _validationMessages.Add("Номер телефона должен быть в формате +7 XXXXXXXXXX!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidateAndRetrieveBirthDate(string input, out DateOnly outBirthDate)
    {
        var isValid = !string.IsNullOrEmpty(input) && DateOnly.TryParse(input, out outBirthDate);
        if (!isValid) _validationMessages.Add($"Поле \"Дата рождения\" не должно выходить за диапазон" +
                                              $"{GetMinBirthDate()} и {GetMaxBirthDate()}!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidateCarNumber(string input)
    {
        var isValid = IsCarNumber(input);
        if (!isValid) _validationMessages.Add($"Номер автомобиля должен быть в формате {SAMPLE_CAR_NUMBER}!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidateCarReleaseYear(int input)
    {
        var isValid = input >= MIN_CAR_RELEASE_YEAR && input <= MAX_CAR_RELEASE_YEAR;
        if (!isValid) _validationMessages.Add("Год выпуска автомобиля не может быть меньше " +
                                              $"{MIN_CAR_RELEASE_YEAR} и больше {MAX_CAR_RELEASE_YEAR}!");
        PassedAllValidations &= isValid;
        return isValid;
    }

    private bool IsCarNumber(string input) => Regex.Match(input, @"^[АВЕКМНОРСТУХ]\d{3}(?<!000)[АВЕКМНОРСТУХ]{2}\d{2,3}$").Success;
    private bool IsCyrillicInitials(string input) => Regex.Match(input, @"^[А-Я]{1}[а-я]+$").Success;
    private bool IsPhoneNumber(string input) => Regex.Match(input, @"^[0-9]{10}$").Success;
}