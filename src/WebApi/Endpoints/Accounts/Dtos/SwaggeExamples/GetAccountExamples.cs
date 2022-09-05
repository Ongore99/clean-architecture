using System.Collections.ObjectModel;
using Core.UseCases.Accounts.Queries.GetUserAccount;
using Gridify;
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
                Transactions = new QueryablePaging<GetUserAccountOutDto.TransactionOutDto>(1,
                    new List<GetUserAccountOutDto.TransactionOutDto>
                {
                    new()
                    {
                        Amount = 1,
                        DateCreated = default
                    }
                }.AsQueryable())
            }
        );
    }
}