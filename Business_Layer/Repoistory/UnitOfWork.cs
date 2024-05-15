using Business_Layer.Interfaces;
using DataAccess_Layer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repoistory
{
    public class UnitOfWork : IUniteOfWork
    {
        private readonly Lazy <IEmployeeRepoistory> _employees;
        private readonly Lazy<IDepartmentRepo> _departments;
        private readonly CompanydbContext _context;

        public UnitOfWork(CompanydbContext context)
        {
            _context = context;
            _employees = new Lazy<IEmployeeRepoistory>(new EmployeeRepo(context));
            _departments = new Lazy < IDepartmentRepo >( new DepartmentRepo(context));   
          

        }
        public IEmployeeRepoistory Employees { get => _employees.Value; }
        public IDepartmentRepo Departments { get => _departments.Value; }

        public async Task<int> CompeleteAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
          await _context.DisposeAsync();
        }
    }
}
