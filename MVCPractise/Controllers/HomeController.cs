using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCPractise.Models;

namespace MVCPractise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PracticeDbContext _context;

        public HomeController(ILogger<HomeController> logger,PracticeDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Student> stuList = _context.Students.ToList();
            return View(stuList);
        }

        public IActionResult Detail(int id)
        {
            if (id != null)
            {
                Student stu = _context.Students.FirstOrDefault(item => item.Id == id);
                if (stu != null)
                {
                    return View(stu);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Add(stu);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : ",ex);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                Student stu = _context.Students.FirstOrDefault(item => item.Id == id);
                if (stu != null)
                {
                    return View(stu);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student stu)
        {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Students.Update(stu);
                        _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                    catch (Exception ex)
                    {   
                        Console.WriteLine("Message : ", ex);
                    }
            }
            else
            {
                Console.WriteLine("Cannot Find Id");
                return RedirectToAction("Index");
            }
            Console.WriteLine("Please enter valid Id:");
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                Student stu = _context.Students.FirstOrDefault(item => item.Id == id);
                if (stu != null)
                {
                    return View(stu);
                }
            }
            Console.WriteLine("Please Enter Id");
                return View();
        }

        [HttpPost]
        public IActionResult Delete(Student stu)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Remove(stu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        //public IActionResult Delete(int id)
        //{
        //    if (id != null)
        //    {
        //        Student stu = _context.Students.FirstOrDefault(item => item.Id == id);
        //        if ()
        //        {

        //        }
        //    }

        //    return RedirectToAction("Index");
        //}

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
}
