using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.Bases;

public class CustomProblemDetails : ProblemDetails
{
        public IDictionary<string, object?> AdditionalData { get; set; } = new Dictionary<string, object?>(StringComparer.Ordinal);
}