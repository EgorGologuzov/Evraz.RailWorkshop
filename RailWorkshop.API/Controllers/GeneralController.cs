using Microsoft.AspNetCore.Mvc;

namespace RailWorkshop.API.Controllers
{
    public class GeneralController : ControllerBase
    {
        [NonAction]
        public IActionResult HandbookNotFound(string handbook)
        {
            return NotFound($"Handbook with name \"{handbook}\" not found");
        }

        [NonAction]
        public IActionResult EntityNotFound()
        {
            return NotFound("Entity with that identification data not found");
        }

        [NonAction]
        public IActionResult InvalidData()
        {
            ModelState.AddModelError("error", "Mistake in data. May be some field skiped or has wrong data format");
            return BadRequest(ModelState);
        }

        [NonAction]
        public IActionResult IdConflict()
        {
            ModelState.AddModelError("error", "Entity with that id already exists");
            return BadRequest(ModelState);
        }
    }
}