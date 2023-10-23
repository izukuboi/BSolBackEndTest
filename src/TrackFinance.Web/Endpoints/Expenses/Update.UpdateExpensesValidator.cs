using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesValidator : AbstractValidator<UpdateExpensesRequest>
{
  public UpdateExpensesValidator()
  {
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
    RuleFor(expense => expense.UserId).NotEmpty().NotNull();
  }
}
