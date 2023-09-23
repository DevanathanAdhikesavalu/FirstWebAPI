using FirstWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WebApiClientConsole
{
    internal class EmployeeClient
    {
        static Uri uri = new Uri("http://localhost:5043/");
        public static async Task CallGetAllEmployee()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = uri;
                //HttpGet
                HttpResponseMessage response = await client.GetAsync("GetEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String x = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(x);
                }
            }
        }
        public static async Task CallGetAllEmployeeJson()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpGet
                HttpResponseMessage response = await client.GetAsync("GetEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(json);
                    foreach (EmployeeViewModel employee in employees) 
                    {
                        await Console.Out.WriteLineAsync($"{employee.EmpId},{employee.FirstName},{employee.LastName},{employee.Title},{employee.HireDate},{employee.BirthDate},{employee.ReportsTo},{employee.City}");
                    }
                }
            }
        }
        public static async Task AddNewEmployee()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                EmployeeViewModel newEmployee = new EmployeeViewModel()
                {
                    FirstName = "Arun",
                    LastName = "Abilash",
                    City = "NewJersy",
                    BirthDate = new DateTime(2000,12,01),
                    HireDate = new DateTime(2023,08,16),
                    Title = "Manager"
                };
                var myContent = JsonConvert.SerializeObject(newEmployee);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //HttPost:
                HttpResponseMessage response = await client.PostAsync("AddNewEmployee", byteContent);
                HttpResponseMessage response1 = await client.GetAsync("GetEmployees");
                response.EnsureSuccessStatusCode();
                if(response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"{response.StatusCode}");

                    List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                    String json = await response1.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(json);
                    foreach (EmployeeViewModel employee in employees)
                    {
                        await Console.Out.WriteLineAsync($"{employee.EmpId},{employee.FirstName},{employee.LastName},{employee.Title},{employee.HireDate},{employee.BirthDate},{employee.ReportsTo},{employee.City}");
                    }
                }
            }
        }
        public static async Task UpdateEmployee(int empId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                EmployeeViewModel updatedEmployee = new EmployeeViewModel()
                {
                    EmpId = empId, 
                    FirstName = "Sudharsan",
                    LastName = "A",
                    City = "Washington DC",
                    BirthDate = new DateTime(2000, 12, 01),
                    HireDate = new DateTime(2023, 08, 16),
                    Title = "DB Administrater",
                    ReportsTo = null
                    
                };

                var myContent = JsonConvert.SerializeObject(updatedEmployee);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // HttpPut or HttpPatch:
                HttpResponseMessage response = await client.PutAsync($"EditEmployee", byteContent); // Assuming the endpoint is named "UpdateEmployee"

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"{response.StatusCode}");
                }
            }
        }
        public static async Task DeleteEmployee(int empId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;

                // HttpDelete:
                HttpResponseMessage response = await client.DeleteAsync($"DeleteEmployee?id={empId}"); // Assuming the endpoint is named "DeleteEmployee"

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"{response.StatusCode}");
                }
            }
        }
    }
}
