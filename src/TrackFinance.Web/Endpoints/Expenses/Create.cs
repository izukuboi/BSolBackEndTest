using System.Net;
using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Create : EndpointBaseAsync
    .WithRequest<CreateExpensesRequest>
    .WithActionResult<CreateExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;
  public Create(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateExpensesRequest.Route)]
  [Produces(MediaTypeNames.Application.Json)]
  [SwaggerOperation(
    Summary = "Creates a new Expense",
    Description = "Creates a new Expense",
    OperationId = "Expenses.Create",
    Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<CreateExpensesResponse>> HandleAsync(CreateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var newExpense = new Transaction(request.Description,
                                     request.Amount,
                                     request.ExpenseType,
                                     request.ExpenseDate,
                                     request.UserId,
                                     TransactionType.Expense);

    var createdRecord = await _repository.AddAsync(newExpense, cancellationToken);

    var response = new CreateExpensesResponse
    (
    statusResult: (int)HttpStatusCode.OK,
    expensesId: createdRecord.Id
    );

    return Ok(response);
  }
}

