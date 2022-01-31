using System.Configuration;
using System.Text.RegularExpressions;

Console.WriteLine("Hi Pramod!!");
Console.WriteLine("Do you wish to get the details of a partiular day? If Yes, press 1, otherwise press 0");
if(int.TryParse(Console.ReadLine(),out int value))
{
    if(value == 1)
    {
        Console.WriteLine("Enter the date for which you need the summary in dd:mm:yyyy format");
        string? dateTimeStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(dateTimeStr))
        {
            if (Regex.IsMatch(dateTimeStr, "^\\d{2}:\\d{2}:\\d{4}$"))
            {
                string[] splitDate = dateTimeStr.Split(new char[] { ':' });
                DateOnly date = new DateOnly(Convert.ToInt32(splitDate[2]), Convert.ToInt32(splitDate[1]), Convert.ToInt32(splitDate[0]));
                string? rootDirectory = ConfigurationManager.AppSettings.Get("EdairyFolder");
                FileInfo FileInfo = new FileInfo($"{rootDirectory}{DateTime.Now.ToShortDateString()}");

                if (FileInfo.Exists)
                {
                    using (StreamReader reader = new StreamReader(FileInfo.FullName))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    } 
                } 

                else
                {
                    Console.WriteLine("Data does not exist for the given date.");
                }
            }

            else
            {
                Console.WriteLine($"The entered date: {dateTimeStr} doesn't match the expected dd:mm:yyyy format.");
                Console.WriteLine("Re-launch the application and enter the correct format.");
            }
        }
    }

    else if(value == 0)
    {
        Console.WriteLine(value: questionConstants.WakeUpQuestion);
        Console.WriteLine("Format hh:mm");
        string? wakeUpTime = Console.ReadLine();
        if (wakeUpTime != null)
        {
            try
            {
                int hour = Convert.ToInt32(wakeUpTime.Split(":")[0]);
                int minute = Convert.ToInt32(wakeUpTime.Split(':')[1]);
                DateTime wakeUpDateTime = new
                    DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                            hour, minute, DateTime.Now.Second);
            }
            catch (FormatException exF)
            {
                Console.WriteLine("Input Hour and minute is not in expected format");
                Console.WriteLine(exF.Message);
                Console.WriteLine("Re-launch the application and give the correct format");
            }

        }
        Console.WriteLine(questionConstants.todayPlanQuestion);
        string? todayPlans = Console.ReadLine();

        Console.WriteLine(questionConstants.howAreYouQuestion);
        string? howAreYou = Console.ReadLine();

        Console.WriteLine(questionConstants.positiveQuestion);
        string? positives = Console.ReadLine();

        Console.WriteLine(questionConstants.negativeQuestion);

        string? negatives = Console.ReadLine();

        string? rootDirectory =  ConfigurationManager.AppSettings.Get("EdairyFolder");

        FileInfo FileInfo = new FileInfo($"{rootDirectory}{DateTime.Now.ToShortDateString()}");

        using (StreamWriter sw = new StreamWriter(FileInfo.FullName))
        {
            sw.WriteLine(questionConstants.WakeUpQuestion);

            sw.WriteLine($" {wakeUpTime}");

            sw.WriteLine(questionConstants.todayPlanQuestion);

            sw.WriteLine($" {todayPlans}");

            sw.WriteLine(questionConstants.howAreYouQuestion);

            sw.WriteLine($" {howAreYou}");

            sw.WriteLine(questionConstants.positiveQuestion);

            sw.WriteLine($" {positives}");

            sw.WriteLine(questionConstants.negativeQuestion);

            sw.WriteLine($" {negatives}");
        }

    }
}
