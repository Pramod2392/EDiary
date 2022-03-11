
namespace DataAccess
{
    public interface ITextDataAccess
    {
        ReadFromTextModel ReadFromText(DateOnly dateString);
        void WriteToText(List<string> dataToText);
    }
}