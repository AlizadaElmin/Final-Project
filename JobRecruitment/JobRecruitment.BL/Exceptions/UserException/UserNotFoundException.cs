using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.UserException;

public class UserNotFoundException : Exception, IBaseException
{
    public int Code => StatusCodes.Status404NotFound;

    public string ErrorMessage { get; }

    public UserNotFoundException()
    {
        ErrorMessage = "User not found";
    }

    public UserNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}