namespace TrackFinance.Web.Endpoints.Expense;

public class ExpenseListRequest
{
  public const string Route = "/Expense/{UserId:int}/user";
  public static string BuildRoute(int userId) => Route.Replace("{UserId:int}", userId.ToString());
  public int UserId { get; set; }
}
