namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesResponse
{
  public ExpenseRecord _expenseRecord { get; set; }
  public UpdateExpensesResponse(ExpenseRecord expenseRecord)
  {
    _expenseRecord = expenseRecord;
  }
}
