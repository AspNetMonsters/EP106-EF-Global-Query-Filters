using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfQueryFilters.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EfQueryFilters.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeContext _context;

        public IndexModel(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> Employees;

        public void OnGet()
        {
            Employees = _context.Employees.ToList();
        }
    }
}
