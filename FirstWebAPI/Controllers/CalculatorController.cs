using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // api/calculator/add?x=10&y=20
        [HttpGet("calculator/add")]
        public int Add(int x,int y)
        {
            return x + y;
        }
        [HttpGet("calculator/sum")]
        public int Sum(int x, int y)
        {
            return x + y + 1000;
        }
        // api/calculator/subtract?x=200&y=10
        [HttpPost]
        public int Subtract(int x, int y)
        {
            return x - y;
        }
        // api/calculator/multiply?x=10&y=20
        [HttpPut]
        public int Multiply(int x, int y)
        {
            return x * y;
        }
        // api/calculator/divide?x=20&y=10
        [HttpDelete]
        public int Divide(int x, int y)
        {
            return x / y;
        }
    }
}
