using Microsoft.AspNetCore.Mvc;
using PresentationBackend.Models;
using PresentationBackend.Storage;

namespace PresentationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPresentations()
        {
            return Ok(PresentationStorage.Presentations);
        }

        [HttpGet("statistic")]
        public IActionResult GetPresentationStatistic(DateTime FromDate, DateTime ToDate)
        {
            int count = PresentationStorage.Presentations
                .Count(p => p.FromDate >= FromDate && p.ToDate <= ToDate);
            return Ok(new { count });
        }

        [HttpPost]
        public IActionResult AddPresentation(Presentation presentation)
        {
            if (PresentationStorage.Presentations.Any(p => p.Name == presentation.Name))
            {
                return BadRequest("A Presentation with this Name already exists !");
            }
            else
            {
                PresentationStorage.Presentations.Add(presentation);
                return Ok(presentation);
            }
        }
    }
}
