using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IUniteOfWork: IAsyncDisposable
    {
        public IEmployeeRepoistory Employees { get; }
        public IDepartmentRepo Departments{ get;  }
        public Task<int> CompeleteAsync();
    }
}
