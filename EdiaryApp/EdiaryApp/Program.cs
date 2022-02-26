

using DataAccess;
using DataAccess.Models;

namespace EdiaryApp;

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
                Console.WriteLine("Enter the date in dd-mm-yyyy format for which you need the summary");
                string? dateTimeStr = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(dateTimeStr))
                {
                    Console.WriteLine("Date is null");
                    Console.WriteLine("Re-launch the application and enter the correct format");
                    return;
                }

                var validationModel = Operation.ValidateDateString(dateTimeStr);
                if (validationModel.IsValid)
                {
                    List<QuestionModel> questions = SqliteDataAccess.GetQuestions();
                    List<AnswerModel> answers =  SqliteDataAccess.GetAnswerForGivendate(dateTimeStr);

                    foreach(AnswerModel answer in answers)
                    {
                        QuestionModel? question = questions.Where(x => x.Id == answer.questionId).FirstOrDefault();
                        if(question is not null)
                        {
                            Console.WriteLine(question.Question);
                        }
                        Console.WriteLine(answer.answer);
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
                List<QuestionModel> questions =  SqliteDataAccess.GetQuestions();
                List<AnswerModel> answerModels = new List<AnswerModel>();
                Dictionary<QuestionModel, string> data = new Dictionary<QuestionModel,string>();
                foreach(var question in questions)
                {
                    Console.WriteLine(question.Question);
                    string? answer = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(answer))
                    {
                        answer = Constants.NODATA;
                    }
                    answerModels.Add(
                        new AnswerModel() 
                        {answer = answer, questionId = question.Id, DateModified = DateTime.Now, reportId = 1});
                }

                SqliteDataAccess.SaveAnswersForADay(answerModels);                
            }
        }
    }

}




