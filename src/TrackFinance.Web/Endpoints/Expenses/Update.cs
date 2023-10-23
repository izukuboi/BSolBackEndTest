using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpensesRequest>
    .WithActionResult<UpdateExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;

  public Update(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateExpensesRequest.Route)]
  [Produces(MediaTypeNames.Application.Json)]
  [SwaggerOperation(
     Summary = "Updates an expense",
     Description = "Updates an expense",
     OperationId = "Expenses.Update",
     Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateExpensesResponse>> HandleAsync(UpdateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var existingExpense = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingExpense is null)
    {
      return NotFound();
    }

    existingExpense.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Expense);

    var response = new UpdateExpensesResponse(
      expenseRecord: new ExpenseRecord(
        existingExpense.Id,
        existingExpense.Description,
        existingExpense.Amount,
        existingExpense.TransactionDescriptionType,
        existingExpense.ExpenseDate,
        existingExpense.UserId,
        existingExpense.TransactionType));

    return Ok(response);
  }
}
