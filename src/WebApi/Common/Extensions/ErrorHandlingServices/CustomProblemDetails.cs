namespace WebApi.Common.Extensions.ErrorHandlingServices;

using Microsoft.AspNetCore.Mvc;

public class CustomProblemDetails : ProblemDetails
{ 
    public int? Code { get; set; }
}