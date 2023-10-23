using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDate : EndpointBaseAsync
    .WithRequest<BalanceByDateRequest>
    .WithActionResult<BalanceByDateResponse>
{
  private readonly ITransactionFinanceService _transactionService;

  public BalanceByDate(ITransactionFinanceService transactionService)
  {
    _transactionService = transactionService;
  }

  [HttpGet(BalanceByDateRequest.Route)]
  [Produces(MediaTypeNames.Application.Json)]
  [SwaggerOperation(
    Summary = "group",
    Description = "group",
    OperationId = "Balance.List",
    Tags = new[] { "BalanceEndpoints" })]
  public override async Task<ActionResult<BalanceByDateResponse>> HandleAsync([FromRoute]BalanceByDateRequest request, CancellationToken cancellationToken = default)
  {
    var transactions = await _transactionService.GetTransactionItemByDateAsync(request.UserId, request.TransactionType, request.StartDate, request.EndDate, cancellationToken);

    if (transactions.Status != ResultStatus.Ok) return BadRequest(transactions.ValidationErrors);

    return Ok(new BalanceByDateResponse
    {
      ExpensesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Expense),
      IncomesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Income)
    });
  }

  private static List<TransactionRecord>? GetTransactionsRecords(List<TransactionDataDto> transactions, TransactionType transactionType)
  {
    return new List<TransactionRecord>(transactions.Where(x => x.TransactionType == transactionType)
                       .Select(item => new TransactionRecord(
                        item.Date,
                        item.DayOfWeek,
                        item.Day,
                        item.TotalAmount,
                        item.TransactionDescriptionType,
                        item.Week,
                        item.Year,
                        item.Month
                       )));
  }
}
