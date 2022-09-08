using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.Extensions.ErrorHandlingServices;

public class CustomProblemDetails : ProblemDetails
{ 
    public int? Code { get; set; }
}