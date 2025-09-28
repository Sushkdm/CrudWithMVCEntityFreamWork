using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public StudentsController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                gmail = viewModel.gmail,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await dBContext.Students.AddAsync(student);
            await dBContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dBContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var student = await dBContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dBContext.Students.FindAsync(viewModel.ID);

            if (student is not null) 
            {
                student.Name = viewModel.Name;
                student.gmail = viewModel.gmail;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dBContext.Students.FindAsync(viewModel.ID);
            if (student is not null) 
            { 
                dBContext.Students.Remove(student);
                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
