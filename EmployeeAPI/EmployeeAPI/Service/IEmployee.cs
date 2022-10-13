using System.Threading.Tasks;
using EmployeeAPI.Models;

namespace EmployeeAPI.Service
{
    public interface IEmployee
    {

        Task<object> GetEmployeeService(int id);
        Task<TblEmployee> GetEmployeeServicehirerchy(int id);
        Task<object> GetCitiesService();
        Task<object> GetCitiesUIService(int CityID, string CityName);
        Task<object> getemployeeID(int id);



    }
}
