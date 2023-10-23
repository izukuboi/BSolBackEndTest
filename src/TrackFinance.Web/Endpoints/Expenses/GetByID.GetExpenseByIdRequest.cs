namespace TrackFinance.Web.Endpoints.Expenses;

public class GetExpenseByIdRequest
{
  public const string ExpenseIdSubRoute = "{Expense:int}";

  public const string Route = $"/Expenses/{ExpenseIdSubRoute}";
  public static string BuildRoute(int expenseId) => Route.Replace(ExpenseIdSubRoute, expenseId.ToString());
  public int ExpenseId { get; set; }
}
