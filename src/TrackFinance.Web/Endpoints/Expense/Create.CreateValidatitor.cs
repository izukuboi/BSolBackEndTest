using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expense;

public class CreateExpenseValidator : AbstractValidator<CreateExpensesRequest>
{
  public CreateExpenseValidator() 
  { 
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
    RuleFor(expense => expense.Amount).GreaterThan(0);
  }
}
