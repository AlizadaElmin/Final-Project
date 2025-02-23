using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.CandidateJobOfferException;

public class InvalidResumeSizeException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public InvalidResumeSizeException()
    {
        ErrorMessage = "Resume file size exceeds the limit.";
    }
    public InvalidResumeSizeException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}