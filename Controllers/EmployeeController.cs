using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSwagger.Controllers
{
  
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    /// <summary>
    /// 雇员控制器
    /// </summary>
    public class EmployeeController : ControllerBase
    {
        private readonly List<Employee> EmployeeList;
        public EmployeeController()
        {

        }

        // GET api/Employee/GetAllEmployee
        /// <summary>
        /// 获取所有Employee
        /// </summary>
        /// <returns>IEnumerable</returns>
        [HttpGet(Name = "GetAllEmployee"), MapToApiVersion("1.0")]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            return Ok(GetEmployees());
        }

        // GET api/v1.1/Employee/GetAllEmployeeV2
        /// <summary>
        /// 获取所有Employee
        /// </summary>
        /// <returns>IEnumerable</returns>
        [HttpGet(Name = "GetAllEmployeeV2"), MapToApiVersion("1.1")]
        public ActionResult<IEnumerable<Employee>> GetAllEmployeeV2()
        {
            return Ok(new { version = "1.1", data =JsonConvert.SerializeObject(GetEmployees())});
        }

        /// <summary>
        /// 根据id获取Employee
        /// </summary>
        // GET api/Employee/GetEmployeeById/id
        [HttpGet("{id}", Name = "GetEmployeeById"), MapToApiVersion("1.0")]
        public Employee GetEmployeeById(int id)
        {
            return GetEmployees().Find(p => p.Id == id);
        }
        /// <summary>
        /// 添加Employee
        /// </summary>
        /// <param name="employee">Employee 对象</param>
        /// <returns>Employee</returns>
        [HttpPost, MapToApiVersion("1.0")]
        public Employee AddEmployee([FromBody] Employee employee)
        {
            return new Employee();
        }


        private List<Employee> GetEmployees()
        {
            return new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                FirstName= "John",
                LastName = "Smith",
                EmailId ="John.Smith@gmail.com"
            },
            new Employee()
            {
                Id = 2,
                FirstName= "Jane",
                LastName = "Doe",
                EmailId ="Jane.Doe@gmail.com"
            }
        };
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }
}
