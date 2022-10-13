using System.Text.Json;
using System.Threading.Tasks;
using EmployeeAPI.Models;
using EmployeeAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.CompareNetObjects;
using NUnit.Framework;

namespace EmployeeAPI.Tests
{

    public class RepositoryTest
    {
        private DbContextOptions<SearchContext> dbContextOptions;


        public RepositoryTest()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder<SearchContext>()
                           .UseInMemoryDatabase("DbcontextTest");
            dbContextOptions = new DbContextOptionsBuilder<SearchContext>().UseInMemoryDatabase("DbcontextTest").ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
            using var context = new SearchContext(dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.AddRange(
                new TblEmployee { EmployeeId = 1, Name = "Anita", City = "Mumbai", Department = "IT", Gender = "F" });
            context.AddRange(
                new TblCity { CityId = 1, CityName = "New Delhi" });
            context.SaveChanges();

        }

        [Test, Category("GetEmployeeRepo")]
        public async Task GetEmployeeRepo_Test()
        {
            var context = new SearchContext(dbContextOptions);
            EmpRepository repo = new EmpRepository(context);
            TblEmployee tblEmployee = new TblEmployee();
            tblEmployee.EmployeeId = 1;
            tblEmployee.Name = "Anita";
            tblEmployee.City = "Mumbai";
            tblEmployee.Department = "IT";
            tblEmployee.Gender = "F";
            var resultactual = await repo.GetEmployeeRepo(1);
            Assert.That(resultactual, IsDeeplyEqual.To(tblEmployee));
        }
        [Test, Category("GetCitiesRepo")]
        public async Task GetCitiRepo_Test()
        {

            var context = new SearchContext(dbContextOptions);
            EmpRepository repo = new EmpRepository(context);
            TblCity tblciti = new TblCity();
            // var resultExpected=  context.TblEmployees.Add(tblEmployee);
            tblciti.CityId = 1;
            tblciti.CityName = "New Delhi";
            var resultactual = await repo.GetCitiesRepo();
            var serializer = JsonSerializer.Serialize(resultactual);
            TblCity[] typedDictionary = JsonSerializer.Deserialize<TblCity[]>(serializer);
            Assert.That(typedDictionary[0], IsDeeplyEqual.To(tblciti));
        }

        [Test, Category("GetCitiesUIRepo")]
        public async Task GetCitiesUIRepo_Test()
        {
            var context = new SearchContext(dbContextOptions);
            EmpRepository repo = new EmpRepository(context);
            TblCity tblciti = new TblCity();
            // var resultExpected=  context.TblEmployees.Add(tblEmployee);
            tblciti.CityId = 1;
            tblciti.CityName = "New Delhi";
            var resultactual = await repo.GetCitiesUIRepo(1, "New Delhi");
            var serializer = JsonSerializer.Serialize(resultactual);
            TblCity[] typedDictionary = JsonSerializer.Deserialize<TblCity[]>(serializer);
            Assert.That(typedDictionary[0], IsDeeplyEqual.To(tblciti));


        }




    }

}