using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.Extensions.FluentValidationServices;

public class CustomValidationProblemDetails : ProblemDetails
{
    public string Type { get; set; } 

    public Dictionary<string, List<CustomFailure>> Errors { get; set; } = new();

    public CustomValidationProblemDetails()
    {
        Title = "One or more validation errors occurred.";
        Status = (int) HttpStatusCode.BadRequest;
        Type = "https://httpstatuses.io/400";
    }
}


public class CustomFailure
{
    public int? ErrorCode { get; set; }
    
    public string ErrorMessage { get; set; }
}