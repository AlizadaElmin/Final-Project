using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.PasswordException;

public class PasswordResetFailedException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }
    public PasswordResetFailedException()
    {
        ErrorMessage = "Unsuccessful reset password.";
    }

    public PasswordResetFailedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}