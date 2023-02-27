namespace AutoCar.Services;

public static class DateTimeSerivce
{
    public static DateOnly GetMaxBirthDate()
    {
        var maxDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
        return maxDate;
    }
    public static DateOnly GetMinBirthDate() => new(1900,01,01);
    public static DateOnly GetTodayDate() => DateOnly.FromDateTime(DateTime.Today);
    public static string ToHTMLString(this DateOnly date) => date.ToString("yyyy'-'MM'-'dd");
    public static string ToFormattedString(this DateOnly date) => date.ToString("dd'/'MM'/'yyyy");
    public static string ToFormattedString(this DateTime date) => date.ToString("dd'/'MM'/'yyyy");
}