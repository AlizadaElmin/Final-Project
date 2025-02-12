using JobRecruitment.Core.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.Exceptions.Common;

public class NotFoundException<T>:Exception,IBaseException where T:BaseEntity
{
    public int Code => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }
    const string _message = "not found";

    public NotFoundException():base(typeof(T).Name+_message)
    {
        ErrorMessage = typeof(T).Name + _message;
    }

    public NotFoundException(string messsage):base(messsage)
    {
        ErrorMessage = messsage;
    }
}