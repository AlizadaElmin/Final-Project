using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.PasswordException;

public class ChangePasswordFailedException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public ChangePasswordFailedException()
    {
        ErrorMessage = "Unsuccessful change password.";
    }
    public ChangePasswordFailedException(string message) : base(message) 
    {
        ErrorMessage = message;
    }
}