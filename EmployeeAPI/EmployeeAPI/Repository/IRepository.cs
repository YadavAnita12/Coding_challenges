using System.Threading.Tasks;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repository
{
    public interface IRepository
    {

        Task<object> GetEmployeeRepo(int id);
        Task<TblEmployee> GetEmployeeRepohirerchy(int id);
        Task<object> GetCitiesRepo();
        Task<object> GetCitiesUIRepo(int CityID, string CityName);
        Task<object> GetEmployeeId(int id);

    }
}
