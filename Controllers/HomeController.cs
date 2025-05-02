using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMD.Models;
using SMD.Models.ModelsDTO;

namespace SMD.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _context;

    public HomeController(ILogger<HomeController> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var dashboard = new Dashboard
        {
            CountCourses = _context.Courses.Where(C=>C.IsDeleted==false).Count(),
            CountStudents = _context.Students.Where(S=>S.IsDeleted==false).Count(),
            CountDepartments = _context.Students.Where(s=>s.IsDeleted==false).Select(s => s.Major ).Distinct().Count(),
            AvarageGPA = (int)_context.Students.Where(S=>S.IsDeleted==false).Average(s => s.GPA)
        };
        return View(dashboard);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
