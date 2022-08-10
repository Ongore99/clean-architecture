using Swashbuckle.AspNetCore.Filters;
using WebApi.Endpoints.Accounts.Dtos.Requests;

namespace WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

public class WithdrawExamples : IMultipleExamplesProvider<WithdrawRequestDto>
{
    /// <inheritdoc />
    public IEnumerable<SwaggerExample<WithdrawRequestDto>> GetExamples()
    {
        yield return SwaggerExample.Create(
            "Positive Balance",
            new WithdrawRequestDto()
            {
                Balance = 1,
            }
        );
        yield return SwaggerExample.Create(
            "Negative Balance",
            new WithdrawRequestDto
            {
                Balance = -1,
            }
        );
    }
}