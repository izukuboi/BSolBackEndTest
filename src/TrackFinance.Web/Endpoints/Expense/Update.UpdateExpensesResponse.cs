using TrackFinance.Web.Endpoints.ProjectEndpoints;

namespace TrackFinance.Web.Endpoints.Expense;

public class UpdateExpensesResponse
{
  public UpdateExpensesResponse(ExpenseRecord expenseRecord)
  {
    ExpenseRecord = expenseRecord;
  }
  public ExpenseRecord ExpenseRecord { get; set; }
}
