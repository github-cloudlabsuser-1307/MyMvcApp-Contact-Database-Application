using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    // Use a private static field for user list
    private static readonly List<User> userlist = new();

    // GET: User
    public IActionResult Index()
    {
        return View(userlist);
    }

    // GET: User/Details/5
    public IActionResult Details(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // GET: User/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid)
            return View(user);
        user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
        userlist.Add(user);
        return RedirectToAction(nameof(Index));
    }

    // GET: User/Edit/5
    public IActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, User user)
    {
        if (!ModelState.IsValid)
            return View(user);
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        // Update other properties as needed
        return RedirectToAction(nameof(Index));
    }

    // GET: User/Delete/5
    public IActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
