using Microsoft.EntityFrameworkCore;
using MyWallet.Core;
using MyWallet.Core.Dal;
using MyWallet.Services.Dto;

namespace MyWallet.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    public ExpenseDto CreateExpense(ExpenseCreateDto expense)
    {
        var exp = new Expense
        {
            Id = Guid.NewGuid(),
            CategoryId = expense.CategoryId,
            DateOfCreation = expense.DateTime ?? DateTime.Now,
            Description = expense.Description,
            Value = expense.Value
        };

        _context.Expenses.Add(exp);
        _context.SaveChanges();

        var category = _context.Categories
            .First(x => x.Id == exp.CategoryId);

        return new ExpenseDto(exp.Id, category.Name, exp.Value, exp.DateOfCreation, exp.Description);
    }

    public IEnumerable<ExpenseDto> GetExpenseList(int year, int month, int? day, Guid? categoryId)
    {
        var exp = FilterExpense(year, month, day, categoryId);

        if (exp is null)
        {
            return new List<ExpenseDto>();
        }

        return exp
            .OrderBy(x => x.DateOfCreation)
            .Include(x => x.Category)
            .Select(x => new ExpenseDto(x.Id, x.Category.Name, x.Value, x.DateOfCreation, x.Description))
            .ToList();
    }

    public decimal GetExpenseSum(int year, int month, int? day, Guid? categoryId)
    {
        decimal result = 0;

        var exp = FilterExpense(year, month, day, categoryId);

        if (exp is null)
        {
            return result;
        }

        var sum = exp.Select(x => x.Value).ToList();
        result = sum.Sum();

        return result;
    }

    private IQueryable<Expense>? FilterExpense(int year, int month, int? day, Guid? categoryId)
    {
        if (year == 0 || month == 0)
        {
            return null;
        }

        var exp = _context.Expenses
            .Where(x => x.DateOfCreation.Year == year && x.DateOfCreation.Month == month);

        if (day is not null)
        {
            exp = exp.Where(x => x.DateOfCreation.Day == day);
        }

        if (categoryId is not null)
        {
            exp = exp.Where(x => x.CategoryId == categoryId);
        }

        return exp;
    }
}
