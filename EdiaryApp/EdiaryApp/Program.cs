namespace EDiaryApp;

static partial class Program
{
    public static void Main()
    {
        Console.WriteLine("Hi Pramod, welcome to EDiary!!");
        Console.WriteLine("Do you wish to get the details of a partiular day? If Yes, press 1, otherwise press 0");
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            if (value == 1)
            {
                Console.WriteLine("Enter the date in dd:mm:yyyy format for which you need the summary");
                string? dateTimeStr = Console.ReadLine();
                var validationModel = Operation.ValidateDateString(dateTimeStr);
                if (validationModel.IsValid)
                {
                    string[] splitDate = dateTimeStr.Split(new char[] { ':' });
                    DateOnly date = new DateOnly(Convert.ToInt32(splitDate[2]), Convert.ToInt32(splitDate[1]), Convert.ToInt32(splitDate[0]));
                    var readFromTextResult = Operation.ReadFromText(date);
                    if (readFromTextResult.IsValid)
                    {
                        Console.WriteLine(readFromTextResult.StatusMessage);
                    }

                    else
                    {
                        Console.WriteLine(readFromTextResult.StatusMessage);
                    }
                }
                else
                {
                    Console.WriteLine(validationModel.ValidationMessage);
                    Console.WriteLine("Re-launch the application and enter the correct format.");
                }
            }

            else if (value == 0)
            {
                List<string> data = new List<string>();
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

                        data.Add(item: questionConstants.WakeUpQuestion);
                        data.Add(wakeUpTime);
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
                data.Add(item: questionConstants.todayPlanQuestion);
                data.Add(todayPlans);

                Console.WriteLine(questionConstants.howAreYouQuestion);
                string? howAreYou = Console.ReadLine();
                data.Add(item: questionConstants.howAreYouQuestion);
                data.Add(howAreYou);

                Console.WriteLine(questionConstants.positiveQuestion);
                string? positives = Console.ReadLine();
                data.Add(item: questionConstants.positiveQuestion);
                data.Add(positives);

                Console.WriteLine(questionConstants.negativeQuestion);
                string? negatives = Console.ReadLine();
                data.Add(item: questionConstants.negativeQuestion);
                data.Add(negatives);

                //FileInfo FileInfo = new FileInfo($"{rootDirectory}{DateTime.Now.ToShortDateString()}");

                Operation.WriteToText(data);

            }
        }
    }

}




