﻿using Microsoft.AspNetCore.Mvc;
using MyWallet.Services;
using MyWallet.Services.Dto;

namespace MyWallet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public ExpenseDto CreateExpense([FromBody] ExpenseCreateDto expense) => _expenseService.CreateExpense(expense);

        [HttpGet("/sum")]
        public decimal GetExpenseSum(int year, int month, int? day, Guid? categoryId) =>
            _expenseService.GetExpenseSum(year, month, day, categoryId);

        [HttpGet("/list")]
        public IEnumerable<ExpenseDto> GetExpenseList(int year, int month, int? day, Guid? categoryId) =>
            _expenseService.GetExpenseList(year, month, day, categoryId);
    }
}