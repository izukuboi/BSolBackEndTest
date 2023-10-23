namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDateResponse
{
  public List<TransactionRecord>? ExpensesTransaction { get; set; }
  public List<TransactionRecord>? IncomesTransaction { get; set; }
}
