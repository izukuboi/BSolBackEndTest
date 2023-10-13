using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expense;

public class UpdateExpensesValidator : AbstractValidator<UpdateExpensesRequest>
{
  public UpdateExpensesValidator() 
  { 
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}
