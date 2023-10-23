using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDateRequest
{
  public const string Route = "/Balance/{UserId}/{TransactionType}";
  public static string BuildRoute(int userId, string? startDate, string? endDate) => Route.Replace("{UserId}", userId.ToString()) +
    $"?startDate={startDate ?? DateTime.Now.ToString("d")}&endDate={endDate ?? DateTime.Now.ToString("d")}";
  public int UserId { get; set; }
  public TransactionType TransactionType { get; set; }

  [FromQuery(Name = "startDate")]
  [BindRequired]
  public DateTime StartDate { get; set; }

  [FromQuery(Name = "endDate")]
  [BindRequired]
  public DateTime EndDate { get; set; }
}
