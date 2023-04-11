using Microsoft.AspNetCore.Mvc;

namespace ProjectKyKo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public List<string> Get()
        {

            return Summaries;
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            for (int i = 0; i < Summaries.Count; i++)
            {
                if (string.Equals(name, Summaries[i])) { break; }
           else  if  (i + 1 == Summaries.Count && !string.Equals(name, Summaries[i]))
                {
                    Summaries.Add(name);
                }
            }
            
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
 
            if (index < 0)
            {
                return BadRequest("�� ������� �������������� ������,������!!");

            }
            if (index >= Summaries.Count)
            {
                return BadRequest("�� ������� ������������� ������,������!!");
            }
            for(int i = 0; i < Summaries.Count; i++)
            {
                if(string.Equals(name,Summaries[i])) { break; } 
                if (i + 1 == Summaries.Count && !string.Equals(name, Summaries[i]))
                {
                    Summaries[index] = name;
                }
            }
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0)

            {
                return BadRequest("�� ������� �������������� ������,������!!");
            }
            if (index >= Summaries.Count)
            {
                return BadRequest("�� ������� ������������� ������,������!!");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpGet("{index}")]
        public string GetIndex(int index)
        {
            if(index < 0 || index >= Summaries.Count)
            {
                return "�����, �� ��� ����������� ������!";
            }

            return Summaries[index];
        }
        [HttpGet("{find-by-name}")]
        public int GetFindByName(string name)
        {
            int m = 0;
            for(int i = 0; i<Summaries.Count; i++)
            {
                if (Summaries[i] == name)
                    m++;
            }
            return m;
        }

        [HttpGet("����������")]
        public IActionResult GeyAll(int? sortStrategy)
        {
            if (sortStrategy > 1)
            {
                return BadRequest("������������ �������� ��������� sortStrategy");
            }
           
            if (sortStrategy == null)
            { 
                return Ok(Summaries);
            }
            if (sortStrategy == 1)
            {
                Summaries.Sort();
            return Ok(Summaries);
            }
            if(sortStrategy == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();
                return Ok(Summaries);
            }
            if (sortStrategy < -1)
            {
                return BadRequest("������������ �������� ��������� sortStrategy");
            }


            return Ok(Summaries);
        }
    }
}