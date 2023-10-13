using TrackFinance.Web.Endpoints.ProjectEndpoints;

namespace TrackFinance.Web.Endpoints.Expense;

public class ExpenseListResponse
{
  public List<ExpenseRecord> Expenses { get; set; } = new();
}
