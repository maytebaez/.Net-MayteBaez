namespace BankAccountsMicroservice.Exceptions
{
    public class ValidationException(string message) : Exception(message)
    {
    }
}