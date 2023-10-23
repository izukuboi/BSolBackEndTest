using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByDateSpec : Specification<Transaction>, ISingleResultSpecification
{
  public ItemsByDateSpec(DateTime startDate, DateTime endDate, TransactionType transactionType, int userId)
  {
    Query.Where(h => h.ExpenseDate.Date >= startDate.Date && h.ExpenseDate.Date <= endDate.Date)
         .Where(h => (transactionType == TransactionType.All) || h.TransactionType == transactionType)
         .Where(h => h.UserId == userId)
         .OrderBy(g => g.ExpenseDate);
  }
}
