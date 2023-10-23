using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUser : EndpointBaseAsync
  .WithRequest<GetHistoricalRecordByUserRequest>
  .WithActionResult<GetHistoricalRecordByUserResponse>
{
  private readonly IRepository<Transaction> _repository;

  public GetHistoricalRecordByUser(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetHistoricalRecordByUserRequest.Route)]
  [SwaggerOperation(
      Summary = "Get Historical Records by user with pagination",
      Description = "Get Historical Records by userId with Keyset pagination",
      OperationId = "HistoricalRecords.GetHistoricalRecordByUser",
      Tags = new[] { "HistoricalRecordsEndpoints" })
  ]
  public override async Task<ActionResult<GetHistoricalRecordByUserResponse>> HandleAsync([FromRoute] GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken = default) =>
    Ok(new GetHistoricalRecordByUserResponse
    {
      HistoricalRecord = (await _repository.ListAsync(cancellationToken))
         .OrderBy(expense => expense.Id)
         .Where(Expense => Expense.Id > request.LastTransactionId) //Keyset pagination https://learn.microsoft.com/en-us/ef/core/querying/pagination#keyset-pagination
         .Where(expense => expense.UserId == request.UserId)
         .Where(date => Convert.ToDateTime(date.ExpenseDate.ToString("d")) >= request.StartDate && Convert.ToDateTime(date.ExpenseDate.ToString("d")) <= request.EndDate)
         .Select(expense => new HistoricalRecord(
                                               transactionId: expense.Id,
                                               description: expense.Description,
                                               transactionDescriptionType: expense.TransactionDescriptionType,
                                               amount: expense.Amount,
                                               expenseDate: expense.ExpenseDate,
                                               transactionType: expense.TransactionType))
         .OrderBy(d => d.expenseDate)
         .Take(request.PageSize)
         .ToList()
    });
}
