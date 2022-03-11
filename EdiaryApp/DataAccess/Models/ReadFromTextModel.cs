public class ReadFromTextModel : IReadFromTextModel
{
    public bool IsValid;
    public string StatusMessage { get; set; }

    public ReadFromTextModel(bool isvalid, string message)
    {
        IsValid = isvalid;
        StatusMessage = message;
    }
}






