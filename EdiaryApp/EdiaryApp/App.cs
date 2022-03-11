using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiaryApp
{
    public class App : IApp
    {
        private readonly ISqliteDataAccess _sqliteDataAccess;
        private readonly IOperation _operation;

        public App(ISqliteDataAccess sqliteDataAccess, IOperation operation)
        {
            this._sqliteDataAccess = sqliteDataAccess;
            this._operation = operation;
        }
        public void Run()
        {
            Console.WriteLine("Hi Pramod, welcome to EDiary!!");
            Console.WriteLine("Do you wish to get the details of a partiular day? If Yes, press 1, otherwise press 0");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value == 1)
                {
                    Console.WriteLine("Enter the date in dd-mm-yyyy format for which you need the summary");
                    string? dateTimeStr = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(dateTimeStr))
                    {
                        Console.WriteLine("Date is null");
                        Console.WriteLine("Re-launch the application and enter the correct format");
                        return;
                    }

                    var validationModel = _operation.ValidateDateString(dateTimeStr);
                    if (validationModel.IsValid)
                    {
                        List<IQuestionModel> questions = _sqliteDataAccess.GetQuestions();
                        List<AnswerModel> answers = _sqliteDataAccess.GetAnswerForGivendate(dateTimeStr);

                        foreach (IAnswerModel answer in answers)
                        {
                            IQuestionModel? question = questions.Where(x => x.Id == answer.questionId).FirstOrDefault();
                            if (question is not null)
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
                    List<IQuestionModel> questions = _sqliteDataAccess.GetQuestions();
                    List<IAnswerModel> answerModels = new List<IAnswerModel>();
                    Dictionary<IQuestionModel, string> data = new Dictionary<IQuestionModel, string>();
                    foreach (var question in questions)
                    {
                        Console.WriteLine(question.Question);
                        string? answer = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(answer))
                        {
                            answer = Constants.NODATA;
                        }
                        answerModels.Add(
                            new AnswerModel()
                            { answer = answer, questionId = question.Id, DateModified = DateTime.Now, reportId = 1 });
                    }

                    _sqliteDataAccess.SaveAnswersForADay(answerModels);
                }
            }

        }
    }
}
