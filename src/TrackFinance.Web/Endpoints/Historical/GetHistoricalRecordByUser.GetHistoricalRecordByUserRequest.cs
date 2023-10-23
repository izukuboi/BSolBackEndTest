using Microsoft.AspNetCore.Mvc;

namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUserRequest
{
  public const string UserIdSubRoute = "{UserId:int}";
  public const string Route = $"/HistoricalRecords/{UserIdSubRoute}/user";
  public const int DefaultPageSize = 5;
  public static string BuildRoute(int userId, string? startDate, string? endDate) =>    Route.Replace(UserIdSubRoute, userId.ToString()) +
    $"?startDate={startDate ?? DateTime.Now.ToString("d")}&endDate={endDate ?? DateTime.Now.ToString("d")}";
  public int UserId { get; set; }

  [FromQuery(Name = "startDate")]
  public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
  [FromQuery(Name = "endDate")]
  public DateTime EndDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
  [FromQuery(Name = "pageSize")]
  public int PageSize { get; set; } = DefaultPageSize;
  [FromQuery(Name = "lastTransactionId")]
  public int LastTransactionId { get; set; } = 0;
}
