using System.Configuration;
using System.Text.RegularExpressions;

namespace EDiaryApp;


public static class Operation
{
    /// <summary>
    /// 
    /// </summary>
    public static DateValidationModel ValidateDateString(string dateString)
    {
        DateValidationModel dateValidationModel;

        if (string.IsNullOrWhiteSpace(dateString))
        {
            dateValidationModel = new DateValidationModel(false, "Entered date is empty");
        }

        else if (!Regex.IsMatch(dateString, "^\\d{2}:\\d{2}:\\d{4}$"))
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
    public static void WriteToText(List<string> dataToText)
    {
        string? rootDirectory = ConfigurationManager.AppSettings.Get("EdairyFolder");
        FileInfo FileInfo = new FileInfo($"{rootDirectory}{DateTime.Now.ToShortDateString()}");
        if (!string.IsNullOrWhiteSpace(rootDirectory))
        {
            using (StreamWriter sw = new StreamWriter(FileInfo.FullName))
            {
                foreach (string data in dataToText)
                {
                    sw.WriteLine(data);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static ReadFromTextModel ReadFromText(DateOnly dateString)
    {
        string? rootDirectory = ConfigurationManager.AppSettings.Get("EdairyFolder");
        FileInfo FileInfo = new FileInfo($"{rootDirectory}{dateString.ToShortDateString()}");
        ReadFromTextModel readFromText;

        if (FileInfo.Exists)
        {
            using (StreamReader reader = new StreamReader(FileInfo.FullName))
            {
                readFromText = new ReadFromTextModel(true, reader.ReadToEnd());

            }
        }

        else
        {
            readFromText = new ReadFromTextModel(false, "Data does not exist for the given date.");
        }

        return readFromText;
    }

}






