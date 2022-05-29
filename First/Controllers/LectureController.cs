using First.Interface;
using Microsoft.AspNetCore.Mvc;

namespace First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;
        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet]
        [Route("GetLectures")]
        public async Task<IActionResult> GetLectures()
        {
            try
            {
                var serviceResult = await _lectureService.GetLectures();
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
