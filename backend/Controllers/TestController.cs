using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models.Entities;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public TestController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTests()
        {
            try
            {
                var allTests = dbContext.tests.OrderByDescending(t => t.CreatedAt).ToList();
                return Ok(new
                { 
                    status = true, 
                    data = allTests 
                });
            } catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddTests(AddTest addTest)
        {
            try {
                var test = new Test()
                {
                    Id = Guid.NewGuid(),
                    Title = addTest.Title,
                    Content = addTest.Content,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                dbContext.tests.Add(test);
                dbContext.SaveChanges();
                return Ok(test);

            } catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTest(Guid id, AddTest updatedTest)
        {
            try
            {
                var test = dbContext.tests.FirstOrDefault(t => t.Id == id);
                if (test == null)
                {
                    return NotFound(new { status = false, message = "Id not found" });
                }
                test.Title = updatedTest.Title;
                test.Content = updatedTest.Content;
                test.UpdatedAt = DateTime.Now;

                dbContext.SaveChanges();

                return Ok(new { status = true, data = test });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTest(Guid id)
        {
            try
            {
                var test = dbContext.tests.FirstOrDefault(t => t.Id == id);
                if (test == null)
                {
                    return NotFound(new { status = false, message = "Id not found" });
                }

                dbContext.tests.Remove(test);
                dbContext.SaveChanges();

                return Ok(new { status = true, message = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchTests([FromQuery] Guid? id)
        {
            try
            {
                IQueryable<Test> testsQuery = dbContext.tests;
                if (id.HasValue)
                {
                    testsQuery = testsQuery.Where(t => t.Id == id.Value);
                }
                var result = testsQuery.ToList();
                return Ok(new
                {
                    status = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }
    }
}
