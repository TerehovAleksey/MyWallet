using MyWallet.Services.Dto;

namespace MyWallet.Services;

public interface IExpenseService
{
    public ExpenseDto? CreateExpense(ExpenseCreateDto expense);
    public IEnumerable<ExpenseDto> GetExpenseList(int year, int month, int? day, Guid? categoryId);
    public decimal GetExpenseSum(int year, int month, int? day, Guid? categoryId);
}
