using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.UserException;

public class LoginFailedException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public LoginFailedException()
    {
        ErrorMessage = "Login failed.";
    }
    public LoginFailedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}