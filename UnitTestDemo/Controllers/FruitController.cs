using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UnitTestDemo.Business;
using UnitTestDemo.Interfaces;

namespace UnitTestDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitController : ControllerBase
    {
        private readonly ILogger<FruitController> _logger;
        private readonly IFruitBusiness _fruitBusiness;

        public FruitController(ILogger<FruitController> logger, IFruitBusiness fruitBusiness)
        {
            _logger = logger;
            _fruitBusiness = fruitBusiness;
        }

        /// <summary>
        /// Gets a fruit from the database depending on an id. 
        /// If the name of the fruit is 5 letters or longer it will be reversed before returning it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get(int id)
        {
            if (id < 0)
                return BadRequest("An id with a value of 1 or higher must be provided.");

            try
            {
                string fruit = _fruitBusiness.GetFruit(id);
                return Ok(fruit);
            }
            catch(ValidationException ex)
            {
                _logger.LogWarning(ex, $"Recieved invalid id: {id}.");
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to get fruit");
                throw;
            }
        }
    }
}