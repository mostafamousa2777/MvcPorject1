using Business_Layer.Interfaces;
using DataAccess_Layer.Context;
using DataAccess_Layer.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repoistory
{
    public class EmployeeRepo : GenericRepo<Employee>,IEmployeeRepoistory
    {
        public EmployeeRepo(CompanydbContext context):base(context) { }

        public  async Task< IEnumerable<Employee>> GetAllByNameAsync(string name)
        {
          return await _context.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e=>e.Department).ToListAsync();
        }
    }
}
