using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Expense;

public record ExpenseRecord(
  int expenseId,
  string description,
  decimal amount,
  TransactionDescriptionType transactionDescriptionType,
  DateTime expenseDate,
  int userId,
  TransactionType transactionType
);
