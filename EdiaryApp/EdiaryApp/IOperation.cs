
namespace EdiaryApp
{
    public interface IOperation
    {
        TimeOnly GetDateTimeFromHourMinString(string hourMinute);
        DateValidationModel ValidateDateString(string dateString);
    }
}