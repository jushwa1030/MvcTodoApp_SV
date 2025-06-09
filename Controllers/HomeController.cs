using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        // قائمة محاكاة لقاعدة البيانات (في الذاكرة)
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "تدرب على MVC Design Pattern", IsComplete = false },
            new TaskItem { Id = 2, Title = "تدرب على N-tier Architecture", IsComplete = false },
            new TaskItem { Id = 3, Title = "تدرب على استخدام git", IsComplete = false },
        };

  
        public IActionResult Index()
        {
            return View(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                int newId = tasks.Max(t => t.Id) + 1;
                var newTask = new TaskItem { Id = newId, Title = title, IsComplete = false };
                tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsComplete = true;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditTask(int id, string newTitle)
        {
            // البحث عن المهمة ياستخدام ID
            var task = tasks.FirstOrDefault(t => t.Id == id);
            // تأكد أن المهمة غير فارغة

            if (task != null && !string.IsNullOrWhiteSpace(newTitle))
            // عدل عنوان المهمة
            {
                task.Title = newTitle;
            }
            return RedirectToAction("Index");
        }
    }
}
