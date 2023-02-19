using System.Text.RegularExpressions;

namespace AutoCar.Services;

public class ValidationService
{
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
        var isValid = IsAllLetters(input);
        if (!isValid) _validationMessages.Add($"Поле \"{fieldName}\" должно состоять только из букв кириллицы!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidatePhoneNumber(string input)
    {
        var isValid = IsPhoneNumber(input);
        if (!isValid) _validationMessages.Add("Поле \"Номер телефона\" должно быть в формате +7 XXXXXXXXXX!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    public bool ValidateAndRetrivedBirthDate(string input, out DateOnly outBirthDate)
    {
        var isValid = !string.IsNullOrEmpty(input) && DateOnly.TryParse(input, out outBirthDate);
        if (!isValid) _validationMessages.Add($"Поле \"Дата рождения\" не должно выходить за диапазон" +
                                              $"{GetMinBirthDate()} и {GetMaxBirthDate()}!");
        PassedAllValidations &= isValid;
        return isValid;
    }
    private bool IsAllLetters(string input) => Regex.Match(input, @"^[А-Яа-я]+$").Success;
    private bool IsPhoneNumber(string input) => Regex.Match(input, @"^[0-9]{10}$").Success;
    
}