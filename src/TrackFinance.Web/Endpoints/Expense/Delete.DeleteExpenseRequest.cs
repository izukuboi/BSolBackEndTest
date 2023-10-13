namespace TrackFinance.Web.Endpoints.Expense;

public class DeleteExpenseRequest
{
  public const string Route = "/Expense/{ExpenseId:int}";
  public static string BuildRoute(int expenseId) => Route.Replace("{ExpenseId:int}", expenseId.ToString());
  public int ExpenseId { get; set; }
}
