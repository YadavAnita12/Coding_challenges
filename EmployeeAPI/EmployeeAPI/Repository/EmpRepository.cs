using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repository
{
    public class EmpRepository : IRepository
    {

       
        private readonly SearchContext searchContext;
        public EmpRepository(SearchContext searchContext)
        {
          //  this.repositoty = Irepo;
            this.searchContext = searchContext;
        }
        public async Task<object> GetEmployeeRepo(int id)
        {

            var val = await searchContext.TblEmployees.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
            return val;
        }

        public async Task<TblEmployee> GetEmployeeRepohirerchy(int id)
        {

            var val = await searchContext.TblEmployees.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
            return val;
        }

        public async Task<object> GetCitiesRepo()
        {
            var Response = await (from ct in searchContext.TblCities
                                  select new
                                  {
                                      ct.CityId,
                                      ct.CityName

                                  }).ToListAsync();

            return Response;

        }
        public async Task<object> GetCitiesUIRepo(int CityID, string CityName)
        {
            var Response = await (from ct in searchContext.TblCities
                                  where ct.CityId == CityID || ct.CityName == CityName
                                  select new
                                  {
                                      CityId = ct.CityId,
                                      CityName = CityName

                                  }).ToListAsync();


            return Response;

        }
        public async Task<object> GetEmployeeId(int id)
        {

           var val= searchContext.TblEmployees.Where(x => x.EmployeeId == id).FirstOrDefault();
            return val;

        }

    }
}
