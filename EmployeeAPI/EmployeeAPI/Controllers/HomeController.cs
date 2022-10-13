using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using EmployeeAPI.Service;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{

    [ApiController]
    [Route("EmployeeAPI/[controller]")]
    public class HomeController : Controller
    {
        

       public IEmployee empservice;

        private readonly ILogger<HomeController> log;

        public HomeController(IEmployee employee,ILogger<HomeController> logger)
        {
            this.empservice = employee;
            this.log = logger;

        }


        [HttpGet("GetEmp")]
        public async Task<IActionResult> GetEmp()
        {
            try
            {
                
                var val= await empservice.GetEmployeeService(1);
                return Ok(val);
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetEmpval")]
        public async Task<Result> GetEmpval()
        {
            try
            {
                string Id = Request.Headers["id"];
                string Name = Request.Headers["Name"];
                TblEmployee tblemp = new TblEmployee();
                tblemp.EmployeeId = 1;
                Result result = new Result((int)HttpStatusCode.OK, "Ok", new ResultData(), "Data found");

                result.resultData.tblemp = await empservice.GetEmployeeServicehirerchy(1);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetCiti-fjhjhj")]
        [HttpGet("GetCiti")]
       public async Task<IActionResult> GetCiti()
        {
            try
            {
                var cities =  await empservice.GetCitiesService();
                return Ok(cities);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetCitifromUI")]
        public async Task<IActionResult> GetCitifromUI()
        {
            try
            {

               string  CItiId= Request.Headers["CitiId"];
               string CitiName= Request.Headers["CitiName"];
                //Result result=new Result((int)HttpStatusCode.Ok, "OK", new ResultData()

                Regex regex = new Regex("[0-9]");
                if (string.IsNullOrEmpty(CItiId) && string.IsNullOrEmpty(CitiName)) throw new Exception("Parameteres are null");
                
                    if (regex.IsMatch(CItiId))
                    {
                        if (CitiName.Length > 4)
                        {
                            var cities = await empservice.GetCitiesUIService(Int32.Parse(CItiId), CitiName);
                            return Ok(cities);
                        }

                    }
                
                return NotFound();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetEmployeeId")]
        public async Task<IActionResult> GetEmployee()
        {

           var val=  await empservice.getemployeeID(1);
            return Ok(val);
        }


    }



}
