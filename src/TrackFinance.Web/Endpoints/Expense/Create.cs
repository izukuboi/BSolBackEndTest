using System.Net;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expense;

public class Create : EndpointBaseAsync
    .WithRequest<CreateExpensesRequest>
    .WithActionResult<CreateExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;

  public Create(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPost("/Expenses")]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "Creates a new Expenses",
    Description = "Creates a new Expenses",
    OperationId = "Expenses.Create",
    Tags = new[] { "ExpensesEndpoints" })
]
  public override async Task<ActionResult<CreateExpensesResponse>> HandleAsync(CreateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var newTransaction = new Transaction(request.Description,
                                     request.Amount,
                                     request.ExpenseType,
                                     request.ExpenseDate,
                                     request.UserId,
                                     TransactionType.Expense);
    var transactionCreatedResult = await _repository.AddAsync(newTransaction, cancellationToken);

    return Ok(new CreateExpensesResponse
    (
    statusResult: (int)HttpStatusCode.OK,
    expensesId: transactionCreatedResult.Id
    ));
  }
}
