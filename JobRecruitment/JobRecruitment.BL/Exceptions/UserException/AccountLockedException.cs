using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.UserException;

public class AccountLockedException : Exception, IBaseException
{
    public int Code => StatusCodes.Status403Forbidden;
    public string ErrorMessage { get; }

    public AccountLockedException(DateTimeOffset lockoutEnd)
    {
        ErrorMessage = $"Your account is locked. Please wait until {lockoutEnd:yyyy-MMM-dd HH:mm:ss}.";
    }
    public AccountLockedException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}