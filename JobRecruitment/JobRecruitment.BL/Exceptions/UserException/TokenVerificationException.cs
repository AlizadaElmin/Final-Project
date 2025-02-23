using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.UserException;

public class TokenVerificationException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public TokenVerificationException()
    {
        ErrorMessage = "Token verification failed.";
    }
    public TokenVerificationException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
