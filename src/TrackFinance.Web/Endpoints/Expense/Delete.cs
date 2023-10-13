using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Expense;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteExpenseRequest>
    .WithoutResult
{
  private readonly IRepository<Transaction> _repository;

  public Delete(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteExpenseRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a Expense",
      Description = "Delete a Expense Saved",
      OperationId = "Expenses.Delete",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromRoute] DeleteExpenseRequest request, CancellationToken cancellationToken = default)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(request.ExpenseId);
    if (aggregateToDelete == null) return NotFound();

    await _repository.DeleteAsync(aggregateToDelete, cancellationToken);

    return NoContent();
  }
}
