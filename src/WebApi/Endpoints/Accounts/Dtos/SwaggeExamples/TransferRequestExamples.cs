using Swashbuckle.AspNetCore.Filters;
using WebApi.Endpoints.Accounts.Dtos.Requests;

namespace WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

public class TransferRequestExamples : IMultipleExamplesProvider<TransferRequestDto>
{
    /// <inheritdoc />
    public IEnumerable<SwaggerExample<TransferRequestDto>> GetExamples()
    {
        yield return SwaggerExample.Create(
            "Positive Amount",
            new TransferRequestDto()
            {
                Amount = 1,
                AccountReceiverId = 2
            }
        );
        yield return SwaggerExample.Create(
            "Negative Amount",
            new TransferRequestDto
            {
                Amount = -1,
                AccountReceiverId = 2
            }
        );
    }
}