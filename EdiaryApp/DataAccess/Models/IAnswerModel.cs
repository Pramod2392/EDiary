
namespace DataAccess.Models
{
    public interface IAnswerModel
    {
        string answer { get; set; }
        DateTime DateModified { get; set; }
        int questionId { get; set; }
        int reportId { get; set; }
    }
}