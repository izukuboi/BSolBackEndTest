using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expense;

public class ExpenseListValidator : AbstractValidator<ExpenseListRequest>
{
  public ExpenseListValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
