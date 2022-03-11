using System.Configuration;
using System.Text.RegularExpressions;

namespace EdiaryApp;


public class Operation : IOperation
{
    /// <summary>
    /// 
    /// </summary>
    public DateValidationModel ValidateDateString(string dateString)
    {
        DateValidationModel dateValidationModel;

        if (string.IsNullOrWhiteSpace(dateString))
        {
            dateValidationModel = new DateValidationModel(false, "Entered date is empty");
        }

        else if (!Regex.IsMatch(dateString, "^\\d{2}-\\d{2}-\\d{4}$"))
        {
            dateValidationModel = new DateValidationModel(false, $"The entered date {dateString} doesn't match the expected format dd:mm:hhhh");
        }

        else
        {
            dateValidationModel = new DateValidationModel(true, "The format is as expected");
        }

        return dateValidationModel;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="hourMinute"></param>
    /// <returns></returns>
    public TimeOnly GetDateTimeFromHourMinString(string hourMinute)
    {
        int hour = Convert.ToInt32(hourMinute.Split(":")[0]);
        int minute = Convert.ToInt32(hourMinute.Split(':')[1]);
        TimeOnly wakeUpTime = new TimeOnly(hour, minute);
        return wakeUpTime;
    }
}






