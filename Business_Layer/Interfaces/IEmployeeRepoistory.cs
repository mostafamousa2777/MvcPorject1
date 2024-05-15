using DataAccess_Layer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IEmployeeRepoistory:IGenericRepo<Employee>
    {
       Task< IEnumerable<Employee>> GetAllByNameAsync(string name);
    }
}
