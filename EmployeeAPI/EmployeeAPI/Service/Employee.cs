using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;
using EmployeeAPI.Repository;

namespace EmployeeAPI.Service
{
    public class Employee : IEmployee
    {
        public readonly SearchContext searchContext;
        private readonly IRepository Irepository;
        public Employee(IRepository Irepo)
        {
            this.Irepository = Irepo;
           
        }

        public async Task<object> GetEmployeeService(int id)
        {

          var response= await Irepository.GetEmployeeRepo(id);
            return response;
          
        }

        public async Task<TblEmployee> GetEmployeeServicehirerchy(int id)
        {

            var response = await Irepository.GetEmployeeRepohirerchy(id);
            return response;

        }

        public async Task<object> GetCitiesService()
        {
            var Response = await Irepository.GetCitiesRepo();
            return Response;

        }

        public async Task<object> GetCitiesUIService(int CityID, string CityName)
        {
            var Response = await Irepository.GetCitiesUIRepo(CityID, CityName);
            return Response;

        }

        public async Task<object> getemployeeID(int id)
        {
           var val= await Irepository.GetEmployeeId(id);
            return val;
        }


    }
}
