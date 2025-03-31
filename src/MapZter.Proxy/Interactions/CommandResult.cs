namespace MapZter.Proxy.Interactions;

public record CommandResult : IResult
{
    public CommandResult(bool isSuccess, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}