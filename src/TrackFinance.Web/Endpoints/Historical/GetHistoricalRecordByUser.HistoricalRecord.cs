using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Historical;

public record HistoricalRecord(int transactionId, string description, TransactionDescriptionType transactionDescriptionType, decimal amount, DateTime expenseDate, TransactionType transactionType);

