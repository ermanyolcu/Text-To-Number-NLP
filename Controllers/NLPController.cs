using Microsoft.AspNetCore.Mvc;
using TextToNumberNLP.Models;

namespace TextToNumberNLP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NLPController : ControllerBase
    {
        // POST api/nlp
        [HttpPost]
        public JsonResult Post([FromBody] UserInputModel input)
        {
            UserOutputModel output = TextToNumberConverter.Convert(input.UserText);
            return new JsonResult(output);
        }
    }
}