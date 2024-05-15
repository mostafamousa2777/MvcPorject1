using Business_Layer.Interfaces;
using DataAccess_Layer.Context;
using DataAccess_Layer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repoistory
{
    public class DepartmentRepo :  GenericRepo<Department>,IDepartmentRepo
    {
        public DepartmentRepo(CompanydbContext context):base(context)
        {
            
        }








    }
}
