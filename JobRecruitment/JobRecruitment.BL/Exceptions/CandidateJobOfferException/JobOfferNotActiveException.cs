using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.CandidateJobOfferException;

public class JobOfferNotActiveException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }

    public JobOfferNotActiveException()
    {
        ErrorMessage = "Job offer status is not active.";
    }
    public JobOfferNotActiveException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}