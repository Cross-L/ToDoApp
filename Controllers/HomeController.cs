using System.Diagnostics;

namespace ToDoApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

public class HomeController(ToDoContext context) : Controller
{
    public IActionResult Index()
    {
        var tasks = context.Tasks.ToList();
        return View(tasks);
    }

    [HttpPost]
    public IActionResult Create(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            context.Tasks.Add(new Task { Title = title, CreatedAt = DateTime.Now });
            context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Complete(int id)
    {
        var task = context.Tasks.Find(id);
        if (task != null)
        {
            task.IsCompleted = true;
            task.LastEditedAt = DateTime.Now; 
            context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var task = context.Tasks.Find(id);
        if (task != null)
        {
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var task = context.Tasks.Find(id);
        if (task == null) return RedirectToAction("Index");
        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(int id, string title)
    {
        var task = context.Tasks.Find(id);
        if (task != null && !string.IsNullOrEmpty(title))
        {
            task.Title = title;
            task.LastEditedAt = DateTime.Now;
            context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}