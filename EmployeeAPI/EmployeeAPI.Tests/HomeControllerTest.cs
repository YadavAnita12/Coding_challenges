using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EmployeeAPI.Controllers;
using EmployeeAPI.Models;
using EmployeeAPI.Repository;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.CompareNetObjects;
using NUnit.Framework;

namespace EmployeeAPI.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<Employee> _MockEmplyee;
        private Mock<IEmployee> _MockIEmployee;
        private Mock<IRepository> _MockIRepository;
        private Mock<ILogger<HomeController>> _MockLogger;


        public int employeeId = 1;

        TblEmployee _TblEmployee = new TblEmployee();
        TblCity _TblCity = new TblCity();

        [SetUp]
        public void Setup()
        {
            _MockEmplyee = new Mock<Employee>();
            _MockIEmployee = new Mock<IEmployee>();
            _MockIRepository = new Mock<IRepository>();
            _MockLogger = new Mock<ILogger<HomeController>>();

        }

        public readonly string Citiid = "1";

        public readonly string CitiName = "New Delhi";

        //checking Not null
        [Test, Category("GetEmp")]
        public void GetEmployeeTest()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);

            _MockIEmployee.Setup(x => x.GetEmployeeService(It.IsAny<int>())).ReturnsAsync(_TblEmployee);

            var ResultActual = homeController.GetEmp();


            Assert.That(ResultActual, Is.Not.Null);

        }




        //Get employee Hirerchy
        [Test, Category("GetEmpval")]
        public async Task GetEmployeeHirerchy()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            homeController.ControllerContext.HttpContext.Request.Headers["id"] = "1";
            homeController.ControllerContext.HttpContext.Request.Headers["Name"] = "Anita";
            //    public int employeeId=1;
            //public string name;
            _TblEmployee.EmployeeId = 1;
            _TblEmployee.Name = "Anita";
            _TblEmployee.City = "Mumbai";
            _TblEmployee.Department = "IT";
            _TblEmployee.Gender = "F";

            Result resultexpected = new Result((int)HttpStatusCode.OK, "Ok", new ResultData(_TblEmployee), "Data found");
            _MockIEmployee.Setup(x => x.GetEmployeeServicehirerchy(It.IsAny<int>())).ReturnsAsync(_TblEmployee);

            var resultactual = await homeController.GetEmpval();

            Assert.That(resultactual, IsDeeplyEqual.To(resultexpected));

        }

   

        //checking with single values
        [Test, Category("GetCiti")]
        public async Task GetCitiTest()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            //    public int employeeId=1;
            //public string name;
            _TblCity.CityId = 1;
            _TblCity.CityName = "New Delhi";


            _MockIEmployee.Setup(x => x.GetCitiesService()).ReturnsAsync(_TblCity);

            var ResultActual = await homeController.GetCiti();

            Assert.That(ResultActual.GetType().GetProperty("Value").GetValue(ResultActual, null), IsDeeplyEqual.To(_TblCity));

        }

        //checking multiple values 
        [Test, Category("GetCiti")]
        public async Task GetCitiTests()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            //    public int employeeId=1;
            //public string name;

            List<TblCity> tblciti = new List<TblCity>();
            _TblCity.CityId = 1;
            _TblCity.CityName = "New Delhi";

            tblciti.Add(_TblCity);
            _TblCity.CityId = 2;
            _TblCity.CityName = "Mumbai";
            tblciti.Add(_TblCity);

            _TblCity.CityId = 3;
            _TblCity.CityName = "Hydrabad";
            tblciti.Add(_TblCity);

            _MockIEmployee.Setup(x => x.GetCitiesService()).ReturnsAsync(_TblCity);

            var ResultActual = await homeController.GetCiti();

            Assert.That(ResultActual.GetType().GetProperty("Value").GetValue(ResultActual, null), IsDeeplyEqual.To(_TblCity));

        }


        //checking Values coming from UI
        [Test, Category("GetCitifromUI")]
        public async Task Getvaluecheck_Citi()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            homeController.ControllerContext.HttpContext.Request.Headers["Citiid"] = "1";
            homeController.ControllerContext.HttpContext.Request.Headers["CitiName"] = "New Delhi";

            _MockIEmployee.Setup(x => x.GetCitiesUIService(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_TblCity);
            var ResultActual = await homeController.GetCitifromUI();
            Assert.That(ResultActual.GetType().GetProperty("Value").GetValue(ResultActual, null), IsDeeplyEqual.To(_TblCity));

        }

        [Test, Category("GetCitifromUI")]
        public async Task GetWrong_valueCheck_CitiId()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            homeController.ControllerContext.HttpContext.Request.Headers["Citiid"] = "A";
            homeController.ControllerContext.HttpContext.Request.Headers["CitiName"] = "New Delhi";

            _MockIEmployee.Setup(x => x.GetCitiesUIService(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_TblCity);
            var ResultActual = await homeController.GetCitifromUI();
            Assert.AreEqual(ResultActual.GetType().GetProperty("StatusCode").GetValue(ResultActual, null), 404);

        }

        [Test, Category("GetCitifromUI")]
        public async Task GetWrong_valueCheckFor_CitiName()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            homeController.ControllerContext.HttpContext.Request.Headers["Citiid"] = "1";
            homeController.ControllerContext.HttpContext.Request.Headers["CitiName"] = "New";

            _MockIEmployee.Setup(x => x.GetCitiesUIService(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_TblCity);
            var ResultActual = await homeController.GetCitifromUI();
            Assert.AreEqual(ResultActual.GetType().GetProperty("StatusCode").GetValue(ResultActual, null), 404);

        }


        [Test, Category("GetCitifromUI")]
        public void GetNull_valueCheckFor_Exception()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            homeController.ControllerContext.HttpContext.Request.Headers["Citiid"] = "";
            homeController.ControllerContext.HttpContext.Request.Headers["CitiName"] = "";

            _MockIEmployee.Setup(x => x.GetCitiesUIService(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_TblCity);
            Assert.ThrowsAsync<Exception>(async () => await homeController.GetCitifromUI());

        }

        [Test, Category("GetEmployeeService")]
        public async Task GetEmpService_Check()
        {

            //    public int employeeId=1;
            //public string name;
            _TblEmployee.EmployeeId = 1;
            _TblEmployee.Name = "Anita";
            _TblEmployee.City = "Mumbai";
            _TblEmployee.Department = "IT";
            _TblEmployee.Gender = "F";


            _MockIRepository.Setup(x => x.GetEmployeeRepo(It.IsAny<int>())).ReturnsAsync(_TblEmployee);
            Employee objEmpService = new Employee(_MockIRepository.Object);

            var ResultActual = await objEmpService.GetEmployeeService(1);

            Assert.That(ResultActual, IsDeeplyEqual.To(_TblEmployee));
        }


        [Test, Category("GetCitiesService")]
        public async Task GetCitiesServiceTest()
        {
            HomeController homeController = new HomeController(_MockIEmployee.Object, _MockLogger.Object);
            //    public int employeeId=1;
            //public string name;
            _TblCity.CityId = 1;
            _TblCity.CityName = "New Delhi";


            _MockIRepository.Setup(x => x.GetCitiesRepo()).ReturnsAsync(_TblCity);

            Employee objEmpService = new Employee(_MockIRepository.Object);

            var ResultActual = await objEmpService.GetCitiesService();

            Assert.That(ResultActual, IsDeeplyEqual.To(_TblCity));

        }

        //checking Values coming from UI
        [Test, Category("GetCitiesUIService")]
        public async Task GetCheckService_Citi()
        {
            _TblCity.CityId = 1;
            _TblCity.CityName = "New Delhi";
            _MockIRepository.Setup(x => x.GetCitiesUIRepo(Convert.ToInt32(Citiid), CitiName)).ReturnsAsync(_TblCity);
            Employee objEmpService = new Employee(_MockIRepository.Object);
            var ResultActual = await objEmpService.GetCitiesUIService(Convert.ToInt32(Citiid), CitiName);
            Assert.That(ResultActual, IsDeeplyEqual.To(_TblCity));

        }



    }
}