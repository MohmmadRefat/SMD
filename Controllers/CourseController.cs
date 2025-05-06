using Microsoft.AspNetCore.Mvc;
using SMD.Models;
using SMD.Models.ModelsDTO;
using SMD.Repository;

namespace SMD.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseRepository? _courseRepository;

        public CourseController(Context context)
        {
            _courseRepository = new CourseRepository(context);
        }

        public async Task<ActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();

            ICollection<CourseDTO> courseDTOs = new List<CourseDTO>();
            if (courses == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var course in courses)
                {
                    var courseDTO = new CourseDTO
                    {
                        Id = course.Id,
                        CourseName = course.CourseName,
                        CourseCode = course.CourseCode,
                        AcademicYear = course.AcademicYear,
                        CreditHours = course.CreditHours
                    };
                    courseDTOs
                        .Add(courseDTO);
                }
            }
                return View(courseDTOs);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
           
            return View();
        }


        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseCreateDTO courseCreateDTO)
        {
            var Course= new Course
            {
                CourseCode = courseCreateDTO.CourseCode,
                CourseName = courseCreateDTO.CourseName,
                AcademicYear = courseCreateDTO.AcademicYear,
                CreditHours = courseCreateDTO.CreditHours,
            };
            await _courseRepository.AddAsync(Course);
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseController/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            var course =await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                var courseUpdateDTO = new CourseUpdateDTO
                {
                    Id=course.Id,
                    CourseCode = course.CourseCode,
                    CourseName = course.CourseName,
                    AcademicYear = course.AcademicYear,
                    CreditHours = course.CreditHours,

                };
                return View(courseUpdateDTO);
            }
                
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseUpdateDTO _courseUpdateDto)
        {
            var Course = new Course
            {
                Id = _courseUpdateDto.Id,
                CourseCode=_courseUpdateDto.CourseCode,
                CourseName=_courseUpdateDto.CourseName,
                AcademicYear=_courseUpdateDto.AcademicYear,
                CreditHours=_courseUpdateDto.CreditHours,

            };
            await _courseRepository.UpdateAsync(Course);
            return RedirectToAction(nameof(Index));

        }

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null) return BadRequest();

            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
