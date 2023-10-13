using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expense;

public class List : EndpointBaseAsync
    .WithRequest<ExpenseListRequest>
    .WithActionResult<ExpenseListResponse>
{
  private readonly IReadRepository<Transaction> _repository;

  public List(IReadRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(ExpenseListRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all Expenses",
      Description = "Gets a list of all Expenses",
      OperationId = "Expense.List",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<ExpenseListResponse>> HandleAsync([FromRoute] ExpenseListRequest expenseListRequest, CancellationToken cancellationToken = default)
  {
    var response = new ExpenseListResponse();
    response.Expenses = (await _repository.ListAsync(cancellationToken)) // TODO: pass cancellation token
        .Where(expense => expense.UserId == expenseListRequest.UserId && expense.TransactionType == TransactionType.Expense)
        .Where(expense => expense.TransactionType == TransactionType.Expense)
        .Select(expense => new ExpenseRecord(expenseId: expense.Id,
                                              description: expense.Description,
                                              amount: expense.Amount,
                                              transactionDescriptionType: expense.TransactionDescriptionType,
                                              expenseDate: expense.ExpenseDate,
                                              userId: expense.UserId,
                                              transactionType: expense.TransactionType))
        .ToList();

    return Ok(response);
  }
}
