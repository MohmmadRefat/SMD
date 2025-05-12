using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SMD.Models;
using SMD.Models.ModelsDTO;
using SMD.Repository;

namespace SMD.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly StudentCourseRepository _studentCourseRepository;
        
        public StudentController(Context context) {
            _studentRepository = new StudentRepository(context);
            _courseRepository = new CourseRepository(context);
            _studentCourseRepository = new StudentCourseRepository(context);
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


            Student student = await _studentRepository.GetStudentWithStudnetCourseAsync(id);
            

            StudentDetailsDTO  studentDetailsDTO = new StudentDetailsDTO
                {
                    Id = id,
                    FullName = student.FullName,
                    Email = student.Email,
                    AcademicYear = student.AcademicYear,
                    Major = student.Major,
                    GPA = student.GPA,
                    TotalHours = student.TotalHours,
                    EnrollmentYear = student.EnrollmentYear
                };
            

            Course course = new Course();


            List<StudentEnrollmentCourseDTO> listEnrollment = new List<StudentEnrollmentCourseDTO>();

            bool bol = true;
            if (student.Courses != null)
            {
                foreach (var studentCourse2 in student.Courses)
                {
                    StudentCourse studentCourse = new StudentCourse();
                    StudentEnrollmentCourseDTO studentEnrollmentCourseDTO = new StudentEnrollmentCourseDTO();
                    studentCourse = await _studentCourseRepository.GetStudentCourse(studentCourse2.StudentId,studentCourse2.CourseId);

                    if (studentCourse != null)
                    {
                        studentEnrollmentCourseDTO.StudentCourseId = studentCourse.Id;

                        studentEnrollmentCourseDTO.Grade = ((double)studentCourse.Grade / 100.0) * 4;

                        course = await _courseRepository.GetByIdAsync(studentCourse.CourseId);

                        if (course == null) continue;

                        if (course == null || course.CourseCode == null) bol = false;
                        studentEnrollmentCourseDTO.CourseCode = course?.CourseCode;

                        if (course.CreditHours == null) bol = false;
                        studentEnrollmentCourseDTO.CreditHours = course.CreditHours;

                        if (course.CourseName == null) bol = false;
                        studentEnrollmentCourseDTO.CourseName = course?.CourseName;

                        if (course.AcademicYear == null) bol = false;
                        studentEnrollmentCourseDTO.AcademicYear = course.AcademicYear;


                        listEnrollment.Add(studentEnrollmentCourseDTO);
                    }
                }
            }


            List<EnrollCourseDTO> enrollCourseDTOs = new List<EnrollCourseDTO>();
            var courses = await _courseRepository.GetAllAsync();
            
            foreach(var cr in courses)
            {
            EnrollCourseDTO enrollCourseDTO = new EnrollCourseDTO();
                enrollCourseDTO.CourseId = cr.Id;
                enrollCourseDTO.CourseName = cr.CourseName;
                enrollCourseDTO.CourseCode = cr.CourseCode;
                
                enrollCourseDTOs.Add(enrollCourseDTO);
            }


            TotalDetailsDTO totalDetailsDTO = new TotalDetailsDTO();
            totalDetailsDTO.studentDetailsDTO = studentDetailsDTO;
            totalDetailsDTO.studentenrollmentCourseDTOs = listEnrollment;
            totalDetailsDTO.enrollCourseDTOs = enrollCourseDTOs;


            return View(totalDetailsDTO);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnrollCourse(StudentCourseDTO studentCourseDTO)
        {
            if (studentCourseDTO == null)
            {
                return BadRequest(ModelState);
            }

            StudentCourse studentCourse = new StudentCourse();
            studentCourse.StudentId = studentCourseDTO.StudentId;
            studentCourse.CourseId = studentCourseDTO.CourseId;
            studentCourse.Grade = (studentCourseDTO.Grade*100)/4;
            studentCourse.Semester = studentCourseDTO.Semester;

            var check=await _studentCourseRepository.GetStudentCourse(studentCourseDTO.StudentId, studentCourseDTO.CourseId);
            if (check == null)
            {
                await _studentCourseRepository.AddAsync(studentCourse);
            }

            return RedirectToAction("Details", new { id = studentCourseDTO.StudentId });
        }

        public async Task<ActionResult> DeleteEnrollmentCourse(int id)
        { 
            var studentCourse = await _studentCourseRepository.GetByIdAsync(id);

            
            if (studentCourse != null)
            {
                await _studentCourseRepository.RemoveStudentCourseAsync(studentCourse.StudentId,studentCourse.CourseId);
            }
            
            return RedirectToAction("Details", new { id = studentCourse.StudentId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditStudentEnrollmentCourse(StudentCourseDTO dto)
        {
            var enrollment = await _studentCourseRepository._dbSet
                .FirstOrDefaultAsync(sc => sc.StudentId == dto.StudentId && sc.CourseId == dto.CourseId);

            if (enrollment == null)
            {
                return NotFound();
            }

            enrollment.Grade = dto.Grade;
            await _studentCourseRepository._context.SaveChangesAsync();

            return Ok();
        }
    }
}
