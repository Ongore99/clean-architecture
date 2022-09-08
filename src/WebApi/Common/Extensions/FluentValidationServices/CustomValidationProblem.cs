using System.Net;

namespace WebApi.Common.Extensions.FluentValidationServices;

public class CustomValidationProblem
{
    public string Title { get; set; } = "One or more validation errors occurred.";

    public int Status { get; set; } = (int) HttpStatusCode.BadRequest;

    public string Type { get; set; } = "https://httpstatuses.io/400";

    public Dictionary<string, List<CustomFailure>> Errors { get; set; } = new();
}


public class CustomFailure
{
    public int? ErrorCode { get; set; }
    
    public string ErrorMessage { get; set; }
}