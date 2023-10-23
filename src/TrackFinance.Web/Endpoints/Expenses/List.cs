using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expenses;

public class List : EndpointBaseAsync
    .WithRequest<ExpensesListRequest>
    .WithActionResult<ExpensesListResponse>
{
  private readonly IReadRepository<Transaction> _repository;

  public List(IReadRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(ExpensesListRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all Expenses",
      Description = "Gets a list of all Expenses",
      OperationId = "Expense.List",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<ExpensesListResponse>> HandleAsync([FromRoute]ExpensesListRequest request, CancellationToken cancellationToken = default)
  {
    var response = new ExpensesListResponse();

    response.Expenses = (await _repository.ListAsync(cancellationToken))
        .Where(income => income.UserId == request.UserId && income.TransactionType == TransactionType.Expense)
        .Select(income => new ExpenseRecord(incomeId: income.Id,
                                           description: income.Description,
                                           amount: income.Amount,
                                           transactionDescriptionType: income.TransactionDescriptionType,
                                           expenseDate: income.ExpenseDate,
                                           userId: income.UserId,
                                           transactionType: income.TransactionType))
        .ToList();

    return Ok(response);
  }
}
