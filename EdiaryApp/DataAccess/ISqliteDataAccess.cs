using DataAccess.Models;

namespace DataAccess
{
    public interface ISqliteDataAccess
    {
        List<AnswerModel> GetAnswerForGivendate(string date);
        List<IQuestionModel> GetQuestions();
        void SaveAnswersForADay(List<IAnswerModel> answers);
    }
}