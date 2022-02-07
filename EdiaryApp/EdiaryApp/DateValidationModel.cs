public class DateValidationModel
{
    public bool IsValid;
    public string ValidationMessage { get; set; }

    public DateValidationModel(bool isvalid, string message)
    {
        IsValid = isvalid;  
        ValidationMessage = message;           
    }
}






