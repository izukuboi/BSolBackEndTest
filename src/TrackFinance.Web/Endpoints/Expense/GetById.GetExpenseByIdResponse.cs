using System.Xml.Linq;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Web.Endpoints.ProjectEndpoints;

namespace TrackFinance.Web.Endpoints.Expense;

public class GetExpenseByIdResponse
{
  public GetExpenseByIdResponse(string description, decimal amount, TransactionDescriptionType expenseType, DateTime expenseDate)
  {
    Description = description;
    Amount = amount;
    ExpenseType = expenseType;
    ExpenseDate = expenseDate;
  }

  public string Description { get; set; }
  public decimal Amount { get; set; }
  public TransactionDescriptionType ExpenseType { get; set; }
  public DateTime ExpenseDate { get; set; }
}
