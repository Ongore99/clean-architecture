using Core.UseCases.Accounts.Queries.GetUserAccount;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Endpoints.Accounts.Dtos.Requests;

namespace WebApi.Endpoints.Accounts.Dtos.SwaggeExamples;

public class GetAccountResponseExamples : IMultipleExamplesProvider<GetUserAccountOutDto>
{
    /// <inheritdoc />
    public IEnumerable<SwaggerExample<GetUserAccountOutDto>> GetExamples()
    {
        yield return SwaggerExample.Create(
            "Positive Transactions",
            new GetUserAccountOutDto()
            {
                CustomerId = 1,
                Balance = 1,
                Transactions = new List<GetUserAccountOutDto.TransactionOutDto>
                {
                    new()
                    {
                        Amount = 100.0m,
                        DateCreated = DateTime.Now
                    }
                }
            }
        );
    }
}