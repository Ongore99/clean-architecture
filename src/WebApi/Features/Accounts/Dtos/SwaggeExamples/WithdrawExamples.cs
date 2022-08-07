using Swashbuckle.AspNetCore.Filters;
using WebApi.Features.Accounts.Dtos.Requests;

namespace WebApi.Features.Accounts.Dtos.SwaggeExamples;

public class WithdrawExamples : IMultipleExamplesProvider<WithdrawRequestDto>
{
    public IEnumerable<SwaggerExample<WithdrawRequestDto>> GetExamples()
    {
        yield return SwaggerExample.Create(
            "Positive Balance",
            new WithdrawRequestDto()
            {
                Balance = 1,
                AccountId = 1,
                UserId = 1
            }
        );
        yield return SwaggerExample.Create(
            "Positive Balance",
            new WithdrawRequestDto()
            {
                Balance = 1,
                AccountId = 1,
                UserId = 1
            }
        );
    }
}