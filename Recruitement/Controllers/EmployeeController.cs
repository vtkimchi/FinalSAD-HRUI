using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitement.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Recruitement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        string url = "http://localhost:54052";
        public async Task<ActionResult> Index()
        {
            List<Employee> EmInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url 
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Get using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Employees/");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
            }
            return View(EmInfo);
        }

    }
}