// See https://aka.ms/new-console-template for more information
using WebApiClientConsole;

Console.WriteLine("API CLIENT!");
EmployeeClient.DeleteEmployee(16).Wait();
Console.ReadLine();
