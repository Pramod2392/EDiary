Console.WriteLine("Hi Pramod!!");
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

FileInfo FileInfo = new FileInfo($"C:\\Users\\Pramod\\Desktop\\EDairy\\{DateTime.Now.ToShortDateString()}");

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
