public class ReadFromTextModel
{
    public bool IsValid;
    public string StatusMessage { get; set; }

    public ReadFromTextModel(bool isvalid, string message)
    {
        IsValid = isvalid;
        StatusMessage = message;
    }
}






