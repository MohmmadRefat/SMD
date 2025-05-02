using Microsoft.AspNetCore.Mvc;
using SMD.Models;
using SMD.Models.ModelsDTO;
using SMD.Repository;

namespace SMD.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentController(IRepository<Student> repository) {
            _studentRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var students = await _studentRepository.GetAllAsync();

            var studentDTOs = students.Select(static s => new StudentDTO
            {
                Id = s.Id,
                FullName = s.FullName,
                Major = s.Major,
                GPA = s.GPA,
            }).ToList();

            return View(studentDTOs);
        }

        public async Task<ActionResult> Details(int id)
        {

            Student student = await _studentRepository.GetByIdAsync(id);

            StudentDetailsDTO studentDetailsDTO = new StudentDetailsDTO
            {
                Id=id,
                FullName = student.FullName,
                Email = student.Email,
                AcademicYear = student.AcademicYear,
                Major = student.Major,
                GPA = student.GPA,
                TotalHours = student.TotalHours,
                EnrollmentYear = student.EnrollmentYear
            };
            return View(studentDetailsDTO);
        }

        //GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentCreateDTO studentCreateDTO)
        {
            if (studentCreateDTO == null)
            {
                return BadRequest("Student cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = new Student
            {
                FullName = studentCreateDTO.FullName,
                Email = studentCreateDTO.Email,
                AcademicYear = studentCreateDTO.AcademicYear,
                Major = studentCreateDTO.Major,
                GPA = studentCreateDTO.GPA,
                TotalHours = studentCreateDTO.TotalHours,
                EnrollmentYear = studentCreateDTO.EnrollmentYear
            };
            await _studentRepository.AddAsync(student);
            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {

            StudentUpdateDTO studentUpdateDTO = new StudentUpdateDTO();
            var student = _studentRepository.GetByIdAsync(id).Result;
            if (student == null)
            {
                return NotFound();
            }
            studentUpdateDTO.Id = student.Id;
            studentUpdateDTO.FullName = student.FullName;
            studentUpdateDTO.Email = student.Email;
            studentUpdateDTO.AcademicYear = student.AcademicYear;
            studentUpdateDTO.Major = student.Major;
            studentUpdateDTO.GPA = student.GPA;
            studentUpdateDTO.TotalHours = student.TotalHours;
            studentUpdateDTO.EnrollmentYear = student.EnrollmentYear;
            // Map other properties as needed

            return View(studentUpdateDTO);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(StudentUpdateDTO studentUpdateDTO)
        {
            if (studentUpdateDTO==null || ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            Student student = new Student
            {
                Id = studentUpdateDTO.Id,
                FullName = studentUpdateDTO.FullName,
                Email = studentUpdateDTO.Email,
                AcademicYear = studentUpdateDTO.AcademicYear,
                Major = studentUpdateDTO.Major,
                GPA = studentUpdateDTO.GPA,
                TotalHours = studentUpdateDTO.TotalHours,
                EnrollmentYear = studentUpdateDTO.EnrollmentYear
            };

            await _studentRepository.UpdateAsync(student);

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _studentRepository.DeleteAsync(id);
           return RedirectToAction(nameof(Index));
        }
    }
}
