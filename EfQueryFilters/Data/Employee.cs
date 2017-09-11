using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfQueryFilters.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Title { get; set; }

        public int CompanyId { get; set; }

        public bool IsDeleted { get; set; }
        
    }
}
