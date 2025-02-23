using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.CandidateJobOfferException;

public class InvalidResumeTypeException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public InvalidResumeTypeException()
    {
        ErrorMessage = "Resume file must be a PDF.";
    }
    public InvalidResumeTypeException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}